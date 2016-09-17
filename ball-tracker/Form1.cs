using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.UI;

namespace ball_tracker
{
    public partial class Form1 : Form
    {
        private Capture webCam;
        private ObjectTracking objectTracking;
        private ROI roi;
        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
        private Boolean trackingStatus = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            imageBox1.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            roi = new ROI();
            webCam = new Capture(0);
            Application.Idle += Application_Idle;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            Image<Bgr, Byte> camImage = webCam.QueryFrame();
            imageBox1.Image = camImage;
        }

        private void imageBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            roi = new ROI();
            roi.setRecStartPoint(e.Location);
            Invalidate();
        }

        private void imageBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Point tempEndPoint = e.Location;
            Point startPoint = roi.getRectStartPoint();
            Point rectPoint = new Point(Math.Min(startPoint.X, tempEndPoint.X), Math.Min(startPoint.Y, tempEndPoint.Y));
            Size rectSize = new Size(Math.Abs(startPoint.X - tempEndPoint.X), Math.Abs(startPoint.Y - tempEndPoint.Y));
            roi.setRect(rectPoint, rectSize);
        }

        private void imageBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void imageBox1_Paint(object sender, PaintEventArgs e)
        {
            if (imageBox1.Image != null)
            {
                if (trackingStatus == false)
                {
                    var rect = roi.getRect();
                    if (rect != null && rect.Width > 0 && rect.Height > 0)
                    {
                        e.Graphics.SetClip(rect, System.Drawing.Drawing2D.CombineMode.Exclude);
                        e.Graphics.FillRectangle(selectionBrush, new Rectangle(0, 0, ((ImageBox)sender).Width, ((ImageBox)sender).Height));
                    }
                }
                else
                {
                    var objectRect = objectTracking.Tracking((Image<Bgr, Byte>)imageBox1.Image);
                    e.Graphics.SetClip(objectRect, System.Drawing.Drawing2D.CombineMode.Exclude);
                    e.Graphics.FillRectangle(selectionBrush, new Rectangle(0, 0, ((ImageBox)sender).Width, ((ImageBox)sender).Height));
                }
            }
        }

        private void tracking_Click(object sender, EventArgs e)
        {
            if (trackingStatus == false)
            {
                Rectangle targetRect = roi.getRect();
                objectTracking = new ObjectTracking((Image<Bgr, Byte>)imageBox1.Image, targetRect);
                trackingStatus = true;
                tracking.Text = "Stop";
            }
            else
            {
                trackingStatus = false;
                tracking.Text = "Tracking";
            }
        }
    }
}
