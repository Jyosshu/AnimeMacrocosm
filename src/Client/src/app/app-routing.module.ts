import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostsComponent } from './posts/posts.component';
import { SeriesComponent } from './series/series.component';
import { DetailComponent } from './series/detail/detail.component';
import { SummaryComponent } from './series/summary/summary.component';

const routes: Routes = [
  {
    path: 'posts',
    component: PostsComponent
  },
  {
    path: 'series',
    component: SeriesComponent
  },
  {
    path: 'series/summary/:id',
    component: SummaryComponent
  },
  {
    path: 'detail',
    component: DetailComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
