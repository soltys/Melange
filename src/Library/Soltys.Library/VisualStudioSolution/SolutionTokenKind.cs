namespace Soltys.Library.VisualStudioSolution
{
    public enum SolutionTokenKind
    {
        Undefined,
        /// <summary>
        /// Example: #
        /// </summary>
        Hash,
        /// <summary>
        /// Example: Project, Global, EndProject
        /// </summary>
        Id,
        /// <summary>
        /// Example: {C67CD1BA-F675-4559-B1FD-A886315A2D1B} 
        /// </summary>
        Guid,
        /// <summary>
        /// Example: "app"
        /// </summary>
        String,
        /// <summary>
        /// Example: (
        /// </summary>
        LParen,
        /// <summary>
        /// Example: )
        /// </summary>
        RParen,
        /// <summary>
        /// Example: =
        /// </summary>
        Equal,
        /// <summary>
        /// Example: ,
        /// </summary>
        Comma,
        /// <summary>
        /// Example: |
        /// </summary>
        Pipe,
        /// <summary>
        /// Example:10.0.40219.1
        /// Example:12
        /// Example:22.20
        /// </summary>
        Version,
    }
}
