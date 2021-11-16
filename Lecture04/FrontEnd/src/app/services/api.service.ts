import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  Counter: number;

  constructor(private http: HttpClient) {
    this.Counter = 7;
  }

  private getHeaders(): HttpHeaders {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');

    // const token = this.userInfoService.getStoredToken();
    // if (token !== null) {
    //   headers = headers.append('Authorization', `Bearer ${token}`);
    // }

    return headers;
  }

  get<T>(url: string, urlParams?: HttpParams): Observable<T>{
    return this.http.get<T>(url,
      {headers: this.getHeaders(), params: urlParams});
  }

  post<T>(url: string, body: any): Observable<T> {
    return this.http.post<T>(url, JSON.stringify(body),
      { headers: this.getHeaders()});
  }

}
