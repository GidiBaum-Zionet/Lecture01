import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {Page01Component} from "./pages/page01/page01.component";
import {Page02Component} from "./pages/page02/page02.component";
import {Page03Component} from "./pages/page03/page03.component";

const routes: Routes = [
  { path: '', redirectTo: '/page01', pathMatch: 'full' },
  { path: 'page01', component: Page01Component},
  { path: 'page02', component: Page02Component},
  { path: 'page03', component: Page03Component},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
