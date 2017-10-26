using System;
using System.ComponentModel;
using iTextSharp.Drawing.Design;
using iTextSharp.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace iTextSharp.Drawing
{
	[Editor("System.Drawing.Design.ImageEditor, System.Drawing.Design", typeof(UITypeEditor)), ImmutableObject(true), TypeConverter(typeof(ImageConverter)), ComVisible(true)]
	[Serializable]
	public abstract class Image : MarshalByRefObject, IDisposable, ICloneable, ISerializable
	{
		public delegate bool GetThumbnailImageAbort();

		private object tag;

		internal IntPtr nativeObject = IntPtr.Zero;

		internal Stream stream;

		[Browsable(false)]
		public int Flags
		{
			get
			{
				int result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageFlags(this.nativeObject, out result));
				return result;
			}
		}

		[Browsable(false)]
		public Guid[] FrameDimensionsList
		{
			get
			{
				uint num;
				GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameDimensionsCount(this.nativeObject, out num));
				Guid[] array = new Guid[num];
				GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameDimensionsList(this.nativeObject, array, num));
				return array;
			}
		}

		[Browsable(false), DefaultValue(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Height
		{
			get
			{
				uint result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageHeight(this.nativeObject, out result));
				return (int)result;
			}
		}

		public float HorizontalResolution
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageHorizontalResolution(this.nativeObject, out result));
				return result;
			}
		}

		[Browsable(false)]
		public ColorPalette Palette
		{
			get
			{
				return this.retrieveGDIPalette();
			}
			set
			{
				this.storeGDIPalette(value);
			}
		}

		public SizeF PhysicalDimension
		{
			get
			{
				float num;
				float num2;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageDimension(this.nativeObject, out num, out num2));
				return new SizeF(num, num2);
			}
		}

		public PixelFormat PixelFormat
		{
			get
			{
				PixelFormat result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImagePixelFormat(this.nativeObject, out result));
				return result;
			}
		}

		[Browsable(false)]
		public int[] PropertyIdList
		{
			get
			{
				uint num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyCount(this.nativeObject, out num));
				int[] array = new int[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyIdList(this.nativeObject, num, array));
				return array;
			}
		}

		[Browsable(false)]
		public PropertyItem[] PropertyItems
		{
			get
			{
				GdipPropertyItem gdipPropertyItem = default(GdipPropertyItem);
				int num;
				int num2;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertySize(this.nativeObject, out num, out num2));
				PropertyItem[] array = new PropertyItem[num2];
				if (num2 == 0)
				{
					return array;
				}
				IntPtr intPtr = Marshal.AllocHGlobal(num * num2);
				try
				{
					GDIPlus.CheckStatus(GDIPlus.GdipGetAllPropertyItems(this.nativeObject, num, num2, intPtr));
					int num3 = Marshal.SizeOf(gdipPropertyItem);
					IntPtr intPtr2 = intPtr;
					int i = 0;
					while (i < num2)
					{
						gdipPropertyItem = (GdipPropertyItem)Marshal.PtrToStructure(intPtr2, typeof(GdipPropertyItem));
						array[i] = new PropertyItem();
						GdipPropertyItem.MarshalTo(gdipPropertyItem, array[i]);
						i++;
						intPtr2 = new IntPtr(intPtr2.ToInt64() + (long)num3);
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return array;
			}
		}

		public ImageFormat RawFormat
		{
			get
			{
				Guid guid;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageRawFormat(this.nativeObject, out guid));
				return new ImageFormat(guid);
			}
		}

		public Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
		}

		[Bindable(true), DefaultValue(null), Localizable(false), TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		public float VerticalResolution
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageVerticalResolution(this.nativeObject, out result));
				return result;
			}
		}

		[Browsable(false), DefaultValue(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Width
		{
			get
			{
				uint result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageWidth(this.nativeObject, out result));
				return (int)result;
			}
		}

		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeObject;
			}
			set
			{
				this.nativeObject = value;
			}
		}

		internal Image()
		{
		}

		internal Image(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				SerializationEntry current = enumerator.Current;
				if (string.Compare(current.Name, "Data", true) == 0)
				{
					byte[] array = (byte[])current.Value;
					if (array != null)
					{
						MemoryStream memoryStream = new MemoryStream(array);
						this.nativeObject = Image.InitFromStream(memoryStream);
						if (GDIPlus.RunningOnWindows())
						{
							this.stream = memoryStream;
						}
					}
				}
			}
		}

		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (this.RawFormat.Equals(ImageFormat.Icon))
				{
					this.Save(memoryStream, ImageFormat.Png);
				}
				else
				{
					this.Save(memoryStream, this.RawFormat);
				}
				si.AddValue("Data", memoryStream.ToArray());
			}
		}

		public static Image FromFile(string filename)
		{
			return Image.FromFile(filename, false);
		}

		public static Image FromFile(string filename, bool useEmbeddedColorManagement)
		{
			if (!File.Exists(filename))
			{
				throw new FileNotFoundException(filename);
			}
			IntPtr handle;
			Status status;
			if (useEmbeddedColorManagement)
			{
				status = GDIPlus.GdipLoadImageFromFileICM(filename, out handle);
			}
			else
			{
				status = GDIPlus.GdipLoadImageFromFile(filename, out handle);
			}
			GDIPlus.CheckStatus(status);
			return Image.CreateFromHandle(handle);
		}

		public static Bitmap FromHbitmap(IntPtr hbitmap)
		{
			return Image.FromHbitmap(hbitmap, IntPtr.Zero);
		}

		public static Bitmap FromHbitmap(IntPtr hbitmap, IntPtr hpalette)
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromHBITMAP(hbitmap, hpalette, out ptr));
			return new Bitmap(ptr);
		}

		public static Image FromStream(Stream stream)
		{
			return Image.LoadFromStream(stream, false);
		}

		[MonoLimitation("useEmbeddedColorManagement  isn't supported.")]
		public static Image FromStream(Stream stream, bool useEmbeddedColorManagement)
		{
			return Image.LoadFromStream(stream, false);
		}

		[MonoLimitation("useEmbeddedColorManagement  and validateImageData aren't supported.")]
		public static Image FromStream(Stream stream, bool useEmbeddedColorManagement, bool validateImageData)
		{
			return Image.LoadFromStream(stream, false);
		}

		internal static Image LoadFromStream(Stream stream, bool keepAlive)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			Image image = Image.CreateFromHandle(Image.InitFromStream(stream));
			if (keepAlive && GDIPlus.RunningOnWindows())
			{
				image.stream = stream;
			}
			return image;
		}

		internal static Image CreateFromHandle(IntPtr handle)
		{
			ImageType imageType;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImageType(handle, out imageType));
			if (imageType == ImageType.Bitmap)
			{
				return new Bitmap(handle);
			}
			if (imageType != ImageType.Metafile)
			{
				throw new NotSupportedException(Locale.GetText("Unknown image type."));
			}
			return new Metafile(handle);
		}

		public static int GetPixelFormatSize(PixelFormat pixfmt)
		{
			int result = 0;
			if (pixfmt <= PixelFormat.Format8bppIndexed)
			{
				if (pixfmt <= PixelFormat.Format32bppRgb)
				{
					if (pixfmt - PixelFormat.Format16bppRgb555 > 1)
					{
						if (pixfmt == PixelFormat.Format24bppRgb)
						{
							result = 24;
							return result;
						}
						if (pixfmt != PixelFormat.Format32bppRgb)
						{
							return result;
						}
						goto IL_A7;
					}
				}
				else
				{
					if (pixfmt == PixelFormat.Format1bppIndexed)
					{
						result = 1;
						return result;
					}
					if (pixfmt == PixelFormat.Format4bppIndexed)
					{
						result = 4;
						return result;
					}
					if (pixfmt != PixelFormat.Format8bppIndexed)
					{
						return result;
					}
					result = 8;
					return result;
				}
			}
			else
			{
				if (pixfmt > PixelFormat.Format16bppGrayScale)
				{
					if (pixfmt <= PixelFormat.Format64bppPArgb)
					{
						if (pixfmt == PixelFormat.Format48bppRgb)
						{
							result = 48;
							return result;
						}
						if (pixfmt != PixelFormat.Format64bppPArgb)
						{
							return result;
						}
					}
					else
					{
						if (pixfmt == PixelFormat.Format32bppArgb)
						{
							goto IL_A7;
						}
						if (pixfmt != PixelFormat.Format64bppArgb)
						{
							return result;
						}
					}
					result = 64;
					return result;
				}
				if (pixfmt != PixelFormat.Format16bppArgb1555)
				{
					if (pixfmt == PixelFormat.Format32bppPArgb)
					{
						goto IL_A7;
					}
					if (pixfmt != PixelFormat.Format16bppGrayScale)
					{
						return result;
					}
				}
			}
			result = 16;
			return result;
			IL_A7:
			result = 32;
			return result;
		}

		public static bool IsAlphaPixelFormat(PixelFormat pixfmt)
		{
			bool result = false;
			if (pixfmt > PixelFormat.Format8bppIndexed)
			{
				if (pixfmt <= PixelFormat.Format16bppGrayScale)
				{
					if (pixfmt != PixelFormat.Format16bppArgb1555 && pixfmt != PixelFormat.Format32bppPArgb)
					{
						if (pixfmt != PixelFormat.Format16bppGrayScale)
						{
							return result;
						}
						goto IL_98;
					}
				}
				else if (pixfmt <= PixelFormat.Format64bppPArgb)
				{
					if (pixfmt == PixelFormat.Format48bppRgb)
					{
						goto IL_98;
					}
					if (pixfmt != PixelFormat.Format64bppPArgb)
					{
						return result;
					}
				}
				else if (pixfmt != PixelFormat.Format32bppArgb && pixfmt != PixelFormat.Format64bppArgb)
				{
					return result;
				}
				result = true;
				return result;
			}
			if (pixfmt <= PixelFormat.Format32bppRgb)
			{
				if (pixfmt - PixelFormat.Format16bppRgb555 > 1 && pixfmt != PixelFormat.Format24bppRgb && pixfmt != PixelFormat.Format32bppRgb)
				{
					return result;
				}
			}
			else if (pixfmt != PixelFormat.Format1bppIndexed && pixfmt != PixelFormat.Format4bppIndexed && pixfmt != PixelFormat.Format8bppIndexed)
			{
				return result;
			}
			IL_98:
			result = false;
			return result;
		}

		public static bool IsCanonicalPixelFormat(PixelFormat pixfmt)
		{
			return (pixfmt & PixelFormat.Canonical) > PixelFormat.DontCare;
		}

		public static bool IsExtendedPixelFormat(PixelFormat pixfmt)
		{
			return (pixfmt & PixelFormat.Extended) > PixelFormat.DontCare;
		}

		internal static IntPtr InitFromStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentException("stream");
			}
			if (!stream.CanSeek)
			{
				byte[] array = new byte[256];
				int num = 0;
				int num2;
				do
				{
					if (array.Length < num + 256)
					{
						byte[] array2 = new byte[array.Length * 2];
						Array.Copy(array, array2, array.Length);
						array = array2;
					}
					num2 = stream.Read(array, num, 256);
					num += num2;
				}
				while (num2 != 0);
				stream = new MemoryStream(array, 0, num);
			}
			IntPtr result;
			Status status;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, true);
				status = GDIPlus.GdipLoadImageFromDelegate_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, out result);
			}
			else
			{
				status = GDIPlus.GdipLoadImageFromStream(new ComIStreamWrapper(stream), out result);
			}
			if (status != Status.Ok)
			{
				return IntPtr.Zero;
			}
			return result;
		}

		public RectangleF GetBounds(ref GraphicsUnit pageUnit)
		{
			RectangleF result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImageBounds(this.nativeObject, out result, ref pageUnit));
			return result;
		}

		public EncoderParameters GetEncoderParameterList(Guid encoder)
		{
			uint num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetEncoderParameterListSize(this.nativeObject, ref encoder, out num));
			IntPtr intPtr = Marshal.AllocHGlobal((int)num);
			EncoderParameters result;
			try
			{
				Status arg_31_0 = GDIPlus.GdipGetEncoderParameterList(this.nativeObject, ref encoder, num, intPtr);
				result = EncoderParameters.FromNativePtr(intPtr);
				GDIPlus.CheckStatus(arg_31_0);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		public int GetFrameCount(FrameDimension dimension)
		{
			Guid guid = dimension.Guid;
			uint result;
			GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameCount(this.nativeObject, ref guid, out result));
			return (int)result;
		}

		public PropertyItem GetPropertyItem(int propid)
		{
			PropertyItem propertyItem = new PropertyItem();
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyItemSize(this.nativeObject, propid, out num));
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyItem(this.nativeObject, propid, num, intPtr));
				GdipPropertyItem.MarshalTo((GdipPropertyItem)Marshal.PtrToStructure(intPtr, typeof(GdipPropertyItem)), propertyItem);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return propertyItem;
		}

		public Image GetThumbnailImage(int thumbWidth, int thumbHeight, Image.GetThumbnailImageAbort callback, IntPtr callbackData)
		{
			if (thumbWidth <= 0 || thumbHeight <= 0)
			{
				throw new OutOfMemoryException("Invalid thumbnail size");
			}
			Image image = new Bitmap(thumbWidth, thumbHeight);
			using (Graphics graphics = Graphics.FromImage(image))
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(graphics.nativeObject, this.nativeObject, 0, 0, thumbWidth, thumbHeight, 0, 0, this.Width, this.Height, GraphicsUnit.Pixel, IntPtr.Zero, null, IntPtr.Zero));
			}
			return image;
		}

		public void RemovePropertyItem(int propid)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRemovePropertyItem(this.nativeObject, propid));
		}

		public void RotateFlip(RotateFlipType rotateFlipType)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipImageRotateFlip(this.nativeObject, rotateFlipType));
		}

		internal ImageCodecInfo findEncoderForFormat(ImageFormat format)
		{
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			ImageCodecInfo result = null;
			if (format.Guid.Equals(ImageFormat.MemoryBmp.Guid))
			{
				format = ImageFormat.Png;
			}
			for (int i = 0; i < imageEncoders.Length; i++)
			{
				if (imageEncoders[i].FormatID.Equals(format.Guid))
				{
					result = imageEncoders[i];
					break;
				}
			}
			return result;
		}

		public void Save(string filename)
		{
			this.Save(filename, this.RawFormat);
		}

		public void Save(string filename, ImageFormat format)
		{
			ImageCodecInfo imageCodecInfo = this.findEncoderForFormat(format);
			if (imageCodecInfo == null)
			{
				imageCodecInfo = this.findEncoderForFormat(this.RawFormat);
				if (imageCodecInfo == null)
				{
					throw new ArgumentException(Locale.GetText("No codec available for saving format '{0}'.", new object[]
					{
						format.Guid
					}), "format");
				}
			}
			this.Save(filename, imageCodecInfo, null);
		}

		public void Save(string filename, ImageCodecInfo encoder, EncoderParameters encoderParams)
		{
			Guid clsid = encoder.Clsid;
			Status status;
			if (encoderParams == null)
			{
				status = GDIPlus.GdipSaveImageToFile(this.nativeObject, filename, ref clsid, IntPtr.Zero);
			}
			else
			{
				IntPtr intPtr = encoderParams.ToNativePtr();
				status = GDIPlus.GdipSaveImageToFile(this.nativeObject, filename, ref clsid, intPtr);
				Marshal.FreeHGlobal(intPtr);
			}
			GDIPlus.CheckStatus(status);
		}

		public void Save(Stream stream, ImageFormat format)
		{
			ImageCodecInfo imageCodecInfo = this.findEncoderForFormat(format);
			if (imageCodecInfo == null)
			{
				throw new ArgumentException("No codec available for format:" + format.Guid);
			}
			this.Save(stream, imageCodecInfo, null);
		}

		public void Save(Stream stream, ImageCodecInfo encoder, EncoderParameters encoderParams)
		{
			Guid clsid = encoder.Clsid;
			IntPtr intPtr;
			if (encoderParams == null)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = encoderParams.ToNativePtr();
			}
			Status status;
			try
			{
				if (GDIPlus.RunningOnUnix())
				{
					GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
					status = GDIPlus.GdipSaveImageToDelegate_linux(this.nativeObject, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, ref clsid, intPtr);
				}
				else
				{
					status = GDIPlus.GdipSaveImageToStream(new HandleRef(this, this.nativeObject), new ComIStreamWrapper(stream), ref clsid, new HandleRef(encoderParams, intPtr));
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			GDIPlus.CheckStatus(status);
		}

		public void SaveAdd(EncoderParameters encoderParams)
		{
			IntPtr intPtr = encoderParams.ToNativePtr();
			Status arg_19_0 = GDIPlus.GdipSaveAdd(this.nativeObject, intPtr);
			Marshal.FreeHGlobal(intPtr);
			GDIPlus.CheckStatus(arg_19_0);
		}

		public void SaveAdd(Image image, EncoderParameters encoderParams)
		{
			IntPtr intPtr = encoderParams.ToNativePtr();
			Status arg_1F_0 = GDIPlus.GdipSaveAddImage(this.nativeObject, image.NativeObject, intPtr);
			Marshal.FreeHGlobal(intPtr);
			GDIPlus.CheckStatus(arg_1F_0);
		}

		public int SelectActiveFrame(FrameDimension dimension, int frameIndex)
		{
			Guid guid = dimension.Guid;
			GDIPlus.CheckStatus(GDIPlus.GdipImageSelectActiveFrame(this.nativeObject, ref guid, frameIndex));
			return frameIndex;
		}

		public void SetPropertyItem(PropertyItem propitem)
		{
			throw new NotImplementedException();
		}

		internal ColorPalette retrieveGDIPalette()
		{
			ColorPalette colorPalette = new ColorPalette();
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImagePaletteSize(this.nativeObject, out num));
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			ColorPalette result;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetImagePalette(this.nativeObject, intPtr, num));
				colorPalette.setFromGDIPalette(intPtr);
				result = colorPalette;
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		internal void storeGDIPalette(ColorPalette palette)
		{
			if (palette == null)
			{
				throw new ArgumentNullException("palette");
			}
			IntPtr gDIPalette = palette.getGDIPalette();
			if (gDIPalette == IntPtr.Zero)
			{
				return;
			}
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetImagePalette(this.nativeObject, gDIPalette));
			}
			finally
			{
				Marshal.FreeHGlobal(gDIPalette);
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		~Image()
		{
			this.Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (GDIPlus.GdiPlusToken != 0uL && this.nativeObject != IntPtr.Zero)
			{
				Status arg_49_0 = GDIPlus.GdipDisposeImage(this.nativeObject);
				if (this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
				this.nativeObject = IntPtr.Zero;
				GDIPlus.CheckStatus(arg_49_0);
			}
		}

		public object Clone()
		{
			if (GDIPlus.RunningOnWindows() && this.stream != null)
			{
				return this.CloneFromStream();
			}
			IntPtr zero = IntPtr.Zero;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneImage(this.NativeObject, out zero));
			if (this is Bitmap)
			{
				return new Bitmap(zero);
			}
			return new Metafile(zero);
		}

		private object CloneFromStream()
		{
			MemoryStream memoryStream = new MemoryStream(new byte[this.stream.Length]);
			int num = (this.stream.Length < 4096L) ? ((int)this.stream.Length) : 4096;
			byte[] array = new byte[num];
			this.stream.Position = 0L;
			do
			{
				num = this.stream.Read(array, 0, num);
				memoryStream.Write(array, 0, num);
			}
			while (num == 4096);
			IntPtr ptr = IntPtr.Zero;
			ptr = Image.InitFromStream(memoryStream);
			if (this is Bitmap)
			{
				return new Bitmap(ptr, memoryStream);
			}
			return new Metafile(ptr, memoryStream);
		}
	}
}
