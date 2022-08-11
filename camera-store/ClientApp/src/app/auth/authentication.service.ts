import { Injectable } from '@angular/core';
import { Repository } from '../models/repository';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class AuthenticationService {
    constructor(private repo: Repository, private router: Router) {}

    name: string;
    password: string;
    confirmPassword: string;
    callbackUrl: string;

    login(): Observable<boolean> {
        return this.repo.login(this.name, this.password).pipe(
            map((response) => {
                if (response.role === 'admin') {
                    localStorage.setItem('authenticated', 'true');
                    localStorage.setItem('isAdmin', 'true');
                    this.password = null;
                    this.router.navigateByUrl(
                        this.callbackUrl || '/admin/overview'
                    );
                } else if (response.role === 'user') {
                    this.password = null;
                    localStorage.setItem('authenticated', 'true');
                    localStorage.setItem('isAdmin', 'false');
                    this.router.navigateByUrl('');
                }
                localStorage.setItem('username', this.name);
                return localStorage.getItem('authenticated') === 'true';
            }),
            catchError((e) => {
                localStorage.setItem('authenticated', 'false');
                return of(false);
            })
        );
    }

    signup(): Observable<boolean> {
        return this.repo.signup(this.name, this.password).pipe(
            map((response) => {
                if (response) {
                    this.password = null;
                    localStorage.setItem('username', this.name);
                    localStorage.setItem('authenticated', 'true');
                    localStorage.setItem('isAdmin', 'false');
                    this.router.navigateByUrl('');
                    return true;
                }
            }),
            catchError((e) => {
                return of(false);
            })
        );
    }

    logout() {
        this.repo.logout();
        localStorage.setItem('authenticated', 'false');
        localStorage.setItem('isAdmin', 'false');
        localStorage.removeItem('username');
        this.router.navigateByUrl('');
    }
}
