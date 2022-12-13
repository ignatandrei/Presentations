public interface AddRetrievedDate
{
    DateTime? RetrievedDate { get; set; }
}
public class SetRetrievedInterceptor : IMaterializationInterceptor
{
    public object InitializedInstance(MaterializationInterceptionData materializationData, object instance)
    {
        if (instance is AddRetrievedDate hasRetrieved)
        {
            hasRetrieved.RetrievedDate = DateTime.UtcNow;
        }

        return instance;
    }
}
