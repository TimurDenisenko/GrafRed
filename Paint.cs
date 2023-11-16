using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using System.Xml.Linq;


namespace GrafRed
{
    public partial class Paint : Form
    {
        MenuStrip MainMenu;
        ToolStripControlHost toolStripControlHost;
        ToolStrip ts;
        ToolStripMenuItem format, tsSaveAs, tsFormat, tsPng, tsGif, tsBmp, tsTiff, tsIcon, tsEmf, tsWmf, tsJpeg, tsFile, tsEdit, tsHelp, tsNew, tsOpen, tsSave, tsExit, tsUndo, tsRedo, tsPen, tsStyle, tsColor, tsSolid, tsDot, tsDashDotDot, tsAbout;
        ToolStripButton tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit;
        PictureBox pb, pb1, pb2,pb3;
        OpenFileDialog ofd;
        DialogResult result;
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        string nimi = "";
        FolderBrowserDialog fbd;
        Panel pl;
        Label lb;
        TrackBar tb;
        int lbx, lby ,lbp;
        bool drawing;
        GraphicsPath currentPath;
        Point oldLocation;
        Pen currentPen;
        ComboBox cb;
        int historyC;
        List<Image> history;
        Colors colors;
        private bool Dragging;
        private int xPos;
        private int yPos;
        public Paint()
        {
            this.Width = 1230;
            this.Height = 930;
            this.Text = "Paint";

            MainMenu = new MenuStrip();
            MainMenu.Location = new Point(0, 0);

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

            cb = new ComboBox();
            cb.DropDownWidth = 5;
            for (int i = 1; i < 101; i++)
            {
                cb.Items.Add(i);
            }
            cb.SelectedText = "Suurus";
            toolStripControlHost = new ToolStripControlHost(cb);
            cb.SelectionChangeCommitted += Cb_SelectionChangeCommitted;

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
            tsJpeg.CheckOnClick = true;

            format = tsPng;

            foreach (ToolStripMenuItem item in new ToolStripMenuItem[] { tsPng, tsGif, tsBmp, tsTiff, tsIcon, tsEmf, tsWmf, tsJpeg, tsSaveAs, tsFormat, tsFile, tsEdit, tsHelp, tsNew, tsOpen, tsSave, tsExit, tsUndo, tsRedo, tsPen, tsStyle, tsColor, tsSolid, tsDot, tsDashDotDot, tsAbout })
            {
                item.Font = new Font("Arial", 9);
                try { item.Image = Image.FromFile("../../../img/" + item.Text.ToLower().Replace(" ", "").Replace("ä", "a") + ".png"); } catch (Exception) { }

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
            tsPen.DropDownItems.Add(toolStripControlHost);
            tsPen.DropDownItems.AddRange(new ToolStripMenuItem[] { tsStyle, tsColor });
            tsStyle.DropDownItems.AddRange(new ToolStripMenuItem[] { tsSolid, tsDot, tsDashDotDot });
            tsHelp.DropDownItems.Add(tsAbout);
            tsFormat.DropDownItems.AddRange(new ToolStripMenuItem[] { tsJpeg, tsPng, tsGif, tsBmp, tsTiff, tsIcon, tsEmf, tsWmf });
            ts.Items.AddRange(new ToolStripButton[] { tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit });

            tsNew.Click += New_Click;
            tsbNew.Click += New_Click;
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
            tsOpen.Click += Open_Click;
            tsbOpen.Click += Open_Click;
            tsSaveAs.Click += SaveAs_Click;
            tsSave.Click += Save_Click;
            tsbSave.Click += Save_Click;
            tsColor.Click += Color_Click;
            tsbColor.Click += Color_Click;
            tsUndo.Click += Undo_Click;
            tsRedo.Click += Redo_Click;

            pb = new PictureBox();
            pb1 = new PictureBox();
            pb2 = new PictureBox();
            pb3 = new PictureBox();
            pb.Size = new Size(1220, 860);
            pb1.Size = new Size(1220, 860);
            pb2.Size = new Size(1220, 860);
            pb3.Size = new Size(1220, 860);
            pb2.Location = new Point(0, 0);
            pb3.Location = new Point(0, 0);
            pb2.Controls.Add(pb);
            pb2.Controls.Add(pb3);
            pb3.BackColor = Color.Transparent;


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
            pl.Location = new Point(pb.Left, pb.Bottom);
            pl.BackColor = Color.LightGray;
            pl.BorderStyle = BorderStyle.Fixed3D;

            tb = new TrackBar();
            tb.Size = new Size(250, 50);
            tb.Location = new Point(pl.Width - tb.Width - 35, 0);
            tb.TickStyle = TickStyle.None;
            tb.Minimum = 1;
            tb.Maximum = 200;
            tb.Value = 100;
            tb.ValueChanged += Tb_ValueChanged;
            pb.MouseMove += Pb_MouseMove;
            pb.MouseUp += Pb_MouseUp;
            pb.MouseDown += Pb_MouseDown;
            pb3.MouseMove += Pb3_MouseMove;
            lbp = tb.Value;

            lb.Location = new Point(ts.Right + 10, 5);
            lb.Text = ($"{lbx}, {lby}, {lbp}%");

            pl.Controls.AddRange(new Control[] { lb, tb });

            drawing = false;
            currentPen = new Pen(Color.Black);
            currentPen.Width = 1;

            history = new List<Image>();

            ControlsAdd(new Control[] { ts, MainMenu, pl,pb2 });
        }
        private void Pb3_MouseMove(object? sender, MouseEventArgs e)
        {
            lbx = e.Location.X - ts.Right;
            lby = e.Location.Y - ts.Top;
            lb.Text = ($"{lbx}, {lby}, {lbp}%");
        }

        private void Redo_Click(object? sender, EventArgs e)
        {
            if (historyC<history.Count - 1)
            {
                pb.Image = new Bitmap(history[++historyC]);
                pb1.Image = pb.Image;
            }
            else MessageBox.Show("Ajalugu on tühi");
        }

        private void Undo_Click(object? sender, EventArgs e)
        {
            if (history.Count != 0 && historyC != 0)
            {
                pb.Image = new Bitmap(history[--historyC]);
                pb1.Image = pb.Image;
            }
            else MessageBox.Show("Ajalugu on tühi");
        }

        private void Color_Click(object? sender, EventArgs e)
        {
            colors = new Colors();
            colors.ShowDialog();
            if (colors.result==DialogResult.OK)
            {
                currentPen.Color = colors.color;
            }
        }

        private void Cb_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            currentPen.Width = int.Parse(cb.SelectedItem.ToString());
        }

