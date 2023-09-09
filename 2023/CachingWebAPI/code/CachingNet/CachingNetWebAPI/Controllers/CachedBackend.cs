namespace CachingNetWebAPI.Controllers;

[AutoActions(template = TemplateIndicator.NoArgs_Is_Get_Else_Post, FieldsName = new[] { "*" }, ExcludeFields = new[] { "_logger" })]
[Route("api/[controller]/[action]")]
[ApiController]
public partial class CachedBackendController : ControllerBase
{
    private readonly clsCachedData cachedData;

    public CachedBackendController(clsCachedData cachedData)
    {
        this.cachedData = cachedData;
    }
    
}
