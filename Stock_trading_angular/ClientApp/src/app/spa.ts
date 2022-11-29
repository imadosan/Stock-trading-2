import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Aksje } from "./Aksje";

@Component({
  selector: "app-root",
  templateUrl: "SPA.html"
})
export class SPA {

  visSkjemaRegistrere: boolean;
  visListe: boolean;
  alleAksjer: Array<Aksje>;
  skjema: FormGroup;
  laster: boolean;

  validering = {
    id: [""],
    fornavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZæøåÆØÅ\\-. ]{2,20}")])
    ],
    etternavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZæøåÆØÅ\\-. ]{2,20}")])
    ],
    aksjenavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZæøåÆØÅ\\-. ]{2,20}")])
    ],
    pris: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9.,]{1,20}")])
    ],
    antall: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9]{1,20}")])
    ]
  }

  constructor(private _http: HttpClient, private fb: FormBuilder) {
    this.skjema = fb.group(this.validering);
  }

  ngOnInit() {
    this.laster = true;
    this.hentAlleAksjer();
    this.visListe = true;
  }

  hentAlleAksjer() {
    this._http.get<Aksje[]>("api/aksje/")
      .subscribe(aksjene => {
        this.alleAksjer = aksjene;
        this.laster = false;
      },
        error => console.log(error),
        () => console.log("ferdig get-api/aksje")
      );
  };

  vedSubmit() {
    if (this.visSkjemaRegistrere) {
      this.lagreAksje();
    }
    else {
      this.endreEnAksje();
    }
  }

  registrerAksje() {
    // må resette verdiene i skjema dersom skjema har blitt brukt til endringer
    this.skjema.setValue({
      id: "",
      fornavn: "",
      etternavn: "",
      aksjenavn: "",
      pris: "",
      antall: ""
    });
    this.skjema.markAsPristine();
    this.visListe = false;
    this.visSkjemaRegistrere = true;
  }

  tilbakeTilListe() {
    this.visListe = true;
  }

  lagreAksje() {
    const lagretAksje = new Aksje();

    lagretAksje.fornavn = this.skjema.value.fornavn;
    lagretAksje.etternavn = this.skjema.value.etternavn;
    lagretAksje.aksjenavn = this.skjema.value.aksjenavn;
    lagretAksje.pris = this.skjema.value.pris;
    lagretAksje.antall = this.skjema.value.antall;

    this._http.post("api/aksje", lagretAksje)
      .subscribe(retur => {
        this.hentAlleAksjer();
        this.visSkjemaRegistrere = false;
        this.visListe = true;
      },
        error => console.log(error)
      );
  };

  sletteAksje(id: number) {
    this._http.delete("api/aksje/" + id)
      .subscribe(retur => {
        this.hentAlleAksjer();
      },
        error => console.log(error)
      );
  };

  endreAksje(id: number) {
    this._http.get<Aksje>("api/aksje/"+id)
      .subscribe(
        aksje => {
          this.skjema.patchValue({ id: aksje.id });
          this.skjema.patchValue({ fornavn: aksje.fornavn });
          this.skjema.patchValue({ etternavn: aksje.etternavn });
          this.skjema.patchValue({ aksjenavn: aksje.aksjenavn });
          this.skjema.patchValue({ pris: aksje.pris });
          this.skjema.patchValue({ antall: aksje.antall });
        },
        error => console.log(error)
      );
    this.visSkjemaRegistrere = false;
    this.visListe = false;
  }

  endreEnAksje() {
    const endretAksje = new Aksje();
    endretAksje.id = this.skjema.value.id;
    endretAksje.fornavn = this.skjema.value.fornavn;
    endretAksje.etternavn = this.skjema.value.etternavn;
    endretAksje.aksjenavn = this.skjema.value.aksjenavn;
    endretAksje.pris = this.skjema.value.pris;
    endretAksje.antall = this.skjema.value.antall;

    this._http.put("api/aksje/", endretAksje)
      .subscribe(
        retur => {
          this.hentAlleAksjer();
          this.visListe = true;
        },
        error => console.log(error)
      );
  }
}
