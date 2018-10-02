import { Component, OnInit } from '@angular/core';
import { AuthService, GoogleLoginProvider } from 'angularx-social-login';
@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss']
})
export class LandingComponent implements OnInit {
  constructor(private socialAuthService: AuthService) {}

  public signinWithGoogle() {
    let socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;

    this.socialAuthService
      .signIn(socialPlatformProvider)
      .then((userData) => {
        //on success
        //this will return user data from google. What you need is a user token which you will send it to the server
        console.log(userData);
        debugger;
        //this.sendToRestApiMethod(userData.idToken);
      })
      .catch((err) => {
        console.log(err);
      });
  }
  //   sendToRestApiMethod(token: string) : void {
  //     this.http.post(“url to google login in your rest api”, { token: token } }
  //       .subscribe(onSuccess => {
  //        //login was successful
  //        //save the token that you got from your REST API in your preferred location i.e. as a Cookie or LocalStorage as you do with normal login
  //      }, onFail => {
  //         //login was unsuccessful
  //         //show an error message
  //      }
  //    );
  //  }
  ngOnInit() {}
}
