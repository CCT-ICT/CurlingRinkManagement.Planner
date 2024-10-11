import { Component, OnInit } from '@angular/core';
import { ActivityTypeService } from '../../services/activity-type.service';
import { ActivityTypeModel } from '../../models/activity-type.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-activity-type-editor',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './activity-type-editor.component.html',
  styleUrl: './activity-type-editor.component.scss'
})
export class ActivityTypeEditorComponent implements OnInit {

  public activityTypes: ActivityTypeModel[] = [];

  public error: string | null = null;

  constructor(private activityTypeService: ActivityTypeService) { }


  ngOnInit(): void {
    this.activityTypeService.GetAll().subscribe({
      next: (activityTypes) => {
        this.activityTypes = activityTypes;
        this.activityTypes.sort((a, b) => {
          if (a.type.toLowerCase() < b.type.toLowerCase()) {
            return -1;
          }
          if (a.type.toLowerCase() > b.type.toLowerCase()) {
            return 1;
          }
          return 0;
        })
      },
      error: (err) => {
        this.error = "Problem retreiving activity types"
      }
    })
  }

  //TODO [BK] Implement actual deletion
  deleteActivityType(index: number) {
    this.activityTypes.splice(index, 1);
  }

  addActivityType() {
    this.activityTypes.push(new ActivityTypeModel());
  }
}
