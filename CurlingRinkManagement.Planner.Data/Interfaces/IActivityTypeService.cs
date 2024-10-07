using CurlingRinkManagement.Planner.Domain.DatabaseModels;

namespace CurlingRinkManagement.Planner.Domain.Interfaces;

public interface IActivityTypeService
{
    List<ActivityType> GetAll();
}

