import {
  UserDataService,
  LanguageDataService,
  CategoryDataService,
  ShopDataService,
  BrandDataService,
  InteriorDataService,
  RecommendationDataService
} from "./services/KendoCenter.service";
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AppRoutingModule } from "./app-routing.module";

import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { AppComponent } from "./app.component";
import { NavbarComponent } from "./navbar/navbar.component";
import { SidebarComponent } from "./sidebar/sidebar.component";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { AdminViewComponent } from "./Admin/admin-view/admin-view.component";
import { FooterComponent } from "./footer/footer.component";
import { GridModule } from "@progress/kendo-angular-grid";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { AdminEditComponent } from "./Admin/admin-edit/admin-edit.component";
import { AdminChangePasswordComponent } from "./Admin/admin-change-password/admin-change-password.component";
import { ShowMessageComponent } from "./show-message/show-message.component";
import { LanguageViewComponent } from "./Language/language-view/language-view.component";
import { LanguageEditComponent } from "./Language/language-edit/language-edit.component";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { CategoryViewComponent } from "./Category/category-view/category-view.component";
import { CategoryEditComponent } from "./Category/category-edit/category-edit.component";
import { ShopViewComponent } from "./shop/shop-view/shop-view.component";
import { ShopEditComponent } from "./shop/shop-edit/shop-edit.component";
import { BrandViewComponent } from "./Brand/brand-view/brand-view.component";
import { BrandEditComponent } from "./Brand/brand-edit/brand-edit.component";
import { InteriorViewComponent } from "./Interior/interior-view/interior-view.component";
import { InteriorEditComponent } from "./Interior/interior-edit/interior-edit.component";
import { ContentEditComponent } from "./form-tools/content-edit/content-edit.component";
import { MyTestComponent } from "./test/my-test/my-test.component";
import { FileEditComponent } from "./form-tools/file-edit/file-edit.component";
import { OptionDetailViewComponent } from "./form-tools/optionDetail/option-detail-view/option-detail-view.component";
import { OptionDetailViewOneComponent } from "./form-tools/optionDetail/option-detail-view-one/option-detail-view-one.component";
import { OptionDetailAddComponent } from "./form-tools/optionDetail/option-detail-add/option-detail-add.component";
import { ChooseRecommendViewComponent } from "./form-tools/choose-recommend/choose-recommend-view/choose-recommend-view.component";
import { RecommendationViewComponent } from "./recommendation/recommendation-view/recommendation-view.component";
import { RecommendationEditComponent } from "./recommendation/recommendation-edit/recommendation-edit.component";
import { LoginComponent } from "./authentication/login/login.component";
import { AlertComponent } from "./form-tools/alert/alert.component";
import { AlertService } from "./services/alert.service";
import { JwtInterceptor } from "./helpers/jwt.interceptor";
import { ErrorInterceptor } from "./helpers/error.interceptor";
import { AuthenticationService } from "./services/authentication.service";
import { SubCategoryEditComponent } from './subCategory/sub-category-edit/sub-category-edit.component';
import { SubCategoryViewComponent } from './subCategory/sub-category-view/sub-category-view.component';
import { CategoryAttachmentComponent } from './form-tools/category-attachment/category-attachment.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ModelSelectOneViewComponent } from './form-tools/model-select-one-view/model-select-one-view.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    SidebarComponent,
    DashboardComponent,
    AdminViewComponent,
    FooterComponent,
    AdminEditComponent,
    AdminChangePasswordComponent,
    ShowMessageComponent,
    LanguageViewComponent,
    LanguageEditComponent,
    CategoryViewComponent,
    CategoryEditComponent,
    ShopViewComponent,
    ShopEditComponent,
    BrandViewComponent,
    BrandEditComponent,
    InteriorViewComponent,
    InteriorEditComponent,
    ContentEditComponent,
    MyTestComponent,
    FileEditComponent,
    OptionDetailViewComponent,
    OptionDetailAddComponent,
    OptionDetailViewOneComponent,
    OptionDetailAddComponent,
    ChooseRecommendViewComponent,
    RecommendationViewComponent,
    RecommendationEditComponent,
    LoginComponent,
    AlertComponent,
    SubCategoryEditComponent,
    SubCategoryViewComponent,
    CategoryAttachmentComponent,
    ModelSelectOneViewComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    AppRoutingModule,
    FormsModule,
    NgbModule.forRoot(),
    NgMultiSelectDropDownModule.forRoot(),
    GridModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ButtonsModule,
    ReactiveFormsModule,
    FormsModule,
    FontAwesomeModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

    UserDataService,
    LanguageDataService,
    CategoryDataService,
    ShopDataService,
    BrandDataService,
    InteriorDataService,
    RecommendationDataService,
    AlertService,
    AuthenticationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
