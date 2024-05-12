using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Emitter
    {
        public float GravitationX = 0;
        public float GravitationY = 0;
        public List<Point> gravityPoints = new List<Point>(); // тут буду хранится точки притяжения
        List<Particle> particles = new List<Particle>();
            public int MousePositionX;
            public int MousePositionY;

            public void UpdateState()
            {
            foreach (var particle in particles)
            {
                particle.Life -= 1;
                if (particle.Life < 0)
                {
                    particle.Life = 20 + Particle.rand.Next(100);

                    particle.X = MousePositionX;
                    particle.Y = MousePositionY;
                    var direction = (double)Particle.rand.Next(360);
                    var speed = 1 + Particle.rand.Next(10);

                    particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
                    particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
                    particle.Radius = 2 + Particle.rand.Next(10);
                }
                else
                {
                    foreach (var point in gravityPoints)
                    {
                        // и так считаем вектор притяжения к точке
                        float gX = point.X - particle.X;
                        float gY = point.Y - particle.Y;

                        // считаем квадрат расстояния между частицей и точкой r^2
                        float r2 = (float)Math.Max(100, gX * gX + gY * gY);//делаем не меньше 100, чтобы не было деления на 0 и очень большого ускорения
                        float M = 100; // сила притяжения к точке, пусть 100 будет

                        // пересчитываем вектор скорости с учетом притяжения к точке
                        particle.SpeedX += (gX) * M / r2;
                        particle.SpeedY += (gY) * M / r2;
                    }

                    // гравитация воздействует на вектор скорости, поэтому пересчитываем его
                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;

                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;
                }
            }
            for (var i = 0; i < 10; ++i)
            {
                if (particles.Count < 500)
                {
                    var particle = new ParticleColorful();
                    particle.FromColor = Color.Yellow;
                    particle.ToColor = Color.FromArgb(0, Color.Magenta); ;
                    particle.X = MousePositionX;
                    particle.Y = MousePositionY;
                    particles.Add(particle);
                }
                else
                {
                    break;
                }
            }
        }

            public void Render(Graphics g)
            {
                foreach (var particle in particles)
                {
                    particle.Draw(g);
                }
                foreach (var point in gravityPoints)
                {
                    g.FillEllipse(
                        new SolidBrush(Color.Red),
                        point.X - 5,
                        point.Y - 5,
                        10,
                        10
                    );
                }
        }
    }
}
