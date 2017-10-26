using System;

namespace iTextSharp.Drawing.Text
{
	public abstract class FontCollection : IDisposable
	{
		internal IntPtr nativeFontCollection = IntPtr.Zero;

		public FontFamily[] Families
		{
			get
			{
				int num = 0;
				if (this.nativeFontCollection == IntPtr.Zero)
				{
					throw new ArgumentException(Locale.GetText("Collection was disposed."));
				}
				int num2;
				GDIPlus.CheckStatus(GDIPlus.GdipGetFontCollectionFamilyCount(this.nativeFontCollection, out num2));
				if (num2 == 0)
				{
					return new FontFamily[0];
				}
				IntPtr[] array = new IntPtr[num2];
				GDIPlus.GdipGetFontCollectionFamilyList(this.nativeFontCollection, num2, array, out num);
				FontFamily[] array2 = new FontFamily[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = new FontFamily(array[i]);
				}
				return array2;
			}
		}

		internal FontCollection()
		{
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(true);
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		~FontCollection()
		{
			this.Dispose(false);
		}
	}
}
