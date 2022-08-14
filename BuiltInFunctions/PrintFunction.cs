namespace MakarovLisp.BuiltInFunctions
{
    public sealed class PrintFunction : BuiltInFunction
    {
        public override string GetName()
        {
            return "print";
        }

        public override string ExecuteFunction(IEnumerable<string> parameters, Dictionary<string, string> variables, int line)
        {
            string content = parameters.First();
            Console.WriteLine(content);
            return content.Length.ToString();
        }
    }
}
