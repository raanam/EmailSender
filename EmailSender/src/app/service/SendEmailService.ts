import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import Email from "../models/Email";

@Injectable()
export class SendEmailService {

  constructor(private http: Http) {

  }

  public send(email: Email): Promise<void> {

    let promise = new Promise<void>((resolve, reject) => {

      this.http.post("/api/Email", email).
        toPromise().then(response => {
          resolve();
        }, reason => {
          reject(reason);
        });

    });

    return promise;

  }
}
