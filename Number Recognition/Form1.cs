using Microsoft.VisualBasic.Devices;
using NetWork_1;

namespace Number_Recognition
{
    public partial class Form1 : Form
    {
        Bitmap map;
        Graphics g;
        Pen pen = new Pen(Color.Black, 30f);
        bool isMouse;
        ArrayPoints arrayPoints;
        dataNetWork NW_Config;
        NetWork NW;

        private class ArrayPoints
        {
            Point[] points;
            int size;
            int index;
            public ArrayPoints(int size)
            {
                if (size <= 0) size = 2;
                this.points = new Point[size];
                this.size = size;
            }
            public void SetPoint(int x, int y)
            {
                if (index >= size) index = 0;
                points[index] = new Point(x, y);
                index++;
            }
            public Point[] GetPoints() => points;
        }
        public Form1()
        {
            InitializeComponent();
            map = new Bitmap(395, 345);
            g = Graphics.FromImage(map);
            arrayPoints = new ArrayPoints(2);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            NW_Config = Learning.ReadDataNetWork("Config.txt");
            NW = new NetWork(NW_Config, "ReLU");
            NW.ReadWeights();
        }
        private void drawingField_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
        }

        private void drawingField_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;

            StreamWriter writer = new StreamWriter("test.txt");
            Bitmap img = (new Bitmap(drawingField.Image, 28, 28));
            int w = img.Width;
            int h = img.Height;
            writer.WriteLine("Examples 1\n7");
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (img.GetPixel(j, i).ToArgb() == Color.Black.ToArgb())
                        writer.Write("1 ");
                    else
                        writer.Write("0 ");
                }
                writer.Write("\n");
            }
            writer.Close();
        }

        private void drawingField_MouseMove(object sender, MouseEventArgs e)
        {
            arrayPoints.SetPoint(e.X, e.Y);
            if (isMouse)
            {
                g.DrawLines(pen, arrayPoints.GetPoints());
                drawingField.Image = map;
                drawingField.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int predict = 0;
            int ex_test = 0;
            Learning.dataInfo[] data_test;
            data_test = Learning.ReadData("test.txt", NW_Config, ref ex_test);
            for (int i = 0; i < ex_test; ++i)
            {
                NW.SetInput(data_test[i].pixels);
                predict = NW.ForwardFeed();
            }
            predictNum.Text = predict.ToString();
            predictNum.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            drawingField.Refresh();
            predictNum.Visible = false;
        }

        private void ñïðàâêàToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Âûïîëíèë ñòóäåíò ãð. 2231122 Øíàéäåð Àðòóð", "Ñïðàâêà");
        }
    }
}
