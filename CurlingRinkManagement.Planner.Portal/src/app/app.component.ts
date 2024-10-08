import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SheetOverviewComponent } from "./components/sheet-overview/sheet-overview.component";
import { TimeOverviewComponent } from "./components/time-overview/time-overview.component";
import { SheetService } from './services/sheet.service';
import { SheetModel } from './models/sheet.model';
import { ActivityTypeModel } from './models/activity-type.model';
import { ActivityTypeService } from './services/activity-type.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SheetOverviewComponent, TimeOverviewComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {

  public sheets: SheetModel[] = [];
  public activityTypes: ActivityTypeModel[] = [];

  constructor(private sheetService: SheetService, private activityTypeService: ActivityTypeService){}

  ngOnInit(): void {
    this.loadActivityTypes();
    this.loadSheets();
  }

  private loadActivityTypes() {
    this.activityTypeService.GetAll().subscribe(a => {
      this.activityTypes = a;
    });
  }

  private loadSheets(){
    this.sheetService.GetAll().subscribe(sheets =>{
      this.sheets = sheets;
      this.sheets.sort((s1, s2) => s1.order - s2.order)
    })
  }

}
