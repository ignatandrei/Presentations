import { test, expect } from '@playwright/test';

test.describe('Test group', () => {
  test('seed', async ({ page }) => {
    // generate code here.
    await page.goto('https://frontend-aspireshop.dev.localhost:7276/');
  });
});
