using System.ComponentModel;
using System.Reflection;

namespace Order.Common.Extensions;

public static class EnumExtensions
{
    public static string GetEnumDescription(this System.Enum value)
    {
        var description = value.GetAttributeFieldValue<DescriptionAttribute>(d => d.Description);
        return description ?? value.ToString();
    }
    
    private static string? GetAttributeFieldValue<TAttribute>(
        this System.Enum value,
        Func<TAttribute, string> fieldSelector)
        where TAttribute : Attribute
    {
        var enumType = value.GetType();
        var enumValueName = value.ToString();
        var fieldInfo = enumType.GetField(enumValueName);

        if (fieldInfo == null) return null;

        var attribute = fieldInfo.GetCustomAttribute(typeof(TAttribute), false);

        return attribute == null ? null : fieldSelector((TAttribute)attribute);
    }
}