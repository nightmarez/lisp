namespace MakarovLisp
{
    public interface IInterpreter
    {
        string ExecuteFunction(string function, Context context);
    }
}
