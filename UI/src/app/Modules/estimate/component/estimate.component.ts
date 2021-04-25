import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { first } from "rxjs/operators";
import { EstimateService } from "src/app/core/services/estimate.service";
import { UserService } from "src/app/core/services/user.service";
import { User } from "src/app/models/user";

@Component({
    selector: 'app-estimate',
    templateUrl: './estimate.component.html',
    styleUrls: ['./estimate.component.css']
})
export class EstimateComponent {
    estimateForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    isDiscountOn = true;
    loggedInUser: User;
    totalPrice: number;
    isTotalAvailable = false;
    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private userService: UserService,
        private estimateService: EstimateService
    ) {
        //redirect to login if not  logged in
        if (this.userService.GetToken() === undefined) {
            this.router.navigate(['/login']);
        }
    }
    ngOnInit() {
        this.estimateForm = this.formBuilder.group({
            goldPrice: ['', Validators.required],
            weight: ['', Validators.required],
            discount:[2, Validators.required]
        });

        this.loggedInUser = this.userService.GetUserContext();
        if (this.loggedInUser !== undefined)
            this.isDiscountOn = this.loggedInUser.type.toLowerCase() === 'Privileged'.toLowerCase();
    }
    // convenience getter for easy access to form fields
    get f() { return this.estimateForm.controls; }

    onSubmit() {
        this.submitted = true;
        if (this.estimateForm.invalid) {
            return;
        }

        this.estimateService.CalculatePrice(this.f.goldPrice.value, this.f.weight.value,
            this.f.discount.value, this.loggedInUser.type)
            .pipe(first())
            .subscribe(
                data => {
                    console.log('price');
                    console.log(data);
                    this.isTotalAvailable = true;
                    this.totalPrice = data.totalPrice;
                },
                error => {
                   console.error('Error occured');
                    this.loading = false;
                });
    }
}