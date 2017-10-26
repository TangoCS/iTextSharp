using System;
using System.ComponentModel;
using iTextSharp.Drawing.Text;

namespace iTextSharp.Drawing
{
	public sealed class StringFormat : MarshalByRefObject, IDisposable, ICloneable
	{
		private IntPtr nativeStrFmt;

		private int language;

		public StringAlignment Alignment
		{
			get
			{
				StringAlignment result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatAlign(this.nativeStrFmt, out result));
				return result;
			}
			set
			{
				if (value < StringAlignment.Near || value > StringAlignment.Far)
				{
					throw new InvalidEnumArgumentException("Alignment");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatAlign(this.nativeStrFmt, value));
			}
		}

		public StringAlignment LineAlignment
		{
			get
			{
				StringAlignment result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatLineAlign(this.nativeStrFmt, out result));
				return result;
			}
			set
			{
				if (value < StringAlignment.Near || value > StringAlignment.Far)
				{
					throw new InvalidEnumArgumentException("Alignment");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatLineAlign(this.nativeStrFmt, value));
			}
		}

		public StringFormatFlags FormatFlags
		{
			get
			{
				StringFormatFlags result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatFlags(this.nativeStrFmt, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatFlags(this.nativeStrFmt, value));
			}
		}

		public HotkeyPrefix HotkeyPrefix
		{
			get
			{
				HotkeyPrefix result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatHotkeyPrefix(this.nativeStrFmt, out result));
				return result;
			}
			set
			{
				if (value < HotkeyPrefix.None || value > HotkeyPrefix.Hide)
				{
					throw new InvalidEnumArgumentException("HotkeyPrefix");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatHotkeyPrefix(this.nativeStrFmt, value));
			}
		}

		public StringTrimming Trimming
		{
			get
			{
				StringTrimming result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatTrimming(this.nativeStrFmt, out result));
				return result;
			}
			set
			{
				if (value < StringTrimming.None || value > StringTrimming.EllipsisPath)
				{
					throw new InvalidEnumArgumentException("Trimming");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatTrimming(this.nativeStrFmt, value));
			}
		}

		public static StringFormat GenericDefault
		{
			get
			{
				IntPtr native;
				GDIPlus.CheckStatus(GDIPlus.GdipStringFormatGetGenericDefault(out native));
				return new StringFormat(native);
			}
		}

		public int DigitSubstitutionLanguage
		{
			get
			{
				return this.language;
			}
		}

		public static StringFormat GenericTypographic
		{
			get
			{
				IntPtr native;
				GDIPlus.CheckStatus(GDIPlus.GdipStringFormatGetGenericTypographic(out native));
				return new StringFormat(native);
			}
		}

		public StringDigitSubstitute DigitSubstitutionMethod
		{
			get
			{
				StringDigitSubstitute result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatDigitSubstitution(this.nativeStrFmt, this.language, out result));
				return result;
			}
		}

		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeStrFmt;
			}
			set
			{
				this.nativeStrFmt = value;
			}
		}

		public StringFormat() : this((StringFormatFlags)0, 0)
		{
		}

		public StringFormat(StringFormatFlags options, int language)
		{
			this.nativeStrFmt = IntPtr.Zero;
			//base..ctor();
			GDIPlus.CheckStatus(GDIPlus.GdipCreateStringFormat(options, language, out this.nativeStrFmt));
		}

		internal StringFormat(IntPtr native)
		{
			this.nativeStrFmt = IntPtr.Zero;
            //base..ctor();
            this.nativeStrFmt = native;
		}

		~StringFormat()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (this.nativeStrFmt != IntPtr.Zero)
			{
				Status arg_28_0 = GDIPlus.GdipDeleteStringFormat(this.nativeStrFmt);
				this.nativeStrFmt = IntPtr.Zero;
				GDIPlus.CheckStatus(arg_28_0);
			}
		}

		public StringFormat(StringFormat format)
		{
			this.nativeStrFmt = IntPtr.Zero;
			//base..ctor();
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCloneStringFormat(format.NativeObject, out this.nativeStrFmt));
		}

		public StringFormat(StringFormatFlags options)
		{
			this.nativeStrFmt = IntPtr.Zero;
			//base..ctor();
			GDIPlus.CheckStatus(GDIPlus.GdipCreateStringFormat(options, 0, out this.nativeStrFmt));
		}

		public void SetMeasurableCharacterRanges(CharacterRange[] ranges)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatMeasurableCharacterRanges(this.nativeStrFmt, ranges.Length, ranges));
		}

		internal int GetMeasurableCharacterRangeCount()
		{
			int result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatMeasurableCharacterRangeCount(this.nativeStrFmt, out result));
			return result;
		}

		public object Clone()
		{
			IntPtr native;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneStringFormat(this.nativeStrFmt, out native));
			return new StringFormat(native);
		}

		public override string ToString()
		{
			return "[StringFormat, FormatFlags=" + this.FormatFlags.ToString() + "]";
		}

		public void SetTabStops(float firstTabOffset, float[] tabStops)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatTabStops(this.nativeStrFmt, firstTabOffset, tabStops.Length, tabStops));
		}

		public void SetDigitSubstitution(int language, StringDigitSubstitute substitute)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatDigitSubstitution(this.nativeStrFmt, this.language, substitute));
		}

		public float[] GetTabStops(out float firstTabOffset)
		{
			int num = 0;
			firstTabOffset = 0f;
			GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatTabStopCount(this.nativeStrFmt, out num));
			float[] array = new float[num];
			if (num != 0)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatTabStops(this.nativeStrFmt, num, out firstTabOffset, array));
			}
			return array;
		}
	}
}
