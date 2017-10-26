using System;
using System.Threading;

namespace iTextSharp.Drawing
{
	internal class WorkerThread
	{
		private EventHandler frameChangeHandler;

		private AnimateEventArgs animateEventArgs;

		private int[] delay;

		public WorkerThread(EventHandler frmChgHandler, AnimateEventArgs aniEvtArgs, int[] delay)
		{
			this.frameChangeHandler = frmChgHandler;
			this.animateEventArgs = aniEvtArgs;
			this.delay = delay;
		}

		public void LoopHandler()
		{
			try
			{
				int num = 0;
				while (true)
				{
					Thread.Sleep(this.delay[num++]);
					this.frameChangeHandler.Invoke(null, this.animateEventArgs);
					if (num == this.delay.Length)
					{
						num = 0;
					}
				}
			}
			catch (ThreadAbortException)
			{
				Thread.ResetAbort();
			}
		}
	}
}
