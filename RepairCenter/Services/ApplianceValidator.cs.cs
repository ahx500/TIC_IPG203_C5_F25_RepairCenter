
using System.Text.RegularExpressions;

namespace RepairCenter.Services
{
    public static class ApplianceValidator
    {
        public static bool IsValidSerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
                return false;

            return Regex.IsMatch(
                serialNumber,
                @"^[A-Za-z0-9-]{3,20}$");
        }

        public static bool IsValidText(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsValidRepairCost(decimal cost)
        {
            return cost >= 0;
        }
    }
}
