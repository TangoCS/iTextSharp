using System;

namespace iTextSharp.Drawing.Drawing2D
{
	public sealed class Blend
	{
		private float[] positions;

		private float[] factors;

		public float[] Factors
		{
			get
			{
				return this.factors;
			}
			set
			{
				this.factors = value;
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

		public Blend()
		{
			this.positions = new float[1];
			this.factors = new float[1];
		}

		public Blend(int count)
		{
			this.positions = new float[count];
			this.factors = new float[count];
		}
	}
}
