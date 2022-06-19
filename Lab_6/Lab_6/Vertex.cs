using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab_6
{
    public partial class Vertex : UserControl
    {
        public bool IsSelected = false;
        public Point Center { get; set; }
        public int Radius { get; set; } =10;
        public Color SelectedColor { get; set; }


        public Vertex(Point center)
        {
           
            InitializeComponent(); 
            Center = center;
            Location = new Point(Center.X-Radius,Center.Y-Radius);
            Width = Height = 2 * Radius;
            BackColor = Color.White;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(0,0, 2*Radius, 2 * Radius);
            Region region = new Region(graphicsPath);
            if (IsSelected)
                graphics.FillRegion(new SolidBrush(BackColor), region);
            else
                //MessageBox.Show("selected");
                graphics.FillRegion(new SolidBrush(SelectedColor), region);//Color.FromArgb((int)(BackColor.A*0.5), BackColor)
            this.Region=region;

           
        }

        private void Vertex_LocationChanged(object sender, EventArgs e)
        {

        }
    }
}
