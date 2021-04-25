import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { User } from "src/app/models/user";
import * as config from "../../config/app.settings.json";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private token: string;
  private loggedInUser: User;
  constructor(private http: HttpClient) {

  }
  public SetToken(authtoken: string): void {
    this.token = authtoken;
  }
  public GetToken(): string {
    return this.token;
  }
  public SetUserContext(user: User) {
    this.loggedInUser = user;
    console.log(this.loggedInUser.firstName);
  }
  public GetUserContext(): User {
    return this.loggedInUser;
  }
  GetUser() {
    return this.http.get<User>(`${config.ApiBaseUrl}/v1/user`)
      .pipe(map(user => {
        this.SetUserContext(user);
        console.log(user);
      }));

  }
}
