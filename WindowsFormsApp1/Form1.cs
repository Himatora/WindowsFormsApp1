using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        TopEmitter emitter; // добавим поле для эмиттера
        GravityPoint red; // добавил поле под первую точку
        GravityPoint yelow; // добавил поле под вторую точку
        GravityPoint green; // добавил поле под первую точку
        GravityPoint blue; // добавил поле под вторую точку
        public Form1()
        {
            InitializeComponent();
            //привязываем изображения
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            emitter = new TopEmitter
            {
                Width = picDisplay.Width,
                GravitationY = 0.25f
            };
            emitters.Add(this.emitter); // все равно добавляю в список emitters, чтобы он рендерился и обновлялся
                                        // добавил гравитон
                                        // привязываем гравитоны к полям
            red = new GravityPoint
            {
                color= Color.Red,
                X = picDisplay.Width / 2 + 200,
                Y = picDisplay.Height / 2,
            };
            yelow = new GravityPoint
            {   
                color= Color.Yellow,
                X = picDisplay.Width / 2 +100,
                Y = picDisplay.Height / 2,
            };
            green = new GravityPoint
            {
                color= Color.Green,
                X = picDisplay.Width / 2 - 100,
                Y = picDisplay.Height / 2,
            };
           blue = new GravityPoint
            {
               color = Color.Blue,
                X = picDisplay.Width / 2 - 200,
                Y = picDisplay.Height / 2,
            };
            // привязываем поля к эмиттеру
            emitter.impactPoints.Add(red);
            emitter.impactPoints.Add(yelow);
            emitter.impactPoints.Add(green);
            emitter.impactPoints.Add(blue);


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
            }
            picDisplay.Invalidate();

        }
        //private int MousePositionX = 0;
        //private int MousePositionY = 0;
        //private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        //{
        //    foreach (var emitter in emitters)
        //    {
        //        emitter.MousePositionX = e.X;
        //        emitter.MousePositionY = e.Y;
        //    }
        //    point1.X = e.X;
        //    point1.Y = e.Y;
        //}

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            red.Y =picDisplay.Height-tbDirection.Value;
            green.Y = picDisplay.Height-tbDirection.Value;
            blue.Y = tbDirection.Value;
            yelow.Y = tbDirection.Value;
            lblDirection.Text = $"{tbDirection.Value}°"; // добавил вывод значения
        }

        private void tbGraviton_Scroll(object sender, EventArgs e)
        {
            blue.Power = tbGraviton.Value;
        }

        private void tbGraviton2_Scroll(object sender, EventArgs e)
        {
            green.Power = tbGraviton2.Value;
        }

        private void tbYelow_Scroll(object sender, EventArgs e)
        {
            yelow.Power=tbYelow.Value;
        }

        private void tbRed_Scroll(object sender, EventArgs e)
        {
            red.Power=tbRed.Value;
        }

        private void bChange_Click(object sender, EventArgs e)
        {
            if(red.color==Color.Red) { 
            red.color = Color.Aqua;
            blue.color = Color.Violet;
            green.color = Color.YellowGreen;
            yelow.color = Color.Orange;}
            else
            {
                red.color = Color.Red;
                blue.color = Color.Blue;
                green.color = Color.Green;
                yelow.color = Color.Yellow;
            }
        }
    }
}
