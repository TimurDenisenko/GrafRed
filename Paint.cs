using Microsoft.VisualBasic;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GrafRed
{
    public partial class Paint : Form
    {
        MenuStrip MainMenu;
        ToolStrip ts;
        ToolStripMenuItem tsFile, tsEdit, tsHelp, tsNew, tsOpen, tsSave, tsExit, tsUndo, tsRedo, tsPen, tsStyle, tsColor, tsSolid, tsDot, tsDashDotDot, tsAbout;
        ToolStripButton tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit;
        PictureBox pb;
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
            tsNew.ShortcutKeys = Keys.Control | Keys.N;
            tsNew.Click += TsNew_Click;
            tsOpen = new ToolStripMenuItem("Open");
            tsOpen.ShortcutKeys = Keys.F3;
            tsExit = new ToolStripMenuItem("Exit");
            tsExit.ShortcutKeys = Keys.Alt | Keys.X;
            tsSave = new ToolStripMenuItem("Save");
            tsSave.ShortcutKeys = Keys.F2;
            tsUndo = new ToolStripMenuItem("Undo");
            tsUndo.ShortcutKeys = Keys.Control | Keys.Z;
            tsRedo = new ToolStripMenuItem("Redo");
            tsRedo.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Z;
            tsPen = new ToolStripMenuItem("Pen");
            tsStyle = new ToolStripMenuItem("Style");
            tsColor = new ToolStripMenuItem("Color");
            tsSolid = new ToolStripMenuItem("Solid");
            tsDot = new ToolStripMenuItem("Dot");
            tsDashDotDot = new ToolStripMenuItem("DashDotDot");
            tsAbout = new ToolStripMenuItem("About");

            foreach (ToolStripMenuItem item in new ToolStripMenuItem[] { tsFile, tsEdit, tsHelp, tsNew, tsOpen, tsSave, tsExit, tsUndo, tsRedo, tsPen, tsStyle, tsColor, tsSolid, tsDot, tsDashDotDot, tsAbout })
            {
                item.Font = new Font("Arial", 9);
                try { item.Image = Image.FromFile("../../../img/"+item.Text.ToLower()+".png"); } catch (Exception) { }
            }

            tsbNew = new ToolStripButton("New");
            tsbOpen = new ToolStripButton("Open");
            tsbSave = new ToolStripButton("Save");
            tsbColor = new ToolStripButton("Color");
            tsbExit = new ToolStripButton("Exit");

            MainMenu.Items.AddRange(new ToolStripMenuItem[] { tsFile, tsEdit, tsHelp });

            tsEdit.DropDownItems.AddRange(new ToolStripMenuItem[] { tsUndo, tsRedo, tsPen });
            tsFile.DropDownItems.AddRange(new ToolStripMenuItem[] { tsNew, tsOpen, tsSave, tsExit });
            tsPen.DropDownItems.AddRange(new ToolStripMenuItem[] { tsStyle, tsColor });
            tsStyle.DropDownItems.AddRange(new ToolStripMenuItem[] { tsSolid, tsDot, tsDashDotDot });
            tsHelp.DropDownItems.Add(tsAbout);

            ts = new ToolStrip();
            ts.AutoSize = false;
            ts.Size = new Size(180, 180);
            ts.Dock = DockStyle.Left;

            ts.Items.AddRange(new ToolStripButton[] { tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit });

            foreach (ToolStripButton item in new ToolStripButton[] { tsbNew, tsbOpen, tsbSave, tsbColor, tsbExit })
            {
                item.Image = Image.FromFile("../../../img/"+item.Text.ToLower()+".png");
                item.DisplayStyle = ToolStripItemDisplayStyle.Image;
                item.ImageScaling = ToolStripItemImageScaling.None;
            }

            pb = new PictureBox();
            pb.Size = new Size(1160,800);
            pb.BackColor = Color.LightGray;

            this.Controls.AddRange(new Control[] {ts,MainMenu,pb});
        }

        private void TsNew_Click(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Kas soovite faili salvestada?", "Salvestada", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {

                Bitmap bmp = new Bitmap(pb.Image);
                bmp.Save(@"..\..\Downloads", ImageFormat.Jpeg);
                bmp.Dispose();
            }
            if (result!= DialogResult.Cancel)
            {
                pb.Image = null;
                pb.Invalidate();
            }
        }
    }
}