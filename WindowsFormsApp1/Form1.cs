﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<Particle> particles = new List<Particle>();
        public Form1()
        {
            InitializeComponent();
            //привязываем изображения
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            /*for (var i = 0; i < 500; ++i)
            {
                var particle = new Particle();
                particle.X = picDisplay.Image.Width / 2;
                particle.Y = picDisplay.Image.Height / 2;

                particles.Add(particle);
            }*/
        }
        private void UpdateState()
        {
            foreach(var particle in particles)
            {
                particle.Life -= 1;
                if(particle.Life < 0)
                {
                    particle.Life = 20 + Particle.rand.Next(100);

                    particle.X=MousePositionX;
                    particle.Y=MousePositionY;
                    var direction = (double)Particle.rand.Next(360);
                    var speed = 1 + Particle.rand.Next(10);

                    particle.SpeedX=(float)(Math.Cos(direction/180*Math.PI)*speed);
                    particle.SpeedY =-(float)(Math.Sin(direction / 180 * Math.PI) * speed);
                    particle.Radius=2+ Particle.rand.Next(10);
                }
                else {
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;
                } 
            }
            for(var i = 0; i < 10; ++i)
            {
                if (particles.Count < 500)
                {
                    var particle = new ParticleColorful();
                    particle.FromColor = Color.Yellow;
                    particle.ToColor = Color.FromArgb(0, Color.Magenta); ;
                    particle.X = MousePositionX;
                    particle.Y=MousePositionY;
                    particles.Add(particle);
                }
                else
                {
                    break;
                }
            }
        }
        private void Render(Graphics g)
        {
            foreach(var particle in particles)
            {
               particle.Draw(g);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                Render(g);
            }
            picDisplay.Invalidate();
        }
        private int MousePositionX = 0;
        private int MousePositionY = 0;
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            MousePositionX=e.X;
            MousePositionY=e.Y;
        }
    }
}
