import { DataCenterService } from './services/DataCenter.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { GridModule } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CategoriesService } from './northwind.service';
import { FooterComponent } from './footer/footer.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { TableUserReviewComponent } from './table-user-review/table-user-review.component';

@NgModule({
  declarations: [
    AppComponent, 
    DashboardComponent,
    NavbarComponent,
    FooterComponent,
    SidebarComponent,
    UserDetailComponent,
    TableUserReviewComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    AppRoutingModule,
    FormsModule,
    GridModule,
    BrowserAnimationsModule,
    HttpClientModule,
    NgbModule.forRoot()
  ],
  providers: [CategoriesService,DataCenterService],
  bootstrap: [AppComponent]
})
export class AppModule { }