using System;
using iTextSharp.Drawing.Imaging;
using System.Threading;

namespace iTextSharp.Drawing
{
	internal class AnimateEventArgs : EventArgs
	{
		private int frameCount;

		private int activeFrame;

		private Thread thread;

		public Thread RunThread
		{
			get
			{
				return this.thread;
			}
			set
			{
				this.thread = value;
			}
		}

		public AnimateEventArgs(Image image)
		{
			this.frameCount = image.GetFrameCount(FrameDimension.Time);
		}

		public int GetNextFrame()
		{
			if (this.activeFrame < this.frameCount - 1)
			{
				this.activeFrame++;
			}
			else
			{
				this.activeFrame = 0;
			}
			return this.activeFrame;
		}
	}
}
