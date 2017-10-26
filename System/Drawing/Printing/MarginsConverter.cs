using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Text.RegularExpressions;

namespace iTextSharp.Drawing.Printing
{
	public class MarginsConverter : ExpandableObjectConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (value == null)
			{
				return new Margins();
			}
			string text = "( |\\t)*";
			text = text + ";" + text;
			Match match = new Regex(string.Concat(new string[]
			{
				"(?<left>\\d+)",
				text,
				"(?<right>\\d+)",
				text,
				"(?<top>\\d+)",
				text,
				"(?<bottom>\\d+)"
			})).Match(value as string);
			if (!match.Success)
			{
				throw new ArgumentException("value");
			}
			int left;
			int right;
			int top;
			int bottom;
			try
			{
				left = int.Parse(match.Groups["left"].Value);
				right = int.Parse(match.Groups["right"].Value);
				top = int.Parse(match.Groups["top"].Value);
				bottom = int.Parse(match.Groups["bottom"].Value);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("value", ex);
			}
			return new Margins(left, right, top, bottom);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is Margins)
			{
				Margins margins = value as Margins;
				return string.Format("{0}; {1}; {2}; {3}", new object[]
				{
					margins.Left,
					margins.Right,
					margins.Top,
					margins.Bottom
				});
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Margins)
			{
				Margins margins2 = (Margins)value;
				return new InstanceDescriptor(typeof(Margins).GetConstructor(new Type[]
				{
					typeof(int),
					typeof(int),
					typeof(int),
					typeof(int)
				}), new object[]
				{
					margins2.Left,
					margins2.Right,
					margins2.Top,
					margins2.Bottom
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			object result;
			try
			{
				result = new Margins
				{
					Left = int.Parse(propertyValues["Left"].ToString()),
					Right = int.Parse(propertyValues["Right"].ToString()),
					Top = int.Parse(propertyValues["Top"].ToString()),
					Bottom = int.Parse(propertyValues["Bottom"].ToString())
				};
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
	}
}
