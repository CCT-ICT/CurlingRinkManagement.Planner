import { DateTimeRange } from "./date-time-range.model";

export class ActivityModel {
    public id: string = crypto.randomUUID();
    public title: string = "";
    public plannedDates: DateTimeRange[] = [];
    public sheetId: string = "00000000-0000-0000-0000-000000000000";
    public activityTypeId: string = "00000000-0000-0000-0000-000000000000";
}