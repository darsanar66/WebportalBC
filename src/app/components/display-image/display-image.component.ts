// import { Component, OnInit } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { Image } from 'src/app/models/image';
// import { ImageService } from 'src/app/services/image.service';
// import { environment } from 'src/environments/environment';

// @Component({
//   selector: 'app-display-image',
//   templateUrl: './display-image.component.html',
// })
// export class DisplayImageComponent implements OnInit {
  
//   images: Image[] = [];
//   imageBaseUrl = environment.baseUrl + '/ImageUpload/';

//   constructor(private imageService: ImageService) { }

//   ngOnInit(): void {
//     this.getAllImages();
//   }

//   getAllImages() {
//     this.imageService.getAllImages().subscribe({
//       next: (images: Image[]) => {
//         this.images = images;
//       },
//       error: (err: any) => {
//         console.log('Error fetching images:', err);
//       }
//     });
//   }
// }
// image-table.component.ts

// import { Component, OnInit } from '@angular/core';

// import { ImageService } from 'src/app/services/image.service';

// @Component({
//   selector: 'app-display-image',
//   templateUrl: './display-image.component.html',

// })
// export class DisplayImageComponent implements OnInit {
//   images: any[] = [];

//   constructor(private imageService: ImageService) { }

//   ngOnInit(): void {
//     this.loadImages();
//   }

//   loadImages() {
//     this.imageService.getAllImages().subscribe(
//       (images: any[]) => {
//         this.images = images;
//       },
//       error => {
//         console.error('Error fetching images:', error);
//         // Handle the error, show an error message to the user, etc.
//       }
//     );
//   }
// }
// image-table.component.ts

import { Component, OnInit } from '@angular/core';
import { DisplayImage } from 'src/app/models/display';
import { Router } from '@angular/router';

import { ImageService } from 'src/app/services/image.service';
// import { ImageService } from 'path/to/image.service';
// import { DisplayImage } from 'path/to/display-image.model';

@Component({
  selector: 'app-display-image',
  templateUrl: './display-image.component.html',

})
export class DisplayImageComponent implements OnInit {
  images: DisplayImage[] = [];
  http: any;
  // router: any;

  constructor(
    private router: Router,
    private imageService: ImageService) { }

  ngOnInit(): void {
    this.loadImages();
  }

  loadImages() {
    this.imageService.getAllImages().subscribe(
      (images: DisplayImage[]) => {
        this.images = images.map(image => ({
          ...image,
          imageUrl: 'data:image/png;base64,' + image.imageContentBase64
        }));
      },
      error => {
        console.error('Error fetching images:', error);
      }
    );
  }
 deleteImageAndComment(id: number, index: number) {
    this.imageService.deleteImageAndComment(id)
      .subscribe(
        () => {
          console.log('Image and comment deleted successfully');
          // Remove the container from the UI
          this.images.splice(index, 1);
        },
        (error) => {
          console.error('Error deleting image and comment:', error);
          // Handle error or display a message to the user
        }
      );
  }

  editImage(id: number) {
    this.router.navigate(['/edit', id]);
 
  }
  downloadFile(GoogleDriveFileId: string): void {
    
    const downloadUrl = `${this.imageService.apiUrl}/DownloadFile/${GoogleDriveFileId}`;
    const link = document.createElement('a');
    link.href = downloadUrl;
    link.download = 'image.png'; // Set the filename as needed
    link.style.display = 'none'; // Hide the link
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    console.log(GoogleDriveFileId);
  }
  
  }
  
  
  
