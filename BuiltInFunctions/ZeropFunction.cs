namespace MakarovLisp.BuiltInFunctions
{
    public sealed class ZeropFunction : BuiltInFunction
    {
        public override string GetName()
        {
            return "zerop";
        }

        public override string ExecuteFunction(IEnumerable<string> parameters, Dictionary<string, string> variables, int line)
        {
            try
            {
                string value = parameters.First();
                double result;

                if (!double.TryParse(value, out result))
                {
                    result = double.Parse(variables[value]);
                }

                return result == 0 ? "true" : "false";
            }
            catch
            {
                return "false";
            }
        }
    }
}
