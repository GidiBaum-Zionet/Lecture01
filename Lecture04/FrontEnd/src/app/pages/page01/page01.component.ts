import { Component, OnInit } from '@angular/core';
import {ApiService} from "../../services/api.service";
import {IElementModel} from "../../models/IElementModel";

@Component({
  selector: 'app-page01',
  templateUrl: './page01.component.html',
  styleUrls: ['./page01.component.scss']
})
export class Page01Component implements OnInit {

  elements: IElementModel[] = [];

  constructor(private api: ApiService) { }

  ngOnInit(): void {
  }


  buttonClicked() {
    this.api.Counter++;
    console.log(this.api.Counter)

    this.api.get<IElementModel[]>("api/Element/Elements")
      .subscribe(value => {

        console.log(value);

        this.elements = value;

      });

  }
}
