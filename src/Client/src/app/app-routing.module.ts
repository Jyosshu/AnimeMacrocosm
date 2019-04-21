import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostsComponent } from './posts/posts.component';
import { SeriesComponent } from './series/series.component';

const routes: Routes = [
  { path: 'posts', component: PostsComponent },
  { path: 'series', component: SeriesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
