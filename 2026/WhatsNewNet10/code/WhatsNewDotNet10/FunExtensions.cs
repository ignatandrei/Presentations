namespace WhatsNewDotNet10;
/// <summary>
/// https://www.alexeyfv.xyz/en/post/2025-11-25-csharp-goes-brrr/
/// </summary>
public static class FunExtensions
{
    //python
    extension(string source)
    {
        /// <summary>
        /// python style string multiplication
        /// </summary>
        /// <param name="str"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string operator *(string str, int count)
        {
            return string.Concat(Enumerable.Repeat(str, count));
        }
    }

    // javascript
    extension(string source)
    {
        public static int operator -(string str, int number) => int.Parse(str) - number;
        public static string operator +(string str, int number) => str + number.ToString();
        public static int operator -(string a, string b) => int.Parse(a) - int.Parse(b);
    }
    //fsharp
    extension<T, TResult>(T)
    {
        public static TResult operator |(T source, Func<T, TResult> f) => f(source);
    }

    public static string ToUpper(string source) => source.ToUpper();
}
