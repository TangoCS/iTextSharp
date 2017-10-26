using System;
using System.ComponentModel;
using iTextSharp.Drawing.Design;
using iTextSharp.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace iTextSharp.Drawing
{
	[Editor("System.Drawing.Design.BitmapEditor, System.Drawing.Design", typeof(UITypeEditor)), ComVisible(true)]
	[Serializable]
	public sealed class Bitmap : Image
	{
		private Bitmap()
		{
		}

		internal Bitmap(IntPtr ptr)
		{
			this.nativeObject = ptr;
		}

		internal Bitmap(IntPtr ptr, Stream stream)
		{
			if (GDIPlus.RunningOnWindows())
			{
				this.stream = stream;
			}
			this.nativeObject = ptr;
		}

		public Bitmap(int width, int height) : this(width, height, PixelFormat.Format32bppArgb)
		{
		}

		public Bitmap(int width, int height, Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			IntPtr nativeObject;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromGraphics(width, height, g.nativeObject, out nativeObject));
			this.nativeObject = nativeObject;
		}

		public Bitmap(int width, int height, PixelFormat format)
		{
			IntPtr nativeObject;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromScan0(width, height, 0, format, IntPtr.Zero, out nativeObject));
			this.nativeObject = nativeObject;
		}

		public Bitmap(Image original) : this(original, original.Width, original.Height)
		{
		}

		public Bitmap(Stream stream) : this(stream, false)
		{
		}

		public Bitmap(string filename) : this(filename, false)
		{
		}

		public Bitmap(Image original, Size newSize) : this(original, newSize.Width, newSize.Height)
		{
		}

		public Bitmap(Stream stream, bool useIcm)
		{
			this.nativeObject = Image.InitFromStream(stream);
		}

		public Bitmap(string filename, bool useIcm)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			IntPtr nativeObject;
			Status status;
			if (useIcm)
			{
				status = GDIPlus.GdipCreateBitmapFromFileICM(filename, out nativeObject);
			}
			else
			{
				status = GDIPlus.GdipCreateBitmapFromFile(filename, out nativeObject);
			}
			GDIPlus.CheckStatus(status);
			this.nativeObject = nativeObject;
		}

		public Bitmap(Type type, string resource)
		{
			if (resource == null)
			{
				throw new ArgumentException("resource");
			}
			if (type == null)
			{
				throw new NullReferenceException();
			}
			Stream manifestResourceStream = type.Assembly.GetManifestResourceStream(type, resource);
			if (manifestResourceStream == null)
			{
				throw new FileNotFoundException(Locale.GetText("Resource '{0}' was not found.", new object[]
				{
					resource
				}));
			}
			this.nativeObject = Image.InitFromStream(manifestResourceStream);
			if (GDIPlus.RunningOnWindows())
			{
				this.stream = manifestResourceStream;
			}
		}

		public Bitmap(Image original, int width, int height) : this(width, height, PixelFormat.Format32bppArgb)
		{
			Graphics expr_13 = Graphics.FromImage(this);
			expr_13.DrawImage(original, 0, 0, width, height);
			expr_13.Dispose();
		}

		public Bitmap(int width, int height, int stride, PixelFormat format, IntPtr scan0)
		{
			IntPtr nativeObject;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromScan0(width, height, stride, format, scan0, out nativeObject));
			this.nativeObject = nativeObject;
		}

		private Bitmap(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public Color GetPixel(int x, int y)
		{
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapGetPixel(this.nativeObject, x, y, out num));
			return Color.FromArgb(num);
		}

		public void SetPixel(int x, int y, Color color)
		{
			Status expr_14 = GDIPlus.GdipBitmapSetPixel(this.nativeObject, x, y, color.ToArgb());
			if (expr_14 == Status.InvalidParameter && (base.PixelFormat & PixelFormat.Indexed) != PixelFormat.DontCare)
			{
				throw new InvalidOperationException(Locale.GetText("SetPixel cannot be called on indexed bitmaps."));
			}
			GDIPlus.CheckStatus(expr_14);
		}

		public Bitmap Clone(Rectangle rect, PixelFormat format)
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneBitmapAreaI(rect.X, rect.Y, rect.Width, rect.Height, format, this.nativeObject, out ptr));
			return new Bitmap(ptr);
		}

		public Bitmap Clone(RectangleF rect, PixelFormat format)
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneBitmapArea(rect.X, rect.Y, rect.Width, rect.Height, format, this.nativeObject, out ptr));
			return new Bitmap(ptr);
		}

		public static Bitmap FromHicon(IntPtr hicon)
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromHICON(hicon, out ptr));
			return new Bitmap(ptr);
		}

		public static Bitmap FromResource(IntPtr hinstance, string bitmapName)
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromResource(hinstance, bitmapName, out ptr));
			return new Bitmap(ptr);
		}

		[EditorBrowsable]
		public IntPtr GetHbitmap()
		{
			return this.GetHbitmap(Color.Gray);
		}

		[EditorBrowsable]
		public IntPtr GetHbitmap(Color background)
		{
			IntPtr result;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateHBITMAPFromBitmap(this.nativeObject, out result, background.ToArgb()));
			return result;
		}

		[EditorBrowsable]
		public IntPtr GetHicon()
		{
			IntPtr result;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateHICONFromBitmap(this.nativeObject, out result));
			return result;
		}

		public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
		{
			BitmapData bitmapData = new BitmapData();
			return this.LockBits(rect, flags, format, bitmapData);
		}

		public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format, BitmapData bitmapData)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapLockBits(this.nativeObject, ref rect, flags, format, bitmapData));
			return bitmapData;
		}

		public void MakeTransparent()
		{
			Color pixel = this.GetPixel(0, 0);
			this.MakeTransparent(pixel);
		}

		public void MakeTransparent(Color transparentColor)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height, PixelFormat.Format32bppArgb);
			Graphics arg_40_0 = Graphics.FromImage(bitmap);
			Rectangle destRect = new Rectangle(0, 0, base.Width, base.Height);
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorKey(transparentColor, transparentColor);
			arg_40_0.DrawImage(this, destRect, 0, 0, base.Width, base.Height, GraphicsUnit.Pixel, imageAttributes);
			IntPtr nativeObject = this.nativeObject;
			this.nativeObject = bitmap.nativeObject;
			bitmap.nativeObject = nativeObject;
			arg_40_0.Dispose();
			bitmap.Dispose();
			imageAttributes.Dispose();
		}

		public void SetResolution(float xDpi, float yDpi)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapSetResolution(this.nativeObject, xDpi, yDpi));
		}

		public void UnlockBits(BitmapData bitmapdata)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapUnlockBits(this.nativeObject, bitmapdata));
		}
	}
}
