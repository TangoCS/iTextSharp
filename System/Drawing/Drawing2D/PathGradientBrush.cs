using System;
using System.ComponentModel;

namespace iTextSharp.Drawing.Drawing2D
{
	[MonoTODO("libgdiplus/cairo doesn't support path gradients - unless it can be mapped to a radial gradient")]
	public sealed class PathGradientBrush : Brush
	{
		public Blend Blend
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientBlendCount(this.nativeObject, out num));
				float[] array = new float[num];
				float[] positions = new float[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientBlend(this.nativeObject, array, positions, num));
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
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientBlend(this.nativeObject, factors, positions, num));
			}
		}

		public Color CenterColor
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientCenterColor(this.nativeObject, out num));
				return Color.FromArgb(num);
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientCenterColor(this.nativeObject, value.ToArgb()));
			}
		}

		public PointF CenterPoint
		{
			get
			{
				PointF result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientCenterPoint(this.nativeObject, out result));
				return result;
			}
			set
			{
				PointF pointF = value;
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientCenterPoint(this.nativeObject, ref pointF));
			}
		}

		public PointF FocusScales
		{
			get
			{
				float num;
				float num2;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientFocusScales(this.nativeObject, out num, out num2));
				return new PointF(num, num2);
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientFocusScales(this.nativeObject, value.X, value.Y));
			}
		}

		public ColorBlend InterpolationColors
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientPresetBlendCount(this.nativeObject, out num));
				if (num < 1)
				{
					num = 1;
				}
				int[] array = new int[num];
				float[] positions = new float[num];
				if (num > 1)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientPresetBlend(this.nativeObject, array, positions, num));
				}
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
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientPresetBlend(this.nativeObject, array, positions, num));
			}
		}

		public RectangleF Rectangle
		{
			get
			{
				RectangleF result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientRect(this.nativeObject, out result));
				return result;
			}
		}

		public Color[] SurroundColors
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientSurroundColorCount(this.nativeObject, out num));
				int[] array = new int[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientSurroundColorsWithCount(this.nativeObject, array, ref num));
				Color[] array2 = new Color[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = Color.FromArgb(array[i]);
				}
				return array2;
			}
			set
			{
				int num = value.Length;
				int[] array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = value[i].ToArgb();
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientSurroundColorsWithCount(this.nativeObject, array, ref num));
			}
		}

		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientTransform(this.nativeObject, matrix.nativeMatrix));
				return matrix;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Transform");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientTransform(this.nativeObject, value.nativeMatrix));
			}
		}

		public WrapMode WrapMode
		{
			get
			{
				WrapMode result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPathGradientWrapMode(this.nativeObject, out result));
				return result;
			}
			set
			{
				if (value < WrapMode.Tile || value > WrapMode.Clamp)
				{
					throw new InvalidEnumArgumentException("WrapMode");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientWrapMode(this.nativeObject, value));
			}
		}

		internal PathGradientBrush(IntPtr native) : base(native)
		{
		}

		public PathGradientBrush(GraphicsPath path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePathGradientFromPath(path.NativeObject, out this.nativeObject));
		}

		public PathGradientBrush(Point[] points) : this(points, WrapMode.Clamp)
		{
		}

		public PathGradientBrush(PointF[] points) : this(points, WrapMode.Clamp)
		{
		}

		public PathGradientBrush(Point[] points, WrapMode wrapMode)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (wrapMode < WrapMode.Tile || wrapMode > WrapMode.Clamp)
			{
				throw new InvalidEnumArgumentException("WrapMode");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePathGradientI(points, points.Length, wrapMode, out this.nativeObject));
		}

		public PathGradientBrush(PointF[] points, WrapMode wrapMode)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (wrapMode < WrapMode.Tile || wrapMode > WrapMode.Clamp)
			{
				throw new InvalidEnumArgumentException("WrapMode");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePathGradient(points, points.Length, wrapMode, out this.nativeObject));
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
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyPathGradientTransform(this.nativeObject, matrix.nativeMatrix, order));
		}

		public void ResetTransform()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetPathGradientTransform(this.nativeObject));
		}

		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		public void RotateTransform(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotatePathGradientTransform(this.nativeObject, angle, order));
		}

		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScalePathGradientTransform(this.nativeObject, sx, sy, order));
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
			GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientLinearBlend(this.nativeObject, focus, scale));
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
			GDIPlus.CheckStatus(GDIPlus.GdipSetPathGradientSigmaBlend(this.nativeObject, focus, scale));
		}

		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslatePathGradientTransform(this.nativeObject, dx, dy, order));
		}

		public override object Clone()
		{
			IntPtr native;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneBrush(this.nativeObject, out native));
			return new PathGradientBrush(native);
		}
	}
}
