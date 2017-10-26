using System;
using System.Collections;
using iTextSharp.Drawing.Imaging;
using System.Threading;

namespace iTextSharp.Drawing
{
	public sealed class ImageAnimator
	{
		private static Hashtable ht = Hashtable.Synchronized(new Hashtable());

		private ImageAnimator()
		{
		}

		public static void Animate(Image image, EventHandler onFrameChangedHandler)
		{
			if (!ImageAnimator.CanAnimate(image))
			{
				return;
			}
			if (ImageAnimator.ht.ContainsKey(image))
			{
				return;
			}
			byte[] value = image.GetPropertyItem(20736).Value;
			int[] array = new int[value.Length >> 2];
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				int num2 = BitConverter.ToInt32(value, i) * 10;
				array[num] = ((num2 < 100) ? 100 : num2);
				i += 4;
				num++;
			}
			AnimateEventArgs animateEventArgs = new AnimateEventArgs(image);
			Thread thread = new Thread(new ThreadStart(new WorkerThread(onFrameChangedHandler, animateEventArgs, array).LoopHandler));
			thread.IsBackground = true;
			animateEventArgs.RunThread = thread;
			ImageAnimator.ht.Add(image, animateEventArgs);
			thread.Start();
		}

		public static bool CanAnimate(Image image)
		{
			if (image == null)
			{
				return false;
			}
			int num = image.FrameDimensionsList.Length;
			if (num < 1)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (image.FrameDimensionsList[i].Equals(FrameDimension.Time.Guid))
				{
					return image.GetFrameCount(FrameDimension.Time) > 1;
				}
			}
			return false;
		}

		public static void StopAnimate(Image image, EventHandler onFrameChangedHandler)
		{
			if (image == null)
			{
				return;
			}
			if (ImageAnimator.ht.ContainsKey(image))
			{
				((AnimateEventArgs)ImageAnimator.ht[image]).RunThread.Abort();
				ImageAnimator.ht.Remove(image);
			}
		}

		public static void UpdateFrames()
		{
            foreach (Image image in ImageAnimator.ht.Keys)
            {
                ImageAnimator.UpdateImageFrame(image);
            }
		}

		public static void UpdateFrames(Image image)
		{
			if (image == null)
			{
				return;
			}
			if (ImageAnimator.ht.ContainsKey(image))
			{
				ImageAnimator.UpdateImageFrame(image);
			}
		}

		private static void UpdateImageFrame(Image image)
		{
			AnimateEventArgs animateEventArgs = (AnimateEventArgs)ImageAnimator.ht[image];
			image.SelectActiveFrame(FrameDimension.Time, animateEventArgs.GetNextFrame());
		}
	}
}
