using System;
using System.IO;

namespace iTextSharp.Drawing.Text
{
	public sealed class PrivateFontCollection : FontCollection
	{
		public PrivateFontCollection()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipNewPrivateFontCollection(out this.nativeFontCollection));
		}

		public void AddFontFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			string fullPath = Path.GetFullPath(filename);
			if (!File.Exists(fullPath))
			{
				throw new FileNotFoundException();
			}
			GDIPlus.CheckStatus(GDIPlus.GdipPrivateAddFontFile(this.nativeFontCollection, fullPath));
		}

		public void AddMemoryFont(IntPtr memory, int length)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipPrivateAddMemoryFont(this.nativeFontCollection, memory, length));
		}

		protected override void Dispose(bool disposing)
		{
			if (this.nativeFontCollection != IntPtr.Zero)
			{
				GDIPlus.GdipDeletePrivateFontCollection(ref this.nativeFontCollection);
				this.nativeFontCollection = IntPtr.Zero;
			}
			base.Dispose(disposing);
		}
	}
}
