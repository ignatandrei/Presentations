using System;

namespace RSCG_TemplatingCommon
{

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class IGenerateDataFromClassAttribute : Attribute
    {
        public IGenerateDataFromClassAttribute(string nameTemplateFile)
        {
            NameTemplateFile = nameTemplateFile;
        }
        public string NameTemplateFile { get; }
    }
}