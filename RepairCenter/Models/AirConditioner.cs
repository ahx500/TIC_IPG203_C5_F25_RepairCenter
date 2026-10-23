
namespace RepairCenter.Models
{
    public class AirConditioner : Appliance
    {
        public bool RefrigerantSystemNeedsRepair { get; set; }

        public AirConditioner(
            string serialNumber,
            string brand,
            string model,
            bool refrigerantSystemNeedsRepair)
            : base(serialNumber, brand, model)
        {
            RefrigerantSystemNeedsRepair = refrigerantSystemNeedsRepair;
        }

        public override decimal CalculateRepairCost()
        {
            decimal cost = 160m;

            if (RefrigerantSystemNeedsRepair)
            {
                cost += 100m;
            }

            return cost;
        }
    }
}
