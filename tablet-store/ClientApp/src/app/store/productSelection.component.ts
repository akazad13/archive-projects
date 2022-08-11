import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../auth/authentication.service';

@Component({
    selector: 'store-products',
    templateUrl: 'productSelection.component.html',
})
export class ProductSelectionComponent implements OnInit {
    authenticated = false;
    isAdmin = false;
    username = '';

    constructor(public authService: AuthenticationService) {}

    ngOnInit(): void {
        if (localStorage.getItem('isAdmin') != null) {
            this.isAdmin =
                localStorage.getItem('isAdmin') === 'true' ? true : false;
        }
        if (localStorage.getItem('authenticated') != null) {
            this.authenticated =
                localStorage.getItem('authenticated') === 'true' ? true : false;
        }
        this.username = localStorage.getItem('username');
    }

    logout() {
        this.authenticated = false;
        this.isAdmin = false;
        this.authService.logout();
    }
}
