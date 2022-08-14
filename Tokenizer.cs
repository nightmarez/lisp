namespace MakarovLisp
{
    public class Tokenizer
    {
        public List<Token> Tokenize(IEnumerable<string> source)
        {
            var result = new List<Token>();
            string current = string.Empty;
            TokenizerState state = TokenizerState.None;
            int line = 0;

            foreach (string sourceLine in source)
            {
                ++line;

                if (sourceLine.Trim().StartsWith(";"))
                {
                    continue;
                }

                foreach (char c in sourceLine)
                {
                    switch (state)
                    {
                        case TokenizerState.None:
                            if (c.IsOpenBracket() || c.IsCloseBracket())
                            {
                                current = string.Empty;
                                result.Add(new Token(c, line));
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
                                result.Add(new Token(c, line));
                            }
                            else if (c == '"')
                            {
                                current = c.ToString();
                                state = TokenizerState.String;
                            }

                            break;

                        case TokenizerState.Literal:
                            if (c.IsOpenBracket() || c.IsCloseBracket())
                            {
                                if (string.IsNullOrEmpty(current))
                                {
                                    throw new TokenizerException("Empty literal");
                                }
                                else
                                {
                                    result.Add(new Token(current, line));
                                    current = string.Empty;
                                    state = TokenizerState.None;
                                }

                                result.Add(new Token(c, line));
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
                                    result.Add(new Token(current, line));
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
                                    result.Add(new Token(current, line));
                                    current = string.Empty;
                                    state = TokenizerState.None;
                                }

                                result.Add(new Token(c, line));
                            }
                            else if (c == '"')
                            {
                                throw new TokenizerException("Unexpected symbol \"");
                            }

                            break;

                        case TokenizerState.Number:
                            if (c.IsOpenBracket() || c.IsCloseBracket())
                            {
                                if (string.IsNullOrEmpty(current))
                                {
                                    throw new TokenizerException("Empty number");
                                }
                                else
                                {
                                    result.Add(new Token(current, line));
                                    current = string.Empty;
                                    state = TokenizerState.None;
                                }

                                result.Add(new Token(c, line));
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
                                    result.Add(new Token(current, line));
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
                                    result.Add(new Token(current, line));
                                    current = string.Empty;
                                    state = TokenizerState.None;
                                }

                                result.Add(new Token(c, line));
                            }
                            else if (c == '"')
                            {
                                throw new TokenizerException("Unexpected symbol \"");
                            }

                            break;

                        case TokenizerState.String:
                            if (c.IsOpenBracket() || c.IsCloseBracket())
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
                                result.Add(new Token(current, line));
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
            }

            return result;
        }
    }
}
