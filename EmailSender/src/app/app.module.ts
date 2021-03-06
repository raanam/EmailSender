import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { SendEmailComponent } from './send-email/send-email.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SendEmailService } from './service/SendEmailService';

@NgModule({
  declarations: [
    AppComponent,
    SendEmailComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule.forRoot()
  ],
  providers: [SendEmailService],
  bootstrap: [AppComponent]
})
export class AppModule { }
