using Custos.XrmHelpers.Infrastructure.Dataverse.Queries;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Services
{
    public class StyleProvider : IStyleProvider
    {
        private readonly IDataverseLogger _logger;
        public string InlineTableStyle { get; }
        public string TableBodyRowEven { get; }
        public string TableBodyRowOdd { get; }
        public string TableHeadColumn { get; }
        public string TableRowData { get; }
        public string LinkStyle { get; }
        public StyleProvider(
            IDataverseLoggerFactory loggerFactory,
            EnvironmentVariableValues environmentVariableValues
        )
        {
            _logger = loggerFactory.Create<StyleProvider>();

            try
            {
                InlineTableStyle = environmentVariableValues.GetValue<string>(InlineTableStyleSchema).Value ?? DefaultInlineTableStyle;
                TableBodyRowEven = environmentVariableValues.GetValue<string>(TableBodyRowEvenSchema).Value?? DefaultTableBodyRowEven;
                TableBodyRowOdd = environmentVariableValues.GetValue<string>(TableBodyRowOddSchema).Value?? DefaultTableBodyRowOdd;
                TableHeadColumn = environmentVariableValues.GetValue<string>(TableHeadColumnSchema).Value ?? DefaultTableHeadColumn;
                TableRowData = environmentVariableValues.GetValue<string>(TableRowDataSchema).Value ?? DefaultTableRowData;
                LinkStyle = DefaultLinkStyle;
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception creating {nameof(StyleProvider)}. Falling back to defaults. Exception: {ex}");

                InlineTableStyle = DefaultInlineTableStyle;
                TableBodyRowEven = DefaultTableBodyRowEven;
                TableBodyRowOdd = DefaultTableBodyRowOdd;
                TableHeadColumn = DefaultTableHeadColumn;
                TableRowData = DefaultTableRowData;
                LinkStyle = DefaultLinkStyle;
            }
        }

        private const string InlineTableStyleSchema = "mthomas_envVar_htmlStyle_inlineTable";
        private const string TableBodyRowEvenSchema = "mthomas_envVar_htmlStyle_tableBodyRowEven";
        private const string TableBodyRowOddSchema = "mthomas_envVar_htmlStyle_tableBodyRowOdd";
        private const string TableHeadColumnSchema = "mthomas_envVar_htmlStyle_tableHeadColumn";
        private const string TableRowDataSchema = "mthomas_envVar_htmlStyle_tableRowData";

        private const string DefaultInlineTableStyle = "width:100%;border-collapse:collapse;font-family:'Segoe UI',Arial,sans-serif;font-size:14px;";
        private const string DefaultTableBodyRowEven = "background-color:#ffffff;";
        private const string DefaultTableBodyRowOdd = "background-color:#f7f7f7;";
        private const string DefaultTableHeadColumn = "padding:8px;border:1px solid #d9d9d9;background-color:#003366;color:#ffffff;font-weight:bold;text-align:left;";
        private const string DefaultTableRowData = "padding:8px;border:1px solid #d9d9d9;";
        private const string DefaultLinkStyle = "color:#0563c1; text-decoration:none; font-weight:600;";
    }
}