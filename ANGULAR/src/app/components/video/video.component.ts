import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VideoService } from 'src/app/services/video.service';

@Component({
  selector: 'app-video',
  templateUrl: './video.component.html',
})
export class VideoComponent {

  frm: FormGroup;
  file: File | null = null; // Initialize with null
  status: any;

  constructor(
    private videoService: VideoService,
    private fb: FormBuilder
  ) {
    this.frm = this.fb.group({
      UserName: ['', Validators.required],
      comment: ['', Validators.required], // Add comment field to form
      file: ['', Validators.required]
    });
  }

  onSubmit() {
    const userName = this.frm.value.UserName;
    const comment = this.frm.value.comment;
    
    if (!userName) {
      console.error('Username is missing');
      return;
    }
  
    if (!comment) {
      console.error('Comment is missing');
      return;
    }
  
    if (!this.file) {
      console.error('Video file is missing');
      return;
    }
  
    this.videoService.uploadVideo(this.file, userName, comment).subscribe(
      (response: any) => {
        console.log('Upload successful:', response);
        this.status = { statusCode: 200, message: 'Upload successful' };
      },
      (error: any) => {
        console.error('Upload failed:', error);
        this.status = { statusCode: 400, message: 'Upload failed. Please try again.' };
      }
    );
  }
  

  onChange(event: any) {
    // Check if files are present
    if (event.target.files && event.target.files.length > 0) {
      // Extract the first file from the array-like object
      this.file = event.target.files[0];
    }
  }

}
