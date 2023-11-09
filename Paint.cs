using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Drawing;
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
        ToolStripMenuItem tsSaveAs,tsFormat,tsJpg,tsPng,tsGif,tsBmp,tsTiff,tsIcon,tsEmf,tsWmf,tsJpeg,tsIco;
        ToolStripMenuItem tsFile, tsEdit, tsHelp, tsNew, tsOpen, tsSave, tsExit, tsUndo, tsRedo, tsPen, tsStyle, tsColor, tsSolid, tsDot, tsDashDotDot, tsAbout;
        ToolStripButton tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit;
        PictureBox pb;
        OpenFileDialog ofd;
        DialogResult result;
        string path = "../../Downloads/";
        string nimi = "";
        ToolStripMenuItem format;
        FolderBrowserDialog fbd;
        public Paint()
        {
            this.Width = 1200;
            this.Height = 900;
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
            //удалить некоторые форматы
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
            tsJpg.CheckOnClick = true;
            tsPng.CheckOnClick = true;
            tsGif.CheckOnClick = true;
            tsBmp.CheckOnClick = true;
            tsTiff.CheckOnClick = true;
            tsIcon.CheckOnClick = true;
            tsEmf.CheckOnClick = true;
            tsWmf.CheckOnClick = true;
            tsJpg.Checked = true;
            tsJpeg.CheckOnClick= true;
            tsIco.CheckOnClick= true;

            format = tsPng;

            foreach (ToolStripMenuItem item in new ToolStripMenuItem[] {tsJpg, tsPng, tsGif, tsBmp, tsTiff, tsIcon, tsEmf, tsWmf, tsJpeg, tsIco})
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
            tsFormat.DropDownItems.AddRange(new ToolStripMenuItem[] {tsJpg, tsPng, tsGif, tsBmp, tsTiff, tsIcon, tsEmf, tsWmf, tsJpeg, tsIco });
            ts.Items.AddRange(new ToolStripButton[] { tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit });

            tsNew.Click += TsNew_Click;
            tsbNew.Click += TsNew_Click;
            tsSolid.Click += Style_Click;
            tsDot.Click += Style_Click;
            tsDashDotDot.Click += Style_Click;
            tsExit.Click += Exit_Click;
            tsbExit.Click += Exit_Click;
            tsJpg.Click += Format_Click;
            tsPng.Click += Format_Click;
            tsGif.Click += Format_Click;
            tsBmp.Click += Format_Click;
            tsTiff.Click += Format_Click;
            tsIcon.Click += Format_Click;
            tsEmf.Click += Format_Click;
            tsWmf.Click += Format_Click;
            tsJpeg.Click += Format_Click;
            tsIco.Click += Format_Click;
            tsOpen.Click +=Open_Click;
            tsbOpen.Click += Open_Click;
            tsSaveAs.Click +=SaveAs_Click;

            pb = new PictureBox();
            pb.Size = new Size(1160,800);
            pb.BackColor = Color.LightGray;

            ofd = new OpenFileDialog()
            {
                FileName = "Valige pildifail",
                Title = "Avage pildifail",
                Filter = "Image Files|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.tiff; *.icon; *.ico; *.emf; *.wmf"

            };
            fbd = new FolderBrowserDialog();
            fbd.Description = "Valige faili salvestamise tee";

            this.Controls.AddRange(new Control[] {ts,MainMenu,pb});
        }

        private void SaveAs_Click(object? sender, EventArgs e)
        {
            if (nimi=="")
            {
                nimi = Interaction.InputBox("Kirjutage failile nimi", "Faili nimi", "img");

            }
            if (nimi!="")
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string path = fbd.SelectedPath;
                    SaveFile();
                }
            }
        }

        private void Open_Click(object? sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = ofd.FileName;
                    using (Stream str = ofd.OpenFile())
                    {
                        pb.Image = new Bitmap(ofd.FileName);
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
        private void Format_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem selected = (ToolStripMenuItem)sender;

            foreach (ToolStripMenuItem item in new ToolStripMenuItem[] { tsJpg, tsPng, tsGif, tsBmp, tsTiff, tsIcon, tsEmf, tsWmf })
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
            }
        }

        private void SaveFile()
        {
            try
            {
                //передавать сюда метод
                Bitmap bmp = new Bitmap(pb.Image);
                bmp.Save(path+"img."+format.Text.ToLower(), ImageFormat.Jpeg);
                bmp.Dispose();
                MessageBox.Show("Fail on salvestatud", "Edu", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Fail on tühi! Joonista midagi.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ImageFormat ImgFormat()
        {//дописать метод
            //switch (format.Text.ToLower())
            //{
            //    case "jpeg":
            //        return ImageFormat.Jpeg;
            //    case "jpeg":
            //        return ImageFormat.Jpeg;
            //}
        }
    }
}