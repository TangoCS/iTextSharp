using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace iTextSharp.Drawing
{
	public class ColorConverter : TypeConverter
	{
		private sealed class CompareColors : IComparer
		{
			public int Compare(object x, object y)
			{
				return string.Compare(((Color)x).Name, ((Color)y).Name);
			}
		}

		private static TypeConverter.StandardValuesCollection cached;

		private static object creatingCached = new object();

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			bool flag = sourceType == typeof(string);
			return flag || base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			bool flag = destinationType == typeof(InstanceDescriptor);
			return flag || base.CanConvertTo(context, destinationType);
		}

		internal static Color StaticConvertFromString(ITypeDescriptorContext context, string s, CultureInfo culture)
		{
			bool flag = culture == null;
			if (flag)
			{
				culture = CultureInfo.InvariantCulture;
			}
			s = s.Trim();
			bool flag2 = s.Length == 0;
			Color result;
			if (flag2)
			{
				result = Color.Empty;
			}
			else
			{
				bool flag3 = char.IsLetter(s[0]);
				if (flag3)
				{
					KnownColor kc;
					try
					{
						kc = (KnownColor)Enum.Parse(typeof(KnownColor), s, true);
					}
					catch (Exception innerException)
					{
						string text = Locale.GetText("Invalid color name '{0}'.", new object[]
						{
							s
						});
						throw new Exception(text, new FormatException(text, innerException));
					}
					result = KnownColors.FromKnownColor(kc);
				}
				else
				{
					string listSeparator = culture.TextInfo.ListSeparator;
					Color color = Color.Empty;
					bool flag4 = s.IndexOf(listSeparator) == -1;
					if (flag4)
					{
						bool flag5 = s[0] == '#';
						int num = flag5 ? 1 : 0;
						bool flag6 = false;
						bool flag7 = s.Length > num + 1 && s[num] == '0';
						if (flag7)
						{
							flag6 = (s[num + 1] == 'x' || s[num + 1] == 'X');
							bool flag8 = flag6;
							if (flag8)
							{
								num += 2;
							}
						}
						bool flag9 = flag5 | flag6;
						if (flag9)
						{
							s = s.Substring(num);
							int num2;
							try
							{
								num2 = int.Parse(s, NumberStyles.HexNumber);
							}
							catch (Exception innerException2)
							{
								string text2 = Locale.GetText("Invalid Int32 value '{0}'.", new object[]
								{
									s
								});
								throw new Exception(text2, innerException2);
							}
							bool flag10 = s.Length < 6 || (s.Length == 6 & flag5 & flag6);
							if (flag10)
							{
								num2 &= 16777215;
							}
							else
							{
								bool flag11 = num2 >> 24 == 0;
								if (flag11)
								{
									num2 |= -16777216;
								}
							}
							color = Color.FromArgb(num2);
						}
					}
					bool isEmpty = color.IsEmpty;
					if (isEmpty)
					{
						Int32Converter int32Converter = new Int32Converter();
						string[] array = s.Split(listSeparator.ToCharArray());
						int[] array2 = new int[array.Length];
						for (int i = 0; i < array2.Length; i++)
						{
							array2[i] = (int)int32Converter.ConvertFrom(context, culture, array[i]);
						}
						switch (array.Length)
						{
						case 1:
							color = Color.FromArgb(array2[0]);
							goto IL_294;
						case 3:
							color = Color.FromArgb(array2[0], array2[1], array2[2]);
							goto IL_294;
						case 4:
							color = Color.FromArgb(array2[0], array2[1], array2[2], array2[3]);
							goto IL_294;
						}
						throw new ArgumentException(s + " is not a valid color value.");
						IL_294:;
					}
					bool flag12 = !color.IsEmpty;
					if (flag12)
					{
						Color color2 = KnownColors.FindColorMatch(color);
						bool flag13 = !color2.IsEmpty;
						if (flag13)
						{
							result = color2;
							return result;
						}
					}
					result = color;
				}
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
				result = ColorConverter.StaticConvertFromString(context, text, culture);
			}
			return result;
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			bool flag = value is Color;
			object result;
			if (flag)
			{
				Color left = (Color)value;
				bool flag2 = destinationType == typeof(string);
				if (flag2)
				{
					bool flag3 = left == Color.Empty;
					if (flag3)
					{
						result = string.Empty;
						return result;
					}
					bool flag4 = left.IsKnownColor || left.IsNamedColor;
					if (flag4)
					{
						result = left.Name;
						return result;
					}
					string listSeparator = culture.TextInfo.ListSeparator;
					StringBuilder stringBuilder = new StringBuilder();
					bool flag5 = left.A != 255;
					if (flag5)
					{
						stringBuilder.Append(left.A);
						stringBuilder.Append(listSeparator);
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(left.R);
					stringBuilder.Append(listSeparator);
					stringBuilder.Append(" ");
					stringBuilder.Append(left.G);
					stringBuilder.Append(listSeparator);
					stringBuilder.Append(" ");
					stringBuilder.Append(left.B);
					result = stringBuilder.ToString();
					return result;
				}
				else
				{
					bool flag6 = destinationType == typeof(InstanceDescriptor);
					if (flag6)
					{
						bool isEmpty = left.IsEmpty;
						if (isEmpty)
						{
							result = new InstanceDescriptor(typeof(Color).GetField("Empty"), null);
							return result;
						}
						bool isSystemColor = left.IsSystemColor;
						if (isSystemColor)
						{
							result = new InstanceDescriptor(typeof(SystemColors).GetProperty(left.Name), null);
							return result;
						}
						bool isKnownColor = left.IsKnownColor;
						if (isKnownColor)
						{
							result = new InstanceDescriptor(typeof(Color).GetProperty(left.Name), null);
							return result;
						}
						MethodInfo method = typeof(Color).GetMethod("FromArgb", new Type[]
						{
							typeof(int),
							typeof(int),
							typeof(int),
							typeof(int)
						});
						result = new InstanceDescriptor(method, new object[]
						{
							left.A,
							left.R,
							left.G,
							left.B
						});
						return result;
					}
				}
			}
			result = base.ConvertTo(context, culture, value, destinationType);
			return result;
		}

		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			object obj = ColorConverter.creatingCached;
			TypeConverter.StandardValuesCollection result;
			lock (obj)
			{
				bool flag2 = ColorConverter.cached != null;
				if (flag2)
				{
					result = ColorConverter.cached;
					return result;
				}
				Array array = Array.CreateInstance(typeof(Color), KnownColors.ArgbValues.Length - 1);
				for (int i = 1; i < KnownColors.ArgbValues.Length; i++)
				{
					array.SetValue(KnownColors.FromKnownColor((KnownColor)i), i - 1);
				}
				Array.Sort(array, 0, array.Length, new ColorConverter.CompareColors());
				ColorConverter.cached = new TypeConverter.StandardValuesCollection(array);
			}
			result = ColorConverter.cached;
			return result;
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
