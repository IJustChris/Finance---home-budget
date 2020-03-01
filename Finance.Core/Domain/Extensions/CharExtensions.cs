namespace Finance.Core.Domain.Extensions
{
    public static class CharExtensions
    {

        public static bool isNumber(this char ch)
        {
            if (ch >= 48 && ch <= 57)
                return true;

            return false;
        }

        public static bool isUpperCaseHexCharacter(this char ch)
        {
            if (ch >= 65 && ch <= 70)
                return true;

            return false;
        }
    }
}
