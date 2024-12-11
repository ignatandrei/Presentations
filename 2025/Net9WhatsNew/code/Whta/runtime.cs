using FeatureSwitchDemo;

namespace Whta;
//runtime
//https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/runtime

static internal class runtime9
{
    //Feature Switch - see csproj with  <RuntimeHostConfigurationOption Include="Feature.IsSupported" Value="false" Trim="true" />
    //difference vs Feature Flags - Feature Flags are runtime configuration, Feature Switches are compile time configuration
    //https://learn.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core
    //TODO: 
    static internal void FeatureSwitchDemo()
    {
        if (MyFeature.IsSupported)
            MyFeature.Implementation();
    }

    //TODO:
    //https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/runtime#loop-counter-variable-direction
    static internal void LoopCounterDemo()
    {
        for (int i = 0; i < 100; i++)
        {
            string x = "asd";
            Console.WriteLine(x);
        }

    }
    //TODO: https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/runtime#inlining-improvements
    //Inlining improvements



}
