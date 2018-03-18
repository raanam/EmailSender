import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.css']
})
export class SendEmailComponent implements OnInit {

  private formModel: FormGroup;

  constructor(private formBuilder: FormBuilder) { }

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

  }

  private buildFormModel(): FormGroup {
    let formModel: FormGroup =
      this.formBuilder.group({
        subject: ["", Validators.required],
        message: ["", Validators.required],
        to: ["", [validateEmailRecipient]],
        cc: ["", [validateEmailRecipient]],
        bcc: ["", [validateEmailRecipient]]
      });

    return formModel;
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
    var emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (emailRegex.test(eachEmail.toLowerCase()) == false) {
      areAllValid = 0;
    }
  });

  if (areAllValid == 0) {
    return { invalidEmail: true };
  }

  return null;

}
