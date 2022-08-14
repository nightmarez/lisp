namespace MakarovLisp
{
    public class InterpreterException : ApplicationException
    {
        public InterpreterException(string reason, int line)
            : base($"Error. Line: {line}. Reason: {reason}.")
        { }
    }
}
