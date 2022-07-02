using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Фрактал.
    /// </summary>
    abstract class Fractal
    {

        /// <summary>
        /// Текущее увеличение.
        /// </summary>
        protected int actualIncrease;

        /// <summary>
        /// Цвета, которые может выбрать пользователь.
        /// </summary>
        protected Color[] colors = { Colors.Black, Colors.Blue, Colors.Red, Colors.Yellow, Colors.Green, Colors.White };

        /// <summary>
        /// Начальный цвет.
        /// </summary>
        protected Color startColor;

        /// <summary>
        /// Конечный цвет.
        /// </summary>
        protected Color endColor;

        /// <summary>
        /// Текущий цвет.
        /// </summary>
        protected Color actualColor;

        /// <summary>
        /// Максимальная глубина рекурсии.
        /// </summary>
        protected int maxDepth;

        /// <summary>
        /// Глубина рекурсии, заданная пользователем.
        /// </summary>
        public int depth { get; set; }

        /// <summary>
        /// Отрисовывает фрактал.
        /// </summary>
        /// <param name="depthNow">Текцщая глубина рекурсии.</param>
        public abstract void DrawFractal(int depthNow = 0);

        /// <summary>
        /// Меняет цвет текущей итерации фрактала.
        /// </summary>
        protected virtual void ChangeActualColor()
        {
            if (depth != 0)
            {
                byte actualR = (byte)(actualColor.R + (endColor.R - startColor.R) / (depth));
                byte actualG = (byte)(actualColor.G + (endColor.G - startColor.G) / (depth));
                byte actualB = (byte)(actualColor.B + (endColor.B - startColor.B) / (depth));
                actualColor = Color.FromRgb(actualR, actualG, actualB);
            }
            else
            {
                actualColor = startColor;
            }
        }
    }
}
