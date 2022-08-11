import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/auth/authentication.service';
import { Order } from '../../models/order.model';

@Component({
    templateUrl: 'checkoutPayment.component.html',
})
export class CheckoutPaymentComponent implements OnInit {
    constructor(
        private router: Router,
        public order: Order,
        public authService: AuthenticationService
    ) {
        if (order.name == null || order.address == null) {
            router.navigateByUrl('/checkout/step1');
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
