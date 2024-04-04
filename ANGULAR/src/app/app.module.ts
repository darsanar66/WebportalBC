import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
// import { ProductComponent } from './components/product/product.component';
// import { DisplayProductsComponent } from './components/display-products/display-products.component';
import { ImageComponent } from './components/image/image.component';
// import { DisplayImagesComponent } from './components/display-images/display-images.component';

import {DisplayImageComponent} from './components/display-image/display-image.component';
import { EditComponent } from './edit/edit.component';
import { DownloadComponent } from './download/download.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { NavbarComponent } from './navbar/navbar.component';







@NgModule({
  declarations: [
    AppComponent,
    // ProductComponent,
    // DisplayProductsComponent,
    ImageComponent,
    DisplayImageComponent,
    EditComponent,
    DownloadComponent,
    LoginComponent,
    SignupComponent,
    NavbarComponent,
    
   



  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, // http client call
    ReactiveFormsModule,
    FormsModule // for using reactive forms
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
