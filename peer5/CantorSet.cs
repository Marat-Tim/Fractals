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
    /// Фрактал "Канторово Множество".
    /// </summary>
    class CantorSet : Fractal
    {

        /// <summary>
        /// Координата по горизонтали 1-ой точки.
        /// </summary>
        private double x1;

        /// <summary>
        /// Координата по горизонтали 2-ой точки.
        /// </summary>
        private double x2;

        /// <summary>
        /// Координата по вертикали.
        /// </summary>
        private double y;

        /// <summary>
        /// Расстояние между отрезками.
        /// </summary>
        private double distance;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public CantorSet()
        {
            maxDepth = 12;
        }

        /// <summary>
        /// Получает настроенные пользователем параметры фрактала.
        /// </summary>
        private void GetParameters()
        {
            depth = (int)((Slider)((StackPanel)MainWindow.MainGrid.Children[2]).Children[1]).Value;
            actualIncrease = (int)((Slider)((StackPanel)MainWindow.MainGrid.Children[2]).Children[13]).Value;
            distance = ((Slider)((StackPanel)MainWindow.MainGrid.Children[2]).Children[3]).Value * actualIncrease;
            startColor = colors[(int)((Slider)((StackPanel)MainWindow.MainGrid.Children[2]).Children[9]).Value - 1];
            endColor = colors[(int)((Slider)((StackPanel)MainWindow.MainGrid.Children[2]).Children[11]).Value - 1];
            actualColor = startColor;

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
                x2 = actualIncrease * MainWindow.CanvasForDrawing.ActualWidth / 5 - x1;
                y = 5;
            }
            if (depthNow == depth)
            {
                return;
            }
            Painter.DrawRectangle((x1, y), (x2, y + actualIncrease), actualColor, actualColor);
            double oldX1 = x1, oldX2 = x2, oldY = y;
            ChangeActualColor();
            Color oldColor = actualColor;
            x2 = oldX1 + (oldX2 - oldX1) / 3;
            y = oldY + 5 + distance;
            DrawFractal(depthNow + 1);
            actualColor = oldColor;
            x1 = oldX1 + 2 * (oldX2 - oldX1) / 3;
            x2 = oldX2;
            y = oldY + 5 + distance;
            DrawFractal(depthNow + 1);
        }


        /// <summary>
        /// Меняет цвет текущей итерации фрактала.
        /// </summary>
        protected override void ChangeActualColor()
        {
            if (depth > 1)
            {
                byte actualR = (byte)(actualColor.R + (endColor.R - startColor.R) / (depth - 1));
                byte actualG = (byte)(actualColor.G + (endColor.G - startColor.G) / (depth - 1));
                byte actualB = (byte)(actualColor.B + (endColor.B - startColor.B) / (depth - 1));
                actualColor = Color.FromRgb(actualR, actualG, actualB);
            }
            else
            {
                actualColor = startColor;
            }
        }

        public override string ConstID => "CantorSet";

        /// <summary>
        /// Возвращает название фрактала.
        /// </summary>
        public override string Name => "Канторово множество";
    }
}
