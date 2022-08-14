namespace MakarovLisp
{
    public class TreeNode
    {
        public TreeNode(string content)
        {
            Content = content;
        }

        public TreeNode(IEnumerable<TreeNode> children)
        {
            Children = children.ToList();
        }
        
        public string? Content { get; }
        public IEnumerable<TreeNode>? Children { get; }
    }
}
