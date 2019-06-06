import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostsComponent } from './posts/posts.component';
import { SeriesComponent } from './series/series.component';
import { DetailComponent } from './series/detail/detail.component';
import { SummaryComponent } from './series/summary/summary.component';
import { LinksComponent } from './links/links.component';

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
    path: 'series/detail/:id',
    component: DetailComponent
  },
  {
    path: 'links',
    component: LinksComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
