using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Lab_6
{
    public partial class Polygon : UserControl
    {
        Point mouseDownPoint;
        Point mouseUpPoint;
        bool IsSelectedVertices = false;
        // bool Draw=false;
        //public List<Vertex> Vertices { get; set; }
        public Polygon()
        {  //Vertices = new List<Vertex>();
            InitializeComponent();
          
            
        }

        private void Polygon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right  )//&&!IsSelectedVertices
            {
                Vertex vertex = new Vertex(e.Location);
                vertex.MouseClick += Vertex_MouseClick;
                vertex.BackColor = Color.Blue;
                vertex.SelectedColor = Color.Yellow;
                vertex.Parent = this;
                Invalidate();
                
                //MessageBox.Show(Vertices.Count.ToString());
            }

        }

        private void Vertex_MouseClick(object sender, MouseEventArgs e)
        {
            Vertex vertex = sender as Vertex;
            if (e.Button == MouseButtons.Right)
            {
               
                vertex.Parent.Invalidate();
                Controls.Remove(vertex);
            }
            else if (e.Button==MouseButtons.Left)
            {
                vertex.IsSelected =!vertex.IsSelected;
                IsSelectedVertices = false;
                foreach (Vertex v in Controls)
                {
                    if (v.IsSelected)
                    {
                        IsSelectedVertices = true;
                        break;
                    }
                }
                vertex.Invalidate();
            }

          
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
           
            if(e.KeyCode==Keys.D)
            {
                
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
           
            if ( Controls.Count>2)
            {
              
                Graphics graphics = e.Graphics;
                graphics.Clear(BackColor);
                GraphicsPath graphicsPath = new GraphicsPath();
                List<Point> points = new List<Point>();
                foreach (Control c in Controls)
                {
                    if(c is Vertex)
                    {
                        points.Add((c as Vertex).Center);
                    }
                }
                graphicsPath.AddPolygon(points.ToArray());
                graphics.DrawPath(new Pen(Color.Blue, 1), graphicsPath);
                graphics.FillPath(Brushes.White, graphicsPath);
            }

        }

        private void Polygon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsSelectedVertices)
                mouseUpPoint = e.Location;
            int dx=mouseUpPoint.X-mouseDownPoint.X;
            int dy = mouseUpPoint.Y - mouseDownPoint.Y;

            if (IsSelectedVertices)
            {
                foreach (Vertex v in Controls)
                {
                    if (v.IsSelected)
                    { 
                        
                        v.Center= new Point { X = v.Center.X + dx, Y = v.Center.Y + dy };
                        v.Location = new Point { X = v.Location.X + dx, Y = v.Location.Y + dy };
                        v.IsSelected = false;
                        v.Invalidate();
                       
                       
                    }
                   IsSelectedVertices = false;
                    Invalidate();
                }
               
               
                
            }
            
        }

        private void Polygon_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && IsSelectedVertices)
                mouseDownPoint = e.Location;
        }
    }
}
