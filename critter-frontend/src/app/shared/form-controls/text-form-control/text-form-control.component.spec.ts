import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextFormControlComponent } from './text-form-control.component';

describe('TextFormControlComponent', () => {
  let component: TextFormControlComponent;
  let fixture: ComponentFixture<TextFormControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TextFormControlComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TextFormControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
