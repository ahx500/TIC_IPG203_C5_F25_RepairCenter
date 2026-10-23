
namespace RepairCenter.Models
{
    public class WashingMachine : Appliance
    {
        public bool MotorNeedsRepair { get; set; }

        public WashingMachine(
            string serialNumber,
            string brand,
            string model,
            bool motorNeedsRepair)
            : base(serialNumber, brand, model)
        {
            MotorNeedsRepair = motorNeedsRepair;
        }

        public override decimal CalculateRepairCost()
        {
            decimal cost = 90m;

            if (MotorNeedsRepair)
            {
                cost += 45m;
            }

            return cost;
        }
    }
}
