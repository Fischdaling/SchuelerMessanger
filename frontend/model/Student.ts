import {Address} from "./Address";
import {Klasse} from "./Klasse";

export type Student = {
  Id: string;
    Vorname :string;
    Nachname : string;
    Geschlecht : symbol;
    GebDatum : Date;
    Address: Address;
    Staatsbuerger : string;
    Klasse : Klasse;
};
