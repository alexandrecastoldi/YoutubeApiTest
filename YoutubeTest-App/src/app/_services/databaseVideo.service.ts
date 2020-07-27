import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Video } from '../_models/Video';
import { SearchResult } from '../_models/SearchResult';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DatabaseVideoService {

  constructor(private http: HttpClient) { }

  postVideo(video: Video){
    return this.http.post(`${environment.baseUrl}/YoutubeVideo`, video);
  }

  deleteVideo(id: string){
    return this.http.delete(`${environment.baseUrl}/YoutubeVideo/${id}`);
  }

  getVideosByTitle(title: string): Observable<Video[]>{
    return this.http.get<Video[]>(`${environment.baseUrl}/YoutubeVideo/getByTitle?title=${title}`);
  }

  getFromYoutube(searchText: string, pageToken: string): Observable<SearchResult>{
    let url = `${environment.baseUrl}/YoutubeVideo/getFromYoutube?query=${searchText}`;
    if (pageToken !== '') {
      url = `${url}&pageToken=${pageToken}`;
    }
    return this.http.get<SearchResult>(url);
  }
}
