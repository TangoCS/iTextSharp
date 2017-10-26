using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace iTextSharp.Drawing
{
	internal sealed class ComIStreamMarshaler : ICustomMarshaler
	{
		private delegate int QueryInterfaceDelegate(IntPtr @this, [In] ref Guid riid, IntPtr ppvObject);

		private delegate int AddRefDelegate(IntPtr @this);

		private delegate int ReleaseDelegate(IntPtr @this);

		private delegate int ReadDelegate(IntPtr @this, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		private delegate int WriteDelegate(IntPtr @this, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pv, int cb, IntPtr pcbWritten);

		private delegate int SeekDelegate(IntPtr @this, long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		private delegate int SetSizeDelegate(IntPtr @this, long libNewSize);

		private delegate int CopyToDelegate(IntPtr @this, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Drawing.ComIStreamMarshaler")] IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		private delegate int CommitDelegate(IntPtr @this, int grfCommitFlags);

		private delegate int RevertDelegate(IntPtr @this);

		private delegate int LockRegionDelegate(IntPtr @this, long libOffset, long cb, int dwLockType);

		private delegate int UnlockRegionDelegate(IntPtr @this, long libOffset, long cb, int dwLockType);

		private delegate int StatDelegate(IntPtr @this, out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag);

		private delegate int CloneDelegate(IntPtr @this, out IntPtr ppstm);

		[StructLayout(LayoutKind.Sequential)]
		private sealed class IStreamInterface
		{
			internal IntPtr lpVtbl;

			internal IntPtr gcHandle;
		}

		[StructLayout(LayoutKind.Sequential)]
		private sealed class IStreamVtbl
		{
			internal ComIStreamMarshaler.QueryInterfaceDelegate QueryInterface;

			internal ComIStreamMarshaler.AddRefDelegate AddRef;

			internal ComIStreamMarshaler.ReleaseDelegate Release;

			internal ComIStreamMarshaler.ReadDelegate Read;

			internal ComIStreamMarshaler.WriteDelegate Write;

			internal ComIStreamMarshaler.SeekDelegate Seek;

			internal ComIStreamMarshaler.SetSizeDelegate SetSize;

			internal ComIStreamMarshaler.CopyToDelegate CopyTo;

			internal ComIStreamMarshaler.CommitDelegate Commit;

			internal ComIStreamMarshaler.RevertDelegate Revert;

			internal ComIStreamMarshaler.LockRegionDelegate LockRegion;

			internal ComIStreamMarshaler.UnlockRegionDelegate UnlockRegion;

			internal ComIStreamMarshaler.StatDelegate Stat;

			internal ComIStreamMarshaler.CloneDelegate Clone;
		}

		private sealed class ManagedToNativeWrapper
		{
			[StructLayout(LayoutKind.Sequential)]
			private sealed class ReleaseSlot
			{
				internal ComIStreamMarshaler.ReleaseDelegate Release;
			}

			private static readonly Guid IID_IUnknown;

			private static readonly Guid IID_IStream;

			private static readonly MethodInfo exceptionGetHResult;

			private static readonly ComIStreamMarshaler.IStreamVtbl managedVtable;

			private static IntPtr comVtable;

			private static int vtableRefCount;

			private IStream managedInterface;

			private IntPtr comInterface;

			private GCHandle gcHandle;

			private int refCount = 1;

			static ManagedToNativeWrapper()
			{
				ComIStreamMarshaler.ManagedToNativeWrapper.IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");
				ComIStreamMarshaler.ManagedToNativeWrapper.IID_IStream = new Guid("0000000C-0000-0000-C000-000000000046");
				ComIStreamMarshaler.ManagedToNativeWrapper.exceptionGetHResult = typeof(Exception).GetProperty("HResult", (BindingFlags)69686, null, typeof(int), new Type[0], null).GetGetMethod(true);
				EventHandler eventHandler = new EventHandler(ComIStreamMarshaler.ManagedToNativeWrapper.OnShutdown);
				AppDomain expr_66 = AppDomain.CurrentDomain;
				expr_66.DomainUnload += eventHandler;
				expr_66.ProcessExit += eventHandler;
				ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable = new ComIStreamMarshaler.IStreamVtbl
				{
					QueryInterface = new ComIStreamMarshaler.QueryInterfaceDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.QueryInterface),
					AddRef = new ComIStreamMarshaler.AddRefDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.AddRef),
					Release = new ComIStreamMarshaler.ReleaseDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Release),
					Read = new ComIStreamMarshaler.ReadDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Read),
					Write = new ComIStreamMarshaler.WriteDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Write),
					Seek = new ComIStreamMarshaler.SeekDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Seek),
					SetSize = new ComIStreamMarshaler.SetSizeDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.SetSize),
					CopyTo = new ComIStreamMarshaler.CopyToDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.CopyTo),
					Commit = new ComIStreamMarshaler.CommitDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Commit),
					Revert = new ComIStreamMarshaler.RevertDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Revert),
					LockRegion = new ComIStreamMarshaler.LockRegionDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.LockRegion),
					UnlockRegion = new ComIStreamMarshaler.UnlockRegionDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.UnlockRegion),
					Stat = new ComIStreamMarshaler.StatDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Stat),
					Clone = new ComIStreamMarshaler.CloneDelegate(ComIStreamMarshaler.ManagedToNativeWrapper.Clone)
				};
				ComIStreamMarshaler.ManagedToNativeWrapper.CreateVtable();
			}

			private ManagedToNativeWrapper(IStream managedInterface)
			{
				ComIStreamMarshaler.IStreamVtbl streamVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (streamVtbl)
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && ComIStreamMarshaler.ManagedToNativeWrapper.comVtable == IntPtr.Zero)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.CreateVtable();
					}
					ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount++;
				}
				try
				{
					this.managedInterface = managedInterface;
					this.gcHandle = GCHandle.Alloc(this);
					ComIStreamMarshaler.IStreamInterface expr_6B = new ComIStreamMarshaler.IStreamInterface();
					expr_6B.lpVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.comVtable;
					expr_6B.gcHandle = (IntPtr)this.gcHandle;
					this.comInterface = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ComIStreamMarshaler.IStreamInterface)));
					Marshal.StructureToPtr(expr_6B, this.comInterface, false);
				}
				catch
				{
					this.Dispose();
					throw;
				}
			}

			private void Dispose()
			{
				if (this.gcHandle.IsAllocated)
				{
					this.gcHandle.Free();
				}
				if (this.comInterface != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.comInterface);
					this.comInterface = IntPtr.Zero;
				}
				this.managedInterface = null;
				ComIStreamMarshaler.IStreamVtbl streamVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (streamVtbl)
				{
					if (--ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && Environment.HasShutdownStarted)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.DisposeVtable();
					}
				}
			}

			private static void OnShutdown(object sender, EventArgs e)
			{
				ComIStreamMarshaler.IStreamVtbl streamVtbl = ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable;
				lock (streamVtbl)
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.vtableRefCount == 0 && ComIStreamMarshaler.ManagedToNativeWrapper.comVtable != IntPtr.Zero)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.DisposeVtable();
					}
				}
			}

			private static void CreateVtable()
			{
				ComIStreamMarshaler.ManagedToNativeWrapper.comVtable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ComIStreamMarshaler.IStreamVtbl)));
				Marshal.StructureToPtr(ComIStreamMarshaler.ManagedToNativeWrapper.managedVtable, ComIStreamMarshaler.ManagedToNativeWrapper.comVtable, false);
			}

			private static void DisposeVtable()
			{
				Marshal.DestroyStructure(ComIStreamMarshaler.ManagedToNativeWrapper.comVtable, typeof(ComIStreamMarshaler.IStreamVtbl));
				Marshal.FreeHGlobal(ComIStreamMarshaler.ManagedToNativeWrapper.comVtable);
				ComIStreamMarshaler.ManagedToNativeWrapper.comVtable = IntPtr.Zero;
			}

			internal static IStream GetUnderlyingInterface(IntPtr comInterface, bool outParam)
			{
				if (Marshal.ReadIntPtr(comInterface) == ComIStreamMarshaler.ManagedToNativeWrapper.comVtable)
				{
					IStream arg_27_0 = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(comInterface).managedInterface;
					if (outParam)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.Release(comInterface);
					}
					return arg_27_0;
				}
				return null;
			}

			internal static IntPtr GetInterface(IStream managedInterface)
			{
				if (managedInterface == null)
				{
					return IntPtr.Zero;
				}
				IntPtr underlyingInterface;
				if ((underlyingInterface = ComIStreamMarshaler.NativeToManagedWrapper.GetUnderlyingInterface(managedInterface)) == IntPtr.Zero)
				{
					underlyingInterface = new ComIStreamMarshaler.ManagedToNativeWrapper(managedInterface).comInterface;
				}
				return underlyingInterface;
			}

			internal static void ReleaseInterface(IntPtr comInterface)
			{
				if (comInterface != IntPtr.Zero)
				{
					IntPtr intPtr = Marshal.ReadIntPtr(comInterface);
					if (intPtr == ComIStreamMarshaler.ManagedToNativeWrapper.comVtable)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper.Release(comInterface);
						return;
					}
					((ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseSlot)Marshal.PtrToStructure((IntPtr)((long)intPtr + (long)(IntPtr.Size * 2)), typeof(ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseSlot))).Release(comInterface);
				}
			}

			private static int GetHRForException(Exception e)
			{
				return (int)ComIStreamMarshaler.ManagedToNativeWrapper.exceptionGetHResult.Invoke(e, null);
			}

			private static ComIStreamMarshaler.ManagedToNativeWrapper GetObject(IntPtr @this)
			{
				return (ComIStreamMarshaler.ManagedToNativeWrapper)((GCHandle)Marshal.ReadIntPtr(@this, IntPtr.Size)).Target;
			}

			private static int QueryInterface(IntPtr @this, ref Guid riid, IntPtr ppvObject)
			{
				int result;
				try
				{
					if (ComIStreamMarshaler.ManagedToNativeWrapper.IID_IUnknown.Equals(riid) || ComIStreamMarshaler.ManagedToNativeWrapper.IID_IStream.Equals(riid))
					{
						Marshal.WriteIntPtr(ppvObject, @this);
						ComIStreamMarshaler.ManagedToNativeWrapper.AddRef(@this);
						result = 0;
					}
					else
					{
						Marshal.WriteIntPtr(ppvObject, IntPtr.Zero);
						result = -2147467262;
					}
				}
				catch (Exception arg_4F_0)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_4F_0);
				}
				return result;
			}

			private static int AddRef(IntPtr @this)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper @object = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this);
					ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper = @object;
					lock (managedToNativeWrapper)
					{
						ComIStreamMarshaler.ManagedToNativeWrapper expr_14 = @object;
						num = expr_14.refCount + 1;
						expr_14.refCount = num;
						num = num;
					}
				}
				catch
				{
					num = 0;
				}
				return num;
			}

			private static int Release(IntPtr @this)
			{
				int num;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper @object = ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this);
					ComIStreamMarshaler.ManagedToNativeWrapper managedToNativeWrapper = @object;
					lock (managedToNativeWrapper)
					{
						if (@object.refCount != 0)
						{
							ComIStreamMarshaler.ManagedToNativeWrapper expr_1C = @object;
							num = expr_1C.refCount - 1;
							expr_1C.refCount = num;
							if (num == 0)
							{
								@object.Dispose();
							}
						}
						num = @object.refCount;
					}
				}
				catch
				{
					num = 0;
				}
				return num;
			}

			private static int Read(IntPtr @this, byte[] pv, int cb, IntPtr pcbRead)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Read(pv, cb, pcbRead);
					result = 0;
				}
				catch (Exception arg_17_0)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_17_0);
				}
				return result;
			}

			private static int Write(IntPtr @this, byte[] pv, int cb, IntPtr pcbWritten)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Write(pv, cb, pcbWritten);
					result = 0;
				}
				catch (Exception arg_17_0)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_17_0);
				}
				return result;
			}

			private static int Seek(IntPtr @this, long dlibMove, int dwOrigin, IntPtr plibNewPosition)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Seek(dlibMove, dwOrigin, plibNewPosition);
					result = 0;
				}
				catch (Exception arg_17_0)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_17_0);
				}
				return result;
			}

			private static int SetSize(IntPtr @this, long libNewSize)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.SetSize(libNewSize);
					result = 0;
				}
				catch (Exception arg_15_0)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_15_0);
				}
				return result;
			}

			private static int CopyTo(IntPtr @this, IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.CopyTo(pstm, cb, pcbRead, pcbWritten);
					result = 0;
				}
				catch (Exception arg_19_0)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_19_0);
				}
				return result;
			}

			private static int Commit(IntPtr @this, int grfCommitFlags)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Commit(grfCommitFlags);
					result = 0;
				}
				catch (Exception arg_15_0)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_15_0);
				}
				return result;
			}

			private static int Revert(IntPtr @this)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Revert();
					result = 0;
				}
				catch (Exception arg_14_0)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_14_0);
				}
				return result;
			}

			private static int LockRegion(IntPtr @this, long libOffset, long cb, int dwLockType)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.LockRegion(libOffset, cb, dwLockType);
					result = 0;
				}
				catch (Exception arg_17_0)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_17_0);
				}
				return result;
			}

			private static int UnlockRegion(IntPtr @this, long libOffset, long cb, int dwLockType)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.UnlockRegion(libOffset, cb, dwLockType);
					result = 0;
				}
				catch (Exception arg_17_0)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_17_0);
				}
				return result;
			}

			private static int Stat(IntPtr @this, out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
			{
				int result;
				try
				{
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Stat(out pstatstg, grfStatFlag);
					result = 0;
				}
				catch (Exception arg_1D_0)
				{
					pstatstg = default(System.Runtime.InteropServices.ComTypes.STATSTG);
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_1D_0);
				}
				return result;
			}

			private static int Clone(IntPtr @this, out IntPtr ppstm)
			{
				ppstm = IntPtr.Zero;
				int result;
				try
				{
					IStream stream;
					ComIStreamMarshaler.ManagedToNativeWrapper.GetObject(@this).managedInterface.Clone(out stream);
					ppstm = ComIStreamMarshaler.ManagedToNativeWrapper.GetInterface(stream);
					result = 0;
				}
				catch (Exception arg_25_0)
				{
					result = ComIStreamMarshaler.ManagedToNativeWrapper.GetHRForException(arg_25_0);
				}
				return result;
			}
		}

		private sealed class NativeToManagedWrapper : IStream
		{
			private IntPtr comInterface;

			private ComIStreamMarshaler.IStreamVtbl managedVtable;

			private NativeToManagedWrapper(IntPtr comInterface, bool outParam)
			{
				this.comInterface = comInterface;
				this.managedVtable = (ComIStreamMarshaler.IStreamVtbl)Marshal.PtrToStructure(Marshal.ReadIntPtr(comInterface), typeof(ComIStreamMarshaler.IStreamVtbl));
				if (!outParam)
				{
					this.managedVtable.AddRef(comInterface);
				}
			}

			~NativeToManagedWrapper()
			{
				this.Dispose(false);
			}

			private void Dispose(bool disposing)
			{
				this.managedVtable.Release(this.comInterface);
				if (disposing)
				{
					this.comInterface = IntPtr.Zero;
					this.managedVtable = null;
					GC.SuppressFinalize(this);
				}
			}

			internal static IntPtr GetUnderlyingInterface(IStream managedInterface)
			{
				if (managedInterface is ComIStreamMarshaler.NativeToManagedWrapper)
				{
					ComIStreamMarshaler.NativeToManagedWrapper nativeToManagedWrapper = (ComIStreamMarshaler.NativeToManagedWrapper)managedInterface;
					nativeToManagedWrapper.managedVtable.AddRef(nativeToManagedWrapper.comInterface);
					return nativeToManagedWrapper.comInterface;
				}
				return IntPtr.Zero;
			}

			internal static IStream GetInterface(IntPtr comInterface, bool outParam)
			{
				if (comInterface == IntPtr.Zero)
				{
					return null;
				}
				return ComIStreamMarshaler.ManagedToNativeWrapper.GetUnderlyingInterface(comInterface, outParam) ?? new ComIStreamMarshaler.NativeToManagedWrapper(comInterface, outParam);
			}

			internal static void ReleaseInterface(IStream managedInterface)
			{
				if (managedInterface is ComIStreamMarshaler.NativeToManagedWrapper)
				{
					((ComIStreamMarshaler.NativeToManagedWrapper)managedInterface).Dispose(true);
				}
			}

			private static void ThrowExceptionForHR(int result)
			{
				if (result < 0)
				{
					throw new COMException(null, result);
				}
			}

			public void Read(byte[] pv, int cb, IntPtr pcbRead)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Read(this.comInterface, pv, cb, pcbRead));
			}

			public void Write(byte[] pv, int cb, IntPtr pcbWritten)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Write(this.comInterface, pv, cb, pcbWritten));
			}

			public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Seek(this.comInterface, dlibMove, dwOrigin, plibNewPosition));
			}

			public void SetSize(long libNewSize)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.SetSize(this.comInterface, libNewSize));
			}

			public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.CopyTo(this.comInterface, pstm, cb, pcbRead, pcbWritten));
			}

			public void Commit(int grfCommitFlags)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Commit(this.comInterface, grfCommitFlags));
			}

			public void Revert()
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Revert(this.comInterface));
			}

			public void LockRegion(long libOffset, long cb, int dwLockType)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.LockRegion(this.comInterface, libOffset, cb, dwLockType));
			}

			public void UnlockRegion(long libOffset, long cb, int dwLockType)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.UnlockRegion(this.comInterface, libOffset, cb, dwLockType));
			}

			public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
			{
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Stat(this.comInterface, out pstatstg, grfStatFlag));
			}

			public void Clone(out IStream ppstm)
			{
				IntPtr intPtr;
				ComIStreamMarshaler.NativeToManagedWrapper.ThrowExceptionForHR(this.managedVtable.Clone(this.comInterface, out intPtr));
				ppstm = ComIStreamMarshaler.NativeToManagedWrapper.GetInterface(intPtr, true);
			}
		}

		private const int S_OK = 0;

		private const int E_NOINTERFACE = -2147467262;

		private static readonly ComIStreamMarshaler defaultInstance = new ComIStreamMarshaler();

		private ComIStreamMarshaler()
		{
		}

		private static ICustomMarshaler GetInstance(string cookie)
		{
			return ComIStreamMarshaler.defaultInstance;
		}

		public IntPtr MarshalManagedToNative(object managedObj)
		{
			return ComIStreamMarshaler.ManagedToNativeWrapper.GetInterface((IStream)managedObj);
		}

		public void CleanUpNativeData(IntPtr pNativeData)
		{
			ComIStreamMarshaler.ManagedToNativeWrapper.ReleaseInterface(pNativeData);
		}

		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return ComIStreamMarshaler.NativeToManagedWrapper.GetInterface(pNativeData, false);
		}

		public void CleanUpManagedData(object managedObj)
		{
			ComIStreamMarshaler.NativeToManagedWrapper.ReleaseInterface((IStream)managedObj);
		}

		public int GetNativeDataSize()
		{
			return -1;
		}
	}
}
