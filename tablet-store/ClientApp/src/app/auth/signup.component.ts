import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './authentication.service';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
    constructor(public authService: AuthenticationService) {}

    showError = false;

    signup() {
        this.showError = false;
        this.authService.signup().subscribe((result) => {
            this.showError = !result;
        });
    }
    ngOnInit() {}
}
