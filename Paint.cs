using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using System.Xml.Linq;


namespace GrafRed
{
    public partial class Paint : Form
    {
        MenuStrip MainMenu;
        ToolStrip ts;
        ToolStripMenuItem tsSaveAs,tsFormat,tsPng,tsGif,tsBmp,tsTiff,tsIcon,tsEmf,tsWmf,tsJpeg;
        ToolStripMenuItem tsFile, tsEdit, tsHelp, tsNew, tsOpen, tsSave, tsExit, tsUndo, tsRedo, tsPen, tsStyle, tsColor, tsSolid, tsDot, tsDashDotDot, tsAbout;
        ToolStripButton tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit;
        PictureBox pb, pb1;
        OpenFileDialog ofd;
        DialogResult result;
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        string nimi = "";
        ToolStripMenuItem format;
        FolderBrowserDialog fbd;
        Panel pl;
        Label lb;
        TrackBar tb;
        int lbx, lby ,lbp;
        private double zoom = 1.0;
        public Paint()
        {
            this.Width = 1230;
            this.Height = 930;
            this.Text = "Paint";

            MainMenu = new MenuStrip();
            MainMenu.Location = new Point(0,0);

            tsFile = new ToolStripMenuItem("File");
            tsEdit = new ToolStripMenuItem("Edit");
            tsHelp = new ToolStripMenuItem("Help");
            tsNew = new ToolStripMenuItem("Uus");
            tsOpen = new ToolStripMenuItem("Ava");
            tsExit = new ToolStripMenuItem("Välju");
            tsSave = new ToolStripMenuItem("Salvesta");
            tsUndo = new ToolStripMenuItem("Undo");
            tsRedo = new ToolStripMenuItem("Redo");
            tsPen = new ToolStripMenuItem("Pliiats");
            tsStyle = new ToolStripMenuItem("Stiil");
            tsColor = new ToolStripMenuItem("Värv");
            tsSolid = new ToolStripMenuItem("Solid");
            tsDot = new ToolStripMenuItem("Dot");
            tsDashDotDot = new ToolStripMenuItem("DashDotDot");
            tsAbout = new ToolStripMenuItem("Umbes");

            tsSaveAs = new ToolStripMenuItem("Salvesta nimega");
            tsFormat = new ToolStripMenuItem("Formaat");
            tsPng = new ToolStripMenuItem("Png");
            tsGif = new ToolStripMenuItem("Gif");
            tsBmp = new ToolStripMenuItem("Bmp");
            tsTiff = new ToolStripMenuItem("Tiff");
            tsIcon = new ToolStripMenuItem("Icon");
            tsEmf = new ToolStripMenuItem("Emf");
            tsWmf = new ToolStripMenuItem("Wmf");
            tsJpeg = new ToolStripMenuItem("Jpeg");
            
            tsbNew = new ToolStripButton("Uus");
            tsbOpen = new ToolStripButton("Ava");
            tsbSave = new ToolStripButton("Salvesta");
            tsbColor = new ToolStripButton("Värv");
            tsbExit = new ToolStripButton("Välju");

            ts = new ToolStrip();
            ts.AutoSize = false;
            ts.Size = new Size(180, 180);
            ts.Dock = DockStyle.Left;

            tsNew.ShortcutKeys = Keys.Control | Keys.N;
            tsOpen.ShortcutKeys = Keys.F3;
            tsExit.ShortcutKeys = Keys.Alt | Keys.X;
            tsSave.ShortcutKeys = Keys.F2;
            tsUndo.ShortcutKeys = Keys.Control | Keys.Z;
            tsRedo.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Z;
            tsSaveAs.ShortcutKeys = Keys.Control | Keys.F2;
            tsAbout.ShortcutKeys = Keys.F1;

            tsDot.CheckOnClick = true;
            tsDashDotDot.CheckOnClick = true;
            tsSolid.CheckOnClick = true;
            tsSolid.Checked = true;
            tsPng.CheckOnClick = true;
            tsPng.Checked = true;
            tsGif.CheckOnClick = true;
            tsBmp.CheckOnClick = true;
            tsTiff.CheckOnClick = true;
            tsIcon.CheckOnClick = true;
            tsEmf.CheckOnClick = true;
            tsWmf.CheckOnClick = true;
            tsJpeg.CheckOnClick= true;

            format = tsPng;

            foreach (ToolStripMenuItem item in new ToolStripMenuItem[] { tsPng, tsGif, tsBmp, tsTiff, tsIcon, tsEmf, tsWmf, tsJpeg})
            {
                item.Font = new Font("Arial", 9);
            }
            foreach (ToolStripMenuItem item in new ToolStripMenuItem[] { tsSaveAs,tsFormat, tsFile, tsEdit, tsHelp, tsNew, tsOpen, tsSave, tsExit, tsUndo, tsRedo, tsPen, tsStyle, tsColor, tsSolid, tsDot, tsDashDotDot, tsAbout })
            {
                item.Font = new Font("Arial", 9);
                try { item.Image = Image.FromFile("../../../img/"+item.Text.ToLower().Replace(" ","").Replace("ä","a")+".png"); } catch (Exception) { }
            }

            foreach (ToolStripButton item in new ToolStripButton[] { tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit })
            {
                item.Image = Image.FromFile("../../../img/" + item.Text.ToLower().Replace("ä", "a") + ".png");
                item.DisplayStyle = ToolStripItemDisplayStyle.Image;
                item.ImageScaling = ToolStripItemImageScaling.None;
            }

            MainMenu.Items.AddRange(new ToolStripMenuItem[] { tsFile, tsEdit, tsHelp });

            tsEdit.DropDownItems.AddRange(new ToolStripMenuItem[] { tsUndo, tsRedo, tsPen });
            tsFile.DropDownItems.AddRange(new ToolStripMenuItem[] { tsNew, tsOpen, tsSave, tsSaveAs, tsFormat, tsExit });
            tsPen.DropDownItems.AddRange(new ToolStripMenuItem[] { tsStyle, tsColor });
            tsStyle.DropDownItems.AddRange(new ToolStripMenuItem[] { tsSolid, tsDot, tsDashDotDot });
            tsHelp.DropDownItems.Add(tsAbout);
            tsFormat.DropDownItems.AddRange(new ToolStripMenuItem[] {tsJpeg, tsPng, tsGif, tsBmp, tsTiff, tsIcon, tsEmf, tsWmf });
            ts.Items.AddRange(new ToolStripButton[] { tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit });

            tsNew.Click += TsNew_Click;
            tsbNew.Click += TsNew_Click;
            tsSolid.Click += Style_Click;
            tsDot.Click += Style_Click;
            tsDashDotDot.Click += Style_Click;
            tsExit.Click += Exit_Click;
            tsbExit.Click += Exit_Click;
            tsPng.Click += Format_Click;
            tsGif.Click += Format_Click;
            tsBmp.Click += Format_Click;
            tsTiff.Click += Format_Click;
            tsIcon.Click += Format_Click;
            tsEmf.Click += Format_Click;
            tsWmf.Click += Format_Click;
            tsJpeg.Click += Format_Click;
            tsOpen.Click +=Open_Click;
            tsbOpen.Click += Open_Click;
            tsSaveAs.Click +=SaveAs_Click;
            tsSave.Click += Save_Click;
            tsbSave.Click += Save_Click;

            pb = new PictureBox();
            pb1 = new PictureBox();
            pb.Size = new Size(1180,825);
            pb1.Size = new Size(1180, 825);
            pb.Location = new Point(0, 0);
            pb.BackColor = Color.LightGray;

            ofd = new OpenFileDialog()
            {
                FileName = "Valige pildifail",
                Title = "Avage pildifail",
                Filter = "Image Files|*.jpeg; *.gif; *.bmp; *.png; *.tiff; *.icon; *.emf; *.wmf"

            };
            fbd = new FolderBrowserDialog();
            fbd.Description = "Valige faili salvestamise tee";

            pl = new Panel();
            lb = new Label();

            pl.Size = new Size(pb.Width, 30);
            pl.Location = new Point(pb.Left, pb.Bottom+20);
            pl.BackColor = Color.LightGray;
            pl.BorderStyle = BorderStyle.Fixed3D;

            tb = new TrackBar();
            tb.Size = new Size(250, 50);
            tb.Location = new Point(pl.Width-tb.Width-35, 0);
            tb.TickStyle = TickStyle.None;
            tb.Minimum = 1;
            tb.Maximum = 200;
            tb.Value = 100;
            tb.ValueChanged += Tb_ValueChanged;
            pb.MouseMove += Pb_MouseMove;
            lbp = tb.Value;

            lb.Location = new Point(ts.Right + 10, 5);
            lb.Text = ($"{lbx}, {lby}, {lbp}%");

            pl.Controls.AddRange(new Control[] { lb,tb});
            ControlsAdd(new Control[] { ts, MainMenu, pb, pl});
        }

