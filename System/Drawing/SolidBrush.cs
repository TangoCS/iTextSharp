using System;

namespace iTextSharp.Drawing
{
	public sealed class SolidBrush : Brush
	{
		internal bool isModifiable = true;

		private Color color;

		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				if (this.isModifiable)
				{
					this.color = value;
					GDIPlus.CheckStatus(GDIPlus.GdipSetSolidFillColor(this.nativeObject, value.ToArgb()));
					return;
				}
				throw new ArgumentException(Locale.GetText("This SolidBrush object can't be modified."));
			}
		}

		internal SolidBrush(IntPtr ptr) : base(ptr)
		{
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetSolidFillColor(ptr, out num));
			this.color = Color.FromArgb(num);
		}

		public SolidBrush(Color color)
		{
			this.color = color;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateSolidFill(color.ToArgb(), out this.nativeObject));
		}

		public override object Clone()
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneBrush(this.nativeObject, out ptr));
			return new SolidBrush(ptr);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.isModifiable)
			{
				throw new ArgumentException(Locale.GetText("This SolidBrush object can't be modified."));
			}
			base.Dispose(disposing);
		}
	}
}
