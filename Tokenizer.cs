namespace MakarovLisp
{
    public class Tokenizer
    {
        public List<string> Tokenize(string source)
        {
            var result = new List<string>();
            string current = string.Empty;
            TokenizerState state = TokenizerState.None;

            foreach (char c in source)
            {
                switch (state)
                {
                    case TokenizerState.None:
                        if (c == '(' || c == ')')
                        {
                            current = string.Empty;
                            result.Add(c.ToString());
                        }
                        else if (char.IsDigit(c))
                        {
                            current = c.ToString();
                            state = TokenizerState.Number;
                        }
                        else if (char.IsLetter(c))
                        {
                            current = c.ToString();
                            state = TokenizerState.Literal;
                        }
                        else if (c.IsOperator())
                        {
                            result.Add(c.ToString());
                        }
                        else if (c == '"')
                        {
                            current = c.ToString();
                            state = TokenizerState.String;
                        }

                        break;

                    case TokenizerState.Literal:
                        if (c == '(' || c == ')')
                        {
                            if (string.IsNullOrEmpty(current))
                            {
                                throw new TokenizerException("Empty literal");
                            }
                            else
                            {
                                result.Add(current);
                                current = string.Empty;
                                state = TokenizerState.None;
                            }

                            result.Add(c.ToString());
                        }
                        else if (char.IsDigit(c))
                        {
                            current += c.ToString();
                        }
                        else if (char.IsLetter(c))
                        {
                            current += c.ToString();
                        }
                        else if (char.IsWhiteSpace(c))
                        {
                            if (string.IsNullOrEmpty(current))
                            {
                                throw new TokenizerException("Empty literal");
                            }
                            else
                            {
                                result.Add(current);
                                current = string.Empty;
                                state = TokenizerState.None;
                            }
                        }
                        else if (c.IsOperator())
                        {
                            if (string.IsNullOrEmpty(current))
                            {
                                throw new TokenizerException("Empty literal");
                            }
                            else
                            {
                                result.Add(current);
                                current = string.Empty;
                                state = TokenizerState.None;
                            }

                            result.Add(c.ToString());
                        }
                        else if (c == '"')
                        {
                            throw new TokenizerException("Unexpected symbol \"");
                        }

                        break;

                    case TokenizerState.Number:
                        if (c == '(' || c == ')')
                        {
                            if (string.IsNullOrEmpty(current))
                            {
                                throw new TokenizerException("Empty number");
                            }
                            else
                            {
                                result.Add(current);
                                current = string.Empty;
                                state = TokenizerState.None;
                            }

                            result.Add(c.ToString());
                        }
                        else if (char.IsDigit(c))
                        {
                            current += c.ToString();
                        }
                        else if (char.IsLetter(c))
                        {
                            throw new TokenizerException("Number expected");
                        }
                        else if (char.IsWhiteSpace(c))
                        {
                            if (string.IsNullOrEmpty(current))
                            {
                                throw new TokenizerException("Empty number");
                            }
                            else
                            {
                                result.Add(current);
                                current = string.Empty;
                                state = TokenizerState.None;
                            }
                        }
                        else if (c.IsOperator())
                        {
                            if (string.IsNullOrEmpty(current))
                            {
                                throw new TokenizerException("Empty number");
                            }
                            else
                            {
                                result.Add(current);
                                current = string.Empty;
                                state = TokenizerState.None;
                            }

                            result.Add(c.ToString());
                        }
                        else if (c == '"')
                        {
                            throw new TokenizerException("Unexpected symbol \"");
                        }

                        break;

                    case TokenizerState.String:
                        if (c == '(' || c == ')')
                        {
                            current += c.ToString();
                        }
                        else if (char.IsDigit(c))
                        {
                            current += c.ToString();
                        }
                        else if (char.IsLetter(c))
                        {
                            current += c.ToString();
                        }
                        else if (char.IsWhiteSpace(c))
                        {
                            current += c.ToString();
                        }
                        else if (c.IsOperator())
                        {
                            current += c.ToString();
                        }
                        else if (c == '"')
                        {
                            current += c.ToString();
                            result.Add(current);
                            current = string.Empty;
                            state = TokenizerState.None;
                        }
                        else
                        {
                            current += c.ToString();
                        }

                        break;
                }
            }

            return result;
        }
    }
}