        private void Pb_MouseMove(object? sender, MouseEventArgs e)
        {
            lbx = e.Location.X - ts.Right;
            lby = e.Location.Y - ts.Top;
            lb.Text = ($"{lbx}, {lby}, {lbp}%");
        }

        private void Tb_ValueChanged(object? sender, EventArgs e)
        {
            if (pb.Image!=null)
            {
                lbp = tb.Value;
                lb.Text = ($"{lbx}, {lby}, {lbp}%");
                Bitmap bmp = new Bitmap(pb1.Image, Convert.ToInt32(pb1.Width * tb.Value / 100), Convert.ToInt32(pb1.Height * tb.Value / 100));
                pb.Image = bmp;
            }
        }

        private void ControlsAdd([Optional] Control[] arrayVisibleTrue, [Optional] Control[] arrayVisibleFalse)
        {
            if (arrayVisibleTrue != null)
            {
                foreach (Control item in arrayVisibleTrue)
                {
                    this.Controls.Add(item);
                }
            }
            if (arrayVisibleFalse != null)
            {
                foreach (Control item in arrayVisibleFalse)
                {
                    this.Controls.Add(item);
                    item.Visible = false;
                }
            }
        }

        private void Save_Click(object? sender, EventArgs e)
        {
            try
            {
                if (nimi == "")
                {
                    nimi = Interaction.InputBox("Kirjutage failile nimi", "Faili nimi", "img");
                }
                SaveFile();
            }
            catch (Exception)
            {
                MessageBox.Show("Midagi on vale!", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAs_Click(object? sender, EventArgs e)
        {
            try
            {
                nimi = Interaction.InputBox("Kirjutage failile nimi", "Faili nimi", "img");
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    path = fbd.SelectedPath;
                    SaveFile();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Midagi on vale!", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Open_Click(object? sender, EventArgs e)
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = ofd.FileName;
                    using (Stream str = ofd.OpenFile())
                    {
                        pb.Image = new Bitmap(ofd.FileName);
                        pb1.Image = new Bitmap(ofd.FileName);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Midagi on vale!", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Format_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem selected = (ToolStripMenuItem)sender;

            foreach (ToolStripMenuItem item in new ToolStripMenuItem[] { tsPng, tsGif, tsBmp, tsTiff, tsIcon, tsEmf, tsWmf, tsJpeg })
            {
                if (item != selected)
                {
                    item.Checked = false;
                }
                else
                {
                    item.Checked = true;
                    format = selected;
                }
            }
        }

        private void Exit_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void Style_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem selected = (ToolStripMenuItem)sender;

            foreach (ToolStripMenuItem item in new ToolStripMenuItem[] { tsSolid, tsDot, tsDashDotDot })
            {
                if (item != selected)
                {
                    item.Checked = false;
                }
                else
                {
                    item.Checked = true;
                }
            }
        }

        private void TsNew_Click(object? sender, EventArgs e)
        {
            result = MessageBox.Show("Kas soovite faili salvestada?", "Salvestada", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                SaveFile();
            }
            if (result!= DialogResult.Cancel)
            {
                pb.Image = null;
                pb.Invalidate();
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }
        }

        private void SaveFile()
        {
            try
            {
                Bitmap bmp = new Bitmap(pb.Image);
                bmp.Save(path+"\\" + nimi+"."+format.Text.ToLower(), ImgFormat());
                MessageBox.Show("Fail on salvestatud", "Edu", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Midagi on vale!", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private ImageFormat ImgFormat()
        {
            switch (format.Text.ToLower())
            {
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "png":
                    return ImageFormat.Png;
                case "gif":
                    return ImageFormat.Gif;
                case "bmp":
                    return ImageFormat.Bmp;
                case "tiff":
                    return ImageFormat.Tiff;
                case "icon":
                    return ImageFormat.Icon;
                case "emf":
                    return ImageFormat.Emf;
                case "wmf":
                    return ImageFormat.Wmf;
                default: 
                    return ImageFormat.Png;
            }
        }
    }
}