using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Enums;

using System.Text.Json;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class TaskFactory
    {
        private readonly IDataverseLogger _logger;
        private readonly IDataverseMapper<ReviewTaskDto> _dataverseMapper;
        public TaskFactory(
            IDataverseLoggerFactory loggerFactory,
            IDataverseMapper<ReviewTaskDto> dataverseMapper
        )
        {
            _logger = loggerFactory.Create<TaskFactory>();
            _dataverseMapper = dataverseMapper;
        }
        public void Build(
            TaskBaseDto baseDto,
            OrganizationRequestCollection organizationRequests,
            EntityReference owner,
            EntityReference regarding,
            DateTime nextReviewDate
        )
        {
            try
            {
                // TODO: implement task priorities
                var nextDueDate = baseDto.CalculateNextDueDate(nextReviewDate);

                var taskDto = new ReviewTaskDto()
                {
                    Description = baseDto.TaskDescription,
                    Subject = baseDto.Subject,
                    RegardingLogicalName = baseDto.RegardingEntityLogicalName,
                    Regarding = regarding,
                    TaskType = baseDto.TaskType,
                    Priority = TaskPriority.Medium,
                    RegardingId = baseDto.EntityId,
                    DueDate = nextDueDate,
                    Owner = owner
                };

                var taskLog = JsonSerializer.Serialize(taskDto, new JsonSerializerOptions{WriteIndented = true});
                _logger.WriteLog($"taskLog: {taskLog}");

                var task = _dataverseMapper.MapToTarget(taskDto);

                organizationRequests.Add(
                    new CreateRequest { Target = task }
                );

                _logger.WriteLog(
                    $"Created task request. " +
                    $"Subject='{taskDto.Subject}', " +
                    $"Owner='{owner.Id}', " +
                    $"DueDate='{taskDto.DueDate:yyyy-MM-dd}'"
                );
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error constructing User Sync Event: ", ex);
                throw;
            }
        }
    }
}