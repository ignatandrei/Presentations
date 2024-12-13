using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace test;


public partial class MyViewModel :ObservableObject
{
    //see also https://ignatandrei.github.io/RSCG_Examples/v2/docs/CommunityToolkit.Mvvm#example--source-csproj-source-files-

    [ObservableProperty]
    public partial string? Name { get; internal set; }

    //[RelayCommand]
    //private void SayHello()
    //{
    //    Console.WriteLine("Hello");
    //}
}
