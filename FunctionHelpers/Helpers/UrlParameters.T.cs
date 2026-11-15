using System.Runtime.CompilerServices;
using Custos.XrmHelpers.Constants;

namespace Custos.FunctionHelpers.Utilities
{
    public static class UrlParameters
    {
        public static T Sanitise<T>(string param, string type)
        {
            bool allowReturn = false;
            var input = param;

            if (string.IsNullOrWhiteSpace(input)) throw new InvalidDataException("SanitiseUrlParams called with empty param. Exiting with exception.");

            switch (type)
            {
                case "alphanumeric":
                    if (IsAlphaNumeric(input)) allowReturn = true;
                    break;
                case "columnname":
                    switch (input)
                    {
                        case CoreStaffMemberFields.DisplayName:
                            allowReturn = true;
                            break;
                        case CoreStaffMemberFields.WorkEmail:
                            allowReturn = true;
                            break;
                        case CoreStaffMemberFields.FullName:
                            allowReturn = true;
                            break;
                        default:
                            var message = $"Invalid attribute name supplied: {input}";
                            throw new SwitchExpressionException(message);
                    }
                    break;
                case "integer":
                    if (input.All(char.IsDigit)) allowReturn = true;
                    break;
                case "guid":
                    if (IsGuid(input)) allowReturn = true;
                    break;
                case "bool":
                    if (IsBool(input)) allowReturn = true;
                    break;
                default:
                    throw new SwitchExpressionException("Invalid type passed to SanitiseUrlParams method.");
            }

            if (allowReturn)
            {
                if (type == "guid")
                {
                    if (!Guid.TryParse(input, out var result))
                    {
                        throw new InvalidDataException($"{input} cannot be parsed to a GUID.");
                    }
                    return (T)(object)result;
                }
                if (type == "bool")
                {
                    if (!bool.TryParse(input, out var result))
                    {
                        throw new InvalidDataException($"{input} cannot be parsed to a bool.");
                    }
                    return (T)(object)result;
                }
                else if (type == "integer")
                {
                    if (!int.TryParse(input, out var result))
                    {
                        throw new InvalidDataException($"{input} cannot be parsed to an int.");
                    }
                    return (T)(object)result;
                }
                else if (type == "alphanumeric")
                {
                    return (T)(object)input;
                }
                return (T)Convert.ChangeType(input, typeof(T));
            }
            else
            {
                throw new InvalidDataException("URL query parameter did not pass sanitisation. Exiting.");
            }
        }
        private static bool IsAlphaNumeric(string str)
        {
            return str.All(char.IsLetterOrDigit);
        }
        private static bool IsStaffId(string str)
        {
            return str.Length <= 6
                && str.Length > 1
                && str.All(char.IsDigit)
                && int.TryParse(str, out int result);
        }
        private static bool IsBool(string str)
        {
            return bool.TryParse(str, out bool _);
        }
        private static bool IsGuid(string str)
        {
            if (str.Length == 36)
            {
                try
                {
                    Guid.TryParse(str, out Guid myGuid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception in IsGuid.TryParse: {0}", ex);
                    throw;
                }
            }
            return str.Length == 36
                && Guid.TryParse(str, out Guid result);
        }
    }
}