import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Image } from '../models/image'; // Import the Image type from the appropriate location
import { DisplayImage } from '../models/display';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  [x: string]: any;
  public  apiUrl = 'http://localhost:5202/api/ImageUpload'; // Update with your backend API URL

  constructor(private http: HttpClient) { }

  uploadImage(formData: FormData, userName: string) {
    return this.http.post(`${this.apiUrl}/Upload?UserName=${userName}`, formData);
  }

  getAllImages(): Observable<DisplayImage[]> {
    return this.http.get<DisplayImage[]>(`${this.apiUrl}/GetAllImages`);
  }
  deleteImageAndComment(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/DeleteImageAndComment/${id}`);
  }
  // editComment(id: number, comment: string): Observable<any> {
  //   return this.http.put<any>(`${this.apiUrl}/EditComment?id=${id}`,  comment );
  // editComment(id: number, comment: string): Observable<any> {
  //     return this.http.post(`${this.apiUrl}/EditComment?id=${id}`, comment);

  editComment(id: number, comment: string): Observable<any> {
    const url = `${this.apiUrl}/EditComment/${id}?comment=${encodeURIComponent(comment)}`;
    return this.http.put(url, null); // Send a PUT request with no request body
    }
    downloadFile(GoogleDriveFileId: string):Observable<any>{
      return this.http.get<string>(`${this.apiUrl}/DownloadFile/${GoogleDriveFileId}`);
    }
  }

