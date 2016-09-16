using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ball_tracker
{
    public partial class Form1 : Form
    {
        Capture webCam;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ////取得第一支網路攝影機
            webCam = new Capture(0);
            Application.Idle += new EventHandler(Application_Idle);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            //取得網路攝影機的影像
            Mat camImage = webCam.QueryFrame();
            int stateSize = 6;
            int measSize = 4;
            int coutrSize = 0;
            var type = Emgu.CV.CvEnum.DepthType.Cv32F;
            KalmanFilter kf = new KalmanFilter(stateSize, measSize, coutrSize, type);
            
            

            imageBox1.Image = camImage;
        }


    }
}
