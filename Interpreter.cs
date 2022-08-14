namespace MakarovLisp
{
    public class Interpreter
    {
        public void Run(IEnumerable<string> source)
        {
            List<Token> tokens = new Tokenizer().Tokenize(source);
            Console.WriteLine("Tokens: {0}", string.Join(' ', tokens));

            Console.WriteLine();
            Console.WriteLine("Tree:");
            TreeNode tree = CreateTree(tokens);
            PrintTree(tree);

            Console.WriteLine();
            Console.WriteLine("Execute...");
            ExecuteTree(tree);
        }

        private TreeNode CreateTree(IEnumerable<Token> tokens)
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
                        children.Add(new TreeNode(token.Value));
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

            return new TreeNode(children);
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
                    Defun(child.Children!);
                }
                else
                {
                    Execute(child.Children!, new Dictionary<string, string>());
                }
            }
        }

        private void Defun(IEnumerable<TreeNode> nodes)
        {
            var defun = nodes.ToList();
            string functionName = defun[1].Content!;
            var functionParams = defun[2].Children;
            var functionBody = defun[3].Children;

            _userFunctions.Add(functionName, (IEnumerable<string> parameters) =>
            {
                string[] paramNames = functionParams.Select(p => p.Content).ToArray();
                string[] paramValues = parameters.ToArray();
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
            nodes = nodes.ToList();
            string function = nodes.First().Content!;
            var parameters = new List<string>();

            if (function == "if")
            {
                var n = nodes.ToList();
                string condition = n[1].Content ?? Execute(n[1].Children, variables);

                if (condition == "true")
                {
                    return n[2].Content ?? Execute(n[2].Children, variables);
                }
                else
                {
                    return n[3].Content ?? Execute(n[3].Children, variables);
                }
            }

            foreach (TreeNode node in nodes.Skip(1))
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

            return ExecuteFunction(function, parameters, variables);
        }

        private string ExecuteFunction(string function, IEnumerable<string> parameters, Dictionary<string, string> variables)
        {
            if (function == "print")
            {
                string content = parameters.First();
                Console.WriteLine(content);
                return content.Length.ToString();
            }

            if (function == "+")
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
                    result += p;
                }

                return result.ToString();
            }

            if (function == "-")
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

            if (function == "*")
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
                    result *= p;
                }

                return result.ToString();
            }

            if (function == "/")
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
                    result /= p;
                }

                return result.ToString();
            }

            if (function == ">")
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

            if (function == "<")
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

                return r1 < r2 ? "true" : "false";
            }

            if (function == "zerop")
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

            if (_userFunctions.ContainsKey(function))
            {
                return _userFunctions[function](parameters);
            }

            throw new Exception($"Function {function} not exists.");
        }

        private Dictionary<string, Func<IEnumerable<string>, string>> _userFunctions = new();
    }
}
