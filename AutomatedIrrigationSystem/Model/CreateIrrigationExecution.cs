using Newtonsoft.Json;

namespace AutomatedIrrigationSystem.Model
{
    public class CreateIrrigationExecution
    {
        [JsonProperty(PropertyName = "irrigationSystemId")]
        public int IrrigationSystemId { get; set; }
    }
}
