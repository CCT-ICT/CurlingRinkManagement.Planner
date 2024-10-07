using CurlingRinkManagement.Planner.Domain.DatabaseModels;
using CurlingRinkManagement.Planner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurlingRinkManagement.Planner.Business.Services;

public class ActivityService(IRepository<Activity> activityRepository) : IActivityService
{
    private readonly IRepository<Activity> activityRepository = activityRepository;

    public Activity Create(Activity activity)
    {
        return activityRepository.Create(activity);
    }

    public void Delete(Guid id)
    {
        var activity = GetById(id);
        activityRepository.Delete(activity);
    }

    public Activity GetById(Guid id)
    {
        var activity = activityRepository.GetAll().Include(a => a.PlannedDates).FirstOrDefault(x => x.Id == id);

        if(activity == null)
            throw new KeyNotFoundException($"Activity with id {id} does not exist");
        return activity;
    }

    public List<Activity> GetAllOnSheet(Guid sheetId, DateTime start, DateTime end)
    {
        var activities = activityRepository.GetAll().Include(a => a.PlannedDates).Include(a => a.ActivityType)
            .Where(a => a.PlannedDates.Any(d => (d.Start.AddMinutes(-d.MinutesBlockedBefore) >= start && d.Start.AddMinutes(-d.MinutesBlockedBefore) <= end) || 
                                                (d.End.AddMinutes(d.MinutesBlockedAfter) >= start && d.End.AddMinutes(d.MinutesBlockedAfter) <= end))).ToList();

        return activities;
    }

    public Activity Update(Activity activity)
    {
        throw new NotImplementedException();
    }
}
