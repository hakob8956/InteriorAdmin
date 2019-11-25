import { SubCategoryEditComponent } from './subCategory/sub-category-edit/sub-category-edit.component';
import { AuthGuard } from "./helpers/auth.guard";
import { RecommendationViewComponent } from "./recommendation/recommendation-view/recommendation-view.component";
import { RecommendationEditComponent } from "./recommendation/recommendation-edit/recommendation-edit.component";
import { MyTestComponent } from "./test/my-test/my-test.component";
import { InteriorViewComponent } from "./Interior/interior-view/interior-view.component";
import { ShopEditComponent } from "./shop/shop-edit/shop-edit.component";
import { ShopViewComponent } from "./shop/shop-view/shop-view.component";
import { CategoryEditComponent } from "./Category/category-edit/category-edit.component";
import { CategoryViewComponent } from "./Category/category-view/category-view.component";
import { LanguageEditComponent } from "./Language/language-edit/language-edit.component";
import { AdminChangePasswordComponent } from "./Admin/admin-change-password/admin-change-password.component";
import { AdminEditComponent } from "./Admin/admin-edit/admin-edit.component";
import { AdminViewComponent } from "./Admin/admin-view/admin-view.component";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { DashboardComponent } from "./dashboard/dashboard.component";
import { LanguageViewComponent } from "./Language/language-view/language-view.component";
import { BrandViewComponent } from "./Brand/brand-view/brand-view.component";
import { BrandEditComponent } from "./Brand/brand-edit/brand-edit.component";
import { InteriorEditComponent } from "./Interior/interior-edit/interior-edit.component";
import { LoginComponent } from "./authentication/login/login.component";
import { SubCategoryViewComponent } from './subCategory/sub-category-view/sub-category-view.component';

const routes: Routes = [
  { path: "", component: DashboardComponent, canActivate: [AuthGuard] },
  {
    path: "dashboard",
    component: DashboardComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "adminView",
    component: AdminViewComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "adminEdit",
    component: AdminEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "adminEdit/:id",
    component: AdminEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "adminChangePassword/:id",
    component: AdminChangePasswordComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "languageView",
    component: LanguageViewComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "languageEdit",
    component: LanguageEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "languageEdit/:id",
    component: LanguageEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "categoryView",
    component: CategoryViewComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "categoryEdit",
    component: CategoryEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "subCategoryView",
    component: SubCategoryViewComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "subCategoryEdit",
    component: SubCategoryEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "subCategoryEdit/:id",
    component: SubCategoryEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "categoryEdit/:id",
    component: CategoryEditComponent,
    canActivate: [AuthGuard]
  },
  { path: "shopView", component: ShopViewComponent, canActivate: [AuthGuard] },
  { path: "shopEdit", component: ShopEditComponent, canActivate: [AuthGuard] },
  {
    path: "shopEdit/:id",
    component: ShopEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "brandView",
    component: BrandViewComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "brandEdit",
    component: BrandEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "brandEdit/:id",
    component: BrandEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "interiorView",
    component: InteriorViewComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "interiorEdit",
    component: InteriorEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "interiorEdit/:id",
    component: InteriorEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "recommendationView",
    component: RecommendationViewComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "recommendationEdit",
    component: RecommendationEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "recommendationEdit/:id",
    component: RecommendationEditComponent,
    canActivate: [AuthGuard]
  },
  { path: "test", component: MyTestComponent },
  { path: "login", component: LoginComponent },
  { path: "**", redirectTo: "" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
