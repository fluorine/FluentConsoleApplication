namespace FluentConsole
{
    /// <summary>
    /// Type that can generate usage documentation about itself.
    /// </summary>
    public interface IUsageDocumentation
    {
        /// <summary>
        /// Short documentation for usage of this item
        /// </summary>
        /// <returns>Usage documentation</returns>
        string GetUsageDocumentation();
    }
}