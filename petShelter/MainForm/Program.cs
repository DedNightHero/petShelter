using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MainForm
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [STAThread]
        static void Main()
        {
            if (System.Diagnostics.Process.GetProcessesByName(Application.ProductName).Length > 1)
            {
                IntPtr hWnd = System.Diagnostics.Process.GetProcessesByName(Application.ProductName)[0].MainWindowHandle;
                SetForegroundWindow(hWnd);
                ShowWindow(hWnd, 5);
                return;
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Authorization());
            }
        }
    }
}