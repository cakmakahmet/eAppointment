import { Pipe, PipeTransform } from '@angular/core';
import { PatientModel } from '../../models/patient.model';

@Pipe({
  name: 'patient',
})
  export class PatientPipe implements PipeTransform {
    transform(value: PatientModel[], search: string): PatientModel[] {
      if(!search){
        return value;
      }
      return value.filter( p=>
        p.fullName.toLowerCase().includes(search.toLocaleLowerCase()) ||
        p.fullAdress.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
        p.city.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
        p.town.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
        p.identityNumber.toLocaleLowerCase().includes(search.toLowerCase())
      )
    }
}
