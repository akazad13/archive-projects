import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/auth/authentication.service';
import { Order } from '../../models/order.model';

@Component({
    templateUrl: 'checkoutSummary.component.html',
})
export class CheckoutSummaryComponent implements OnInit {
    authenticated = false;
    isAdmin = false;
    username = '';
    constructor(
        private router: Router,
        public order: Order,
        public authService: AuthenticationService
    ) {
        if (
            order.payment.cardNumber == null ||
            order.payment.cardExpiry == null ||
            order.payment.cardSecurityCode == null
        ) {
            router.navigateByUrl('/checkout/step2');
        }
    }

    submitOrder() {
        this.order.submit();
        this.router.navigateByUrl('/checkout/confirmation');
    }

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
