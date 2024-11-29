namespace WhtaCSharp13;

internal static class ShowParams
{

    public static string Sum(params int[] numbers)
    {
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        return $"Sum is {sum}";
    }
    public static string Sum2(params IList<int> numbers)
    {
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        return $"Sum is {sum}";
    }
}
