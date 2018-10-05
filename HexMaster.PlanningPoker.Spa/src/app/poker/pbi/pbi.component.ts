import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Chart } from 'chart.js';

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
    var barChartHome = new Chart('salesBarChart1', {
      type: 'bar',
      options: {
        scales: {
          xAxes: [
            {
              display: false,
              barPercentage: 0.2
            }
          ],
          yAxes: [
            {
              display: false
            }
          ]
        },
        legend: {
          display: false
        }
      },
      data: {
        labels: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'],
        datasets: [
          {
            label: 'Data Set 1',
            backgroundColor: [
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99'
            ],
            borderColor: [
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99',
              '#EF8C99'
            ],
            borderWidth: 0.2,
            data: [35, 55, 65, 85, 40, 30, 18, 35, 20, 70]
          }
        ]
      }
    });
  }
}
