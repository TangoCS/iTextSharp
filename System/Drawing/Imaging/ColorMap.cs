using System;

namespace iTextSharp.Drawing.Imaging
{
	public sealed class ColorMap
	{
		private Color newColor;

		private Color oldColor;

		public Color NewColor
		{
			get
			{
				return this.newColor;
			}
			set
			{
				this.newColor = value;
			}
		}

		public Color OldColor
		{
			get
			{
				return this.oldColor;
			}
			set
			{
				this.oldColor = value;
			}
		}
	}
}
