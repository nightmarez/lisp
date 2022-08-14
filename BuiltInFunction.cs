namespace MakarovLisp
{
    public abstract class BuiltInFunction
    {
        public abstract string GetName();

        public abstract string ExecuteFunction(IEnumerable<string> parameters, Dictionary<string, string> variables, int line);
    }
}
