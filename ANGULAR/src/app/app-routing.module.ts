// import { NgModule } from '@angular/core';
// import { RouterModule, Routes } from '@angular/router';
// import { ImageComponent } from './components/image/image.component'; 
// import { DisplayImageComponent } from './components/display-image/display-image.component';
// import { EditComponent } from './edit/edit.component';
//  // Import EditComponent

// const routes: Routes = [
//   {
//     path: 'add-image',
//     component: ImageComponent
//   },
//   {
//     path: 'display-image',
//     component: DisplayImageComponent
//   },
//   {
//     path: 'edit', // Define a route for the edit page with a parameter for the image ID
//     component: EditComponent // Specify the EditComponent as the component to load
//   },
//   {
//     path: 'redirect',
//     redirectTo: '/edit',
//     pathMatch: 'full'
//   },
  
//   {
//     path: 'redirect',
//     redirectTo: '/display-image',
//     pathMatch: 'full'
//   },
//   {
//     path: '**', // Catch-all route for handling unknown paths
//     redirectTo: '/display-image'
//   }
// ];

// @NgModule({
//   imports: [RouterModule.forRoot(routes)],
//   exports: [RouterModule]
// })
// export class AppRoutingModule { }
// // import { RouterModule, Routes } from '@angular/router';
// // // import { DisplayProductsComponent } from './components/display-products/display-products.component';
// // // import { ProductComponent } from './components/product/product.component';
// // import { ImageComponent } from './components/image/image.component'; 
// // // import { DisplayImagesComponent } from './components/display-images/display-images.component';
// //  // Import CommentComponent
// // import { NgModule } from '@angular/core';
// // import { DisplayImageComponent } from './components/display-image/display-image.component';
// // // import { DisplayImageComponent } from './components/display-image/display-image.component';



// // const routes: Routes = [
// //   // {
// //   //   path: 'add-product',
// //   //   component: ProductComponent
// //   // },
// //   {
// //     path: 'add-image',
// //     component: ImageComponent
// //   },
// //   {
// //     path: 'display-image',
// //     component: DisplayImageComponent
// //   },

// //   {
// //     path: 'redirect',
// //     redirectTo: '/display-image',
// //     pathMatch: 'full'
// //   },

 

  
// //   {
// //     path: 'redirect',
// //     redirectTo: '/add-image',
// //     pathMatch: 'full'
// //   },
  
  
// // ];

// // @NgModule({
// //   imports: [RouterModule.forRoot(routes)],
// //   exports: [RouterModule]
// // })
// // export class AppRoutingModule { }
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ImageComponent } from './components/image/image.component'; 
import { DisplayImageComponent } from './components/display-image/display-image.component';
import { EditComponent } from './edit/edit.component';
import { DownloadComponent } from './download/download.component';
import { LoginComponent } from './login/login.component';
import { NavbarComponent } from './navbar/navbar.component';

const routes: Routes = [

  // {
  //   path: 'download/:id',
  //   component: DownloadComponent
  // },
  {
    path: 'login', // Define a route for the edit page with a parameter for the image ID
    component: LoginComponent // Specify the EditComponent as the component to load
  },
  {
    path: 'add-image',
    component: ImageComponent
  },
  {
    path: 'display-image',
    component: DisplayImageComponent
  },
  {
    path: 'edit/:id', // Define a route for the edit page with a parameter for the image ID
    component: EditComponent // Specify the EditComponent as the component to load
  },
  // {
  //   path: 'redirect',
  //   redirectTo: '/display-image',
  //   pathMatch: 'full'
  // },
  // {
  //   path: '**', // Catch-all route for handling unknown paths
  //   redirectTo: '/display-image'
  // },
  {
    path: 'add-navbar',
    component: NavbarComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
