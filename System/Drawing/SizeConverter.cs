using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace iTextSharp.Drawing
{
	public class SizeConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			bool flag = sourceType == typeof(string);
			return flag || base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			bool flag = destinationType == typeof(string);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = destinationType == typeof(InstanceDescriptor);
				result = (flag2 || base.CanConvertTo(context, destinationType));
			}
			return result;
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			bool flag = culture == null;
			if (flag)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string text = value as string;
			bool flag2 = text == null;
			object result;
			if (flag2)
			{
				result = base.ConvertFrom(context, culture, value);
			}
			else
			{
				string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
				Int32Converter int32Converter = new Int32Converter();
				int[] array2 = new int[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (int)int32Converter.ConvertFromString(context, culture, array[i]);
				}
				bool flag3 = array.Length != 2;
				if (flag3)
				{
					throw new ArgumentException("Failed to parse Text(" + text + ") expected text in the format \"Width,Height.\"");
				}
				result = new Size(array2[0], array2[1]);
			}
			return result;
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			bool flag = culture == null;
			if (flag)
			{
				culture = CultureInfo.CurrentCulture;
			}
			bool flag2 = value is Size;
			object result;
			if (flag2)
			{
				Size size = (Size)value;
				bool flag3 = destinationType == typeof(string);
				if (flag3)
				{
					result = size.Width.ToString(culture) + culture.TextInfo.ListSeparator + " " + size.Height.ToString(culture);
					return result;
				}
				bool flag4 = destinationType == typeof(InstanceDescriptor);
				if (flag4)
				{
					ConstructorInfo constructor = typeof(Size).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int)
					});
					result = new InstanceDescriptor(constructor, new object[]
					{
						size.Width,
						size.Height
					});
					return result;
				}
			}
			result = base.ConvertTo(context, culture, value, destinationType);
			return result;
		}

		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			object obj = propertyValues["Width"];
			object obj2 = propertyValues["Height"];
			bool flag = obj == null || obj2 == null;
			if (flag)
			{
				throw new ArgumentException("propertyValues");
			}
			int width = (int)obj;
			int height = (int)obj2;
			return new Size(width, height);
		}

		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			bool flag = value is Size;
			PropertyDescriptorCollection properties;
			if (flag)
			{
				properties = TypeDescriptor.GetProperties(value, attributes);
			}
			else
			{
				properties = base.GetProperties(context, value, attributes);
			}
			return properties;
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
