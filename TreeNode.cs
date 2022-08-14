namespace MakarovLisp
{
    public class TreeNode
    {
        public TreeNode(string content, int line)
        {
            Content = content;
            Line = line;
        }

        public TreeNode(IEnumerable<TreeNode> children, int line)
        {
            Children = children.ToList();
            Line = line;
        }
        
        public string? Content { get; }
        public IEnumerable<TreeNode>? Children { get; }
        public int Line { get; }
    }
}
