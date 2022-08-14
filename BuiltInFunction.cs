namespace MakarovLisp
{
    public abstract class BuiltInFunction
    {
        public abstract string GetName();

        public abstract string ExecuteFunction(IInterpreter interpreter, Context context);
    }
}
