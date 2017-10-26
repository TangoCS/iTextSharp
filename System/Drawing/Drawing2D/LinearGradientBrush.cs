using System;
using System.ComponentModel;

namespace iTextSharp.Drawing.Drawing2D
{
	public sealed class LinearGradientBrush : Brush
	{
		private RectangleF rectangle;

		public Blend Blend
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineBlendCount(this.nativeObject, out num));
				float[] array = new float[num];
				float[] positions = new float[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineBlend(this.nativeObject, array, positions, num));
				return new Blend
				{
					Factors = array,
					Positions = positions
				};
			}
			set
			{
				float[] factors = value.Factors;
				float[] positions = value.Positions;
				int num = factors.Length;
				if (num == 0 || positions.Length == 0)
				{
					throw new ArgumentException("Invalid Blend object. It should have at least 2 elements in each of the factors and positions arrays.");
				}
				if (num != positions.Length)
				{
					throw new ArgumentException("Invalid Blend object. It should contain the same number of factors and positions values.");
				}
				if (positions[0] != 0f)
				{
					throw new ArgumentException("Invalid Blend object. The positions array must have 0.0 as its first element.");
				}
				if (positions[num - 1] != 1f)
				{
					throw new ArgumentException("Invalid Blend object. The positions array must have 1.0 as its last element.");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineBlend(this.nativeObject, factors, positions, num));
			}
		}

		[MonoTODO("The GammaCorrection value is ignored when using libgdiplus.")]
		public bool GammaCorrection
		{
			get
			{
				bool result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineGammaCorrection(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineGammaCorrection(this.nativeObject, value));
			}
		}

		public ColorBlend InterpolationColors
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLinePresetBlendCount(this.nativeObject, out num));
				int[] array = new int[num];
				float[] positions = new float[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetLinePresetBlend(this.nativeObject, array, positions, num));
				ColorBlend colorBlend = new ColorBlend();
				Color[] array2 = new Color[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = Color.FromArgb(array[i]);
				}
				colorBlend.Colors = array2;
				colorBlend.Positions = positions;
				return colorBlend;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("InterpolationColors is null");
				}
				Color[] colors = value.Colors;
				float[] positions = value.Positions;
				int num = colors.Length;
				if (num == 0 || positions.Length == 0)
				{
					throw new ArgumentException("Invalid ColorBlend object. It should have at least 2 elements in each of the colors and positions arrays.");
				}
				if (num != positions.Length)
				{
					throw new ArgumentException("Invalid ColorBlend object. It should contain the same number of positions and color values.");
				}
				if (positions[0] != 0f)
				{
					throw new ArgumentException("Invalid ColorBlend object. The positions array must have 0.0 as its first element.");
				}
				if (positions[num - 1] != 1f)
				{
					throw new ArgumentException("Invalid ColorBlend object. The positions array must have 1.0 as its last element.");
				}
				int[] array = new int[colors.Length];
				for (int i = 0; i < colors.Length; i++)
				{
					array[i] = colors[i].ToArgb();
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLinePresetBlend(this.nativeObject, array, positions, num));
			}
		}

		public Color[] LinearColors
		{
			get
			{
				int[] array = new int[2];
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineColors(this.nativeObject, array));
				return new Color[]
				{
					Color.FromArgb(array[0]),
					Color.FromArgb(array[1])
				};
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineColors(this.nativeObject, value[0].ToArgb(), value[1].ToArgb()));
			}
		}

		public RectangleF Rectangle
		{
			get
			{
				return this.rectangle;
			}
		}

		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineTransform(this.nativeObject, matrix.nativeMatrix));
				return matrix;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Transform");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineTransform(this.nativeObject, value.nativeMatrix));
			}
		}

		public WrapMode WrapMode
		{
			get
			{
				WrapMode result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetLineWrapMode(this.nativeObject, out result));
				return result;
			}
			set
			{
				if (value < WrapMode.Tile || value > WrapMode.Clamp)
				{
					throw new InvalidEnumArgumentException("WrapMode");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetLineWrapMode(this.nativeObject, value));
			}
		}

		internal LinearGradientBrush(IntPtr native) : base(native)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipGetLineRect(native, out this.rectangle));
		}

		public LinearGradientBrush(Point point1, Point point2, Color color1, Color color2)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushI(ref point1, ref point2, color1.ToArgb(), color2.ToArgb(), WrapMode.Tile, out this.nativeObject));
			GDIPlus.CheckStatus(GDIPlus.GdipGetLineRect(this.nativeObject, out this.rectangle));
		}

		public LinearGradientBrush(PointF point1, PointF point2, Color color1, Color color2)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrush(ref point1, ref point2, color1.ToArgb(), color2.ToArgb(), WrapMode.Tile, out this.nativeObject));
			GDIPlus.CheckStatus(GDIPlus.GdipGetLineRect(this.nativeObject, out this.rectangle));
		}

		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRectI(ref rect, color1.ToArgb(), color2.ToArgb(), linearGradientMode, WrapMode.Tile, out this.nativeObject));
			this.rectangle = rect;
		}

		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle) : this(rect, color1, color2, angle, false)
		{
		}

		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRect(ref rect, color1.ToArgb(), color2.ToArgb(), linearGradientMode, WrapMode.Tile, out this.nativeObject));
			this.rectangle = rect;
		}

		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle) : this(rect, color1, color2, angle, false)
		{
		}

		public LinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle, bool isAngleScaleable)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRectWithAngleI(ref rect, color1.ToArgb(), color2.ToArgb(), angle, isAngleScaleable, WrapMode.Tile, out this.nativeObject));
			this.rectangle = rect;
		}

		public LinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle, bool isAngleScaleable)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateLineBrushFromRectWithAngle(ref rect, color1.ToArgb(), color2.ToArgb(), angle, isAngleScaleable, WrapMode.Tile, out this.nativeObject));
			this.rectangle = rect;
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
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyLineTransform(this.nativeObject, matrix.nativeMatrix, order));
		}

		public void ResetTransform()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetLineTransform(this.nativeObject));
		}

		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		public void RotateTransform(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotateLineTransform(this.nativeObject, angle, order));
		}

		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScaleLineTransform(this.nativeObject, sx, sy, order));
		}

		public void SetBlendTriangularShape(float focus)
		{
			this.SetBlendTriangularShape(focus, 1f);
		}

		public void SetBlendTriangularShape(float focus, float scale)
		{
			if (focus < 0f || focus > 1f || scale < 0f || scale > 1f)
			{
				throw new ArgumentException("Invalid parameter passed.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetLineLinearBlend(this.nativeObject, focus, scale));
		}

		public void SetSigmaBellShape(float focus)
		{
			this.SetSigmaBellShape(focus, 1f);
		}

		public void SetSigmaBellShape(float focus, float scale)
		{
			if (focus < 0f || focus > 1f || scale < 0f || scale > 1f)
			{
				throw new ArgumentException("Invalid parameter passed.");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetLineSigmaBlend(this.nativeObject, focus, scale));
		}

		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateLineTransform(this.nativeObject, dx, dy, order));
		}

		public override object Clone()
		{
			IntPtr native;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneBrush(this.nativeObject, out native));
			return new LinearGradientBrush(native);
		}
	}
}
