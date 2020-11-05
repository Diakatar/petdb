import { Injectable } from '@angular/core';
import { HttpClient} from "@angular/common/http";
import { Person } from './person';
import {Pet} from "./pet";

@Injectable()
export class DataService {

    private url = "/api/v1/persons";
    private typesUrl = "/api/v1/animaltypes";
    private petsUrl = "api/v1/pets";
    
    constructor(private http: HttpClient) {
    }

    getPersons() {
        return this.http.get(this.url);
    }
    
    getPersonPets(id: number) {
        return this.http.get(this.url + '/' + id + '/pets')
    }

    getPerson(id: number) {
        return this.http.get(this.url + '/' + id);
    }

    createPerson(person: Person) {
        return this.http.post(this.url, person);
    }
    updatePerson(person: Person) {

        return this.http.put(this.url, person);
    }
    deletePerson(id: number) {
        return this.http.delete(this.url + '/' + id);
    }

    getAnimalTypes() {
        return this.http.get(this.typesUrl);
    }

    deletePet(id: number) {
        return this.http.delete(this.petsUrl + '/' + id);
    }

    createPet(pet: Pet) {
        return this.http.post(this.petsUrl, pet);
    }

    updatePet(pet: Pet) {
        return this.http.put(this.petsUrl, pet);
    }
}