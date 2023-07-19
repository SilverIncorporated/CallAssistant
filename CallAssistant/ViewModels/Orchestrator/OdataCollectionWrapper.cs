namespace CallAssistant.ViewModels.Orchestrator
{
    public class OdataCollectionWrapper<T> where T : class
    {
        public IEnumerable<T> Value { get; set; }
    }
}
