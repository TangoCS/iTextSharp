using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using iTextSharp.Drawing.Text;
using System.Globalization;
using System.Text;

namespace iTextSharp.Drawing
{
	public class FontConverter : TypeConverter
	{
		public sealed class FontNameConverter : TypeConverter, IDisposable
		{
			private FontFamily[] fonts;

			public FontNameConverter()
			{
				this.fonts = FontFamily.Families;
			}

			void IDisposable.Dispose()
			{
			}

			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					return value;
				}
				return base.ConvertFrom(context, culture, value);
			}

			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				string[] array = new string[this.fonts.Length];
				int i = this.fonts.Length;
				while (i > 0)
				{
					i--;
					array[i] = this.fonts[i].Name;
				}
				return new TypeConverter.StandardValuesCollection(array);
			}

			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}

		public class FontUnitConverter : EnumConverter
		{
			public FontUnitConverter() : base(typeof(GraphicsUnit))
			{
			}

			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				return base.GetStandardValues(context);
			}
		}

		~FontConverter()
		{
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is Font)
			{
				Font font = (Font)value;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(font.Name).Append(culture.TextInfo.ListSeparator[0].ToString() + " ");
				stringBuilder.Append(font.Size);
				switch (font.Unit)
				{
				case GraphicsUnit.World:
					stringBuilder.Append("world");
					break;
				case GraphicsUnit.Display:
					stringBuilder.Append("display");
					break;
				case GraphicsUnit.Pixel:
					stringBuilder.Append("px");
					break;
				case GraphicsUnit.Point:
					stringBuilder.Append("pt");
					break;
				case GraphicsUnit.Inch:
					stringBuilder.Append("in");
					break;
				case GraphicsUnit.Document:
					stringBuilder.Append("doc");
					break;
				case GraphicsUnit.Millimeter:
					stringBuilder.Append("mm");
					break;
				}
				if (font.Style != FontStyle.Regular)
				{
					stringBuilder.Append(culture.TextInfo.ListSeparator[0].ToString() + " style=").Append(font.Style);
				}
				return stringBuilder.ToString();
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Font)
			{
				Font font2 = (Font)value;
				return new InstanceDescriptor(typeof(Font).GetConstructor(new Type[]
				{
					typeof(string),
					typeof(float),
					typeof(FontStyle),
					typeof(GraphicsUnit)
				}), new object[]
				{
					font2.Name,
					font2.Size,
					font2.Style,
					font2.Unit
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			text = text.Trim();
			if (text.Length == 0)
			{
				return null;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string[] array = text.Split(new char[]
			{
				culture.TextInfo.ListSeparator[0]
			});
			if (array.Length < 1)
			{
				throw new ArgumentException("Failed to parse font format");
			}
			text = array[0];
			float emSize = 8f;
			string text2 = "px";
			GraphicsUnit unit = GraphicsUnit.Pixel;
			if (array.Length > 1)
			{
				for (int i = 0; i < array[1].Length; i++)
				{
					if (char.IsLetter(array[1][i]))
					{
						emSize = (float)TypeDescriptor.GetConverter(typeof(float)).ConvertFromString(context, culture, array[1].Substring(0, i));
						text2 = array[1].Substring(i);
						break;
					}
				}
				if (text2 == "display")
				{
					unit = GraphicsUnit.Display;
				}
				else if (text2 == "doc")
				{
					unit = GraphicsUnit.Document;
				}
				else if (text2 == "pt")
				{
					unit = GraphicsUnit.Point;
				}
				else if (text2 == "in")
				{
					unit = GraphicsUnit.Inch;
				}
				else if (text2 == "mm")
				{
					unit = GraphicsUnit.Millimeter;
				}
				else if (text2 == "px")
				{
					unit = GraphicsUnit.Pixel;
				}
				else if (text2 == "world")
				{
					unit = GraphicsUnit.World;
				}
			}
			FontStyle fontStyle = FontStyle.Regular;
			if (array.Length > 2)
			{
				for (int j = 2; j < array.Length; j++)
				{
					string expr_176 = array[j];
					if (expr_176.IndexOf("Regular") != -1)
					{
						fontStyle |= FontStyle.Regular;
					}
					if (expr_176.IndexOf("Bold") != -1)
					{
						fontStyle |= FontStyle.Bold;
					}
					if (expr_176.IndexOf("Italic") != -1)
					{
						fontStyle |= FontStyle.Italic;
					}
					if (expr_176.IndexOf("Strikeout") != -1)
					{
						fontStyle |= FontStyle.Strikeout;
					}
					if (expr_176.IndexOf("Underline") != -1)
					{
						fontStyle |= FontStyle.Underline;
					}
				}
			}
			return new Font(text, emSize, fontStyle, unit);
		}

		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			byte gdiCharSet = 1;
			float emSize = 8f;
			string text = null;
			bool gdiVerticalFont = false;
			FontStyle fontStyle = FontStyle.Regular;
			FontFamily fontFamily = null;
			GraphicsUnit unit = GraphicsUnit.Point;
			object obj;
			if ((obj = propertyValues["GdiCharSet"]) != null)
			{
				gdiCharSet = (byte)obj;
			}
			if ((obj = propertyValues["Size"]) != null)
			{
				emSize = (float)obj;
			}
			if ((obj = propertyValues["Unit"]) != null)
			{
				unit = (GraphicsUnit)obj;
			}
			if ((obj = propertyValues["Name"]) != null)
			{
				text = (string)obj;
			}
			if ((obj = propertyValues["GdiVerticalFont"]) != null)
			{
				gdiVerticalFont = (bool)obj;
			}
			if ((obj = propertyValues["Bold"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Bold;
			}
			if ((obj = propertyValues["Italic"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Italic;
			}
			if ((obj = propertyValues["Strikeout"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Strikeout;
			}
			if ((obj = propertyValues["Underline"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Underline;
			}
			if (text == null)
			{
				fontFamily = new FontFamily("Tahoma");
			}
			else
			{
				text = text.ToLower();
				FontFamily[] families = new InstalledFontCollection().Families;
				for (int i = 0; i < families.Length; i++)
				{
					FontFamily fontFamily2 = families[i];
					if (text == fontFamily2.Name.ToLower())
					{
						fontFamily = fontFamily2;
						break;
					}
				}
				if (fontFamily == null)
				{
					families = new PrivateFontCollection().Families;
					for (int i = 0; i < families.Length; i++)
					{
						FontFamily fontFamily3 = families[i];
						if (text == fontFamily3.Name.ToLower())
						{
							fontFamily = fontFamily3;
							break;
						}
					}
				}
				if (fontFamily == null)
				{
					fontFamily = FontFamily.GenericSansSerif;
				}
			}
			return new Font(fontFamily, emSize, fontStyle, unit, gdiCharSet, gdiVerticalFont);
		}

		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (value is Font)
			{
				return TypeDescriptor.GetProperties(value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
