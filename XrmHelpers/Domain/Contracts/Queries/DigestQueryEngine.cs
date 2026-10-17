using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class DigestQueryEngine<TDto>{
        private readonly IDataverseLogger _logger;
        private readonly IDataverseContext _context;
        private readonly IQueryMapper<TDto> _queryMapper;
        public DigestQueryEngine(
            IDataverseLoggerFactory loggerFactory,
            IDataverseContext context,
            IQueryMapper<TDto> queryMapper
        )
        {
            _context = context;
            _logger = loggerFactory.Create<DigestQueryEngine<TDto>>();
            _queryMapper = queryMapper;
        }
        public IEnumerable<(string to, DigestDto<TDto> digest)>? GetDigests(
            string title,
            string description,
            IEnumerable<DigestMetric> metrics,
            string targetEntity,
            ColumnSet columns
        )
        {
            _logger.WriteLog($"Entered {nameof(GetDigests)}");
            _logger.WriteLog($"Executing query on {targetEntity}");

            // ensure columns has ownerid attribute
            columns.AddColumn("ownerid");

            // query + cache all open records for the given entity
            QueryExpression query = new QueryExpression(targetEntity)
            {
                ColumnSet = columns,
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "statecode",
                            ConditionOperator.Equal,
                            Status.Active
                        )
                    }
                }
            };

            var results = _context.Service.RetrieveMultiple(query);
            
            if (results.Entities.Count == 0)
            {
                _logger.WriteLog($"No active records returned for {targetEntity}");
                return null;
            }

            _logger.WriteLog($"Query returned a total of {results.Entities.Count} records. ");

            /* GROUP ENTITIES BY OWNER BEFORE DTO MAPPING */
            var groupedEntities = results.Entities
                .GroupBy(
                    entity =>
                    {
                        var owner = entity.GetAttributeValue<EntityReference>("ownerid");

                        if (owner == null)
                        {
                            var err = new InvalidOperationException($"Entity: {entity.Id} has no owner.");
                            _logger.WriteLog(err, "Exception: ");
                            throw err;
                        }
                        return (owner.LogicalName, owner.Id);
                    }
                );

            var users = groupedEntities
                .Where(group => group.Key.LogicalName == "systemuser")
                .Select(group => group.Key.Id)
                .Distinct()
                .ToList();

            var userQuery = new QueryExpression("systemuser")
            {
                ColumnSet = new ColumnSet("systemuserid", "internalemailaddress"),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "systemuserid",
                            ConditionOperator.In,
                            users.Cast<object>().ToArray()
                        )
                    }
                }
            };

            var luUsers = _context.Service
                .RetrieveMultiple(userQuery)
                .Entities
                .Where(e => e.Contains("systemuserid") && e.Contains("internalemailaddress"))
                .ToDictionary(
                    e => e.Id,
                    e => e.GetAttributeValue<string>("internalemailaddress")
                );

            var teams = groupedEntities
                .Where(group => group.Key.LogicalName == "team")
                .Select(group => group.Key.Id)
                .Distinct()
                .ToList();

            var teamQuery = new QueryExpression("teammembership");

            teamQuery.ColumnSet = new ColumnSet(false);

            var membersLink = teamQuery.AddLink(
                "systemuser",
                "systemuserid",
                "systemuserid"
            );
            membersLink.EntityAlias = "user";
            membersLink.Columns = new ColumnSet("internalemailaddress");

            membersLink.LinkCriteria.AddCondition(
                "teamid",
                ConditionOperator.In,
                teams.Cast<object>().ToArray()
            );

            var luTeamMembers = _context.Service
                .RetrieveMultiple(teamQuery)
                .Entities
                .GroupBy(entity => entity.GetAttributeValue<Guid>("teamid"))
                .ToDictionary(
                    group => group.Key,
                    group => string.Join(
                        ';',
                        group.Select(
                            m => ((AliasedValue)m["user.internalemailaddress"]).Value?.ToString())
                        .Where(e => !string.IsNullOrWhiteSpace(e))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                    )
                );

            foreach (var group in groupedEntities)
            {
                string recipients = string.Empty;
                var (logicalName, id) = group.Key;

                // Retrieve USER or TEAM MEMBERS
                if (logicalName == "systemuser")
                {
                    if (!luUsers.TryGetValue(id, out recipients))
                    {
                        _logger.WriteLog($"No email address found in lookup for user id: {id}");
                        continue;
                    }
                }
                else
                {
                    if (!luTeamMembers.TryGetValue(id, out recipients))
                    {
                        _logger.WriteLog($"No recipients found in lookup for team id: {id}");
                        continue;
                    }
                }

                var dtos = group.ToList()
                    .Select(entity => _queryMapper.MapToDto(entity))
                    .ToList();

                var digest = new DigestDto<TDto>
                {
                    Title = title,
                    Description = description,
                    Records = dtos
                };

                return (recipients, digest);
            }

            return null;
        }
    }
}