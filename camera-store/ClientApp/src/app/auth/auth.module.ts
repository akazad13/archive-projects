import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthenticationService } from './authentication.service';
import { AuthenticationComponent } from './authentication.component';
import { AuthenticationGuard } from './authentication.guard';
import { SignupComponent } from './signup.component';

@NgModule({
    imports: [RouterModule, FormsModule, CommonModule],
    declarations: [AuthenticationComponent, SignupComponent],
    providers: [AuthenticationService, AuthenticationGuard],
    exports: [AuthenticationComponent, SignupComponent],
})
export class AuthModule {}
