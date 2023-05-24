import { DataService } from 'src/app/services/data.service';
import Chart from 'chart.js/auto';

import { TranslateService } from '@ngx-translate/core';
import { Component, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-crossing-chart',
  templateUrl: './crossing-chart.component.html',
})
export class CrossingChartComponent {
  @ViewChild('canvas', { static: true })
  canvas!: ElementRef<HTMLCanvasElement>;

  chartTitle = 'vehicle ratings';
  pieChartLabels = ['marginal', 'good', 'replace'];

  constructor(
    private dataService: DataService,
    private translate: TranslateService
  ) {}

  ngOnInit() {
    this.updateChart();
  }

  updateChart() {
    this.dataService.fetchData().subscribe((data) => {
      const counts = data.reduce((acc: any, item: any) => {
        const count = acc[item.vehicleRating] || 0;
        return { ...acc, [item.vehicleRating]: count + 1 };
      }, {});

      const translatedLabels = this.pieChartLabels.map((label) =>
        this.translate.instant(label)
      );

      const chartData = {
        labels: translatedLabels,
        datasets: [
          {
            data: Object.values(counts),
            backgroundColor: ['#ffc107', '#28a745', '#dc3545'],
          },
        ],
      };

      const canvas = this.canvas.nativeElement;
      const ctx = canvas.getContext('2d')!;
      const chart = new Chart(ctx, {
        type: 'pie',
        data: chartData,
        options: {
          responsive: true,
          maintainAspectRatio: false,
        },
      });
    });
  }
}
