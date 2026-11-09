using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
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
            OrganizationRequestCollection organizationRequests
        )
        {
            try
            {
                // TODO: implement task priorities
                var nextReviewDate = baseDto.CalculateNextReviewDate();
                var nextDueDate = baseDto.NextDueDate
                    ?? baseDto.CalculateNextDueDate(nextReviewDate);

                var taskDto = new ReviewTaskDto()
                {
                    Description = baseDto.TaskDescription,
                    Subject = baseDto.Subject,
                    RegardingLogicalName = baseDto.RegardingEntityLogicalName,
                    TaskType = baseDto.TaskType,
                    Priority = TaskPriority.Medium,
                    DueDate = nextDueDate
                    // owner
                };

                var baseUpdate = baseDto.GetReviewMetadataUpdate();
                organizationRequests.Add(
                    new UpdateRequest { Target = baseUpdate }
                );
                _logger.WriteLog($"Created base record update with logicalName = {baseUpdate.LogicalName} and id = {baseUpdate.Id} and added to EntityCollection");

                var task = _dataverseMapper.MapToTarget(taskDto);
                organizationRequests.Add(
                    new CreateRequest { Target = task }
                );
                _logger.WriteLog($"Created new Task and added to EntityCollection");
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Error constructing User Sync Event: ", ex);
                throw;
            }
        }
    }
}