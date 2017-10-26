using System;
using System.ComponentModel;
using iTextSharp.Drawing.Drawing2D;
using iTextSharp.Drawing.Imaging;

namespace iTextSharp.Drawing
{
	public sealed class TextureBrush : Brush
	{
		public Image Image
		{
			get
			{
				if (this.nativeObject == IntPtr.Zero)
				{
					throw new ArgumentException("Object was disposed");
				}
				IntPtr ptr;
				GDIPlus.CheckStatus(GDIPlus.GdipGetTextureImage(this.nativeObject, out ptr));
				return new Bitmap(ptr);
			}
		}

		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				GDIPlus.CheckStatus(GDIPlus.GdipGetTextureTransform(this.nativeObject, matrix.nativeMatrix));
				return matrix;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Transform");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetTextureTransform(this.nativeObject, value.nativeMatrix));
			}
		}

		public WrapMode WrapMode
		{
			get
			{
				WrapMode result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetTextureWrapMode(this.nativeObject, out result));
				return result;
			}
			set
			{
				if (value < WrapMode.Tile || value > WrapMode.Clamp)
				{
					throw new InvalidEnumArgumentException("WrapMode");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetTextureWrapMode(this.nativeObject, value));
			}
		}

		internal TextureBrush(IntPtr ptr) : base(ptr)
		{
		}

		public TextureBrush(Image bitmap) : this(bitmap, WrapMode.Tile)
		{
		}

		public TextureBrush(Image image, Rectangle dstRect) : this(image, WrapMode.Tile, dstRect)
		{
		}

		public TextureBrush(Image image, RectangleF dstRect) : this(image, WrapMode.Tile, dstRect)
		{
		}

		public TextureBrush(Image image, WrapMode wrapMode)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (wrapMode < WrapMode.Tile || wrapMode > WrapMode.Clamp)
			{
				throw new InvalidEnumArgumentException("WrapMode");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateTexture(image.nativeObject, wrapMode, out this.nativeObject));
		}

		[MonoLimitation("ImageAttributes are ignored when using libgdiplus")]
		public TextureBrush(Image image, Rectangle dstRect, ImageAttributes imageAttr)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			IntPtr imageAttributes = (imageAttr == null) ? IntPtr.Zero : imageAttr.NativeObject;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateTextureIAI(image.nativeObject, imageAttributes, dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height, out this.nativeObject));
		}

		[MonoLimitation("ImageAttributes are ignored when using libgdiplus")]
		public TextureBrush(Image image, RectangleF dstRect, ImageAttributes imageAttr)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			IntPtr imageAttributes = (imageAttr == null) ? IntPtr.Zero : imageAttr.NativeObject;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateTextureIA(image.nativeObject, imageAttributes, dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height, out this.nativeObject));
		}

		public TextureBrush(Image image, WrapMode wrapMode, Rectangle dstRect)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (wrapMode < WrapMode.Tile || wrapMode > WrapMode.Clamp)
			{
				throw new InvalidEnumArgumentException("WrapMode");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateTexture2I(image.nativeObject, wrapMode, dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height, out this.nativeObject));
		}

		public TextureBrush(Image image, WrapMode wrapMode, RectangleF dstRect)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (wrapMode < WrapMode.Tile || wrapMode > WrapMode.Clamp)
			{
				throw new InvalidEnumArgumentException("WrapMode");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreateTexture2(image.nativeObject, wrapMode, dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height, out this.nativeObject));
		}

		public override object Clone()
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneBrush(this.nativeObject, out ptr));
			return new TextureBrush(ptr);
		}

		public void MultiplyTransform(Matrix matrix)
		{
			this.MultiplyTransform(matrix, MatrixOrder.Prepend);
		}

		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyTextureTransform(this.nativeObject, matrix.nativeMatrix, order));
		}

		public void ResetTransform()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetTextureTransform(this.nativeObject));
		}

		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		public void RotateTransform(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotateTextureTransform(this.nativeObject, angle, order));
		}

		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScaleTextureTransform(this.nativeObject, sx, sy, order));
		}

		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateTextureTransform(this.nativeObject, dx, dy, order));
		}
	}
}
