// spec: specs/aspireshop-basic-operations.test.plan.md
// seed: tests/seed.spec.ts

import { test, expect } from '@playwright/test';

test.describe('Product Catalog Display', () => {
  test('Display Product Catalog Page', async ({ page }) => {
    // Navigate to the homepage
    await page.goto('https://frontend-aspireshop.dev.localhost:7276/');
    
    // 1. Navigate to the homepage - verify page title and header elements
    await expect(page.getByRole('heading', { name: '<AspireShop />' })).toBeVisible();
    await expect(page.getByRole('heading', { name: 'Product Catalog' })).toBeVisible();
    await expect(page.getByRole('button', { name: ' 2' })).toBeVisible();

    // 2. Verify product grid layout - check specific products are visible
    await expect(page.getByRole('heading', { name: '.NET Bot Black Hoodie' })).toBeVisible();
    await expect(page.getByRole('heading', { name: '.NET Black & White Mug' })).toBeVisible();
    await expect(page.getByRole('heading', { name: 'Prism White T-Shirt' })).toBeVisible();

    // 2. Verify product grid layout - check prices are displayed in currency format
    await expect(page.getByText('$19.50')).toBeVisible();
    await expect(page.getByText('$8.50')).toBeVisible();

    // 2. Verify product grid layout - check products have add to cart buttons and images
    await expect(page.getByRole('button', { name: 'Add .NET Bot Black Hoodie to cart?' })).toBeVisible();
    await expect(page.getByRole('img', { name: '.NET Bot Black Hoodie' })).toBeVisible();

    // 3. Check footer information
    await expect(page.getByRole('link', { name: '© 2026 • MIT Licensed' })).toBeVisible();
    await expect(page.getByText('Powered by .NET 10.0.2')).toBeVisible();
    await expect(page.getByRole('link', { name: 'GitHub Repository' })).toBeVisible();
  });
});