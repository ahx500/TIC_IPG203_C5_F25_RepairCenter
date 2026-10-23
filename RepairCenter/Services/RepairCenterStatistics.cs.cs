
using System;
using System.Collections.Generic;
using RepairCenter.Models;

namespace RepairCenter.Services
{
    public static class RepairCenterStatistics
    {
        private static readonly List<Appliance> _appliances =
            new List<Appliance>();

        private static readonly HashSet<string> _serialNumbers =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public static int TotalDevices
        {
            get { return _appliances.Count; }
        }

        public static decimal TotalRepairCost
        {
            get
            {
                decimal total = 0;

                foreach (Appliance appliance in _appliances)
                {
                    decimal cost = appliance.CalculateRepairCost();

                    if (!ApplianceValidator.IsValidRepairCost(cost))
                    {
                        throw new InvalidOperationException(
                            "A repair cost cannot be negative.");
                    }

                    total += cost;
                }

                return total;
            }
        }

        public static void RegisterDevice(Appliance appliance)
        {
            if (appliance == null)
            {
                throw new ArgumentNullException(nameof(appliance));
            }

            if (!_serialNumbers.Add(appliance.SerialNumber))
            {
                throw new InvalidOperationException(
                    "A device with this serial number is already registered.");
            }

            _appliances.Add(appliance);
        }

        public static int GetDeviceCountByStatus(string status)
        {
            if (!ApplianceValidator.IsValidText(status))
            {
                throw new ArgumentException(
                    "Status cannot be empty.", nameof(status));
            }

            int count = 0;

            foreach (Appliance appliance in _appliances)
            {
                if (string.Equals(
                    appliance.Status,
                    status.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
