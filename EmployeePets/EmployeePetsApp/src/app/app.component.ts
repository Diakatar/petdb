import {Component, OnInit} from '@angular/core';
import { DataService } from './data.service';
import { Person } from './person';
import { Pet } from "./pet";
import { AnimalType} from "./animalType";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [DataService]
})
export class AppComponent implements OnInit {
  
  title: string = "Employee Pets Application";
  person: Person = new Person();
  pet: Pet = new Pet();
  selected: Person = null;
  persons: Person[];
  pets: Pet[];
  tableMode: boolean = true;
  personMode: boolean = true;
  animalTypes: AnimalType[];
  
  constructor(private dataService: DataService) {  }
  
  ngOnInit(): void {
    this.loadPersons();
    this.loadAnimalTypes();
  }
  
  setPersons(data: Person[]) {
    this.persons = data;
    this.selectTop();
  }
  
  loadPersons() {
    this.dataService.getPersons().subscribe((data: Person[]) => this.setPersons(data));
  }
  
  loadAnimalTypes(){
    this.dataService.getAnimalTypes().subscribe((data: AnimalType[]) => this.animalTypes = data);
  }
  
  savePerson(){
    if (this.person.id == null)
      this.dataService.createPerson(this.person).subscribe((data:Person)=>this.persons.push(data));
    else 
      this.dataService.updatePerson(this.person).subscribe(() => this.loadPersons());
    this.rowClick(this.person);
    this.cancelPerson();
  }
  
  editPerson(person: Person) {
    this.person = person;
  }
  
  cancelPerson() {
    this.person = new Person();
    this.tableMode = true;
  }
  
  deletePerson(person: Person) {
    this.dataService.deletePerson(person.id).subscribe(()=>this.loadPersons());    
  }
  
  addPerson() {
    this.cancelPerson();
    this.tableMode = false;
  }
  
  selectTop(){
    
    if (this.persons.length > 0)
      this.selectionChanged(this.persons[0]);
    else 
      this.selectionChanged(null);
  }

  rowClick(p: Person) {
    if (p?.id != this.selected?.id)
      this.selectionChanged(p);
  }
  
  selectionChanged(p: Person)
  {
    this.selected = p;
    this.loadPets();
  }

  private loadPets() {
    if (this.selected?.id != null)
      this.dataService.getPersonPets(this.selected.id).subscribe((data: Pet[])=>this.pets = data);
  }
  
  addPet(p: Person) {
    //this.pet = new Pet();
    this.pet.owner = p;
    this.personMode = this.tableMode = false;
  }

  editPet(p: Pet) {
    this.pet = p;
  }

  deletePet(p: Pet) {
    this.dataService.deletePet(p.id).subscribe(()=>this.loadPets());
  }
  
  cancelPet()
  {
    this.tableMode = this.personMode = true;
    this.pet = new Pet();
  }

  savePet() {
    if (!this.pet?.id) {
      this.dataService.createPet(this.pet).subscribe((data: Pet) => this.pets?.push(data));
    }
    else {
      this.dataService.updatePet(this.pet).subscribe(() => this.loadPets());
    }
    this.cancelPet();
  }

  compareById(itemOne, itemTwo) {
    return itemOne && itemTwo && itemOne.Id == itemTwo.Id;
  }
}
