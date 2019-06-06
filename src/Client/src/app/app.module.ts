import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PostsComponent } from './posts/posts.component';

import { PostsService } from './posts/posts.service';
import { SeriesService } from './series/series.service';

import { SeriesComponent } from './series/series.component';
import { DetailComponent } from './series/detail/detail.component';
import { SummaryComponent } from './series/summary/summary.component';

import { RequestCache } from './cache/request-cache.service';
import { CachingInterceptor } from './cache/cache-interceptor';
import { LinksComponent } from './links/links.component';

@NgModule({
  declarations: [
    AppComponent,
    PostsComponent,
    SeriesComponent,
    DetailComponent,
    SummaryComponent,
    LinksComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    // RequestCache,
    // { provide: HTTP_INTERCEPTORS, useClass: CachingInterceptor, multi: true },
    PostsService,
    SeriesService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
