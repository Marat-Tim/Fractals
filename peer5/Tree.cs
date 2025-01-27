﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fractals
{
    class Tree : Fractal
    {
        /// <summary>
        /// Отношение следующей ветки к предыдущей.
        /// </summary>
        private double ratio;

        /// <summary>
        /// Начальный угол между горизонтальной прямой и левой веткой.
        /// </summary>
        private double angle1;

        /// <summary>
        /// Начальный угол между горизонтальной прямой и правой веткой.
        /// </summary>
        private double angle2;

        /// <summary>
        /// Текущий угол между горизонтальной прямой и левой веткой.
        /// </summary>
        private double actualAngle1;

        /// <summary>
        /// Текущий угол между горизонтальной прямой и правой веткой.
        /// </summary>
        private double actualAngle2;

        /// <summary>
        /// Текущая координата по горизонтали.
        /// </summary>
        private double x;

        /// <summary>
        /// Текущая координата по вертикали.
        /// </summary>
        private double y;

        /// <summary>
        /// Изначальная длина ветки.
        /// </summary>
        private double length;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Tree()
        {
            maxDepth = 13;
        }

        /// <summary>
        /// Получает настроенные пользователем параметры фрактала.
        /// </summary>
        private void GetParameters()
        {
            depth = (int)((Slider)MainWindow.PanelWithFractalSettings.Children[1]).Value;
            ratio = ((Slider)MainWindow.PanelWithFractalSettings.Children[4]).Value;
            angle1 = ((Slider)MainWindow.PanelWithFractalSettings.Children[6]).Value;
            angle2 = ((Slider)MainWindow.PanelWithFractalSettings.Children[8]).Value;
            startColor = ((Xceed.Wpf.Toolkit.ColorPicker)MainWindow.PanelWithFractalSettings.Children[10]).SelectedColor
                ?? Colors.Black;
            endColor = ((Xceed.Wpf.Toolkit.ColorPicker)MainWindow.PanelWithFractalSettings.Children[12]).SelectedColor
                ?? Colors.Black;
            actualColor = startColor;
            actualIncrease = ((Slider)MainWindow.PanelWithFractalSettings.Children[14]).Value / 100;
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
                Painter.DrawLine((MainWindow.CanvasForDrawing.ActualWidth / 2, MainWindow.CanvasForDrawing.ActualHeight),
                                 90, actualIncrease * MainWindow.CanvasForDrawing.ActualHeight / 45, actualColor);
                ChangeActualColor();
                x = MainWindow.CanvasForDrawing.ActualWidth / 2;
                length = actualIncrease * MainWindow.CanvasForDrawing.ActualHeight / 45;
                y = MainWindow.CanvasForDrawing.ActualHeight - actualIncrease * MainWindow.CanvasForDrawing.ActualHeight / 45;
                actualAngle1 = angle1;
                actualAngle2 = angle2;
            }
            if (depthNow == depth)
            {
                return;
            }
            Painter.DrawLine((x, y), 180 - actualAngle1, length * Math.Pow(ratio, depthNow + 1), actualColor);
            Painter.DrawLine((x, y), actualAngle2, length * Math.Pow(ratio, depthNow + 1), actualColor);
            ChangeActualColor();
            Color oldColor = actualColor;
            double oldX = x, oldY = y, oldAngle1 = actualAngle1, oldAngle2 = actualAngle2;
            actualAngle1 = oldAngle1 - angle1;
            actualAngle2 = oldAngle2 + angle2;
            x = oldX - length * Math.Cos(Angles.FromDegToRad(oldAngle1)) * Math.Pow(ratio, depthNow + 1);
            y = oldY - length * Math.Sin(Angles.FromDegToRad(oldAngle1)) * Math.Pow(ratio, depthNow + 1);
            DrawFractal(depthNow + 1);
            actualColor = oldColor;
            actualAngle1 = oldAngle1 + angle1;
            actualAngle2 = oldAngle2 - angle2;
            x = oldX + length * Math.Cos(Angles.FromDegToRad(oldAngle2)) * Math.Pow(ratio, depthNow + 1);
            y = oldY - length * Math.Sin(Angles.FromDegToRad(oldAngle2)) * Math.Pow(ratio, depthNow + 1);
            DrawFractal(depthNow + 1);
        }

        public override string ConstID => "Tree";

        /// <summary>
        /// Возвращает название фрактала.
        /// </summary>
        public override string Name => "Обдуваемое ветром фрактальное дерево";
    }
}
