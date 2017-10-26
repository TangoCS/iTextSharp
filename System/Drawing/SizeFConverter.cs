using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace iTextSharp.Drawing
{
	public class SizeFConverter : TypeConverter
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
			string text = value as string;
			bool flag = text == null;
			object result;
			if (flag)
			{
				result = base.ConvertFrom(context, culture, value);
			}
			else
			{
				bool flag2 = culture == null;
				if (flag2)
				{
					culture = CultureInfo.CurrentCulture;
				}
				string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
				SingleConverter singleConverter = new SingleConverter();
				float[] array2 = new float[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (float)singleConverter.ConvertFromString(context, culture, array[i]);
				}
				bool flag3 = array.Length != 2;
				if (flag3)
				{
					throw new ArgumentException("Failed to parse Text(" + text + ") expected text in the format \"Width,Height.\"");
				}
				result = new SizeF(array2[0], array2[1]);
			}
			return result;
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			bool flag = value is SizeF;
			object result;
			if (flag)
			{
				SizeF sizeF = (SizeF)value;
				bool flag2 = destinationType == typeof(string);
				if (flag2)
				{
					result = sizeF.Width.ToString(culture) + culture.TextInfo.ListSeparator + " " + sizeF.Height.ToString(culture);
					return result;
				}
				bool flag3 = destinationType == typeof(InstanceDescriptor);
				if (flag3)
				{
					ConstructorInfo constructor = typeof(SizeF).GetConstructor(new Type[]
					{
						typeof(float),
						typeof(float)
					});
					result = new InstanceDescriptor(constructor, new object[]
					{
						sizeF.Width,
						sizeF.Height
					});
					return result;
				}
			}
			result = base.ConvertTo(context, culture, value, destinationType);
			return result;
		}

		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			float width = (float)propertyValues["Width"];
			float height = (float)propertyValues["Height"];
			return new SizeF(width, height);
		}

		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			bool flag = value is SizeF;
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
