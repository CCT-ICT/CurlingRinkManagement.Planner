import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SheetOverviewComponent } from "./components/sheet-overview/sheet-overview.component";
import { TimeOverviewComponent } from "./components/time-overview/time-overview.component";
import { SheetService } from './services/sheet.service';
import { SheetModel } from './models/sheet.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SheetOverviewComponent, TimeOverviewComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {

  public sheets: SheetModel[] = [];
  constructor(private sheetService: SheetService){}

  ngOnInit(): void {
    this.sheetService.GetAll().subscribe(sheets =>{
      this.sheets = sheets;
      this.sheets.sort((s1, s2) => s1.order - s2.order)
    })
  }

}
