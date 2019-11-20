import { RecommendationViewComponent } from './recommendation/recommendation-view/recommendation-view.component';
import { RecommendationEditComponent } from './recommendation/recommendation-edit/recommendation-edit.component';
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

const routes: Routes = [
  { path: "", redirectTo: "/adminView", pathMatch: "full" },
  { path: "dashboard", component: DashboardComponent },
  { path: "adminView", component: AdminViewComponent },
  { path: "adminEdit", component: AdminEditComponent },
  { path: "adminEdit/:id", component: AdminEditComponent },
  { path: "adminChangePassword/:id", component: AdminChangePasswordComponent },
  { path: "languageView", component: LanguageViewComponent },
  { path: "languageEdit", component: LanguageEditComponent },
  { path: "languageEdit/:id", component: LanguageEditComponent },
  { path: "categoryView", component: CategoryViewComponent },
  { path: "categoryEdit", component: CategoryEditComponent },
  { path: "categoryEdit/:id", component: CategoryEditComponent },
  { path: "shopView", component: ShopViewComponent },
  { path: "shopEdit", component: ShopEditComponent },
  { path: "shopEdit/:id", component: ShopEditComponent },
  { path: "brandView", component: BrandViewComponent },
  { path: "brandEdit", component: BrandEditComponent },
  { path: "brandEdit/:id", component: BrandEditComponent },
  { path: "interiorView", component: InteriorViewComponent },
  { path: "interiorEdit", component: InteriorEditComponent },
  { path: "interiorEdit/:id", component: InteriorEditComponent },
  { path: "recommendationView", component: RecommendationViewComponent },
  { path: "recommendationEdit", component: RecommendationEditComponent },
  { path: "recommendationEdit/:id", component: RecommendationEditComponent },
  { path: "test", component: MyTestComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
