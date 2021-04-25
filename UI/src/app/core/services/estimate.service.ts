import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { Price } from "src/app/models/price";
import * as config from "../../config/app.settings.json";
@Injectable({
    providedIn: 'root'
})
export class EstimateService {
    constructor(private http: HttpClient) { }

    CalculatePrice(goldPrice: number, weight: number, discount: number, customerType: string) {
        console.log(goldPrice);
        let url = '';
        if (customerType.toLowerCase() === 'privileged') {
            url = `${config.ApiBaseUrl}/v1/price/${goldPrice}/${weight}?discount=${discount}`;
        }
        else {
            url = `${config.ApiBaseUrl}/v1/price/${goldPrice}/${weight}`
        }

        return this.http.get<Price>(url)
            .pipe(map(price => {
                console.log('url' + url);
                console.log(price.totalPrice);
                return price
            }));
    }
}