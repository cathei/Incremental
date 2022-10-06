// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.ComponentModel;
using System.Globalization;

namespace Cathei.Mathematics
{
    /// <summary>
    /// Standard type converter that can be used with XAML, Json.NET, etc.
    /// </summary>
    public class IncrementalTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string str)
                return Incremental.Parse(str, culture);

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return typeof(string) == destinationType || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (typeof(string) == destinationType && value is Incremental incremental)
                return incremental.ToString(culture);

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}