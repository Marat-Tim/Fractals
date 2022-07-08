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
    /// Фрактал "Ковер Серпинского".
    /// </summary>
    class SierpinskiСarpet : Fractal
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
        public SierpinskiСarpet()
        {
            maxDepth = 6;
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
                ?? Colors.White;
            actualColor = startColor;
            actualIncrease = ((Slider)MainWindow.PanelWithFractalSettings.Children[7]).Value / 100;
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
                x1 = actualIncrease;
                y1 = actualIncrease;
                x2 = actualIncrease * MainWindow.CanvasForDrawing.ActualWidth / 5 - x1;
                y2 = actualIncrease * MainWindow.CanvasForDrawing.ActualHeight / 5 - y1;
                Painter.DrawRectangle((x1, y1), (x2, y2), actualColor, actualColor);
                ChangeActualColor();
            }
            if (depthNow == depth)
            {
                return;
            }
            Painter.DrawRectangle((x1 + (x2 - x1) / 3, y1 + (y2 - y1) / 3),
                                  (x1 + 2 * (x2 - x1) / 3, y1 + 2 * (y2 - y1) / 3), 
                                  actualColor, actualColor);
            ChangeActualColor();
            Color oldColor = actualColor;
            double oldX1 = x1, oldY1 = y1, oldX2 = x2, oldY2 = y2;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i != 1 || j != 1)
                    {
                        actualColor = oldColor;
                        x1 = oldX1 + i * (oldX2 - oldX1) / 3;
                        y1 = oldY1 + j * (oldY2 - oldY1) / 3;
                        x2 = oldX1 + (i + 1) * (oldX2 - oldX1) / 3;
                        y2 = oldY1 + (j + 1) * (oldY2 - oldY1) / 3;
                        DrawFractal(depthNow + 1);
                    }
                }
            }
        }

        public override string ConstID => "SierpinskiCarpet";

        /// <summary>
        /// Возвращает название фрактала.
        /// </summary>
        public override string Name => "Ковер Серпинского";
    }
}
