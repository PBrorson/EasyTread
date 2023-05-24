import { NgChartsModule } from 'ng2-charts';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxPaginationModule } from 'ngx-pagination';

import { registerLocaleData } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import localeSv from '@angular/common/locales/sv';
import { LOCALE_ID, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { NgbDropdownModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {
  TranslateLoader,
  TranslateModule,
  TranslateService,
} from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppComponent } from './app.component';
import { CrossingTableComponent } from './components/layout/crossing-table/crossing-table.component';
import { DatePickerComponent } from './components/layout/date-picker/date-picker.component';
import { HeaderComponent } from './components/layout/header/header.component';
import { NotificationComponent } from './components/layout/notification/notification.component';
import { PaginationComponent } from './components/layout/pagination/pagination.component';
import { SearchBarComponent } from './components/layout/search-bar/search-bar.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar.component';
import { CrossingBadTiresComponent } from './components/views/crossing-bad-tires/crossing-bad-tires.component';
import { CrossingDetailsComponent } from './components/views/crossing-details/crossing-details.component';
import { CrossingInfoComponent } from './components/views/crossing-info/crossing-info.component';
import { HomeComponent } from './components/views/home/home.component';
import { CrossingChartComponent } from './components/views/crossing-chart/crossing-chart.component';
import { API_BASE_URL_EASYTREAD, CrossingClient } from './domain/client';

const routes: Routes = [
  { path: '', component: CrossingTableComponent },
  { path: 'crossing-info', component: CrossingInfoComponent },
  { path: 'crossing-details', component: CrossingDetailsComponent },
  { path: 'crossing-bad-tires', component: CrossingBadTiresComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DatePickerComponent,
    CrossingInfoComponent,
    CrossingDetailsComponent,
    CrossingBadTiresComponent,
    HeaderComponent,
    CrossingChartComponent,
    SidebarComponent,
    PaginationComponent,
    CrossingTableComponent,
    SearchBarComponent,
    NotificationComponent,
  ],
  imports: [
    BrowserModule,
    NgbDropdownModule,
    NgChartsModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    RouterModule.forRoot(routes),
    ReactiveFormsModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (http: HttpClient) =>
          new TranslateHttpLoader(http, '/assets/i18n/', '.json'),
        deps: [HttpClient],
      },
    }),
    NgxPaginationModule,
    NgbModule,
  ],
  exports: [RouterModule],
  providers: [
    CrossingClient,
    { provide: API_BASE_URL_EASYTREAD, useValue: 'https://localhost:7065' },
    { provide: LOCALE_ID, useValue: 'se' },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
  private messages: string[] = [];

  constructor(private translate: TranslateService) {
    registerLocaleData(localeSv);

    this.translate.setDefaultLang('se');
    this.translate.use('se');
  }
}
