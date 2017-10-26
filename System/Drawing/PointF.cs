using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace iTextSharp.Drawing
{
	[ComVisible(true)]
	[Serializable]
	public struct PointF
	{
		private float x;

		private float y;

		public static readonly PointF Empty;

		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return (double)this.x == 0.0 && (double)this.y == 0.0;
			}
		}

		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		public static PointF operator +(PointF pt, Size sz)
		{
			return new PointF(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
		}

		public static PointF operator +(PointF pt, SizeF sz)
		{
			return new PointF(pt.X + sz.Width, pt.Y + sz.Height);
		}

		public static bool operator ==(PointF left, PointF right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(PointF left, PointF right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		public static PointF operator -(PointF pt, Size sz)
		{
			return new PointF(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
		}

		public static PointF operator -(PointF pt, SizeF sz)
		{
			return new PointF(pt.X - sz.Width, pt.Y - sz.Height);
		}

		public PointF(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is PointF);
			return !flag && this == (PointF)obj;
		}

		public override int GetHashCode()
		{
			return (int)this.x ^ (int)this.y;
		}

		public override string ToString()
		{
			return string.Format("{{X={0}, Y={1}}}", this.x.ToString(CultureInfo.CurrentCulture), this.y.ToString(CultureInfo.CurrentCulture));
		}

		public static PointF Add(PointF pt, Size sz)
		{
			return new PointF(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
		}

		public static PointF Add(PointF pt, SizeF sz)
		{
			return new PointF(pt.X + sz.Width, pt.Y + sz.Height);
		}

		public static PointF Subtract(PointF pt, Size sz)
		{
			return new PointF(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
		}

		public static PointF Subtract(PointF pt, SizeF sz)
		{
			return new PointF(pt.X - sz.Width, pt.Y - sz.Height);
		}
	}
}
