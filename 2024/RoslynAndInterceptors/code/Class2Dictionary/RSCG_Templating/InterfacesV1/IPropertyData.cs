namespace RSCG_TemplatingCommon.InterfacesV1
{

    public interface IPropertyData
    {
        bool CanCallSetMethod { get; }
        bool CanCallGetMethod { get; }
        string PropertyName { get; set; }
        string PropertyType { get; }
    }

}