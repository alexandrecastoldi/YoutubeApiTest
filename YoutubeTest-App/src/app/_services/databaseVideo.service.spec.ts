/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DatabaseVideoService } from './databaseVideo.service';

describe('Service: DatabaseVideo', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DatabaseVideoService]
    });
  });

  it('should ...', inject([DatabaseVideoService], (service: DatabaseVideoService) => {
    expect(service).toBeTruthy();
  }));
});
