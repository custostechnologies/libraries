namespace custos.FunctionHelpers.Types
{
    public class CustomQueryParameters
    {
        public string? attributeName { get; set; }
        public string? searchTerm { get; set; }
        public int topCount { get; set; }
        public bool includeImages { get; set; }
    }
}