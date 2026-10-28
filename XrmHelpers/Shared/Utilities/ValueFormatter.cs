namespace spgsWebTools.XrmHelpers.Shared.Utilities
{
    public static class ValueFormatter
    {
        public static string Format(object? value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is DateTime dateTime)
            {
                return dateTime.ToString("dd/MM/yyyy HH:mm");
            }

            if (value is DateTime? && (value as DateTime?).HasValue)
            {
                DateTime val = (DateTime)value;
                return val.ToString("dd/MM/yyyy HH:mm");
            }

            if (value is bool boolValue)
            {
                return boolValue ? "Yes" : "No";
            }

            if (value is bool? && (value as bool?).HasValue)
            {
                return (bool)value ? "Yes" : "No";
            }

            return value.ToString() ?? string.Empty;
        }
    }
}