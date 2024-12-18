using CurlingRinkManagement.Planner.Domain.DatabaseModels;
using CurlingRinkManagement.Planner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurlingRinkManagement.Planner.Business.Services;

public class ActivityService(IRepository<Activity> activityRepository, IRepository<DateTimeRange> dateTimeRepository) : IActivityService
{
    private readonly IRepository<Activity> _activityRepository = activityRepository;

    public Activity Create(Activity activity)
    {
        return _activityRepository.Create(activity);
    }

    public void Delete(Guid id)
    {
        var activity = GetById(id);
        _activityRepository.Delete(activity);
    }

    public Activity GetById(Guid id)
    {
        var activity = _activityRepository.GetAll().Include(a => a.PlannedDates).FirstOrDefault(x => x.Id == id);

        if(activity == null)
            throw new KeyNotFoundException($"Activity with id {id} does not exist");
        return activity;
    }

    public List<Activity> GetAllOnSheet(Guid sheetId, DateTime start, DateTime end)
    {
        var activities = _activityRepository.GetAll().Include(a => a.PlannedDates).Include(a => a.ActivityType)
            .Where(a => a.SheetId == sheetId)
            .Where(a => a.PlannedDates.Any(d => (d.Start.AddMinutes(-d.MinutesBlockedBefore) >= start && d.Start.AddMinutes(-d.MinutesBlockedBefore) <= end) || 
                                                (d.End.AddMinutes(d.MinutesBlockedAfter) >= start && d.End.AddMinutes(d.MinutesBlockedAfter) <= end))).ToList();

        return activities;
    }

    public Activity Update(Activity activity)
    {
        var toUpdate = GetById(activity.Id);

        toUpdate.ActivityTypeId = activity.ActivityTypeId;
        toUpdate.PlannedDates = activity.PlannedDates;
        toUpdate.Title = activity.Title;

        return _activityRepository.Update(toUpdate);
    }
}
