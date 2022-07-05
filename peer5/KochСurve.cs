using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fractals
{
    /// <summary>
    /// Фрактал "Прямая Коха".
    /// </summary>
    class KochСurve : Fractal
    {
        /// <summary>
        /// Координата по горизонтали 1-ой точки.
        /// </summary>
        private double x1;

        /// <summary>
        /// Координата по вертикали 1-ой точки.
        /// </summary>
        private double y1;

        /// <summary>
        /// Координата по горизонтали 2-ой точки.
        /// </summary>
        private double x2;

        /// <summary>
        /// Координата по вертикали 2-ой точки.
        /// </summary>
        private double y2;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public KochСurve()
        {
            maxDepth = 7;
        }

        /// <summary>
        /// Получает настроенные пользователем параметры фрактала.
        /// </summary>
        private void GetParameters()
        {
            depth = (int)((Slider)((StackPanel)MainWindow.MainGrid.Children[5]).Children[1]).Value;
            startColor = colors[(int)((Slider)((StackPanel)MainWindow.MainGrid.Children[5]).Children[7]).Value - 1];
            endColor = colors[(int)((Slider)((StackPanel)MainWindow.MainGrid.Children[5]).Children[9]).Value - 1];
            actualColor = startColor;
            actualIncrease = (int)((Slider)((StackPanel)MainWindow.MainGrid.Children[5]).Children[11]).Value;
        }

        /// <summary>
        /// Отрисовывает фрактал.
        /// </summary>
        /// <param name="depthNow">Текущая глубина рекурсии.</param>
        public override void DrawFractal(int depthNow = 0)
        {
            if (depthNow == 0)
            {
                GetParameters();
                x1 = actualIncrease;
                y1 = MainWindow.CanvasForDrawing.ActualHeight - 5;
                x2 = actualIncrease * MainWindow.CanvasForDrawing.ActualWidth / 5 - x1;
                y2 = y1;
                Painter.DrawLine((x1, y1), (x2, y2), actualColor);
                ChangeActualColor();
            }
            if (depthNow == depth)
            {
                return;
            }
            double oldX1 = x1, oldY1 = y1, oldX2 = x2, oldY2 = y2;
            Painter.DrawLine((oldX1 + (oldX2 - oldX1) / 3, oldY1 + (oldY2 - oldY1) / 3),
                                  (oldX1 + 2 * (oldX2 - oldX1) / 3, oldY1 + 2 * (oldY2 - oldY1) / 3),
                                  Colors.White, 2);
            Painter.DrawLine((oldX1 + (oldX2 - oldX1) / 3, oldY1 + (oldY2 - oldY1) / 3),
                             ((oldX1 + oldX2) / 2 - Math.Sin(Math.PI / 3) * (oldY1 - oldY2) / 3,
                             (oldY1 + oldY2) / 2 - Math.Cos(Math.PI / 6) * (oldX2 - oldX1) / 3),
                             actualColor);
            Painter.DrawLine((oldX1 + 2 * (oldX2 - oldX1) / 3, oldY1 + 2 * (oldY2 - oldY1) / 3),
                             ((oldX1 + oldX2) / 2 - Math.Sin(Math.PI / 3) * (oldY1 - oldY2) / 3,
                             (oldY1 + oldY2) / 2 - Math.Cos(Math.PI / 6) * (oldX2 - oldX1) / 3),
                             actualColor);
            ChangeActualColor();
            Color oldColor = actualColor;
            x2 = oldX1 + (oldX2 - oldX1) / 3;
            y2 = oldY1 + (oldY2 - oldY1) / 3;
            DrawFractal(depthNow + 1);
            actualColor = oldColor;
            x1 = oldX1 + (oldX2 - oldX1) / 3;
            y1 = oldY1 + (oldY2 - oldY1) / 3;
            x2 = (oldX1 + oldX2) / 2 - Math.Sin(Math.PI / 3) * (oldY1 - oldY2) / 3;
            y2 = (oldY1 + oldY2) / 2 - Math.Cos(Math.PI / 6) * (oldX2 - oldX1) / 3;
            DrawFractal(depthNow + 1);
            actualColor = oldColor;
            x1 = (oldX1 + oldX2) / 2 - Math.Sin(Math.PI / 3) * (oldY1 - oldY2) / 3;
            y1 = (oldY1 + oldY2) / 2 - Math.Cos(Math.PI / 6) * (oldX2 - oldX1) / 3;
            x2 = oldX1 + 2 * (oldX2 - oldX1) / 3;
            y2 = oldY1 + 2 * (oldY2 - oldY1) / 3;
            DrawFractal(depthNow + 1);
            actualColor = oldColor;
            x1 = oldX1 + 2 * (oldX2 - oldX1) / 3;
            y1 = oldY1 + 2 * (oldY2 - oldY1) / 3;
            x2 = oldX2;
            y2 = oldY2;
            DrawFractal(depthNow + 1);
        }

        /// <summary>
        /// Возвращает название фрактала.
        /// </summary>
        /// <returns>Название фрактала.</returns>
        public override string Name => "Прямая коха";
    }
}
