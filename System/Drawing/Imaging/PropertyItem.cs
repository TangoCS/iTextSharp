using System;

namespace iTextSharp.Drawing.Imaging
{
	public sealed class PropertyItem
	{
		private int id;

		private int len;

		private short type;

		private byte[] value;

		public int Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		public int Len
		{
			get
			{
				return this.len;
			}
			set
			{
				this.len = value;
			}
		}

		public short Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		public byte[] Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		internal PropertyItem()
		{
		}
	}
}
