using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace iTextSharp.Drawing.Printing
{
	internal class PrintingServicesWin32 : PrintingServices
	{
		internal struct PRINTER_INFO
		{
			public IntPtr pServerName;

			public IntPtr pPrinterName;

			public IntPtr pShareName;

			public IntPtr pPortName;

			public IntPtr pDriverName;

			public IntPtr pComment;

			public IntPtr pLocation;

			public IntPtr pDevMode;

			public IntPtr pSepFile;

			public IntPtr pPrintProcessor;

			public IntPtr pDatatype;

			public IntPtr pParameters;

			public IntPtr pSecurityDescriptor;

			public uint Attributes;

			public uint Priority;

			public uint DefaultPriority;

			public uint StartTime;

			public uint UntilTime;

			public uint Status;

			public uint cJobs;

			public uint AveragePPM;
		}

		internal struct DOCINFO
		{
			public int cbSize;

			public IntPtr lpszDocName;

			public IntPtr lpszOutput;

			public IntPtr lpszDatatype;

			public int fwType;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DEVMODE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;

			public short dmSpecVersion;

			public short dmDriverVersion;

			public short dmSize;

			public short dmDriverExtra;

			public int dmFields;

			public short dmOrientation;

			public short dmPaperSize;

			public short dmPaperLength;

			public short dmPaperWidth;

			public short dmScale;

			public short dmCopies;

			public short dmDefaultSource;

			public short dmPrintQuality;

			public short dmColor;

			public short dmDuplex;

			public short dmYResolution;

			public short dmTTOption;

			public short dmCollate;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmFormName;

			public short dmLogPixels;

			public short dmBitsPerPel;

			public int dmPelsWidth;

			public int dmPelsHeight;

			public int dmDisplayFlags;

			public int dmDisplayFrequency;

			public int dmICMMethod;

			public int dmICMIntent;

			public int dmMediaType;

			public int dmDitherType;

			public int dmReserved1;

			public int dmReserved2;

			public int dmPanningWidth;

			public int dmPanningHeight;
		}

		internal enum DCCapabilities : short
		{
			DC_FIELDS = 1,
			DC_PAPERS,
			DC_PAPERSIZE,
			DC_MINEXTENT,
			DC_MAXEXTENT,
			DC_BINS,
			DC_DUPLEX,
			DC_SIZE,
			DC_EXTRA,
			DC_VERSION,
			DC_DRIVER,
			DC_BINNAMES,
			DC_ENUMRESOLUTIONS,
			DC_FILEDEPENDENCIES,
			DC_TRUETYPE,
			DC_PAPERNAMES,
			DC_ORIENTATION,
			DC_COPIES,
			DC_BINADJUST,
			DC_EMF_COMPLIANT,
			DC_DATATYPE_PRODUCED,
			DC_COLLATE,
			DC_MANUFACTURER,
			DC_MODEL,
			DC_PERSONALITY,
			DC_PRINTRATE,
			DC_PRINTRATEUNIT,
			DC_PRINTERMEM,
			DC_MEDIAREADY,
			DC_STAPLE,
			DC_PRINTRATEPPM,
			DC_COLORDEVICE,
			DC_NUP
		}

		[Flags]
		internal enum PrinterStatus : uint
		{
			PS_PAUSED = 1u,
			PS_ERROR = 2u,
			PS_PENDING_DELETION = 4u,
			PS_PAPER_JAM = 8u,
			PS_PAPER_OUT = 16u,
			PS_MANUAL_FEED = 32u,
			PS_PAPER_PROBLEM = 64u,
			PS_OFFLINE = 128u,
			PS_IO_ACTIVE = 256u,
			PS_BUSY = 512u,
			PS_PRINTING = 1024u,
			PS_OUTPUT_BIN_FULL = 2048u,
			PS_NOT_AVAILABLE = 4096u,
			PS_WAITING = 8192u,
			PS_PROCESSING = 16384u,
			PS_INITIALIZING = 32768u,
			PS_WARMING_UP = 65536u,
			PS_TONER_LOW = 131072u,
			PS_NO_TONER = 262144u,
			PS_PAGE_PUNT = 524288u,
			PS_USER_INTERVENTION = 1048576u,
			PS_OUT_OF_MEMORY = 2097152u,
			PS_DOOR_OPEN = 4194304u,
			PS_SERVER_UNKNOWN = 8388608u,
			PS_POWER_SAVE = 16777216u
		}

		internal enum DevCapabilities
		{
			TECHNOLOGY = 2
		}

		internal enum PrinterType
		{
			DT_PLOTTER,
			DT_RASDIPLAY,
			DT_RASPRINTER,
			DT_RASCAMERA,
			DT_CHARSTREAM,
			DT_METAFILE,
			DT_DISPFILE
		}

		[Flags]
		internal enum EnumPrinters : uint
		{
			PRINTER_ENUM_DEFAULT = 1u,
			PRINTER_ENUM_LOCAL = 2u,
			PRINTER_ENUM_CONNECTIONS = 4u,
			PRINTER_ENUM_FAVORITE = 4u,
			PRINTER_ENUM_NAME = 8u,
			PRINTER_ENUM_REMOTE = 16u,
			PRINTER_ENUM_SHARED = 32u,
			PRINTER_ENUM_NETWORK = 64u
		}

		private bool is_printer_valid;

		internal override string DefaultPrinter
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				int capacity = stringBuilder.Capacity;
				if (PrintingServicesWin32.Win32GetDefaultPrinter(stringBuilder, ref capacity) > 0 && this.IsPrinterValid(stringBuilder.ToString()))
				{
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		internal static PrinterSettings.StringCollection InstalledPrinters
		{
			get
			{
				PrinterSettings.StringCollection stringCollection = new PrinterSettings.StringCollection(new string[0]);
				uint num = 0u;
				uint num2 = 0u;
				PrintingServicesWin32.Win32EnumPrinters(6, null, 2u, IntPtr.Zero, 0u, ref num, ref num2);
				if (num <= 0u)
				{
					return stringCollection;
				}
				IntPtr intPtr2;
				IntPtr intPtr = intPtr2 = Marshal.AllocHGlobal((int)num);
				try
				{
					PrintingServicesWin32.Win32EnumPrinters(6, null, 2u, intPtr, num, ref num, ref num2);
					int num3 = 0;
					while ((long)num3 < (long)((ulong)num2))
					{
						PrintingServicesWin32.PRINTER_INFO pRINTER_INFO = (PrintingServicesWin32.PRINTER_INFO)Marshal.PtrToStructure(intPtr2, typeof(PrintingServicesWin32.PRINTER_INFO));
						string value = Marshal.PtrToStringUni(pRINTER_INFO.pPrinterName);
						stringCollection.Add(value);
						intPtr2 = new IntPtr(intPtr2.ToInt64() + (long)Marshal.SizeOf(pRINTER_INFO));
						num3++;
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return stringCollection;
			}
		}

		internal PrintingServicesWin32()
		{
		}

		internal override bool IsPrinterValid(string printer)
		{
			if (printer == null | printer == string.Empty)
			{
				return false;
			}
			int num = PrintingServicesWin32.Win32DocumentProperties(IntPtr.Zero, IntPtr.Zero, printer, IntPtr.Zero, IntPtr.Zero, 0);
			this.is_printer_valid = (num > 0);
			return this.is_printer_valid;
		}

		internal override void LoadPrinterSettings(string printer, PrinterSettings settings)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			settings.maximum_copies = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_COPIES, IntPtr.Zero, IntPtr.Zero);
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_DUPLEX, IntPtr.Zero, IntPtr.Zero);
			settings.can_duplex = (num == 1);
			num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_COLORDEVICE, IntPtr.Zero, IntPtr.Zero);
			settings.supports_color = (num == 1);
			num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_ORIENTATION, IntPtr.Zero, IntPtr.Zero);
			if (num != -1)
			{
				settings.landscape_angle = num;
			}
			IntPtr arg_8C_0 = IntPtr.Zero;
			IntPtr expr_9A = PrintingServicesWin32.Win32CreateIC(null, printer, null, IntPtr.Zero);
			num = PrintingServicesWin32.Win32GetDeviceCaps(expr_9A, 2);
			settings.is_plotter = (num == 0);
			PrintingServicesWin32.Win32DeleteDC(expr_9A);
			try
			{
				PrintingServicesWin32.Win32OpenPrinter(printer, out zero, IntPtr.Zero);
				num = PrintingServicesWin32.Win32DocumentProperties(IntPtr.Zero, zero, null, IntPtr.Zero, IntPtr.Zero, 0);
				if (num >= 0)
				{
					intPtr = Marshal.AllocHGlobal(num);
					num = PrintingServicesWin32.Win32DocumentProperties(IntPtr.Zero, zero, null, intPtr, IntPtr.Zero, 2);
					PrintingServicesWin32.DEVMODE dEVMODE = (PrintingServicesWin32.DEVMODE)Marshal.PtrToStructure(intPtr, typeof(PrintingServicesWin32.DEVMODE));
					this.LoadPrinterPaperSizes(printer, settings);
					foreach (PaperSize paperSize in settings.PaperSizes)
					{
                        if (paperSize.Kind == (PaperKind)dEVMODE.dmPaperSize)
                        {
                            settings.DefaultPageSettings.PaperSize = paperSize;
                            break;
                        }
					}
					this.LoadPrinterPaperSources(printer, settings);
					foreach (PaperSource paperSource in settings.PaperSources)
					{
                        if (paperSource.Kind == (PaperSourceKind)dEVMODE.dmDefaultSource)
                        {
                            settings.DefaultPageSettings.PaperSource = paperSource;
                            break;
                        }
					}
				}
			}
			finally
			{
				PrintingServicesWin32.Win32ClosePrinter(zero);
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		internal override void LoadPrinterResolutions(string printer, PrinterSettings settings)
		{
			IntPtr intPtr = IntPtr.Zero;
			settings.PrinterResolutions.Clear();
			base.LoadDefaultResolutions(settings.PrinterResolutions);
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_ENUMRESOLUTIONS, IntPtr.Zero, IntPtr.Zero);
			if (num == -1)
			{
				return;
			}
			IntPtr intPtr2;
			intPtr = (intPtr2 = Marshal.AllocHGlobal(num * 2 * Marshal.SizeOf(intPtr)));
			num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_ENUMRESOLUTIONS, intPtr, IntPtr.Zero);
			if (num != -1)
			{
				for (int i = 0; i < num; i++)
				{
					int num2 = Marshal.ReadInt32(intPtr2);
					intPtr2 = new IntPtr(intPtr2.ToInt64() + (long)Marshal.SizeOf(num2));
					int num3 = Marshal.ReadInt32(intPtr2);
					intPtr2 = new IntPtr(intPtr2.ToInt64() + (long)Marshal.SizeOf(num3));
					settings.PrinterResolutions.Add(new PrinterResolution(num2, num3, PrinterResolutionKind.Custom));
				}
			}
			Marshal.FreeHGlobal(intPtr);
		}

		private void LoadPrinterPaperSizes(string printer, PrinterSettings settings)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			if (settings.PaperSizes == null)
			{
				settings.paper_sizes = new PrinterSettings.PaperSizeCollection(new PaperSize[0]);
			}
			else
			{
				settings.PaperSizes.Clear();
			}
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERSIZE, IntPtr.Zero, IntPtr.Zero);
			if (num == -1)
			{
				return;
			}
			try
			{
				IntPtr intPtr4;
				intPtr2 = (intPtr4 = Marshal.AllocHGlobal(num * 2 * 4));
				IntPtr intPtr5;
				intPtr = (intPtr5 = Marshal.AllocHGlobal(num * 64 * 2));
				IntPtr intPtr6;
				intPtr3 = (intPtr6 = Marshal.AllocHGlobal(num * 2));
				int num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERSIZE, intPtr2, IntPtr.Zero);
				if (num2 != -1)
				{
					num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERS, intPtr3, IntPtr.Zero);
					num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERNAMES, intPtr, IntPtr.Zero);
					for (int i = 0; i < num2; i++)
					{
						int num3 = Marshal.ReadInt32(intPtr4, i * 8);
						int num4 = Marshal.ReadInt32(intPtr4, i * 8 + 4);
						num3 = PrinterUnitConvert.Convert(num3, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
						num4 = PrinterUnitConvert.Convert(num4, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
						string arg_128_0 = Marshal.PtrToStringUni(intPtr5);
						intPtr5 = new IntPtr(intPtr5.ToInt64() + 128L);
						PaperKind kind = (PaperKind)Marshal.ReadInt16(intPtr6);
						intPtr6 = new IntPtr(intPtr6.ToInt64() + 2L);
						PaperSize paperSize = new PaperSize(arg_128_0, num3, num4);
						paperSize.SetKind(kind);
						settings.PaperSizes.Add(paperSize);
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr3);
				}
			}
		}

		internal static bool StartDoc(GraphicsPrinter gr, string doc_name, string output_file)
		{
			PrintingServicesWin32.DOCINFO dOCINFO = default(PrintingServicesWin32.DOCINFO);
			dOCINFO.cbSize = Marshal.SizeOf(dOCINFO);
			dOCINFO.lpszDocName = Marshal.StringToHGlobalUni(doc_name);
			dOCINFO.lpszOutput = IntPtr.Zero;
			dOCINFO.lpszDatatype = IntPtr.Zero;
			dOCINFO.fwType = 0;
			int arg_5B_0 = PrintingServicesWin32.Win32StartDoc(gr.Hdc, ref dOCINFO);
			Marshal.FreeHGlobal(dOCINFO.lpszDocName);
			return arg_5B_0 > 0;
		}

		private void LoadPrinterPaperSources(string printer, PrinterSettings settings)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			if (settings.PaperSources == null)
			{
				settings.paper_sources = new PrinterSettings.PaperSourceCollection(new PaperSource[0]);
			}
			else
			{
				settings.PaperSources.Clear();
			}
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_BINNAMES, IntPtr.Zero, IntPtr.Zero);
			if (num == -1)
			{
				return;
			}
			try
			{
				IntPtr intPtr3;
				intPtr = (intPtr3 = Marshal.AllocHGlobal(num * 2 * 24));
				IntPtr intPtr4;
				intPtr2 = (intPtr4 = Marshal.AllocHGlobal(num * 2));
				int num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_BINNAMES, intPtr, IntPtr.Zero);
				if (num2 != -1)
				{
					num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_BINS, intPtr2, IntPtr.Zero);
					for (int i = 0; i < num2; i++)
					{
						string sourceName = Marshal.PtrToStringUni(intPtr3);
						PaperSourceKind kind = (PaperSourceKind)Marshal.ReadInt16(intPtr4);
						settings.PaperSources.Add(new PaperSource(sourceName, kind));
						intPtr3 = new IntPtr(intPtr3.ToInt64() + 48L);
						intPtr4 = new IntPtr(intPtr4.ToInt64() + 2L);
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		internal static bool StartPage(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.Win32StartPage(gr.Hdc) > 0;
		}

		internal static bool EndPage(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.Win32EndPage(gr.Hdc) > 0;
		}

		internal static bool EndDoc(GraphicsPrinter gr)
		{
			int arg_23_0 = PrintingServicesWin32.Win32EndDoc(gr.Hdc);
			PrintingServicesWin32.Win32DeleteDC(gr.Hdc);
			gr.Graphics.Dispose();
			return arg_23_0 > 0;
		}

		internal static IntPtr CreateGraphicsContext(PrinterSettings settings, PageSettings default_page_settings)
		{
			IntPtr arg_05_0 = IntPtr.Zero;
			return PrintingServicesWin32.Win32CreateDC(null, settings.PrinterName, null, IntPtr.Zero);
		}

		internal override void GetPrintDialogInfo(string printer, ref string port, ref string type, ref string status, ref string comment)
		{
			PrintingServicesWin32.PRINTER_INFO pRINTER_INFO = default(PrintingServicesWin32.PRINTER_INFO);
			int num = 0;
			IntPtr intPtr;
			PrintingServicesWin32.Win32OpenPrinter(printer, out intPtr, IntPtr.Zero);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			PrintingServicesWin32.Win32GetPrinter(intPtr, 2, IntPtr.Zero, 0, ref num);
			IntPtr intPtr2 = Marshal.AllocHGlobal(num);
			PrintingServicesWin32.Win32GetPrinter(intPtr, 2, intPtr2, num, ref num);
			pRINTER_INFO = (PrintingServicesWin32.PRINTER_INFO)Marshal.PtrToStructure(intPtr2, typeof(PrintingServicesWin32.PRINTER_INFO));
			Marshal.FreeHGlobal(intPtr2);
			port = Marshal.PtrToStringUni(pRINTER_INFO.pPortName);
			comment = Marshal.PtrToStringUni(pRINTER_INFO.pComment);
			type = Marshal.PtrToStringUni(pRINTER_INFO.pDriverName);
			status = this.GetPrinterStatusMsg(pRINTER_INFO.Status);
			PrintingServicesWin32.Win32ClosePrinter(intPtr);
		}

		private string GetPrinterStatusMsg(uint status)
		{
			string text = string.Empty;
			if (status == 0u)
			{
				return "Ready";
			}
			if ((status & 1u) != 0u)
			{
				text += "Paused; ";
			}
			if ((status & 2u) != 0u)
			{
				text += "Error; ";
			}
			if ((status & 4u) != 0u)
			{
				text += "Pending deletion; ";
			}
			if ((status & 8u) != 0u)
			{
				text += "Paper jam; ";
			}
			if ((status & 16u) != 0u)
			{
				text += "Paper out; ";
			}
			if ((status & 32u) != 0u)
			{
				text += "Manual feed; ";
			}
			if ((status & 64u) != 0u)
			{
				text += "Paper problem; ";
			}
			if ((status & 128u) != 0u)
			{
				text += "Offline; ";
			}
			if ((status & 256u) != 0u)
			{
				text += "I/O active; ";
			}
			if ((status & 512u) != 0u)
			{
				text += "Busy; ";
			}
			if ((status & 1024u) != 0u)
			{
				text += "Printing; ";
			}
			if ((status & 2048u) != 0u)
			{
				text += "Output bin full; ";
			}
			if ((status & 4096u) != 0u)
			{
				text += "Not available; ";
			}
			if ((status & 8192u) != 0u)
			{
				text += "Waiting; ";
			}
			if ((status & 16384u) != 0u)
			{
				text += "Processing; ";
			}
			if ((status & 32768u) != 0u)
			{
				text += "Initializing; ";
			}
			if ((status & 65536u) != 0u)
			{
				text += "Warming up; ";
			}
			if ((status & 131072u) != 0u)
			{
				text += "Toner low; ";
			}
			if ((status & 262144u) != 0u)
			{
				text += "No toner; ";
			}
			if ((status & 524288u) != 0u)
			{
				text += "Page punt; ";
			}
			if ((status & 1048576u) != 0u)
			{
				text += "User intervention; ";
			}
			if ((status & 2097152u) != 0u)
			{
				text += "Out of memory; ";
			}
			if ((status & 4194304u) != 0u)
			{
				text += "Door open; ";
			}
			if ((status & 8388608u) != 0u)
			{
				text += "Server unkown; ";
			}
			if ((status & 16777216u) != 0u)
			{
				text += "Power save; ";
			}
			return text;
		}

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "OpenPrinter", SetLastError = true)]
		private static extern int Win32OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "GetPrinter", SetLastError = true)]
		private static extern int Win32GetPrinter(IntPtr hPrinter, int level, IntPtr dwBuf, int size, ref int dwNeeded);

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "ClosePrinter", SetLastError = true)]
		private static extern int Win32ClosePrinter(IntPtr hPrinter);

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "DeviceCapabilities", SetLastError = true)]
		private static extern int Win32DeviceCapabilities(string device, string port, PrintingServicesWin32.DCCapabilities cap, IntPtr outputBuffer, IntPtr deviceMode);

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "EnumPrinters", SetLastError = true)]
		private static extern int Win32EnumPrinters(int Flags, string Name, uint Level, IntPtr pPrinterEnum, uint cbBuf, ref uint pcbNeeded, ref uint pcReturned);

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "GetDefaultPrinter", SetLastError = true)]
		private static extern int Win32GetDefaultPrinter(StringBuilder buffer, ref int bufferSize);

		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "DocumentProperties", SetLastError = true)]
		private static extern int Win32DocumentProperties(IntPtr hwnd, IntPtr hPrinter, string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

		[DllImport("gdi32.dll", EntryPoint = "CreateDC")]
		private static extern IntPtr Win32CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

		[DllImport("gdi32.dll", EntryPoint = "CreateIC")]
		private static extern IntPtr Win32CreateIC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, EntryPoint = "StartDoc")]
		private static extern int Win32StartDoc(IntPtr hdc, [In] ref PrintingServicesWin32.DOCINFO lpdi);

		[DllImport("gdi32.dll", EntryPoint = "StartPage")]
		private static extern int Win32StartPage(IntPtr hDC);

		[DllImport("gdi32.dll", EntryPoint = "EndPage")]
		private static extern int Win32EndPage(IntPtr hdc);

		[DllImport("gdi32.dll", EntryPoint = "EndDoc")]
		private static extern int Win32EndDoc(IntPtr hdc);

		[DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
		public static extern IntPtr Win32DeleteDC(IntPtr hDc);

		[DllImport("gdi32.dll", EntryPoint = "GetDeviceCaps")]
		public static extern int Win32GetDeviceCaps(IntPtr hDc, int index);
	}
}
