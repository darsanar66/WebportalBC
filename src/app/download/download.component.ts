import { Component } from '@angular/core';
import { ImageService } from '../services/image.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-download',
  templateUrl: './download.component.html',
  styleUrls: ['./download.component.css']
})
export class DownloadComponent {
  constructor(private imageService: ImageService) {}

  download(fileId: string): void {
    this.imageService.downloadFile(fileId).subscribe(
      (response: HttpResponse<Blob>) => {
        const blob = new Blob([response.body!], { type: 'application/octet-stream' });
        const downloadUrl = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = downloadUrl;
        link.download = 'downloaded_file.ext'; // Set the downloaded file name
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        window.URL.revokeObjectURL(downloadUrl);
      },
      (      error: any) => {
        console.error('Error downloading file:', error);
        // Handle error display or notification as needed
      }
    );
  }
  }

