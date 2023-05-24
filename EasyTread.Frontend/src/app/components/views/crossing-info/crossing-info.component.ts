import { DOCUMENT } from '@angular/common';
import { Component, Inject, Input } from '@angular/core';

@Component({
  selector: 'app-crossing-info',
  templateUrl: './crossing-info.component.html',
  styleUrls: ['./crossing-info.component.css'],
})
export class CrossingInfoComponent {
  @Input() item: any;

  constructor(@Inject(DOCUMENT) private document: Document) {}

  checkTireAlignment(item: any): boolean {
    return item.tires.some((tire: any) => {
      const info = tire.propertySetResponse.wearPatternResponse.info;
      return info === 'linearWearLeft' || info === 'linearWearRight';
    });
  }

  checkTirePressure(item: any): boolean {
    return item.tires.some((tire: any) => {
      const info = tire.propertySetResponse.wearPatternResponse.info;
      return (
        info === 'parabolicWearCentered' || info === 'parabolicWearOutside'
      );
    });
  }

  checkTireRating(item: any): boolean {
    return item.tires.some((tire: any) => {
      const info = tire.regionResponse[0].value;
      return info <= 4;
    });
  }

  openReportInNewWindow() {
    const baseUrl = 'http://localhost:8282';
    const completeUrl = baseUrl + this.item.reportLink;
    this.document.defaultView?.open(completeUrl, '_blank');
  }

  getImageForWearPattern(info: string): string {
    let imagePath = '';

    switch (info) {
      case 'parabolicWearCentered':
        imagePath = '../../../../assets/parabolicwearcentered.png';
        break;
      case 'parabolicWearOutside':
        imagePath = '../../../../assets/parabolicwearoutside.png';
        break;
      case 'linearWearLeft':
        imagePath = '../../../../assets/linearwearleft.png';
        break;
      case 'linearWearRight':
        imagePath = '../../../../assets/linearwearright.png';
        break;
      default:
        imagePath = '../../../../assets/ok.png';
    }

    return imagePath;
  }

  statusInfo: {
    [key: string]: { icon: string; message: string; colorClass: string };
  } = {
    NEUTRAL: {
      icon: 'bi-check-circle-fill',
      message: 'neutral',
      colorClass: 'text-secondary',
    },
    GOOD: {
      icon: 'bi-check-circle-fill',
      message: 'good',
      colorClass: 'text-success',
    },
    MARGINAL: {
      icon: 'bi-exclamation-triangle-fill',
      message: 'Lågt mönsterdjup',
      colorClass: 'text-warning',
    },
    REPLACE: {
      icon: 'bi-exclamation-triangle-fill',
      message: 'replace',
      colorClass: 'text-danger',
    },
  };

  checkWearPattern(wearPatternInfo: string): boolean {
    return (
      wearPatternInfo.includes('linearWearLeft') ||
      wearPatternInfo.includes('linearWearRight') ||
      wearPatternInfo.includes('parabolicWearCentered') ||
      wearPatternInfo.includes('parabolicWearOutside')
    );
  }

  wearPatternInfo: {
    [key: string]: { icon: string; message: string; colorClass: string };
  } = {
    linearWearLeft: {
      icon: 'bi-exclamation-triangle-fill',
      message: 'linearWearLeft',
      colorClass: 'text-warning',
    },
    linearWearRight: {
      icon: 'bi-exclamation-triangle-fill',
      message: 'linearWearRight',
      colorClass: 'text-warning',
    },
    parabolicWearCentered: {
      icon: 'bi-exclamation-triangle-fill',
      message: 'parabolicWearCentered',
      colorClass: 'text-warning',
    },
    parabolicWearOutside: {
      icon: 'bi-exclamation-triangle-fill',
      message: 'parabolicWearOutside',
      colorClass: 'text-warning',
    },
  };
}
