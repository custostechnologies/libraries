using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Infrastructure.Dataverse;
using custos.XrmHelpers.Infrastructure.Dataverse.Services;
using custos.XrmHelpers.Infrastructure.Services;

namespace custos.XrmHelpers.Domain.Contracts
{
    public class DigestQueryEngine<TDto>{
        private readonly IDataverseLogger _logger;
        private readonly IDataverseContext _context;
        private readonly IQueryMapper<TDto> _queryMapper;
        private readonly IDigestConfiguration<TDto> _digestConfig;
        private readonly IStyleProvider _styleProvider;
        public DigestQueryEngine(
            IDataverseLoggerFactory loggerFactory,
            IDataverseContext context,
            IQueryMapper<TDto> queryMapper,
            IDigestConfiguration<TDto> digestConfig,
            IStyleProvider styleProvider
        )
        {
            _context = context;
            _logger = loggerFactory.Create<DigestQueryEngine<TDto>>();
            _queryMapper = queryMapper;
            _digestConfig = digestConfig;
            _styleProvider = styleProvider;
        }
        public IReadOnlyCollection<(string to, DigestDto<TDto> digest)> GetDigests(
            string title,
            string description,
            string targetEntity,
            ColumnSet columns
        )
        {
            var digests = new List<(string to, DigestDto<TDto>)>();
            _logger.WriteLog($"Entered {nameof(GetDigests)}");
            _logger.WriteLog($"Executing query on {targetEntity}");

            var openRecords = RetrieveOpenRecords(columns, targetEntity);

            if (openRecords == null)
            {
                _logger.WriteLog($"Dataverse query returned null on target: {targetEntity}");
                return [];
            }

            /* GROUP ENTITIES BY OWNER BEFORE DTO MAPPING */
            var groupedEntities = openRecords.Entities
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
                )
                .ToList();

            var teamLookups = BuildTeamLookups(groupedEntities);

            //  TODO: enumerate teams results into groupedTeams once and pass into lu method
            var luTeamMembers = teamLookups.membersLu;
            var luTeamNames = teamLookups.nameLu;
            var luUsers = BuildUsersLookup(groupedEntities);

            foreach (var group in groupedEntities)
            {
                string to = string.Empty;
                string userName = string.Empty;
                string teamName = string.Empty;
                var (logicalName, id) = group.Key;

                // Retrieve USER or TEAM MEMBERS
                if (logicalName == "systemuser")
                {
                    if (!luUsers.TryGetValue(id, out var recipient))
                    {
                        _logger.WriteLog($"No email address found in lookup for user id: {id}");
                        continue;
                    }

                    userName = recipient.first;
                    to = recipient.email;
                }
                else
                {
                    if (!luTeamMembers.TryGetValue(id, out var recipients))
                    {
                        _logger.WriteLog($"No recipients found in lookup for team id: {id}");
                        continue;
                    }

                    if (!luTeamNames.TryGetValue(id, out var luTeamName))
                    {
                        _logger.WriteLog($"No recipients found in lookup for team id: {id}");
                        continue;
                    }

                    to = recipients;
                    teamName = luTeamName;
                }

                var dtos = group.ToList()
                    .Select(entity => _queryMapper.MapToDto(entity))
                    .ToList();

                var metrics = _digestConfig
                    .Metrics
                    .SelectMany(metric => metric.BuildMetric(dtos))
                    .ToList();

                var digestName = string.IsNullOrWhiteSpace(teamName)
                    ? userName
                    : teamName.Contains("Team")
                        ? teamName
                        : $"{teamName} Team";

                var digest = new DigestDto<TDto>
                {
                    Title = title,
                    Description = description,
                    Records = dtos,
                    Name = digestName,
                    Metrics = metrics
                };

                digests.Add((to, digest));
            }

