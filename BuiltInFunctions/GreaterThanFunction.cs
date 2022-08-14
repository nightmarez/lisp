namespace MakarovLisp.BuiltInFunctions
{
    public sealed class GreaterThanFunction : BuiltInFunction
    {
        public override string GetName()
        {
            return ">";
        }

        public override string ExecuteFunction(IEnumerable<string> parameters, Dictionary<string, string> variables, int line)
        {
            string p1 = parameters.First();
            string p2 = parameters.Last();
            double r1;
            double r2;

            if (!double.TryParse(p1, out r1))
            {
                r1 = double.Parse(variables[p1]);
            }

            if (!double.TryParse(p2, out r2))
            {
                r2 = double.Parse(variables[p2]);
            }

            return r1 > r2 ? "true" : "false";
        }
    }
}
