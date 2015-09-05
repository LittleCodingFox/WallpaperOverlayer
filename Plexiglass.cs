using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WallpaperOverlayer
{
    enum WindowLongFlags : int
    {
        GWL_EXSTYLE = -20,
        GWLP_HINSTANCE = -6,
        GWLP_HWNDPARENT = -8,
        GWL_ID = -12,
        GWL_STYLE = -16,
        GWL_USERDATA = -21,
        GWL_WNDPROC = -4,
        DWLP_USER = 0x8,
        DWLP_MSGRESULT = 0x0,
        DWLP_DLGPROC = 0x4
    };

    enum WindowStyleExFlags : int
    {
        WS_TRANSPARENT = 0x20,
        WS_LAYERED = 0x80000
    };
    
    enum LWA : int
    {
        ColorKey = 0x1,
        Alpha = 0x2
    };
    
    public class Plexiglass : Form
    {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll")]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
        
        [DllImport("user32.dll")]
        static extern Boolean SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);

        public Plexiglass()
        {
            FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;
            StartPosition = FormStartPosition.Manual;
            AutoScaleMode = AutoScaleMode.None;
            Location = new Point(0, 0);
            TopMost = true;
            ClientSize = Screen.PrimaryScreen.Bounds.Size;

            BackColor = Color.FromArgb(1, 2, 3);
            TransparencyKey = this.BackColor;

            Opacity = 0.5f;

            Load += Plexiglass_Load;
        }

        void Plexiglass_Load(object sender, EventArgs e)
        {
            SetWindowLong(Handle, (int)WindowLongFlags.GWL_EXSTYLE, GetWindowLong(Handle, (int)WindowLongFlags.GWL_EXSTYLE) | (int)WindowStyleExFlags.WS_LAYERED | (int)WindowStyleExFlags.WS_TRANSPARENT);

            //MakeTransparent(Handle, 0.6f);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        public void MakeTransparent(IntPtr Handle, float Percent)
        {
            SetWindowLong(Handle, (int)WindowLongFlags.GWL_EXSTYLE, GetWindowLong(Handle, (int)WindowLongFlags.GWL_EXSTYLE) | (int)WindowStyleExFlags.WS_LAYERED | (int)WindowStyleExFlags.WS_TRANSPARENT);

            SetLayeredWindowAttributes(Handle, 0, (byte)(Percent * 255), LWA.Alpha);
        }
    }
}
