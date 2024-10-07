import { DateTimeRange } from "./date-time-range.model";

export class ActivityModel {
    public id: string = ""
    public title: string = ""
    public plannedDates: DateTimeRange[] = [];
    public sheetId: string = "";
    public activityTypeId: string = "";
}