import {DataService} from "./data.service";
import {Component} from "@angular/core";

export class Person {
    constructor( 
        public id?: number, 
        public name?: string, 
        public lastName?: string,
        public isEmployee?: boolean) {}
}