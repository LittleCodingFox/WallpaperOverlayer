using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperOverlayer
{
    public partial class frmConfig : Form
    {
        private frmMain MainInstance;

        public frmConfig(frmMain main)
        {
            InitializeComponent();
            MainInstance = main;

            trackBar1.ValueChanged += trackBar1_ValueChanged;
        }

        void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            MainInstance.UpdatePercent(trackBar1.Value / 100.0f);
        }

        public void Init()
        {
            Show();
        }
    }
}
