import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Profile } from '../interfaces/profile.interface';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  http = inject(HttpClient)

  baseApiUrl = 'https://localhost:7182/api/'

  getTestAccounts(){
    return this.http.get<Profile[]>(`${this.baseApiUrl}Account/testAccounts`)
  }
}
