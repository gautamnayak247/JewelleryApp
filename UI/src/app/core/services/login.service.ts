import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { AuthToken } from 'src/app/models/token';
import { UserService } from './user.service';
import * as config from "../../config/app.settings.json";

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(private http: HttpClient, private userService: UserService) {}

  signIn(userId: string, password: string) {
    console.log(userId);
    console.log(password);
    return this.http.post<AuthToken>(`${config.ApiBaseUrl}/v1/login`, { userId, password })
      .pipe(map(authToken => {
        this.userService.SetToken(authToken.token);
        console.log(authToken.token);
      }));
  }
}
