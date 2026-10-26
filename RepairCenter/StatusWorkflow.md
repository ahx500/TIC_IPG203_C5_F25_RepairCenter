# Appliance Status Workflow

## Purpose
This document describes the maintenance status workflow used in the Repair Center application.

## Device Statuses

- **Waiting:** The appliance is registered and waiting for maintenance.
- **In Progress:** Maintenance has started.
- **Repaired:** Maintenance has been completed successfully.

## Status Transitions

1. A newly created appliance starts with the `Waiting` status.
2. Calling `StartMaintenance()` changes the status from `Waiting` to `In Progress`.
3. Calling `CompleteMaintenance()` changes the status from `In Progress` to `Repaired`.
4. Invalid status transitions are rejected by the application.

## Status Change Event

The `StatusChanged` event notifies subscribed methods when an appliance's status changes. The event arguments contain the serial number, the previous status, and the new status.

## Conclusion

The status workflow helps maintain consistent appliance states and provides notifications when maintenance progress changes.