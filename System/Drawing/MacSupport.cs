using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace iTextSharp.Drawing
{
	internal static class MacSupport
	{
		internal static Hashtable contextReference;

		internal static object lockobj;

		internal static Delegate hwnd_delegate;

		static MacSupport()
		{
			MacSupport.contextReference = new Hashtable();
			MacSupport.lockobj = new object();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				Assembly assembly = assemblies[i];
				if (string.Equals(assembly.GetName().Name, "System.Windows.Forms"))
				{
					Type type = assembly.GetType("System.Windows.Forms.XplatUICarbon");
					if (type != null)
					{
						MacSupport.hwnd_delegate = (Delegate)type.GetField("HwndDelegate", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					}
				}
			}
		}

		internal static CocoaContext GetCGContextForNSView(IntPtr handle)
		{
			IntPtr intPtr = MacSupport.objc_msgSend(MacSupport.objc_msgSend(MacSupport.objc_getClass("NSGraphicsContext"), MacSupport.sel_registerName("currentContext")), MacSupport.sel_registerName("graphicsPort"));
			Rect rect = default(Rect);
			MacSupport.CGContextSaveGState(intPtr);
			MacSupport.objc_msgSend_stret(ref rect, handle, MacSupport.sel_registerName("bounds"));
			if (MacSupport.bool_objc_msgSend(handle, MacSupport.sel_registerName("isFlipped")))
			{
				MacSupport.CGContextTranslateCTM(intPtr, rect.origin.x, rect.size.height);
				MacSupport.CGContextScaleCTM(intPtr, 1f, -1f);
			}
			return new CocoaContext(intPtr, (int)rect.size.width, (int)rect.size.height);
		}

		internal static CarbonContext GetCGContextForView(IntPtr handle)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr port = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			intPtr2 = MacSupport.GetControlOwner(handle);
			if (handle == IntPtr.Zero || intPtr2 == IntPtr.Zero)
			{
				port = MacSupport.GetQDGlobalsThePort();
				MacSupport.CreateCGContextForPort(port, ref intPtr);
				Rect rect = MacSupport.CGDisplayBounds(MacSupport.CGMainDisplayID());
				return new CarbonContext(port, intPtr, (int)rect.size.width, (int)rect.size.height);
			}
			QDRect qDRect = default(QDRect);
			Rect rect2 = default(Rect);
			port = MacSupport.GetWindowPort(intPtr2);
			intPtr = MacSupport.GetContext(port);
			MacSupport.GetWindowBounds(intPtr2, 32u, ref qDRect);
			MacSupport.HIViewGetBounds(handle, ref rect2);
			MacSupport.HIViewConvertRect(ref rect2, handle, IntPtr.Zero);
			if (rect2.size.height < 0f)
			{
				rect2.size.height = 0f;
			}
			if (rect2.size.width < 0f)
			{
				rect2.size.width = 0f;
			}
			MacSupport.CGContextTranslateCTM(intPtr, rect2.origin.x, (float)(qDRect.bottom - qDRect.top) - (rect2.origin.y + rect2.size.height));
			Rect rect3 = new Rect(0f, 0f, rect2.size.width, rect2.size.height);
			MacSupport.CGContextSaveGState(intPtr);
			Rectangle[] array = (Rectangle[])MacSupport.hwnd_delegate.DynamicInvoke(new object[]
			{
				handle
			});
			if (array != null && array.Length != 0)
			{
				int num = array.Length;
				MacSupport.CGContextBeginPath(intPtr);
				MacSupport.CGContextAddRect(intPtr, rect3);
				for (int i = 0; i < num; i++)
				{
					MacSupport.CGContextAddRect(intPtr, new Rect((float)array[i].X, rect2.size.height - (float)array[i].Y - (float)array[i].Height, (float)array[i].Width, (float)array[i].Height));
				}
				MacSupport.CGContextClosePath(intPtr);
				MacSupport.CGContextEOClip(intPtr);
			}
			else
			{
				MacSupport.CGContextBeginPath(intPtr);
				MacSupport.CGContextAddRect(intPtr, rect3);
				MacSupport.CGContextClosePath(intPtr);
				MacSupport.CGContextClip(intPtr);
			}
			return new CarbonContext(port, intPtr, (int)rect2.size.width, (int)rect2.size.height);
		}

		internal static IntPtr GetContext(IntPtr port)
		{
			IntPtr zero = IntPtr.Zero;
			object obj = MacSupport.lockobj;
			lock (obj)
			{
				MacSupport.CreateCGContextForPort(port, ref zero);
			}
			return zero;
		}

		internal static void ReleaseContext(IntPtr port, IntPtr context)
		{
			MacSupport.CGContextRestoreGState(context);
			object obj = MacSupport.lockobj;
			lock (obj)
			{
				MacSupport.CFRelease(context);
			}
		}

		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_getClass(string className);

		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_msgSend(IntPtr basePtr, IntPtr selector, string argument);

		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_msgSend(IntPtr basePtr, IntPtr selector);

		[DllImport("libobjc.dylib")]
		public static extern void objc_msgSend_stret(ref Rect arect, IntPtr basePtr, IntPtr selector);

		[DllImport("libobjc.dylib", EntryPoint = "objc_msgSend")]
		public static extern bool bool_objc_msgSend(IntPtr handle, IntPtr selector);

		[DllImport("libobjc.dylib")]
		public static extern IntPtr sel_registerName(string selectorName);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr CGMainDisplayID();

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern Rect CGDisplayBounds(IntPtr display);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewGetBounds(IntPtr vHnd, ref Rect r);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewConvertRect(ref Rect r, IntPtr a, IntPtr b);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetControlOwner(IntPtr aView);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int GetWindowBounds(IntPtr wHnd, uint reg, ref QDRect rect);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetWindowPort(IntPtr hWnd);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetQDGlobalsThePort();

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CreateCGContextForPort(IntPtr port, ref IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CFRelease(IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void QDBeginCGContext(IntPtr port, ref IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void QDEndCGContext(IntPtr port, ref IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int CGContextClipToRect(IntPtr context, Rect clip);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int CGContextClipToRects(IntPtr context, Rect[] clip_rects, int count);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextTranslateCTM(IntPtr context, float tx, float ty);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextScaleCTM(IntPtr context, float x, float y);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextFlush(IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextSynchronize(IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr CGPathCreateMutable();

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGPathAddRects(IntPtr path, IntPtr _void, Rect[] rects, int count);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGPathAddRect(IntPtr path, IntPtr _void, Rect rect);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextAddRects(IntPtr context, Rect[] rects, int count);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextAddRect(IntPtr context, Rect rect);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextBeginPath(IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextClosePath(IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextAddPath(IntPtr context, IntPtr path);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextClip(IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextEOClip(IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextEOFillPath(IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextSaveGState(IntPtr context);

		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextRestoreGState(IntPtr context);
	}
}
