namespace MakarovLisp
{
    public static class StringExtensions
    {
        public static bool IsOpenBracket(this string symbol)
        {
            symbol = symbol.Trim();
            return symbol.Length == 1 && symbol[0].IsOpenBracket();
        }

        public static bool IsCloseBracket(this string symbol)
        {
            symbol = symbol.Trim();
            return symbol.Length == 1 && symbol[0].IsCloseBracket();
        }
    }
}
