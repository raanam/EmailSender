import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import Email from '../models/Email';
import { SendEmailService } from '../service/SendEmailService';

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.css']
})
export class SendEmailComponent implements OnInit {

  private formModel: FormGroup;
  private sendingEmail: boolean;

  constructor(private formBuilder: FormBuilder, private sendEmailSvc: SendEmailService) { }

  ngOnInit() {
    this.formModel = this.buildFormModel();
  }

  public send(): void {

    if (!this.formModel.get("to").value &&
      !this.formModel.get("cc").value &&
      !this.formModel.get("bcc").value) {
      alert("Atleast one email recipient is required. Please enter value in To, Cc Or Bcc.");
      return;
    }

    let email: Email = new Email();
    email.to = this.formModel.get("to").value ? this.formModel.get("to").value.split(",") : [];
    email.cc = this.formModel.get("cc").value ? this.formModel.get("cc").value.split(",") : [];
    email.bcc = this.formModel.get("bcc").value ? this.formModel.get("bcc").value.split(",") : [];
    email.subject = this.formModel.get("subject").value;
    email.message = this.formModel.get("message").value;

    this.sendingEmail = true;
    this.sendEmailSvc.send(email).then(() => {
      this.sendingEmail = false;
      this.reset();
      alert("Email sent successfully.");
    }, error => {
      this.sendingEmail = false;
      alert("Error sending email -> " + error);
    });

  }

  private buildFormModel(): FormGroup {
    let formModel: FormGroup =
      this.formBuilder.group({
        subject: ["", Validators.required],
        message: ["", Validators.required],
        to: ["", [Validators.required, validateEmailRecipient]],
        cc: ["", [validateEmailRecipient]],
        bcc: ["", [validateEmailRecipient]]
      });

    return formModel;
  }

  private reset(): void {
    this.formModel = this.buildFormModel();
  }

}

export function validateEmailRecipient(control: AbstractControl): any {

  // Check only if some value is entered.
  if (!control.value) {
    return null;
  }

  let areAllValid: number = 1;

  let enteredEmailIds: string[] = control.value.split(",");
  enteredEmailIds.forEach(eachEmail => {
    var trimmed = eachEmail.trim();
    var emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (emailRegex.test(trimmed.toLowerCase()) == false) {
      areAllValid = 0;
    }
  });

  if (areAllValid == 0) {
    return { invalidEmail: true };
  }

  return null;

}
