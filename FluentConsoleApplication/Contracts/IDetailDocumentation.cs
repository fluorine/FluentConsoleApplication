namespace FluentConsole
{
    /// <summary>
    /// Type that can generate detail documentation about itself.
    /// </summary>
    public interface IDetailDocumentation
    {
        /// <summary>
        /// Get details documentation for this type.
        /// </summary>
        /// <returns>Detail Documentation as a <see cref="string"/></returns>
        string GetDetailDocumentation(bool includeType = true);
    }
}