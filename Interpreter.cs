using System.Reflection;

namespace MakarovLisp
{
    public class Interpreter : IInterpreter
    {
        private readonly Dictionary<string, BuiltInFunction> _builtInFunctions = new();
        private readonly Dictionary<string, Func<Context, string>> _userFunctions = new();

        public void Run(IEnumerable<string> source)
        {
            List<Token> tokens = new Tokenizer().Tokenize(source);
            Console.WriteLine("Tokens: {0}", string.Join(' ', tokens.Select(token => token.Value)));

            Console.WriteLine();
            Console.WriteLine("Tree:");
            TreeNode tree = CreateTree(tokens);
            PrintTree(tree);

            InitBuiltInFunctions();

            Console.WriteLine();
            Console.WriteLine("Execute...");
            ExecuteTree(tree);
        }

        private void InitBuiltInFunctions()
        {
            var baseClass = typeof(BuiltInFunction);

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsSubclassOf(baseClass))
                {
                    var instance = (BuiltInFunction)Activator.CreateInstance(type)!;
                    _builtInFunctions.Add(instance.GetName(), instance);
                }
            }
        }

        private TreeNode CreateTree(List<Token> tokens)
        {
            var children = new List<TreeNode>();
            int deep = 0;
            var inner = new List<Token>();

            foreach (Token token in tokens)
            {
                if (deep == 0)
                {
                    if (token.Value.IsOpenBracket())
                    {
                        deep++;
                    }
                    else if (token.Value.IsCloseBracket())
                    {
                        throw new InterpreterException("Too many brackets", token.Line);
                    }
                    else
                    {
                        children.Add(new TreeNode(token.Value, token.Line));
                    }
                }
                else
                {
                    if (token.Value.IsOpenBracket())
                    {
                        deep++;
                        inner.Add(token);
                    }
                    else if (token.Value.IsCloseBracket())
                    {
                        if (deep > 1)
                        {
                            inner.Add(token);
                        }

                        deep--;

                        if (deep == 0 && inner.Count > 0)
                        {
                            children.Add(CreateTree(inner));
                            inner.Clear();
                        }
                    }
                    else if (deep > 0)
                    {
                        inner.Add(token);
                    }
                }
            }

            return new TreeNode(children, tokens.Any() ? tokens.First().Line : 1);
        }

        private void PrintTree(TreeNode node)
        {
            foreach (TreeNode child in node.Children!)
            {
                Print(child.Children!, 0);
            }
        }

        private void Print(IEnumerable<TreeNode> nodes, int deep)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Content is not null)
                {
                    for (int i = 0; i < deep; ++i)
                    {
                        Console.Write("    ");
                    }

                    Console.WriteLine(node.Content);
                }
                else if (node.Children is not null)
                {
                    Print(node.Children, deep + 1);
                }
            }
        }

        private void ExecuteTree(TreeNode node)
        {
            foreach (TreeNode child in node.Children!)
            {
                if (child.Children!.First().Content == "defun")
                {
                    Defun(child.Children!, node.Line);
                }
                else
                {
                    Execute(child.Children!, new Dictionary<string, string>());
                }
            }
        }

        private void Defun(IEnumerable<TreeNode> nodes, int line)
        {
            var defun = nodes.ToList();
            string functionName = defun[1].Content!;
            var functionParams = defun[2].Children;
            var functionBody = defun[3].Children;

            _userFunctions.Add(functionName, context =>
            {
                string[] paramNames = functionParams.Select(p => p.Content).ToArray();
                string[] paramValues = context.Parameters.ToArray();
                var kvp = new Dictionary<string, string>();

                for (int i = 0; i < paramNames.Length; ++i)
                {
                    kvp.Add(paramNames[i], paramValues[i]);
                }

                return Execute(functionBody, kvp);
            });
        }

        private string Execute(IEnumerable<TreeNode> nodes, Dictionary<string, string> variables)
        {
            var nodesList = nodes.ToList();
            string function = nodesList[0].Content!;
            var parameters = new List<string>();

            if (function == "if")
            {
                string condition = nodesList[1].Content ?? Execute(nodesList[1].Children, variables);

                if (condition == "true")
                {
                    return nodesList[2].Content ?? Execute(nodesList[2].Children, variables);
                }
                else
                {
                    return nodesList[3].Content ?? Execute(nodesList[3].Children, variables);
                }
            }

            foreach (TreeNode node in nodesList.Skip(1))
            {
                if (node.Content is not null)
                {
                    parameters.Add(node.Content);
                }
                else if (node.Children is not null)
                {
                    parameters.Add(Execute(node.Children, variables));
                }
            }

            var context = new Context(parameters, variables, nodesList[0].Line);

            return ExecuteFunction(function, context);
        }

        public string ExecuteFunction(string function, Context context)
        {
            if (_userFunctions.ContainsKey(function))
            {
                return _userFunctions[function](context);
            }

            if (_builtInFunctions.ContainsKey(function))
            {
                return _builtInFunctions[function].ExecuteFunction(this, context);
            }

            throw new InterpreterException($"Function \"{function}\" not exists", context.Line);
        }
    }
}
