namespace RSCG_TemplatingCommon.InterfacesV1
{

    public interface IClassData
    {
        string? className { get; set; }
        string[] Interfaces { get; set; }
        IMethodData[] methods { get; set; }
        string? nameSpace { get; set; }
        IPropertyData[] properties { get; set; }
    }
}