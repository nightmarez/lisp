using MakarovLisp;

Console.WriteLine("LISP interpreter");
Console.WriteLine();

try
{
    var source = File.ReadLines("..\\..\\..\\demo.lisp");

    var interpreter = new Interpreter();

    interpreter.Run(source);
}
catch (TokenizerException e)
{
    Console.WriteLine(e.Message);
}
catch (InterpreterException e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine();
Console.WriteLine("Done");
