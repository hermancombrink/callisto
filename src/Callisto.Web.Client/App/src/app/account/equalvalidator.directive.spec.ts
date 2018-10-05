import { EqualValidator } from './equalvalidator.directive';

describe('EqualvalidatorDirective', () => {
  it('should create an instance', () => {
    const directive = new EqualValidator('', '');
    expect(directive).toBeTruthy();
  });
});
