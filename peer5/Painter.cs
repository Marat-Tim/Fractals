using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Класс для рисования.
    /// </summary>
    class Painter
    {
        /// <summary>
        /// Рисует линию по 2 точкам.
        /// </summary>
        /// <param name="p1">Первая точка.</param>
        /// <param name="p2">Вторая точка.</param>
        /// <param name="color">Цвет линии.</param>
        /// <param name="strokeThickness">Ширина линии.</param>
        public static void DrawLine((double, double) p1, (double, double) p2, Color color, double strokeThickness = 1)
        {
            var line = new Line();
            (line.X1, line.Y1, line.X2, line.Y2) = (p1.Item1, p1.Item2, p2.Item1, p2.Item2);
            line.Stroke = new SolidColorBrush(color);
            line.StrokeThickness = strokeThickness;
            MainWindow.CanvasForDrawing.Children.Add(line);
        }

        /// <summary>
        /// Рисует линию длины length из точки p под углом angle.
        /// </summary>
        /// <param name="p">Точка из которой нужно нарисовать линию.</param>
        /// <param name="angle">Угол под которым надо нарисовать.</param>
        /// <param name="length">Длина прямой, которую надо рисовать.</param>
        /// <param name="color">Цвет прямой.</param>
        public static void DrawLine((double, double) p, double angle, double length, Color color)
        {
            double angleInRadians = angle * Math.PI / 180;
            DrawLine((p.Item1, p.Item2),
                (p.Item1 + length * Math.Cos(angleInRadians), p.Item2 - length * Math.Sin(angleInRadians)), color);
        }

        /// <summary>
        /// Рисует прямоугольник по 2 точкам с цветом заливки fillColor и цветом границы strokeColor. 
        /// </summary>
        /// <param name="p1">Первая точка.</param>
        /// <param name="p2">Вторая точка.</param>
        /// <param name="fillColor">Цвет заливки.</param>
        /// <param name="strokeColor">Цвет границы.</param>
        public static void DrawRectangle((double, double) p1,
                                         (double, double) p2,
                                         Color fillColor, Color strokeColor)
        {
            var rectangle = new Rectangle();
            rectangle.Margin = new Thickness(p1.Item1, p1.Item2,
                MainWindow.CanvasForDrawing.ActualWidth - p2.Item1, MainWindow.CanvasForDrawing.ActualHeight - p2.Item2);
            rectangle.Width = p2.Item1 - p1.Item1;
            rectangle.Height = p2.Item2 - p1.Item2;
            rectangle.Fill = new SolidColorBrush(fillColor);
            rectangle.Stroke = new SolidColorBrush(strokeColor);
            MainWindow.CanvasForDrawing.Children.Add(rectangle);
        }

        /// <summary>
        /// Рисует теругольник по 3 точкам.
        /// </summary>
        /// <param name="p1">Точка 1.</param>
        /// <param name="p2">Точка 2.</param>
        /// <param name="p3">Точка 3.</param>
        /// <param name="color">Цвет треугольника.</param>
        public static void DrawTriangle((double, double) p1, (double, double) p2, (double, double) p3, Color color)
        {
            DrawLine(p1, p2, color);
            DrawLine(p1, p3, color);
            DrawLine(p2, p3, color);
        }

    }
}
