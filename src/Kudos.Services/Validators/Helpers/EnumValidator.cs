using Kudos.Domain.Exceptions;
using System.ComponentModel;

namespace Kudos.Services.Validators.Helpers
{
    public static class EnumValidator
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                return attribute.Description;

            throw new KudoReasonEnumNotFoundException();
        }

        public static string GetEnumValueByDescription<T>(this string description) where T : Enum
        {
            foreach (Enum enumItem in Enum.GetValues(typeof(T)))
            {
                if (enumItem.GetEnumDescription() == description)
                    return enumItem.GetEnumDescription();
            }
            throw new KudoReasonDescriptionNotFoundException();
        }
    }
}
