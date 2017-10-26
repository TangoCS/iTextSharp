using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace iTextSharp.Drawing
{
	public class RectangleConverter : TypeConverter
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
				Int32Converter int32Converter = new Int32Converter();
				int[] array2 = new int[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (int)int32Converter.ConvertFromString(context, culture, array[i]);
				}
				bool flag3 = array.Length != 4;
				if (flag3)
				{
					throw new ArgumentException("Failed to parse Text(" + text + ") expected text in the format \"x,y,Width,Height.\"");
				}
				result = new Rectangle(array2[0], array2[1], array2[2], array2[3]);
			}
			return result;
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			bool flag = value is Rectangle;
			object result;
			if (flag)
			{
				Rectangle rectangle = (Rectangle)value;
				bool flag2 = destinationType == typeof(string);
				if (flag2)
				{
					string listSeparator = culture.TextInfo.ListSeparator;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(rectangle.X.ToString(culture));
					stringBuilder.Append(listSeparator);
					stringBuilder.Append(" ");
					stringBuilder.Append(rectangle.Y.ToString(culture));
					stringBuilder.Append(listSeparator);
					stringBuilder.Append(" ");
					stringBuilder.Append(rectangle.Width.ToString(culture));
					stringBuilder.Append(listSeparator);
					stringBuilder.Append(" ");
					stringBuilder.Append(rectangle.Height.ToString(culture));
					result = stringBuilder.ToString();
					return result;
				}
				bool flag3 = destinationType == typeof(InstanceDescriptor);
				if (flag3)
				{
					ConstructorInfo constructor = typeof(Rectangle).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int)
					});
					result = new InstanceDescriptor(constructor, new object[]
					{
						rectangle.X,
						rectangle.Y,
						rectangle.Width,
						rectangle.Height
					});
					return result;
				}
			}
			result = base.ConvertTo(context, culture, value, destinationType);
			return result;
		}

		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			object obj = propertyValues["X"];
			object obj2 = propertyValues["Y"];
			object obj3 = propertyValues["Width"];
			object obj4 = propertyValues["Height"];
			bool flag = obj == null || obj2 == null || obj3 == null || obj4 == null;
			if (flag)
			{
				throw new ArgumentException("propertyValues");
			}
			int x = (int)obj;
			int y = (int)obj2;
			int width = (int)obj3;
			int height = (int)obj4;
			return new Rectangle(x, y, width, height);
		}

		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			bool flag = value is Rectangle;
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
