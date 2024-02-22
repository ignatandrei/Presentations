namespace RSCG_TemplatingCommon.InterfacesV1
{
    public interface IMethodData
    {
        string FileName { get; }
        int Line { get; }
        string MethodName { get; }
        string NameVariable { get; }
        string[] parameters { get; }

        string MethodCall { get; }
        string MethodDeclaration { get; }
    }
}