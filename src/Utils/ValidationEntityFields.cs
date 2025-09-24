using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;

namespace Utils.ValidationEntityFields;

public static class Validation
{
    public static void ValidateRequiredStringsFields<T>(this T entity, params string[] fields)
        where T : class
    {
        foreach (var field in fields)
        {
            var property = typeof(T).GetProperty(field, BindingFlags.Public | BindingFlags.Instance);

            if (property is null)
                throw new ValidationException
                ($"Propriedade `{field}` não encontrada em {typeof(T).Name}");

            var value = property.GetValue(entity) as string;
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException
                ($"O campo `{field}` é obrigatório e não pode ser nulo, vazio ou espaço em branco.");
        }
    }
    public static void ValidateGuidField<T>(this T entity, Guid idValue) where T : class
    {
        if (idValue == Guid.Empty)
            throw new ValidationException
            ($"`{typeof(T).Name}.Id` não pode ser Guid.Empty");
    }
    public static void ValidateAuditField<T>(this T entity, DateTime dateTimeValue) where T : class
    {
        if (dateTimeValue == DateTime.MinValue)
            throw new ValidationException
            ($"`{typeof(T).Name}.CreatedAt` não pode ser DateTime.MinValue");
    }
    public static void ValidateDocumentFields<T>(this T entity, string number, string type) where T : class
    {
        if (string.IsNullOrWhiteSpace(number) ||
            string.IsNullOrWhiteSpace(type)
           )
            throw new ValidationException
            ($"`{typeof(T).Name}.Document` não pode ter numero nem tipo nulos ou vazios.");
    }
    public static void ValidateRequiredJsonField<T>(this T entity, JsonDocument? jsonDoc) where T : class
    {
        if (jsonDoc is null)
            throw new ValidationException
            ($"`{typeof(T).Name}.Settings` não pode ser nulo");

        if (jsonDoc.RootElement.ValueKind == JsonValueKind.Undefined ||
            jsonDoc.RootElement.ValueKind == JsonValueKind.Null
           )
            throw new ValidationException
            ($"`{typeof(T).Name}.Settings` não pode estar vazio ou indefinido");
    }
    public static void ValidateRequiredList<T, TItem>(this T entity, List<TItem> list, string fieldName) where T : class
    {
        if (list is null || !list.Any())
            throw new ValidationException
            (  $"`{typeof(T).Name}.{fieldName}` deve conter pelo menos um item");
    }
}
