using System;
using System.Collections;

namespace iTextSharp.Drawing.Printing
{
	public class PreviewPrintController : PrintController
	{
		private bool useantialias;

		private ArrayList pageInfoList;

		public override bool IsPreview
		{
			get
			{
				return true;
			}
		}

		public virtual bool UseAntiAlias
		{
			get
			{
				return this.useantialias;
			}
			set
			{
				this.useantialias = value;
			}
		}

		public PreviewPrintController()
		{
			this.pageInfoList = new ArrayList();
		}

		[MonoTODO]
		public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
		{
		}

		[MonoTODO]
		public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
		{
			if (!document.PrinterSettings.IsValid)
			{
				throw new InvalidPrinterException(document.PrinterSettings);
			}
            foreach (PreviewPageInfo pageInfo in this.pageInfoList)
            {
                pageInfo.Image.Dispose();
            }
			this.pageInfoList.Clear();
		}

		[MonoTODO]
		public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
		{
		}

		[MonoTODO]
		public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
		{
			Image image = new Bitmap(e.PageSettings.PaperSize.Width, e.PageSettings.PaperSize.Height);
			PreviewPageInfo previewPageInfo = new PreviewPageInfo(image, new Size(e.PageSettings.PaperSize.Width, e.PageSettings.PaperSize.Height));
			this.pageInfoList.Add(previewPageInfo);
			Graphics expr_6A = Graphics.FromImage(previewPageInfo.Image);
			expr_6A.FillRectangle(new SolidBrush(Color.White), new Rectangle(new Point(0, 0), new Size(image.Width, image.Height)));
			return expr_6A;
		}

		public PreviewPageInfo[] GetPreviewPageInfo()
		{
			PreviewPageInfo[] array = new PreviewPageInfo[this.pageInfoList.Count];
			this.pageInfoList.CopyTo(array);
			return array;
		}
	}
}
