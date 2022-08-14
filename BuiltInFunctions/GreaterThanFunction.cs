namespace MakarovLisp.BuiltInFunctions
{
    public sealed class GreaterThanFunction : BuiltInFunction
    {
        public override string GetName()
        {
            return ">";
        }

        public override string ExecuteFunction(IInterpreter interpreter, Context context)
        {
            string p1 = context.Parameters.First();
            string p2 = context.Parameters.Last();
            double r1;
            double r2;

            if (!double.TryParse(p1, out r1))
            {
                r1 = double.Parse(context.Variables[p1]);
            }

            if (!double.TryParse(p2, out r2))
            {
                r2 = double.Parse(context.Variables[p2]);
            }

            return r1 > r2 ? "true" : "false";
        }
    }
}
