import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VideoService {
  private apiUrl = 'http://localhost:5202/api/VideoUpload'; // Update with your backend API URL

  constructor(private http: HttpClient) { }

  uploadVideo(file: File, userName: string, comment: string): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('UserName', userName);
    formData.append('comment', comment);
    return this.http.post(`${this.apiUrl}/Upload?UserName=${userName}`, formData, {
        headers: { 'Content-Type': 'multipart/form-data' }
      });
      
  }

  // Other methods in your VideoService
}

