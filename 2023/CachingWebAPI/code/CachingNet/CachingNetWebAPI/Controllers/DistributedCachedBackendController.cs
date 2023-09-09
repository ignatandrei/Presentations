namespace CachingNetWebAPI.Controllers;

[AutoActions(template = TemplateIndicator.NoArgs_Is_Get_Else_Post, FieldsName = new[] { "*" }, ExcludeFields = new[] { "_logger" })]
[Route("api/[controller]/[action]")]
[ApiController]
public partial class DistributedCachedBackendController : ControllerBase
{
    private readonly clsDistributedCachedData cachedData;

    public DistributedCachedBackendController(clsDistributedCachedData cachedData)
    {
        this.cachedData = cachedData;
    }
    
}
