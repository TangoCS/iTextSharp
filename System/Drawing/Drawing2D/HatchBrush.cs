using System;

namespace iTextSharp.Drawing.Drawing2D
{
	public sealed class HatchBrush : Brush
	{
		public Color BackgroundColor
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetHatchBackgroundColor(this.nativeObject, out num));
				return Color.FromArgb(num);
			}
		}

		public Color ForegroundColor
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetHatchForegroundColor(this.nativeObject, out num));
				return Color.FromArgb(num);
			}
		}

		public HatchStyle HatchStyle
		{
			get
			{
				HatchStyle result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetHatchStyle(this.nativeObject, out result));
				return result;
			}
		}

		internal HatchBrush(IntPtr ptr) : base(ptr)
		{
		}

		public HatchBrush(HatchStyle hatchstyle, Color foreColor) : this(hatchstyle, foreColor, Color.Black)
		{
		}

		public HatchBrush(HatchStyle hatchstyle, Color foreColor, Color backColor)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateHatchBrush(hatchstyle, foreColor.ToArgb(), backColor.ToArgb(), out this.nativeObject));
		}

		public override object Clone()
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneBrush(this.nativeObject, out ptr));
			return new HatchBrush(ptr);
		}
	}
}
