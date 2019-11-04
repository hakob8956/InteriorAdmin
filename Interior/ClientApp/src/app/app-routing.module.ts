import { LanguageEditComponent } from './Language/language-edit/language-edit.component';
import { AdminChangePasswordComponent } from "./Admin/admin-change-password/admin-change-password.component";
import { AdminEditComponent } from "./Admin/admin-edit/admin-edit.component";
import { AdminViewComponent } from "./Admin/admin-view/admin-view.component";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { DashboardComponent } from "./dashboard/dashboard.component";
import { LanguageViewComponent } from "./Language/language-view/language-view.component";

const routes: Routes = [
  { path: "", redirectTo: "/adminView", pathMatch: "full" },
  { path: "dashboard", component: DashboardComponent },
  { path: "adminView", component: AdminViewComponent },
  { path: "adminEdit", component: AdminEditComponent },
  { path: "adminEdit/:id", component: AdminEditComponent },
  { path: "adminChangePassword/:id", component: AdminChangePasswordComponent },
  { path: "languageView", component: LanguageViewComponent },
  { path: "languageEdit", component: LanguageEditComponent },
  { path: "languageEdit/:id", component: LanguageEditComponent }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
