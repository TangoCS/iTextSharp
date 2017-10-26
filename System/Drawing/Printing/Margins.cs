using System;
using System.ComponentModel;

namespace iTextSharp.Drawing.Printing
{
	[TypeConverter(typeof(MarginsConverter))]
	[Serializable]
	public class Margins : ICloneable
	{
		private int left;

		private int right;

		private int top;

		private int bottom;

		public int Left
		{
			get
			{
				return this.left;
			}
			set
			{
				if (value < 0)
				{
					this.InvalidMargin("left");
				}
				this.left = value;
			}
		}

		public int Right
		{
			get
			{
				return this.right;
			}
			set
			{
				if (value < 0)
				{
					this.InvalidMargin("right");
				}
				this.right = value;
			}
		}

		public int Top
		{
			get
			{
				return this.top;
			}
			set
			{
				if (value < 0)
				{
					this.InvalidMargin("top");
				}
				this.top = value;
			}
		}

		public int Bottom
		{
			get
			{
				return this.bottom;
			}
			set
			{
				if (value < 0)
				{
					this.InvalidMargin("bottom");
				}
				this.bottom = value;
			}
		}

		public Margins()
		{
			this.left = 100;
			this.right = 100;
			this.top = 100;
			this.bottom = 100;
		}

		public Margins(int left, int right, int top, int bottom)
		{
			this.Left = left;
			this.Right = right;
			this.Top = top;
			this.Bottom = bottom;
		}

		private void InvalidMargin(string property)
		{
			throw new ArgumentException(Locale.GetText("All Margins must be greater than 0"), property);
		}

		public object Clone()
		{
			return new Margins(this.left, this.right, this.top, this.bottom);
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as Margins);
		}

		private bool Equals(Margins m)
		{
			return m != null && (m.Left == this.left && m.Right == this.right && m.Top == this.top) && m.Bottom == this.bottom;
		}

		public override int GetHashCode()
		{
			return this.left | this.right << 8 | this.right >> 24 | this.top << 16 | this.top >> 16 | this.bottom << 24 | this.bottom >> 8;
		}

		public override string ToString()
		{
			return string.Format("[Margins Left={0} Right={1} Top={2} Bottom={3}]", new object[]
			{
				this.left,
				this.right,
				this.top,
				this.bottom
			});
		}

		public static bool operator ==(Margins m1, Margins m2)
		{
			if (m1 == null)
			{
				return m2 == null;
			}
			return m1.Equals(m2);
		}

		public static bool operator !=(Margins m1, Margins m2)
		{
			if (m1 == null)
			{
				return m2 != null;
			}
			return !m1.Equals(m2);
		}
	}
}
