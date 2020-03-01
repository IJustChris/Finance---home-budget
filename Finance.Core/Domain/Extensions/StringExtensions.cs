using System.Text;

namespace Finance.Core.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool isEmpty(this string value)
            => string.IsNullOrEmpty(value) ? false : string.IsNullOrWhiteSpace(value);

        public static bool isHexColor(this string color)
        {
            if (color?[0] == '#' || color.Length.isAnyOfGivenValue(6,3,4,7))
            {
                if (color.Length == 6 && color[0] != '#')
                {
                    color = new StringBuilder("#").Append(color).ToString();
                }

                foreach (var ch in color.Substring(1))
                {
                    if (!ch.isNumber() && !ch.isUpperCaseHexCharacter())
                        return false;
                }
            }
            else
                return false;

            return true;
        }

    }
}
