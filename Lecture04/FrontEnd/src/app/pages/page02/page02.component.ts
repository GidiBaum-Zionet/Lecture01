import { Component, OnInit } from '@angular/core';
import {ApiService} from "../../services/api.service";
import {IPartModel} from "../../models/IPartModel";

@Component({
  selector: 'app-page02',
  templateUrl: './page02.component.html',
  styleUrls: ['./page02.component.scss']
})
export class Page02Component implements OnInit {

  formula = '';
  molParts:IPartModel[] = [];

  constructor(public api: ApiService) { }

  ngOnInit(): void {
  }

  parseFormula() {

    console.log(this.formula);

    this.api.get<IPartModel[]>(`api/element/parse/${this.formula}`)
      .subscribe(value => {

        this.molParts = value;
      });


  }

  }
