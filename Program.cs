using MakarovLisp;

Console.WriteLine("LISP interpreter");
Console.WriteLine();

var source = File.ReadLines("..\\..\\..\\demo.lisp");

var interpreter = new Interpreter();

interpreter.Run(source);

Console.WriteLine();
Console.WriteLine("Done");
