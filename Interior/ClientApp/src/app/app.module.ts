import { UserDataService, LanguageDataService } from './services/KendoCenter.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminViewComponent } from './Admin/admin-view/admin-view.component';
import { FooterComponent } from './footer/footer.component';
import { GridModule } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { InfoDetailComponent } from './info-detail/info-detail.component';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { AdminEditComponent } from './Admin/admin-edit/admin-edit.component';
import { AdminChangePasswordComponent } from './Admin/admin-change-password/admin-change-password.component';
import { ShowMessageComponent } from './show-message/show-message.component';
import { LanguageEditComponent } from './Language/language-edit/language-edit.component';
import { LanguageViewComponent } from './Language/language-view/language-view.component';



@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    SidebarComponent,
    DashboardComponent,
    AdminViewComponent,
    FooterComponent,
    InfoDetailComponent,
    AdminEditComponent,
    AdminChangePasswordComponent,
    ShowMessageComponent,
    LanguageEditComponent,
    LanguageViewComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    AppRoutingModule,
    FormsModule,
    NgbModule.forRoot(),
    GridModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ButtonsModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [UserDataService,LanguageDataService],
  bootstrap: [AppComponent]
})
export class AppModule { }