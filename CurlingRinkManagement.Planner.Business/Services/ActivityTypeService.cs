using CurlingRinkManagement.Planner.Domain.DatabaseModels;
using CurlingRinkManagement.Planner.Domain.Interfaces;

namespace CurlingRinkManagement.Planner.Business.Services;

public class ActivityTypeService(IRepository<ActivityType> activityTypeRepository) : IActivityTypeService
{
    private readonly IRepository<ActivityType> _activityTypeRepository = activityTypeRepository;

    public List<ActivityType> GetAll()
    {
        return _activityTypeRepository.GetAll().ToList();
    }
}