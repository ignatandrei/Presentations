namespace RSCG_TimeBombComment;
[Generator]
public class GenerateFromCommentsIncremental : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {

        //get the syntax provider
        GenerateCommentsAsError.Initialize(context);
        
    }

}
