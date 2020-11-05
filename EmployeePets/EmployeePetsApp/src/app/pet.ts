import {Person} from "./person";
import {AnimalType} from "./animalType";

export class Pet {
    constructor(
        public id?: number,
        public name?: string,
        public type?: AnimalType,
        public owner?: Person
    ) {
    }
}