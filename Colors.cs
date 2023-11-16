using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafRed
{
    public partial class Colors : Form
    {
        Label lbRed, lbGreen, lbBlue;
        HScrollBar sbRed, sbGreen, sbBlue;
        NumericUpDown nudRed, nudGreen, nudBlue;
        PictureBox pb;
        Button btnOk, btnCancel, btnOther;
        ColorDialog cd;
        public Color color { get; set; }
        public DialogResult result { get; set; }

        public Colors()
        {
            this.Width = 550;
            this.Height = 200;
            this.Text = "Colors";
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lbRed = new Label()
            {
                Text = "Punane",
                Location = new Point(20, 20)
            };
            lbGreen = new Label()
            {
                Text = "Roheline",
                Location = new Point(lbRed.Left, lbRed.Top+30)
            };
            lbBlue = new Label()
            {
                Text = "Sinine",
                Location = new Point(lbRed.Left, lbGreen.Top + 30)
            };
            foreach (Label item in new Label[] { lbRed, lbGreen, lbBlue })
            {
                item.Font = new Font("Arial", 10);
                item.Width = 65;
            }


            sbRed = new HScrollBar();
            sbRed.Location = new Point(lbRed.Right, lbRed.Top);
            sbGreen = new HScrollBar();
            sbGreen.Location = new Point(lbRed.Right, lbGreen.Top);
            sbBlue = new HScrollBar();
            sbBlue.Location = new Point(lbRed.Right, lbBlue.Top);
            foreach(HScrollBar item in new HScrollBar[] { sbRed , sbBlue, sbGreen})
            {
                item.Minimum = 0;
                item.Maximum = 255;
                item.LargeChange = 1;
                item.Width = 200;
                item.ValueChanged += Colors_ValueChanged;
            }


            nudRed = new NumericUpDown();
            nudRed.Location = new Point(sbRed.Right+10 , sbRed.Top);
            nudGreen = new NumericUpDown();
            nudGreen.Location = new Point(sbRed.Right + 10, lbGreen.Top);
            nudBlue = new NumericUpDown();
            nudBlue.Location = new Point(sbRed.Right + 10, sbBlue.Top);
            foreach (NumericUpDown item in new NumericUpDown[] { nudRed, nudBlue, nudGreen })
            {
                item.Minimum = 0;
                item.Maximum = 255;
                item.Increment = 1;
                item.Width = 50;
                item.ValueChanged += Colors_ValueChanged;
            }


            pb = new PictureBox();
            pb.Size = new Size(nudRed.Height*3+15, nudRed.Height * 3+15);
            pb.Location = new Point(nudRed.Right+20, lbRed.Top);
            pb.BackColor = Color.Black;

            btnOk = new Button();
            btnOk.Text = "Ok";
            btnOk.Location = new Point(lbRed.Left, Height-80);
            btnOk.Click += BtnOk_Click;
            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(lbRed.Left+100, btnOk.Top);
            btnCancel.Click += BtnCancel_Click;
            btnOther = new Button();
            btnOther.Text = "Lihtne valik";
            btnOther.Location = new Point(pb.Left, btnOk.Top);
            btnOther.Width = pb.Width;
            btnOther.Click += BtnOther_Click;
            cd = new ColorDialog();

            Controls.AddRange(new Control[] { lbRed, lbGreen, lbBlue, sbRed, sbGreen, sbBlue, nudRed, nudGreen, nudBlue, pb,btnOk, btnCancel, btnOther });
        }

        private void BtnOther_Click(object? sender, EventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
            {
                color = cd.Color;
                nudRed.Value = color.R;
                nudBlue.Value = color.G;
                nudGreen.Value = color.B;
                sbRed.Value = color.R;
                sbBlue.Value = color.G;
                sbGreen.Value = color.B;
                pb.BackColor = Color.FromArgb(cd.Color.ToArgb());
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            result = DialogResult.Cancel;
            this.Close();
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            color = pb.BackColor;
            result = DialogResult.OK;
            this.Close();
        }

        private void Colors_ValueChanged(object? sender, EventArgs e)
        { 
            if (new List<HScrollBar>() { sbRed, sbBlue, sbGreen }.Contains(sender))
            {
                nudRed.Value = sbRed.Value;
                nudBlue.Value = sbBlue.Value;
                nudGreen.Value = sbGreen.Value;
            }
            else 
            {
                sbRed.Value = Convert.ToInt32(nudRed.Value);
                sbBlue.Value = Convert.ToInt32(nudBlue.Value);
                sbGreen.Value = Convert.ToInt32(nudGreen.Value);
            }
            pb.BackColor = Color.FromArgb(sbRed.Value, sbGreen.Value, sbBlue.Value);
        }
    }
}
