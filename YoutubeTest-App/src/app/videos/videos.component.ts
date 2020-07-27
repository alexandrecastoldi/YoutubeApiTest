import { Component, OnInit } from '@angular/core';
import { Video } from '../_models/Video';
import { SearchResult } from '../_models/SearchResult';
import { ToastrService } from 'ngx-toastr';
import { DatabaseVideoService } from '../_services/databaseVideo.service';

@Component({
  selector: 'app-videos',
  templateUrl: './videos.component.html',
  styleUrls: ['./videos.component.css']
})
export class VideosComponent implements OnInit{

  searchText = '';
  textSearched = '';
  searchOptionId = '1';
  searchOptions: any = [
    {
      id: 1,
      name: 'Youtube'
    },
    {
      id: 2,
      name: 'Database'
    }
  ];
  nextPage = '';
  videos: Video[];

  constructor(
    private databaseVideoService: DatabaseVideoService,
    private toastr: ToastrService) { }

  ngOnInit() {
  }

  searchVideos(){
    this.textSearched = this.searchText;
    this.videos = [];
    this.onScroll();
  }

  onScroll(){
    if (this.searchOptionId === '1')
    {
      this.databaseVideoService.getFromYoutube(this.textSearched, this.nextPage).subscribe(
        (_result: SearchResult) => {
          this.nextPage = _result.nextPage;
          _result.videos.forEach(element => {
            this.videos.push(element);
          });
        }, error => {
          this.toastr.error(`Error trying to load videos: ${error}`);
        }
      );
    }else{
      this.databaseVideoService.getVideosByTitle(this.textSearched).subscribe(
        (_result: Video[]) => {
          this.videos = _result;
        }, error => {
          this.toastr.error(`Error trying to load videos: ${error}`);
        }
      );
    }
  }

  saveVideo(video: Video){
    this.databaseVideoService.postVideo(video).subscribe(
      () => {
        this.toastr.success('Video added to database!');
      }, error => {
        this.toastr.error(`Error trying to add video: ${error}`);
      }
    );
  }

  removeVideo(video: Video){
    this.databaseVideoService.deleteVideo(video.id).subscribe(
      () => {
        this.toastr.success('Video removed from database!');
      }, error => {
        this.toastr.error(`Error trying to remove video: ${error}`);
        console.log(error);
      }
    );
  }

}
