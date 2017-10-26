using System;

namespace iTextSharp.Drawing.Drawing2D
{
	public sealed class ColorBlend
	{
		private float[] positions;

		private Color[] colors;

		public Color[] Colors
		{
			get
			{
				return this.colors;
			}
			set
			{
				this.colors = value;
			}
		}

		public float[] Positions
		{
			get
			{
				return this.positions;
			}
			set
			{
				this.positions = value;
			}
		}

		public ColorBlend()
		{
			this.positions = new float[1];
			this.colors = new Color[1];
		}

		public ColorBlend(int count)
		{
			this.positions = new float[count];
			this.colors = new Color[count];
		}
	}
}
