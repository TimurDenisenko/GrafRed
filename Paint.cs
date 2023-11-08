using Microsoft.VisualBasic;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GrafRed
{
    public partial class Paint : Form
    {
        MenuStrip MainMenu;
        ToolStrip ts;
        ToolStripMenuItem tsSaveAs,tsFormat,tsJpg,tsPng,tsGif,tsBmp,tsTiff,tsIcon,tsEmf,tsWmf;
        ToolStripMenuItem tsFile, tsEdit, tsHelp, tsNew, tsOpen, tsSave, tsExit, tsUndo, tsRedo, tsPen, tsStyle, tsColor, tsSolid, tsDot, tsDashDotDot, tsAbout;
        ToolStripButton tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit;
        PictureBox pb;
        DialogResult result;
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
            tsNew = new ToolStripMenuItem("New");
            tsOpen = new ToolStripMenuItem("Open");
            tsExit = new ToolStripMenuItem("Exit");
            tsSave = new ToolStripMenuItem("Save");
            tsUndo = new ToolStripMenuItem("Undo");
            tsRedo = new ToolStripMenuItem("Redo");
            tsPen = new ToolStripMenuItem("Pen");
            tsStyle = new ToolStripMenuItem("Style");
            tsColor = new ToolStripMenuItem("Color");
            tsSolid = new ToolStripMenuItem("Solid");
            tsDot = new ToolStripMenuItem("Dot");
            tsDashDotDot = new ToolStripMenuItem("DashDotDot");
            tsAbout = new ToolStripMenuItem("About");

            tsSaveAs = new ToolStripMenuItem("Save as");
            tsFormat = new ToolStripMenuItem("Format");
            tsJpg = new ToolStripMenuItem("Jpg");
            tsPng = new ToolStripMenuItem("Png");
            tsGif = new ToolStripMenuItem("Gif");
            tsBmp = new ToolStripMenuItem("Bmp");
            tsTiff = new ToolStripMenuItem("Tiff");
            tsIcon = new ToolStripMenuItem("Icon");
            tsEmf = new ToolStripMenuItem("Emf");
            tsWmf = new ToolStripMenuItem("Wmf");

            tsbNew = new ToolStripButton("New");
            tsbOpen = new ToolStripButton("Open");
            tsbSave = new ToolStripButton("Save");
            tsbColor = new ToolStripButton("Color");
            tsbExit = new ToolStripButton("Exit");

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

            foreach (ToolStripMenuItem item in new ToolStripMenuItem[] {tsJpg, tsPng, tsGif, tsBmp, tsTiff, tsIcon, tsEmf, tsWmf})
            {
                item.Font = new Font("Arial", 9);
            }
            foreach (ToolStripMenuItem item in new ToolStripMenuItem[] { tsSaveAs,tsFormat, tsFile, tsEdit, tsHelp, tsNew, tsOpen, tsSave, tsExit, tsUndo, tsRedo, tsPen, tsStyle, tsColor, tsSolid, tsDot, tsDashDotDot, tsAbout })
            {
                item.Font = new Font("Arial", 9);
                try { item.Image = Image.FromFile("../../../img/"+item.Text.ToLower().Replace(" ","")+".png"); } catch (Exception) { }
            }

            foreach (ToolStripButton item in new ToolStripButton[] { tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit })
            {
                item.Image = Image.FromFile("../../../img/" + item.Text.ToLower() + ".png");
                item.DisplayStyle = ToolStripItemDisplayStyle.Image;
                item.ImageScaling = ToolStripItemImageScaling.None;
            }

            MainMenu.Items.AddRange(new ToolStripMenuItem[] { tsFile, tsEdit, tsHelp });

            tsEdit.DropDownItems.AddRange(new ToolStripMenuItem[] { tsUndo, tsRedo, tsPen });
            tsFile.DropDownItems.AddRange(new ToolStripMenuItem[] { tsNew, tsOpen, tsSave, tsSaveAs, tsFormat, tsExit });
            tsPen.DropDownItems.AddRange(new ToolStripMenuItem[] { tsStyle, tsColor });
            tsStyle.DropDownItems.AddRange(new ToolStripMenuItem[] { tsSolid, tsDot, tsDashDotDot });
            tsHelp.DropDownItems.Add(tsAbout);
            tsFormat.DropDownItems.AddRange(new ToolStripMenuItem[] {tsJpg, tsPng, tsGif, tsBmp, tsTiff, tsIcon, tsEmf, tsWmf });
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

            pb = new PictureBox();
            pb.Size = new Size(1160,800);
            pb.BackColor = Color.LightGray;

            this.Controls.AddRange(new Control[] {ts,MainMenu,pb});
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
                Bitmap bmp = new Bitmap(pb.Image);
                bmp.Save("../../Downloads/img.jpg", ImageFormat.Jpeg);
                bmp.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Fail on tühi! Joonista midagi.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}