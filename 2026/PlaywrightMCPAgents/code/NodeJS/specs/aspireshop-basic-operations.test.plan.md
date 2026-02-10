# AspireShop Frontend Basic Operations Test Plan

## Application Overview

The AspireShop frontend is an e-commerce product catalog application built with .NET technology. It displays a paginated grid of products with shopping cart functionality, allowing users to browse products across multiple pages and add items to their cart. The application includes external links to documentation and source code repositories.

## Test Scenarios

### 1. Product Catalog Display

**Seed:** `tests/seed.spec.ts`

#### 1.1. Display Product Catalog Page

**File:** `tests/product-catalog-display/display-catalog.spec.ts`

**Steps:**
  1. Navigate to the homepage
    - expect: Page title should be 'Aspire Shop Product Catalog'
    - expect: Header should display '<AspireShop />' title
    - expect: Header should display 'Product Catalog' subtitle
    - expect: Shopping cart icon should be visible with count '0'
  2. Verify product grid layout
    - expect: Products should be displayed in a grid layout
    - expect: Each product should show an image, title, description, price, and 'Add to cart' button
    - expect: Products visible should include '.NET Bot Black Hoodie', '.NET Black & White Mug', 'Prism White T-Shirt', etc.
    - expect: Prices should be displayed in currency format (e.g., $19.50, $8.50, $12.00)
  3. Check footer information
    - expect: Footer should display '© 2026 • MIT Licensed' link
    - expect: Footer should show 'Powered by .NET 10.0.2'
    - expect: Footer should contain 'GitHub Repository' link with icon

### 2. Shopping Cart Functionality

**Seed:** `tests/seed.spec.ts`

#### 2.1. Add Single Product to Cart

**File:** `tests/shopping-cart/add-single-product.spec.ts`

**Steps:**
  1. Click 'Add .NET Bot Black Hoodie to cart?' button
    - expect: Cart counter should increment from '0' to '1'
    - expect: Page should remain on the product catalog
    - expect: No error messages should appear

#### 2.2. Add Multiple Products to Cart

**File:** `tests/shopping-cart/add-multiple-products.spec.ts`

**Steps:**
  1. Add '.NET Bot Black Hoodie' to cart
    - expect: Cart counter should show '1'
  2. Add '.NET Black & White Mug' to cart
    - expect: Cart counter should increment to '2'
  3. Add 'Prism White T-Shirt' to cart
    - expect: Cart counter should increment to '3'

#### 2.3. Cart State Persistence

**File:** `tests/shopping-cart/cart-persistence.spec.ts`

**Steps:**
  1. Add multiple products to cart (at least 2 items)
    - expect: Cart counter should show correct count
  2. Refresh the page
    - expect: Cart counter should maintain the same count after page refresh
    - expect: Cart state should persist across page reloads

#### 2.4. Cart Click Behavior

**File:** `tests/shopping-cart/cart-click.spec.ts`

**Steps:**
  1. Add a product to cart to get count > 0
    - expect: Cart counter should show '1'
  2. Click on the cart button
    - expect: Cart counter should reset to '0' (current behavior - this may be a toggle or clear action)
    - expect: Page should remain on the product catalog

### 3. Product Pagination

**Seed:** `tests/seed.spec.ts`

#### 3.1. Navigate to Next Page

**File:** `tests/pagination/next-page.spec.ts`

**Steps:**
  1. Click the 'Next' link in pagination
    - expect: URL should change to include '?after=9' parameter
    - expect: Different products should be displayed (e.g., 'Cup<T> White Mug', '.NET Foundation Sheet')
    - expect: 'Previous' link should become active/clickable
    - expect: Page should load successfully with new products

#### 3.2. Navigate to Previous Page

**File:** `tests/pagination/previous-page.spec.ts`

**Steps:**
  1. Navigate to the next page first
    - expect: URL should include pagination parameter
  2. Click the 'Previous' link in pagination
    - expect: URL should change to include '?before=9' parameter
    - expect: Should return to the original first page products
    - expect: Original products should be visible (.NET Bot Black Hoodie, etc.)
    - expect: 'Next' link should be available

#### 3.3. Pagination State Management

**File:** `tests/pagination/pagination-state.spec.ts`

**Steps:**
  1. Add products to cart on first page
    - expect: Cart counter should show added items
  2. Navigate to next page
    - expect: Cart state should be maintained across pages
    - expect: Cart counter should show same count on second page
  3. Navigate back to first page
    - expect: Cart state should still be maintained
    - expect: Cart counter should show same count on return

### 4. External Links and Footer

**Seed:** `tests/seed.spec.ts`

#### 4.1. GitHub Repository Link

**File:** `tests/external-links/github-repository.spec.ts`

**Steps:**
  1. Click on 'GitHub Repository' link in footer
    - expect: Link should open in a new tab
    - expect: New tab should navigate to 'https://github.com/dotnet/aspire-samples/tree/main/samples/AspireShop'
    - expect: Original page should remain in the first tab
    - expect: GitHub page should load (may show 'File not found' if repository is private/unavailable)

#### 4.2. MIT License Link

**File:** `tests/external-links/mit-license.spec.ts`

**Steps:**
  1. Click on '© 2026 • MIT Licensed' link in footer
    - expect: Link should open in a new tab
    - expect: New tab should navigate to 'https://github.com/dotnet/aspire-samples/blob/main/LICENSE'
    - expect: Original page should remain in the first tab
    - expect: License page should load successfully

### 5. Error Handling and Edge Cases

**Seed:** `tests/seed.spec.ts`

#### 5.1. Product Image Loading

**File:** `tests/edge-cases/product-images.spec.ts`

**Steps:**
  1. Check all product images on the first page
    - expect: All product images should have meaningful alt text (product names)
    - expect: Images should load without errors
    - expect: Missing images should have appropriate fallbacks or alt text

#### 5.2. Rapid Cart Additions

**File:** `tests/edge-cases/rapid-cart-additions.spec.ts`

**Steps:**
  1. Rapidly click the same 'Add to cart' button multiple times
    - expect: Cart counter should increment appropriately
    - expect: No duplicate additions should occur if not intended
    - expect: Application should remain responsive
    - expect: No JavaScript errors should occur

#### 5.3. Page Load Performance

**File:** `tests/edge-cases/page-performance.spec.ts`

**Steps:**
  1. Monitor page load times and resource loading
    - expect: Page should load within reasonable time (< 3 seconds)
    - expect: All critical resources should load successfully
    - expect: No console errors should appear in browser console
    - expect: Favicon loading error is acceptable (noted in testing)
