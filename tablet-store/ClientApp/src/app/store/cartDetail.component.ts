import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../auth/authentication.service';
import { Cart } from '../models/cart.model';

@Component({
    templateUrl: 'cartDetail.component.html',
})
export class CartDetailComponent implements OnInit {
    authenticated = false;
    isAdmin = false;
    username = '';

    constructor(public authService: AuthenticationService, public cart: Cart) {}

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
}
