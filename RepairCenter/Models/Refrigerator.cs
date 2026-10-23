
using System;

namespace RepairCenter.Models
{
    public class Refrigerator : Appliance
    {
        public bool CompressorNeedsRepair { get; set; }

        public Refrigerator(
            string serialNumber,
            string brand,
            string model,
            bool compressorNeedsRepair)
            : base(serialNumber, brand, model)
        {
            CompressorNeedsRepair = compressorNeedsRepair;
        }

        public override decimal CalculateRepairCost()
        {
            decimal cost = 120m;

            if (CompressorNeedsRepair)
            {
                cost += 80m;
            }

            return cost;
        }
    }
}
