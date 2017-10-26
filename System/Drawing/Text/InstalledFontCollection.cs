using System;

namespace iTextSharp.Drawing.Text
{
	public sealed class InstalledFontCollection : FontCollection
	{
		public InstalledFontCollection()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipNewInstalledFontCollection(out this.nativeFontCollection));
		}
	}
}
