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
    /// Фрактал "Треугольник Серпинского".
    /// </summary>
    class SierpinskiTriangle : Fractal
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
        /// Координата по горизонтали 3-ой точки.
        /// </summary>
        private double x3;

        /// <summary>
        /// Координата по вертикали 3-ой точки.
        /// </summary>
        private double y3;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public SierpinskiTriangle()
        {
            maxDepth = 9;
        }

        /// <summary>
        /// Получает настроенные пользователем параметры фрактала.
        /// </summary>
        private void GetParameters()
        {
            depth = (int)((Slider)MainWindow.PanelWithFractalSettings.Children[1]).Value;
            startColor = ((Xceed.Wpf.Toolkit.ColorPicker)MainWindow.PanelWithFractalSettings.Children[3]).SelectedColor
                ?? Colors.Black;
            endColor = ((Xceed.Wpf.Toolkit.ColorPicker)MainWindow.PanelWithFractalSettings.Children[5]).SelectedColor
                ?? Colors.Black;
            actualColor = startColor;
            actualIncrease = (int)((Slider)MainWindow.PanelWithFractalSettings.Children[7]).Value;
        }

        /// <summary>
        /// Отрисовывает фрактал.
        /// </summary>
        /// <param name="depthNow">Текцщая глубина рекурсии.</param>
        public override void DrawFractal(int depthNow = 0)
        {
            if (depthNow == 0)
            {
                GetParameters();
                x1 = 80 / 5;
                y1 = MainWindow.CanvasForDrawing.ActualHeight - 10;
                x2 = actualIncrease * MainWindow.CanvasForDrawing.ActualWidth / 5 - x1;
                y2 = y1;
                x3 = (x1 + x2) / 2 - Math.Sin(Math.PI / 3) * (y1 - y2);
                y3 = (y1 + y2) / 2 - Math.Cos(Math.PI / 6) * (x2 - x1);
                Painter.DrawTriangle((x1, y1), (x2, y2), (x3, y3), actualColor);
                ChangeActualColor();
            }
            if (depthNow == depth)
            {
                return;
            }
            double newX1 = (x1 + x3) / 2, newY1 = (y1 + y3) / 2;
            double newX2 = (x2 + x3) / 2, newY2 = (y2 + y3) / 2;
            double newX3 = (x1 + x2) / 2, newY3 = (y1 + y2) / 2;
            Painter.DrawTriangle((newX1, newY1), (newX2, newY2), (newX3, newY3), actualColor);
            ChangeActualColor();
            Color oldColor = actualColor;
            double oldX1 = x1, oldY1 = y1, oldX2 = x2, oldY2 = y2, oldX3 = x3, oldY3 = y3;
            x1 = oldX1; y1 = oldY1;
            x2 = newX1; y2 = newY1;
            x3 = newX3; y3 = newY3;
            DrawFractal(depthNow + 1);
            actualColor = oldColor;
            x1 = newX1; y1 = newY1;
            x2 = oldX3; y2 = oldY3;
            x3 = newX2; y3 = newY2;
            DrawFractal(depthNow + 1);
            actualColor = oldColor;
            x1 = newX3; y1 = newY3;
            x2 = newX2; y2 = newY2;
            x3 = oldX2; y3 = oldY2;
            DrawFractal(depthNow + 1);
        }

        public override string ConstID => "SierpinskiTriangle";

        /// <summary>
        /// Возвращает название фрактала.
        /// </summary>
        public override string Name => "Треугольник Серпинского";
    }
}
