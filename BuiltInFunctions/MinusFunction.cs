namespace MakarovLisp.BuiltInFunctions
{
    public sealed class MinusFunction : BuiltInFunction
    {
        public override string GetName()
        {
            return "-";
        }

        public override string ExecuteFunction(IEnumerable<string> parameters, Dictionary<string, string> variables, int line)
        {
            string value = parameters.First();
            double result;

            if (!double.TryParse(value, out result))
            {
                result = double.Parse(variables[value]);
            }

            foreach (string parameter in parameters.Skip(1))
            {
                double p = double.Parse(parameter);
                result -= p;
            }

            return result.ToString();
        }
    }
}
