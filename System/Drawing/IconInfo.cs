using System;

namespace iTextSharp.Drawing
{
	internal struct IconInfo
	{
		private int fIcon;

		public int xHotspot;

		public int yHotspot;

		public IntPtr hbmMask;

		public IntPtr hbmColor;

		public bool IsIcon
		{
			get
			{
				return this.fIcon == 1;
			}
			set
			{
				this.fIcon = (value ? 1 : 0);
			}
		}
	}
}
