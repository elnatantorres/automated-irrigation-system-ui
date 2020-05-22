namespace AutomatedIrrigationSystem.Model
{
    public class GetIrrigationExecution
    {
        public Result[] results { get; set; }
    }

    public class Result
    {
        public int Id { get; set; }
        public int IrrigationSystemId { get; set; }
        public string InitialExecutionDateTime { get; set; }
        public string FinalExecutionDateTime { get; set; }
        public int UserId { get; set; }
    }

}