        private void Pb_MouseUp(object? sender, MouseEventArgs e)
        {
            Dragging = false;
            history.RemoveRange(historyC + 1, history.Count - historyC - 1);
            history.Add(new Bitmap(pb.Image));
            if (historyC + 1 < 10) historyC++;
            if (history.Count - 1 == 10) history.RemoveAt(0);
            drawing = false;
            try
            {
                if (currentPath!=null) currentPath.Dispose();
                pb1.Image = new Bitmap(pb.Image);
            }
            catch (Exception){  }
        }

        private void Pb_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                Dragging = true;
                xPos = e.X;
                yPos = e.Y;
            }
            if (pb.Image == null)
            {
                MessageBox.Show("Esmalt looge fail");
            }
            if (e.Button == MouseButtons.Left)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
            }
        }

        private void Pb_MouseMove(object? sender, MouseEventArgs e)
        {
            try
            {
                Control c = sender as Control;
                if (Dragging && c != null)
                {
                    int newposLeft = e.X + c.Left - xPos;
                    int newposTop = e.Y + c.Top - yPos;
                    c.Top = newposTop;
                    c.Left = newposLeft;
                }
                if (drawing && pb.Image != null)
                {
                    Graphics g = Graphics.FromImage(pb.Image);
                    currentPath.AddLine(oldLocation, e.Location);
                    g.DrawPath(currentPen, currentPath);
                    oldLocation = e.Location;
                    g.Dispose();
                    pb.Invalidate();
                }
            }
            catch (Exception) { throw; }
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
            if (pb.Image != null)
            {
                result = MessageBox.Show("Kas soovite faili salvestada?", "Salvestada", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
            }
            if (result != null)
            {
                tb.Value = 100;
                lbp = tb.Value;
                lb.Text = ($"{lbx}, {lby}, {lbp}%");
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                history.Clear();
                historyC = 0;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var filePath = ofd.FileName;
                        using (Stream str = ofd.OpenFile())
                        {
                            pb.SizeMode = PictureBoxSizeMode.AutoSize;
                            pb.Image = new Bitmap(ofd.FileName);
                            pb1.Image = new Bitmap(ofd.FileName);
                            history.Add(pb.Image);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Midagi on vale!", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            pb.Size = new Size(1180, 800);
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
            if (pb.Image != null)
            {
                result = MessageBox.Show("Kas soovite faili salvestada?", "Salvestada", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
            }
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
            if (tsSolid.Checked)
            {
                currentPen.DashStyle = DashStyle.Solid;
            }
            if (tsDot.Checked)
            {
                currentPen.DashStyle = DashStyle.Dot;
            }
            if (tsDashDotDot.Checked)
            {
                currentPen.DashStyle = DashStyle.DashDotDot;
            }
        }

        private void New_Click(object? sender, EventArgs e)
        {
            if (pb.Image!=null)
            {
                result = MessageBox.Show("Kas soovite faili salvestada?", "Salvestada", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }

            }
            if (result != DialogResult.Cancel)
            {
                tb.Value = 100;
                lbp = tb.Value;
                lb.Text = ($"{lbx}, {lby}, {lbp}%");
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                pb.Image = Image.FromFile("../../../img/valge.png");
                pb1.Image = Image.FromFile("../../../img/valge.png");
                pb.Invalidate();
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                history.Clear();
                historyC = 0;
                history.Add(pb.Image);
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