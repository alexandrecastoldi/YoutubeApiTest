import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { VideosComponent } from './videos/videos.component';
import { DatabaseVideoService } from './_services/databaseVideo.service';

@NgModule({
   declarations: [
      AppComponent,
      VideosComponent
   ],
   imports: [
      BrowserModule,
      InfiniteScrollModule,
      TooltipModule.forRoot(),
      ToastrModule.forRoot({
         timeOut: 3000,
         preventDuplicates: true,
         progressBar: true
      }),
      BrowserAnimationsModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule
   ],
   providers: [
      DatabaseVideoService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }

platformBrowserDynamic().bootstrapModule(AppModule);