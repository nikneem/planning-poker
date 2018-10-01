import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-pbi',
  templateUrl: './pbi.component.html',
  styleUrls: ['./pbi.component.scss']
})
export class PbiComponent implements OnInit {
  public id: number;

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');
  }
}
