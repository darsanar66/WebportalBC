import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
})
export class ImageComponent {

  frm: FormGroup;
  file: File | null = null; // Initialize with null
  status: any;

  constructor(
    private imageService: ImageService,
    private fb: FormBuilder
  ) {
    this.frm = this.fb.group({
      UserName: ['', Validators.required],
      comment: [''], // Add comment field to form
      file: ['', Validators.required]
    });
  }

  onSubmit() {
    const userName = this.frm.value.UserName;
    const comment = this.frm.value.comment; // Get comment from form
    const formData = new FormData();
  
    if (!userName) {
      console.error('Username is missing');
      return;
    }

    // Check if file is present
    if (!this.file) {
      console.error('Image file is missing');
      return;
    }
    
    // Append username, comment, and file to FormData
    formData.append('file', this.file);
    formData.append('comment', comment);

    // Send the FormData to the backend along with username and comment
    this.imageService.uploadImage(formData, userName).subscribe(
      (response: any) => {
        console.log('Upload successful:', response);
        this.status = { statusCode: 200, message: 'Upload successful' };
        this.frm.reset();
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
