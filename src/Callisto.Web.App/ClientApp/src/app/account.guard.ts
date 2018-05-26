// auth.guard.ts
import { Injectable } from '@angular/core';
import { Router, CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './core/auth.service';
import { DetailsComponent } from './account/details/details.component';

@Injectable()
export class AccountGuard implements CanDeactivate<DetailsComponent> {
    canDeactivate(component: DetailsComponent) {
        return component.isComplete();
    }

    constructor(private router: Router) { }

}
