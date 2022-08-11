import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/auth/authentication.service';
import { Order } from '../../models/order.model';

@Component({
    templateUrl: 'checkoutDetails.component.html',
})
export class CheckoutDetailsComponent implements OnInit {
    constructor(
        private router: Router,
        public order: Order,
        public authService: AuthenticationService
    ) {
        if (order.products.length == 0) {
            this.router.navigateByUrl('/cart');
        }
    }

    authenticated = false;
    isAdmin = false;
    username = '';

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
