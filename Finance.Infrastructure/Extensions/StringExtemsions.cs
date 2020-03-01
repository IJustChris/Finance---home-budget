namespace Finance.Infrastructure.Extensions
{
    public static class StringExtemsions
    {
        public static bool Empty(this string value)
            => string.IsNullOrWhiteSpace(value) ? false : string.IsNullOrEmpty(value);
    }
}
