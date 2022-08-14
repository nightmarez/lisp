namespace MakarovLisp.BuiltInFunctions
{
    public sealed class DivideFunction : BuiltInFunction
    {
        public override string GetName()
        {
            return "/";
        }

        public override string ExecuteFunction(IInterpreter interpreter, Context context)
        {
            string value = context.Parameters.First();
            double result;

            if (!double.TryParse(value, out result))
            {
                result = double.Parse(context.Variables[value]);
            }

            foreach (string parameter in context.Parameters.Skip(1))
            {
                double p = double.Parse(parameter);
                result /= p;
            }

            return result.ToString();
        }
    }
}
