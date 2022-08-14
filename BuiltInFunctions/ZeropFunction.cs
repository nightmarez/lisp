namespace MakarovLisp.BuiltInFunctions
{
    public sealed class ZeropFunction : BuiltInFunction
    {
        public override string GetName()
        {
            return "zerop";
        }

        public override string ExecuteFunction(IInterpreter interpreter, Context context)
        {
            try
            {
                string value = context.Parameters.First();
                double result;

                if (!double.TryParse(value, out result))
                {
                    result = double.Parse(context.Variables[value]);
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
