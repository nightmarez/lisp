namespace MakarovLisp
{
    public class TokenizerException : ApplicationException
    {
        public TokenizerException(string reason)
            : base($"Tokenizer Error: {reason}.")
        { }
    }
}
