import { HttpClient, provideHttpClient, withFetch } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { Contact } from '../models/contact.model';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, AsyncPipe, FormsModule, ReactiveFormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'

  
})
export class App {
  http = inject(HttpClient);

  // creating a form s.t when a user submits a form it is correct
  contactsForm = new FormGroup({
    name: new FormControl<string>(''), // ('') gives an initial value
    email : new FormControl<string | null>(null),
    phone : new FormControl<string>(''),
    favourite : new FormControl<boolean>(false), 
  })

  contacts$ = this.getContacts(); // $ denotes observable

  onFormSubmit() {
    const addContactRequest = {
      // this object is model the same as the controller contacts method
      name : this.contactsForm.value.name,
      email : this.contactsForm.value.email,
      phone : this.contactsForm.value.phone,
      favourite : this.contactsForm.value.favourite,
    }
    // POST request to get find the contacts
    this.http.post<Contact>('http://localhost:5080/api/Contacts', addContactRequest).subscribe({ // addContactRequest still produces an observable, the observable must be subscribed to for it to be valid
      next: ( value ) => { // perform the next necessary functions with the value returned in the post form
        console.log(value)

        this.contacts$ = this.getContacts(); // resets the contacts list straight away
        this.contactsForm.reset(); // resetting the form
      }
    })

  }

  // DELTE request
  onDelete(id: string) {
    this.http.delete(`http://localhost:5080/api/Contacts/${id}`).subscribe({
      next: ( value ) => {
        alert('Item deleted');
        this.contacts$ = this.getContacts();

      }
    })
  }

  // GET request
  // call api to get all contacts back to display in UI
private getContacts() : Observable<Contact[]> { // return a Observable<Contacts[]> data type - Observable means it will handle multiple inputs emitted over time- live updates
    // making an http call
    return this.http.get<Contact[]>('http://localhost:5080/api/Contacts'); // the complete path of the api for table Contacts
}
}


