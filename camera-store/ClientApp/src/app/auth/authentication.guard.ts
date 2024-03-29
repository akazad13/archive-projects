import { Injectable } from '@angular/core';
import {
    Router,
    ActivatedRouteSnapshot,
    RouterStateSnapshot,
} from '@angular/router';
import { AuthenticationService } from './authentication.service';

@Injectable()
export class AuthenticationGuard {
    constructor(
        private router: Router,
        private authService: AuthenticationService
    ) {}

    canActivateChild(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): boolean {
        if (localStorage.getItem('authenticated') === 'true') {
            return true;
        } else {
            if (route.url.toString() !== '') {
                this.authService.callbackUrl = '/admin/' + route.url.toString();
            }
            this.router.navigateByUrl('/admin/login');
            return false;
        }
    }
}
