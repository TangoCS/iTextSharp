using System;

namespace iTextSharp.Drawing.Drawing2D
{
	public sealed class GraphicsContainer : MarshalByRefObject
	{
		private uint nativeState;

		internal uint NativeObject
		{
			get
			{
				return this.nativeState;
			}
		}

		internal GraphicsContainer(uint state)
		{
			this.nativeState = state;
		}
	}
}
