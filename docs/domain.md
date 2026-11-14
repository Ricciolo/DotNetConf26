# Orders domain rules

## Available in the initial sample

- Customer name must contain 1–120 non-whitespace characters.
- An order must contain 1–100 lines.
- SKU must contain 1–64 characters and is normalized to uppercase.
- SKU is unique within an order, case-insensitively.
- Quantity must be positive; unit price must not be negative.
- Order total is calculated from line quantity times unit price.
- A new order starts in `Submitted` state and has a UTC creation timestamp.

## Live-demo feature, not yet implemented

The speaker's exercise is to add **order cancellation**:

- Only an order in `Submitted` state may be cancelled.
- A successful cancellation changes status to `Cancelled`.
- An unknown order ID must return HTTP 404.
- A second cancellation of the same order must return HTTP 409.
- Route: `POST /orders/{id:guid}/cancel`.
- Existing routes and calculations must keep working.

These requirements intentionally live in domain documentation and the feature skill, not in existing implementation.
