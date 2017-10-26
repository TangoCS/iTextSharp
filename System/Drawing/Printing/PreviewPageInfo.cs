using System;

namespace iTextSharp.Drawing.Printing
{
	public sealed class PreviewPageInfo
	{
		private Image image;

		private Size physicalSize;

		public Image Image
		{
			get
			{
				return this.image;
			}
		}

		public Size PhysicalSize
		{
			get
			{
				return this.physicalSize;
			}
		}

		public PreviewPageInfo(Image image, Size physicalSize)
		{
			this.image = image;
			this.physicalSize = physicalSize;
		}
	}
}
