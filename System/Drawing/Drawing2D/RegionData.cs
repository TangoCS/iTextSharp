using System;

namespace iTextSharp.Drawing.Drawing2D
{
	public sealed class RegionData
	{
		private byte[] data;

		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		internal RegionData()
		{
		}
	}
}
