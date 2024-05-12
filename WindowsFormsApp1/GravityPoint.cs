using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class GravityPoint : ImpactPoint
    {
        public int Power = 100; // сила притяжения
        public Color color = Color.Red;

        // а сюда по сути скопировали с минимальными правками то что было в UpdateState
        public override void ImpactParticle(Particle particle,Emitter emitter)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            double r = Math.Sqrt(gX * gX + gY * gY); // считаем расстояние от центра точки до центра частицы
            if (r + particle.Radius < Power / 2) // если частица оказалась внутри окружности
            {
                // то притягиваем ее
                float r2 = (float)Math.Max(100, gX * gX + gY * gY);
                particle.SpeedX += gX * Power / r2;
                particle.SpeedY += gY * Power / r2;
                particle.colorTo= color;
                particle.colorFrom = Color.FromArgb(0, color);
            }
        }
        public override void Render(Graphics g)
        {
            // буду рисовать окружность с диаметром равным Power
            g.DrawEllipse(
                   new Pen(color),
                   X - Power / 2,
                   Y - Power / 2,
                   Power,
                   Power
               );
            //var stringFormat = new StringFormat(); // создаем экземпляр класса
            //stringFormat.Alignment = StringAlignment.Center; // выравнивание по горизонтали
            //stringFormat.LineAlignment = StringAlignment.Center; // выравнивание по вертикали

            //// обязательно выносим текст и шрифт в переменные
            //var text = $"Я гравитон\nc силой {Power}";
            //var font = new Font("Verdana", 10);

            //// вызываем MeasureString, чтобы померить размеры текста
            //var size = g.MeasureString(text, font);

            //// рисуем подложнку под текст
            //g.FillRectangle(
            //    new SolidBrush(Color.Red),
            //    X - size.Width / 2, // так как я выравнивал текст по центру то подложка должна быть центрирована относительно X,Y
            //    Y - size.Height / 2,
            //    size.Width,
            //    size.Height
            //);

           //g.DrawString(
           //$"Я гравитон\nc силой {Power}", // надпись, можно перенос строки вставлять (если вы Катя, то может не работать и надо использовать \r\n)
           //new Font("Verdana", 10), // шрифт и его размер
           //new SolidBrush(Color.White), // цвет шрифта
           //X, // расположение в пространстве
           //Y,
           //stringFormat // передаем инфу о выравнивании
           //);
           // // ну и текст рисую уже на базе переменных
           // g.DrawString(
           //     text,
           //     font,
           //     new SolidBrush(Color.White),
           //     X,
           //     Y
           // );

        }
    }
}
