using System;

namespace iTextSharp.Drawing.Drawing2D
{
	public sealed class AdjustableArrowCap : CustomLineCap
	{
		public bool Filled
		{
			get
			{
				bool result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapFillState(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapFillState(this.nativeObject, value));
			}
		}

		public float Width
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapWidth(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapWidth(this.nativeObject, value));
			}
		}

		public float Height
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapHeight(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapHeight(this.nativeObject, value));
			}
		}

		public float MiddleInset
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapMiddleInset(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapMiddleInset(this.nativeObject, value));
			}
		}

		internal AdjustableArrowCap(IntPtr ptr) : base(ptr)
		{
		}

		public AdjustableArrowCap(float width, float height) : this(width, height, true)
		{
		}

		public AdjustableArrowCap(float width, float height, bool isFilled)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateAdjustableArrowCap(height, width, isFilled, out this.nativeObject));
		}
	}
}
