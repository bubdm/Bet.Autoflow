namespace Bet.Autoflow
{
    /// <summary>
    /// The default interface that assist with the type identification thru the workflow engine.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecializedBy<T>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value> The T Type</value>
        T Id { get; set; }
    }
}
