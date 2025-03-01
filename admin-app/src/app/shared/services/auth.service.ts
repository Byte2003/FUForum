import { Injectable } from '@angular/core';
import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { BehaviorSubject } from 'rxjs';
import { BaseService } from './base.service';
import { Profile } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private manager = new UserManager(getClientSettings());
  private user!: User | null;

  constructor() {
    super();

    this.manager.getUser().then(user => {
      this.user = user;
      this._authNavStatusSource.next(this.isAuthenticated());
    });
    
  }

  login() {
    this.manager.signinRedirect().then(() => {
      console.log('AuthService: signinRedirect success');
    }).catch(err => {
      console.error('AuthService: signinRedirect error', err);
    });
  }

  async completeAuthentication() {
    this.user = await this.manager.signinRedirectCallback();
    this._authNavStatusSource.next(this.isAuthenticated());
  }

  isAuthenticated(): boolean {
    const isAuthenticated = this.user != null && !this.user.expired;
    return isAuthenticated;
  }

  get authorizationHeaderValue(): string | null {
    if (this.user) {
      console.log('AuthService: authorizationHeaderValue', `${this.user.token_type} ${this.user.access_token}`);
      return `${this.user.token_type} ${this.user.access_token}`;
    }
    console.log('AuthService: authorizationHeaderValue', null);
    return null;
  }

  get name(): string {
    return this.user != null ? this.user.profile.name : '';
  }

  get getProfile(): Profile {
    // console.log('getProfile called, user:', this.user);
    return this.user != null ? this.user.profile : null;
  }

  async signout() {
    await this.manager.signoutRedirect();
  }
}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: "https://localhost:7017",
    client_id: "angular_admin",
    redirect_uri: "http://localhost:4200/auth-callback",
    post_logout_redirect_uri: 'http://localhost:4200/',
    response_type: 'code',
    scope: 'openid profile api.fuforum.access',
    filterProtocolClaims: true,
    loadUserInfo: true,
    automaticSilentRenew: true,
    silent_redirect_uri: 'http://localhost:4200/silent-refresh.html'
  };
}