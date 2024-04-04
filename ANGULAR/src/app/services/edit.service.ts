// edit.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EditService {

  constructor(private http: HttpClient) { }

  editComment(Id: number, newComment: string): Observable<any> {
    const apiUrl = `'http://localhost:5202/api/ImageUpload/EditComment/${Id}`;
    return this.http.put(apiUrl, { comment: newComment });
  }
}
