using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;

namespace SpecialServices
{
	//SingleProgamInstance uses a mutex synchronization object
	// to ensure that only one copy of process is running at
	// a particular time.  It also allows for UI identification
	// of the intial process by bring that window to the foreground.
	public class SingleProgramInstance : IDisposable
	{
		#region Platform Invoke

		//Win32 API calls necesary to raise an unowned processs main window
		[DllImport("user32.dll")] 
		private static extern bool SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll")] 
		private static extern bool ShowWindowAsync(IntPtr hWnd,int nCmdShow);
		[DllImport("user32.dll")] 
		private static extern bool IsIconic(IntPtr hWnd);

		private const int SW_RESTORE = 9;

		#endregion

		private Mutex processSync;

		public SingleProgramInstance()
			: this(string.Empty)
		{
		}

		public SingleProgramInstance(string identifier)
		{	
			processSync = new Mutex(false,
				Assembly.GetExecutingAssembly().GetName().Name + identifier);
		}

		~SingleProgramInstance()
		{
			//Release mutex (if necessary) 
			//This should have been accomplished using Dispose() 
			this.Release();
		}

		public bool IsSingleInstance
		{
			get
			{
				if (processSync.WaitOne(0, false))
					return true;
				else
					return false;
			}
		}

		public void RaiseOtherProcess()
		{
			Process proc = Process.GetCurrentProcess();
			// Using Process.ProcessName does not function properly when
			// the name exceeds 15 characters. Using the assembly name
			// takes care of this problem and is more accruate than other
			// work arounds.
			string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
			foreach (Process otherProc in Process.GetProcessesByName(assemblyName))
			{
				//ignore this process
				if (proc.Id != otherProc.Id)
				{
					// Found a "same named process".
					// Assume it is the one we want brought to the foreground.
					// Use the Win32 API to bring it to the foreground.
					IntPtr hWnd = otherProc.MainWindowHandle;
					if (IsIconic(hWnd))
						ShowWindowAsync(hWnd,SW_RESTORE);
					SetForegroundWindow(hWnd);
					return;
				}
			}
		}

		#region Implementation of IDisposable

		private void Release()
		{
			try
			{
				if (processSync.WaitOne(0, false))
				{
					//If we owne the mutex than release it so that
					// other "same" processes can now start.
					processSync.ReleaseMutex();
				}
				processSync.Close();
			}
			catch { }
		}

		public void Dispose()
		{
			//release mutex (if necessary) and notify 
			// the garbage collector to ignore the destructor
			this.Release();
			GC.SuppressFinalize(this);
		}
	
		#endregion
	}
}

	