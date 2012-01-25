namespace ActivityStreamSharp
{
    internal interface IForgivingExpandoObject
    {
        /// <summary>
        /// The underlying ExpandoObject.
        /// </summary>
        dynamic Expando { get; set; }
    }
}