            return digests;
        }
        private EntityCollection? RetrieveOpenRecords(ColumnSet columns, string targetEntity)
        {
            // ensure columns has ownerid attribute
            if (!columns.Columns.Contains("ownerid")) columns.AddColumn("ownerid");

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
                            (int)Status.Active
                        )
                    }
                }
            };

            var results = _context.Runtime.UserService.RetrieveMultiple(query);

            if (results.Entities.Count == 0)
            {
                _logger.WriteLog($"No active records returned for {targetEntity}");
                return null;
            } else
            {
                _logger.WriteLog($"Query returned a total of {results.Entities.Count} records. ");
                return results;
            } 
        }
        private (
            Dictionary<Guid, string> membersLu,
            Dictionary<Guid, string> nameLu 
        ) BuildTeamLookups(
            IEnumerable<
                IGrouping<(string LogicalName, Guid Id), Entity>
            > groupedEntities)
        {
            var teams = groupedEntities
                .Where(group => group.Key.LogicalName == "team")
                .Select(group => group.Key.Id)
                .Distinct()
                .ToList();

            if (!teams.Any()) return ([], []);

            var teamQuery = new QueryExpression("teammembership")
            {
                ColumnSet = new ColumnSet("teamid")
            };

            var membersLink = teamQuery.AddLink(
                "systemuser",
                "systemuserid",
                "systemuserid"
            );
            membersLink.EntityAlias = "user";
            membersLink.Columns = new ColumnSet("internalemailaddress");

            var teamLink = teamQuery.AddLink(
                "team",
                "teamid",
                "teamid"
            );
            teamLink.Columns = new ColumnSet("teamid", "name");
            teamLink.EntityAlias = "team";

            teamQuery.Criteria.AddCondition(
                "teamid",
                ConditionOperator.In,
                teams.Cast<object>().ToArray()
            );

            var results = _context.Runtime.UserService
                .RetrieveMultiple(teamQuery)
                .Entities;

            var membersLu = results
                .GroupBy(entity => entity.GetAttributeValue<Guid>("teamid"))
                .ToDictionary(
                    group => group.Key,
                    group => string.Join(
                        ";",
                        /*group.Select(
                            m =>
                            {
                                return (
                                    EmailAddress: m.Attributes.TryGetValue("user.internalemailaddress", out var email)
                                        ? ((AliasedValue)email).Value?.ToString()
                                        : null,
                                    FirstName: m.Attributes.TryGetValue("user.firstname", out var first)
                                        ? ((AliasedValue)first).Value?.ToString()
                                        : null
                                );
                            }
                        )*/
                        group.Select(
                            m => m.Attributes.TryGetValue("user.internalemailaddress", out var email)
                                        ? ((AliasedValue)email).Value?.ToString()
                                        : null
                        )
                        .Where(e => !string.IsNullOrWhiteSpace(e))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                    )
                );

            var nameLu = results
                .GroupBy(entity => entity.GetAttributeValue<Guid>("teamid"))
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(
                        m => ((AliasedValue)m["team.name"]).Value?.ToString())
                        .FirstOrDefault(n => !string.IsNullOrWhiteSpace(n)) ?? string.Empty
                    );

            return (membersLu, nameLu);
        }
        private Dictionary<Guid, (string email, string first)> BuildUsersLookup(IEnumerable<IGrouping<(string LogicalName, Guid Id), Entity>> groupedEntities)
        {
            var users = groupedEntities
                .Where(group => group.Key.LogicalName == "systemuser")
                .Select(group => group.Key.Id)
                .Distinct()
                .ToList();

            if (!users.Any()) return [];

            var userQuery = new QueryExpression("systemuser")
            {
                ColumnSet = new ColumnSet("systemuserid", "internalemailaddress", "firstname"),
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

            return _context.Runtime.UserService
                .RetrieveMultiple(userQuery)
                .Entities
                .Where(e =>
                    !string.IsNullOrWhiteSpace(e.GetAttributeValue<string>("internalemailaddress"))
                )
                .ToDictionary(
                    e => e.Id,
                    e => (email: e.GetAttributeValue<string>("internalemailaddress"), first: e.GetAttributeValue<string>("firstname"))
                );
        }
    }
}