using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using Custos.XrmHelpers.Infrastructure.Dataverse.Models;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class Teams
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        public Teams(IDataverseContext context, IDataverseLoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.Create<Systemusers>();
        }
        public ITeam? GetMetadataById(Guid teamId)
        {
            _logger.WriteLog("Building RetrieveMetadata query expression on systemuser");

            var query = new QueryExpression("team")
            {
                ColumnSet = new ColumnSet("teamid", "name"),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "teamid",
                            ConditionOperator.Equal,
                            teamId
                        )
                    }
                }
            };


            var membershipLink = query.AddLink(
                "teammembership",
                "teamid",
                "teamid"
            );

            var membersLink = membershipLink.AddLink(
                "systemuser",
                "systemuserid",
                "systemuserid"
            );

            membersLink.EntityAlias = "user";
            
            membersLink.Columns = new ColumnSet(
                "internalemailaddress",
                "fullname",
                "firstname",
                "lastname",
                "systemuserid"
            );

            var results = _context.Runtime.UserService
                .RetrieveMultiple(query)
                .Entities;

            if (!results.Any()){ return null; }
            
            var id = results[0].GetAttributeValue<Guid>("teamid");
            var name = results[0].GetAttributeValue<string>("name");

            List<User> teamMembers = new List<User>();

            foreach (var entity in results)
            {
                var fullname = entity.Attributes.TryGetValue("user.fullname", out var full)
                    ? ((AliasedValue)full).Value?.ToString()
                    : "";
                var lastname = entity.Attributes.TryGetValue("user.lastname", out var last)
                    ? ((AliasedValue)last).Value?.ToString()
                    : "";
                var firstname = entity.Attributes.TryGetValue("user.firstname", out var first)
                    ? ((AliasedValue)first).Value?.ToString()
                    : "";
                var userId = entity.Attributes.TryGetValue("user.systemuserid", out var guid)
                    ? (Guid)((AliasedValue)guid).Value
                    : Guid.Empty;
                var emailaddress = entity.Attributes.TryGetValue("user.internalemailaddress", out var email)
                    ? ((AliasedValue)email).Value?.ToString()
                    : "";

                teamMembers.Add(
                    new User
                    {
                        FullName = fullname ?? "",
                        LastName = lastname ?? "",
                        FirstName = firstname ?? "",
                        Email = emailaddress ?? "",
                        Id = userId
                    }
                );
            }

            _logger.WriteLog("Executing RetrieveMetadata query expression on systemuser");
            return new Team
            {
                Name = name ?? "",
                TeamId = id,
                Members = teamMembers
            };
        }
        public Guid ResolveTeamIdByName(string teamName)
        {
            _logger.WriteLog($"Executing ResolveTeamIdByName: {teamName}");
            QueryExpression query = new QueryExpression("team")
            {
                TopCount = 1,
                ColumnSet = new ColumnSet("teamid", "name"),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            "name",
                            ConditionOperator.Equal,
                            teamName
                        )
                    }
                }
            };

            var results = _context.Runtime.UserService.RetrieveMultiple(query);

            if (results.Entities == null)
            {
                throw new Exception($"No team found with name matching: {teamName}");
            } else if (results.Entities.Count > 1)
            {
                throw new Exception("Multiple teams were found matching the given name.");
            } else
            {
                return results.Entities.First().Id;
            }
        }
    }
}