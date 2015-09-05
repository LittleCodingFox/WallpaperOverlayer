using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperOverlayer
{
    public partial class frmMain : Plexiglass
    {
        public static int ConfigurationMessage = WindowsMessageHelper.RegisterWindowMessage("__CONFIGURATION__");

        string[] Extensions = new string[] {
            "jpeg", "jpg", "png", "bmp", "gif"
        };

        public PictureBox Picture;
        public List<string> WallpaperNames = new List<string>();
        public int CurrentWallpaper = -1;
        public Timer WallpaperTimer = new Timer();
        frmConfig configForm;

        public frmMain()
        {
            InitializeComponent();

            Picture = new PictureBox();

            Controls.Add(Picture);

            Picture.Size = Size;
            Picture.Location = new Point(0, 0);
            Picture.SizeMode = PictureBoxSizeMode.Zoom;

            try
            {
                DirectoryInfo info = new DirectoryInfo(Environment.CurrentDirectory + "/Wallpapers/");

                foreach(string ext in Extensions)
                {
                    FileInfo[] files = info.GetFiles("*." + ext);


                    foreach(FileInfo fileinfo in files)
                    {
                        WallpaperNames.Add(fileinfo.FullName);
                    }
                }

                System.Random rnd = new System.Random();
                WallpaperNames = WallpaperNames.OrderBy((r => rnd.Next())).ToList();
            }
            catch(Exception)
            {
                Environment.Exit(1);
            }

            LoadNext();

            WallpaperTimer.Interval = 15000;
            WallpaperTimer.Tick += WallpaperTimer_Tick;
            WallpaperTimer.Enabled = true;

            Load += Form1_Load;

            configForm = new frmConfig(this);
        }

        public void UpdatePercent(float Percent)
        {
            Opacity = Percent;
        }

        void Form1_Load(object sender, EventArgs e)
        {
            configForm.Show();
        }

        void WallpaperTimer_Tick(object sender, EventArgs e)
        {
            LoadNext();
        }

        void LoadNext()
        {
            CurrentWallpaper++;

            if (CurrentWallpaper >= WallpaperNames.Count)
                CurrentWallpaper = 0;

            if (WallpaperNames.Count == 0)
            {
                MessageBox.Show("Missing images on Wallpapers folder!");

                Environment.Exit(1);

                return;
            }

            LoadImage(WallpaperNames[CurrentWallpaper]);
        }

        void LoadImage(string name)
        {
            try
            {
                Picture.Image = Image.FromFile(name, true);
            }
            catch(Exception)
            {
            }
        }
    }
}
