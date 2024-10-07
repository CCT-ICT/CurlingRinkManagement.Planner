import { Component, Input, input, OnInit } from '@angular/core';
import { EventModel } from '../../models/event.model';
import { ActivityTypeModel } from '../../models/activity-type.model';
import { SheetModel } from '../../models/sheet.model';
import { ActivityService } from '../../services/activity.service';
import { ActivityTypeService } from '../../services/activity-type.service';

@Component({
  selector: 'app-sheet-overview',
  standalone: true,
  imports: [],
  templateUrl: './sheet-overview.component.html',
  styleUrl: './sheet-overview.component.scss'
})
export class SheetOverviewComponent implements OnInit {
  @Input()
  public sheet: SheetModel = new SheetModel();

  public detailInMinutes: number = 15;
  public heightInPixels = 60;
  public times: Date[] = [];
  public day: Date = new Date();

  public currentEvent: EventModel | null = null;

  private creating: boolean = false;

  public events: EventModel[] = []
  public activityTypes: ActivityTypeModel[] = []

  constructor(private activityService: ActivityService, private activityTypeService: ActivityTypeService) { }


  ngOnInit(): void {
    this.loadTimes();
    this.loadActivityTypes();
    this.loadActivities();
  }

  private loadTimes() {
    this.times = [];
    let currentTime = 0;
    while (currentTime < 24 * 60) {
      let today = new Date(this.day.getFullYear(), this.day.getMonth(), this.day.getDate());
      today.setMinutes(currentTime);
      this.times.push(today);
      currentTime += this.detailInMinutes;
    }
  }

  private loadActivityTypes() {
    this.activityTypeService.GetAll().subscribe(a => {
      this.activityTypes = a;
    });
  }
  private loadActivities() {
    let start = new Date(this.day.getFullYear(), this.day.getMonth(), this.day.getDate());
    let end = new Date(this.day.getFullYear(), this.day.getMonth(), this.day.getDate());
    end.setHours(24);
    this.activityService.getInRange(this.sheet.id, start, end).subscribe(activities => {
      activities.forEach(activity => {
        activity.plannedDates.forEach(p => {
          this.events.push({
            timeStart: p.start,
            timeEnd: p.end,
            activity: activity,
            originalStart: p.start
          });
        })
      })
    });
  }


  getEvent(time: Date) {
    return this.events.find(e => e.timeStart.getHours() >= time.getHours() && e.timeStart.getHours() < time.getHours() + 1 && e.timeStart.getMinutes() >= time.getMinutes() && e.timeStart.getMinutes() < time.getMinutes() + 15)
  }

  getLength(event: any) {
    if (event.timeEnd == null) return 15;
    return (event.timeEnd.getHours() - event.timeStart.getHours()) * 60 + (event.timeEnd.getMinutes() - event.timeStart.getMinutes())
  }

  startClick(time: Date) {
    if (this.creating) return;
    this.creating = true;
    this.currentEvent = {
      timeStart: time,
      originalStart: time,
      timeEnd: this.addMinutes(time, 15),
      activity: null
    }
    this.events.push(this.currentEvent);
  }

  hover(time: Date) {
    if (this.currentEvent == null) return;
    this.changeEndTime(time);
  }
  endClick(time: Date) {
    if (this.currentEvent == null) return;
    this.changeEndTime(time);
    this.currentEvent = null;
    document.getElementById(this.sheet.name);
  }

  changeEndTime(time: Date) {
    if (this.currentEvent == null) return;

    if (time < this.currentEvent.originalStart) {
      this.currentEvent.timeEnd = this.currentEvent.originalStart
      this.currentEvent.timeStart = time;
    }
    else if (time === this.currentEvent.originalStart) {
      this.currentEvent.timeStart = this.currentEvent.originalStart;
      this.currentEvent.timeEnd = this.addMinutes(this.currentEvent.originalStart, 15);
    }
    else {
      this.currentEvent.timeStart = this.currentEvent.originalStart;
      this.currentEvent.timeEnd = time;
    }
  }

  addMinutes(date: Date, minutes: number) {
    let end = new Date(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes())
    end.setMinutes(date.getMinutes() + minutes);
    return end;
  }
}
