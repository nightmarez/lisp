namespace MakarovLisp
{
    public class Token
    {
        public Token(string value, int line)
        {
            Value = value;
            Line = line;
        }

        public Token(char value, int line)
        {
            Value = value.ToString();
            Line = line;
        }

        public string Value { get; }
        public int Line { get; }
    }
}
