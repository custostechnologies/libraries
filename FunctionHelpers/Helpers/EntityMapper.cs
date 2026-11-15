using System.Text.Json;
using Microsoft.Xrm.Sdk;

namespace custos.FunctionHelpers.Extensions
{
    public static class EntityMapper
    {
        public static Entity MapJsonToEntity(
            string jsonPayload,
            string targetTable,
            HashSet<string> singleSelectOptions,
            HashSet<string> multiSelectOptions
        )
        {
            var entity = new Entity(targetTable);

            using var doc = JsonDocument.Parse(jsonPayload);

            foreach (var property in doc.RootElement.EnumerateObject())
            {
                var key = property.Name;
                var element = property.Value;

                Console.WriteLine(
                    $"key: {key}, valueKind: {element.ValueKind}, value: {element.GetRawText()}"
                );

                object? value = element.ValueKind switch
                {
                    JsonValueKind.Null => null,

                    JsonValueKind.True => true,

                    JsonValueKind.False => false,

                    JsonValueKind.String when element.TryGetDateTime(out var dateValue)
                        => dateValue,

                    JsonValueKind.String
                        => element.GetString(),

                    JsonValueKind.Number when singleSelectOptions.Contains(key)
                        => new OptionSetValue(element.GetInt32()),

                    JsonValueKind.Number when element.TryGetInt32(out var intValue)
                        => intValue,

                    JsonValueKind.Number when element.TryGetDecimal(out var decimalValue)
                        => decimalValue,

                    JsonValueKind.Array when multiSelectOptions.Contains(key)
                        => new OptionSetValueCollection(
                            element
                                .EnumerateArray()
                                .Select(x => new OptionSetValue(x.GetInt32()))
                                .ToList()
                        ),

                    JsonValueKind.Array
                        => element
                            .EnumerateArray()
                            .Select(x => x.GetRawText())
                            .ToList(),

                    _ => throw new NotSupportedException(
                        $"Unsupported value kind '{element.ValueKind}' for attribute '{key}'."
                    )
                };

                entity[key] = value;
            }

            return entity;
        }
    }
}