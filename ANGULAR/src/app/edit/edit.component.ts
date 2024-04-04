import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ImageService } from '../services/image.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
})
export class EditComponent implements OnInit {
  id: number = 0; // Initialize with a default value
  frm: FormGroup;
  status: any;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private imageService: ImageService
  ) {
    this.frm = this.fb.group({
      comment: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const idString = params['id'];
      const idNumber = parseInt(idString, 10);
      if (!isNaN(idNumber) && idNumber > 0) {
        this.id = idNumber;
      } else {
        console.error('Invalid ID');
        // Handle the invalid ID scenario here, such as redirecting to a different page or showing an error message
      }
    });
  }

  onSubmit() {
    const editedComment: string = this.frm.value.comment.trim();

    if (!editedComment) {
      console.error('Edited comment is missing');
      return;
    }

    this.imageService.editComment(this.id, editedComment).subscribe(
      (response: any) => {
        console.log('Comment edited successfully:', response);
        this.status = { statusCode: 200, message: 'Comment edited successfully' };
      },
      (error: any) => {
        console.error('Error editing comment:', error);
        this.status = { statusCode: error.status, message: error.message };
      }
    );
  }
}
