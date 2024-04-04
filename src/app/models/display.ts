// display-image.model.ts

export interface DisplayImage {
  imageContentBase64: string;
  id: number;
  UserName: string;
  file: File;
  imageUrl: string;
  comment: string;
  GoogleDriveFileId:string;
}
