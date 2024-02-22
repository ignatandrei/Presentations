using RSCG_TemplatingCommon.InterfacesV1;

namespace RSCG_Templating.ImplementV1;

internal class PropertyData : IPropertyData
{
    private readonly IPropertySymbol propertySymbol;
    public string PropertyName { get; set; }
    public string PropertyType { get; set; }
    public bool CanCallSetMethod { get; set; }
    public bool CanCallGetMethod { get; set; }
    public Accessibility GetAccesibility { get; private set; }
    public Accessibility SetAccesibility { get; private set; }

    public PropertyData(IPropertySymbol propertySymbol)
    {
        this.propertySymbol = propertySymbol;
        PropertyName = propertySymbol.Name;
        PropertyType = propertySymbol.Type.ToDisplayString();
        var getAcces = propertySymbol.GetMethod;
        CanCallGetMethod = getAcces != null;
        if (CanCallGetMethod)
        {
            GetAccesibility= (Accessibility)((int)getAcces!.DeclaredAccessibility);
        }
        var setAcces = propertySymbol.SetMethod;
        CanCallSetMethod = setAcces != null;
        if(CanCallSetMethod)
        {
            SetAccesibility = (Accessibility)((int)setAcces!.DeclaredAccessibility);
        }

    }

    //public string PropertyCode
    //{

    //    get
    //    {


    //        var get = CanCallGetMethod ? $$"""
    //            get{
    //            return original.{{PropertyName}};
    //            }
    //    """ : "";
    //        var set = CanCallSetMethod ? $$"""
    //        set{
    //            original.{{PropertyName}}=value;
    //        }            

    //    """ : "";
    //        return $$"""
    //        public {{Type}} {{PropertyName}} {
    //            {{get}}
    //            {{set}}            
    //        } 
    //        """;

    //    }
    //}

}


