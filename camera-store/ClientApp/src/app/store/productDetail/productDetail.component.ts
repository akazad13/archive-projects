import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/auth/authentication.service';
import { Cart } from 'src/app/models/cart.model';
import { NavigationService } from 'src/app/models/navigation.service';
import { Product } from 'src/app/models/product.model';
import { Repository } from 'src/app/models/repository';

@Component({
    selector: 'app-productDetail',
    templateUrl: './productDetail.component.html',
    styleUrls: ['./productDetail.component.scss'],
})
export class ProductDetailComponent implements OnInit {
    authenticated = false;
    isAdmin = false;
    username = '';
    constructor(
        private repo: Repository,
        router: Router,
        activeRoute: ActivatedRoute,
        public service: NavigationService,
        private cart: Cart,
        public authService: AuthenticationService
    ) {
        const id = Number.parseInt(activeRoute.snapshot.params.id);
        if (id) {
            this.repo.getProduct(id);
        } else {
            router.navigateByUrl('/');
        }
    }

    get product(): Product {
        return this.repo.product;
    }

    addToCart(product: Product) {
        this.cart.addProduct(product);
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
