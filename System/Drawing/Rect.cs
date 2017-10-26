using System;

namespace iTextSharp.Drawing
{
	internal struct Rect
	{
		public CGPoint origin;

		public CGSize size;

		public Rect(float x, float y, float width, float height)
		{
			this.origin.x = x;
			this.origin.y = y;
			this.size.width = width;
			this.size.height = height;
		}
	}
}
