namespace MakarovLisp
{
    public static class CharExtensions
    {
        public static bool IsOperator(this char c)
        {
            const string operators = "+-/*^!=";

            foreach (char op in operators)
            {
                if (c == op)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
