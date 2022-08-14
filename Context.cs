namespace MakarovLisp
{
    public class Context
    {
        public Context(IEnumerable<string> parameters, IDictionary<string, string> variables, int line)
        {
            Parameters = parameters;
            Variables = variables;
            Line = line;
        }

        public IEnumerable<string> Parameters { get; }

        public IDictionary<string, string> Variables { get; }

        public int Line { get; }
    }
}
