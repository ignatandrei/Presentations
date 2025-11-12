using System.Reflection.Metadata.Ecma335;

namespace WhatsNewDotNet10;

internal static class CSharp14
{
    # region extensions
    public static bool IsEmptyAndrei<T>(this List<T>? collection)
    {
        if (collection == null) return true;
        return collection.Count==0;
    }
    
    //https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#extension-members
    extension<TSource>(List<TSource>? source)
    {
        //function on instance of TSource[]
        public bool IsEmptyNew()
        {
            return (source?.Count??0) == 0;
        }
        public static List<TSource> Combine(List<TSource>? first, List<TSource>? second) { 
            return [.. Enumerable.Concat(first ?? Enumerable.Empty<TSource>(), second ?? Enumerable.Empty<TSource>())];
        }

        //property on instance of TSource[]
        public bool IsEmptyProperty => (source?.Count ?? 0) == 0;
        //function on class TSource
        public static List<TSource>? Zero => new List<TSource>();
        
    }
    #endregion
}
