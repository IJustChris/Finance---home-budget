

public static class intExtensions
{

    public static bool isAnyOfGivenValue(this int value, params int[] values)
    {
        foreach (var item in values)
            if (value == item)
                return true;

        return false;
    }

    public static bool isEmpty(this int value)
        => value <= 0 ? true : false;
}