namespace MakarovLisp
{
    public static class CharExtensions
    {
        public static bool IsOperator(this char symbol)
        {
            return "+-/*^!=".Any(ch => ch == symbol);
        }

        public static bool IsOpenBracket(this char symbol)
        {
            return symbol == '(';
        }

        public static bool IsCloseBracket(this char symbol)
        {
            return symbol == ')';
        }
    }
}
