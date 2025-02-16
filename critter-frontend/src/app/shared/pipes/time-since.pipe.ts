import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeSince',
  standalone: true,
})
export class TimeSincePipe implements PipeTransform {
  transform(value: string, ...args: unknown[]): unknown {
    const pastDate = new Date(value);
    const now = new Date();

    const seconds = Math.floor((now.getTime() - pastDate.getTime()) / 1000);

    if (seconds < 60) return `${seconds}s`;
    const minutes = Math.floor(seconds / 60);
    if (minutes < 60) return `${minutes}m`;
    const hours = Math.floor(minutes / 60);
    if (hours < 24) return `${hours}h`;
    const days = Math.floor(hours / 24);
    if (days < 7) return `${days}d`;
    const weeks = Math.floor(days / 7);
    if (weeks < 4) return `${weeks}w`;
    const months = Math.floor(days / 30);
    if (months < 12) return `${months}M`;
    const years = Math.floor(days / 365);

    return `${years} years ago`;
  }
}
