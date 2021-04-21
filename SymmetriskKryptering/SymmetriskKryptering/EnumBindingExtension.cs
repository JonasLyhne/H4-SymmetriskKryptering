using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;

namespace SymmetriskKryptering
{
    public class EnumBindingExtension : MarkupExtension
    {
        public Type EnumType { get; private set; }
        public EnumBindingExtension(Type enumType)
        {
            if (enumType is null || !enumType.IsEnum)
            {
                throw new Exception("Enum must not be null, and must by of type enum");
            }
            EnumType = enumType;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);
        }
    }
}
