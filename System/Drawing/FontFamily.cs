using System;
using iTextSharp.Drawing.Text;
using System.Text;

namespace iTextSharp.Drawing
{
	public sealed class FontFamily : MarshalByRefObject, IDisposable
	{
		private string name;

		private IntPtr nativeFontFamily = IntPtr.Zero;

		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeFontFamily;
			}
		}

		public string Name
		{
			get
			{
				if (this.nativeFontFamily == IntPtr.Zero)
				{
					throw new ArgumentException("Name", Locale.GetText("Object was disposed."));
				}
				if (this.name == null)
				{
					this.refreshName();
				}
				return this.name;
			}
		}

		public static FontFamily GenericMonospace
		{
			get
			{
				return new FontFamily(GenericFontFamilies.Monospace);
			}
		}

		public static FontFamily GenericSansSerif
		{
			get
			{
				return new FontFamily(GenericFontFamilies.SansSerif);
			}
		}

		public static FontFamily GenericSerif
		{
			get
			{
				return new FontFamily(GenericFontFamilies.Serif);
			}
		}

		public static FontFamily[] Families
		{
			get
			{
				return new InstalledFontCollection().Families;
			}
		}

		internal FontFamily(IntPtr fntfamily)
		{
			this.nativeFontFamily = fntfamily;
		}

		internal void refreshName()
		{
			if (this.nativeFontFamily == IntPtr.Zero)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(32);
			GDIPlus.CheckStatus(GDIPlus.GdipGetFamilyName(this.nativeFontFamily, stringBuilder, 0));
			this.name = stringBuilder.ToString();
		}

		~FontFamily()
		{
			this.Dispose();
		}

		public FontFamily(GenericFontFamilies genericFamily)
		{
			Status status;
			switch (genericFamily)
			{
			case GenericFontFamilies.Serif:
				status = GDIPlus.GdipGetGenericFontFamilySerif(out this.nativeFontFamily);
				goto IL_4D;
			case GenericFontFamilies.SansSerif:
				status = GDIPlus.GdipGetGenericFontFamilySansSerif(out this.nativeFontFamily);
				goto IL_4D;
			}
			status = GDIPlus.GdipGetGenericFontFamilyMonospace(out this.nativeFontFamily);
			IL_4D:
			GDIPlus.CheckStatus(status);
		}

		public FontFamily(string name) : this(name, null)
		{
		}

		public FontFamily(string name, FontCollection fontCollection)
		{
			IntPtr collection = (fontCollection == null) ? IntPtr.Zero : fontCollection.nativeFontCollection;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFontFamilyFromName(name, collection, out this.nativeFontFamily));
		}

		public int GetCellAscent(FontStyle style)
		{
			short result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetCellAscent(this.nativeFontFamily, (int)style, out result));
			return (int)result;
		}

		public int GetCellDescent(FontStyle style)
		{
			short result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetCellDescent(this.nativeFontFamily, (int)style, out result));
			return (int)result;
		}

		public int GetEmHeight(FontStyle style)
		{
			short result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetEmHeight(this.nativeFontFamily, (int)style, out result));
			return (int)result;
		}

		public int GetLineSpacing(FontStyle style)
		{
			short result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetLineSpacing(this.nativeFontFamily, (int)style, out result));
			return (int)result;
		}

		[MonoDocumentationNote("When used with libgdiplus this method always return true (styles are created on demand).")]
		public bool IsStyleAvailable(FontStyle style)
		{
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsStyleAvailable(this.nativeFontFamily, (int)style, out result));
			return result;
		}

		public void Dispose()
		{
			if (this.nativeFontFamily != IntPtr.Zero)
			{
				Status arg_2E_0 = GDIPlus.GdipDeleteFontFamily(this.nativeFontFamily);
				this.nativeFontFamily = IntPtr.Zero;
				GC.SuppressFinalize(this);
				GDIPlus.CheckStatus(arg_2E_0);
			}
		}

		public override bool Equals(object obj)
		{
			FontFamily fontFamily = obj as FontFamily;
			return fontFamily != null && this.Name == fontFamily.Name;
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		public static FontFamily[] GetFamilies(Graphics graphics)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			return new InstalledFontCollection().Families;
		}

		[MonoLimitation("The language parameter is ignored. We always return the name using the default system language.")]
		public string GetName(int language)
		{
			return this.Name;
		}

		public override string ToString()
		{
			return "[FontFamily: Name=" + this.Name + "]";
		}
	}
}
