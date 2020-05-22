namespace AutomatedIrrigationSystem.Model
{
    public class IrrigationSystem
    {
        public int Id { get; set; }
        public string Plantation { get; set; }
        public string Irrigator { get; set; }
        public string Controller { get; set; }
        public string SolenoidValve { get; set; }
        public string Sensor { get; set; }

    }
}
