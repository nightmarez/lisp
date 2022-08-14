namespace MakarovLisp.BuiltInFunctions
{
    public sealed class PrintFunction : BuiltInFunction
    {
        public override string GetName()
        {
            return "print";
        }

        public override string ExecuteFunction(IInterpreter interpreter, Context context)
        {
            string content = context.Parameters.First();
            Console.WriteLine(content);
            return content.Length.ToString();
        }
    }
}
