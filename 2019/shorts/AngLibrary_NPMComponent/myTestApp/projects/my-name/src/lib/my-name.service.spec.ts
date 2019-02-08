import { TestBed } from '@angular/core/testing';

import { MyNameService } from './my-name.service';

describe('MyNameService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MyNameService = TestBed.get(MyNameService);
    expect(service).toBeTruthy();
  });
});
