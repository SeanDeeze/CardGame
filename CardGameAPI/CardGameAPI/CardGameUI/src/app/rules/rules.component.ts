import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-rules',
  templateUrl: './rules.component.html',
  styleUrls: ['./rules.component.css']
})
export class RulesComponent implements OnInit
{
  jaxieURL: string;
  constructor() { }

  ngOnInit()
  {
    this.jaxieURL = environment.imageBase + '/jaxie.jpg';
  }

}
