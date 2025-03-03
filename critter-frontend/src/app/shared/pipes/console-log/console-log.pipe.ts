import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'consoleLog',
  standalone: true,
})
export class ConsoleLogPipe implements PipeTransform {
  transform(value: unknown, ...args: unknown[]) {
    console.log(value);
  }
}
