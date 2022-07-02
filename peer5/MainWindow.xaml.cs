using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Текущий фрактал.
        /// </summary>
        private Fractal selectedFractal;

        /// <summary>
        /// Холст для рисования фрактала.
        /// </summary>
        public static Canvas CanvasForDrawing { get; set; }

        /// <summary>
        /// Место на котором расположены:
        /// 1. Холст для рисования;
        /// 2. Параметры фракталов;
        /// 3. Элементы меню.
        /// </summary>
        public static Grid MainGrid { get; set; }

        /// <summary>
        /// Основное окно.
        /// </summary>
        public static Window MyWindow { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            CanvasForDrawing = Canvas0;
            MainGrid = Grid0;
            MyWindow = Window0;
        }

        /// <summary>
        /// Смена текущего фрактала на Дерево.
        /// </summary>
        /// <param name="sender">Объект, пославший событие.</param>
        /// <param name="e">Пользователь выбрал пункт в меню.</param>
        private void MenuItemTree_Click(object sender, RoutedEventArgs e)
        {
            selectedFractal = new Tree();
            ChangeSelectedFractal();
            selectedFractal.DrawFractal();
        }

        /// <summary>
        /// Смена текущего фрактала на Кривую Коха.
        /// </summary>
        /// <param name="sender">Объект, пославший событие.</param>
        /// <param name="e">Пользователь выбрал пункт в меню.</param>
        private void MenuItemKochCurve_Click(object sender, RoutedEventArgs e)
        {
            selectedFractal = new KochСurve();
            ChangeSelectedFractal();
            selectedFractal.DrawFractal();
        }

        /// <summary>
        /// Смена текущего фрактала на Ковер Серпинского.
        /// </summary>
        /// <param name="sender">Объект, пославший событие.</param>
        /// <param name="e">Пользователь выбрал пункт в меню.</param>
        private void MenuItemSierpinskiCarpet_Click(object sender, RoutedEventArgs e)
        {
            selectedFractal = new SierpinskiСarpet();
            ChangeSelectedFractal();
            selectedFractal.DrawFractal();
        }

        /// <summary>
        /// Смена текущего фрактала на Треугольник Серпинского.
        /// </summary>
        /// <param name="sender">Объект, пославший событие.</param>
        /// <param name="e">Пользователь выбрал пункт в меню.</param>
        private void MenuItemSierpinskiTriangle_Click(object sender, RoutedEventArgs e)
        {
            selectedFractal = new SierpinskiTriangle();
            ChangeSelectedFractal();
            selectedFractal.DrawFractal();
        }

        /// <summary>
        /// Смена текущего фрактала на Канторово Множество.
        /// </summary>
        /// <param name="sender">Объект, пославший событие.</param>
        /// <param name="e">Пользователь выбрал пункт в меню.</param>
        private void MenuItemCantorSet_Click(object sender, RoutedEventArgs e)
        {
            selectedFractal = new CantorSet();
            ChangeSelectedFractal();
            selectedFractal.DrawFractal();
        }

        /// <summary>
        /// Отрисовка границы холста для рисовния и вывод окошка с предупреждением.
        /// </summary>
        /// <param name="sender">Объект, пославший событие.</param>
        /// <param name="e">Пользователь выбрал пункт в меню.</param>
        private void Canvas0_Loaded(object sender, RoutedEventArgs e)
        {
            Painter.DrawRectangle((0, 0),
                                  (CanvasForDrawing.ActualWidth, CanvasForDrawing.ActualHeight), 
                                  Colors.White, Colors.Black);
            MessageBox.Show("При большой глубине рекурсии приложение будет работать долго.",
                "Предупреждение");
        }

        /// <summary>
        /// Отрисовка текущего фрактала.
        /// </summary>
        /// <param name="sender">Объект, пославший событие.</param>
        /// <param name="e">Пользователь выбрал пункт в меню.</param>
        private void DrawSelectedFractal(object sender, RoutedEventArgs e)
        {
            if (CanvasForDrawing != null)
            {
                CanvasForDrawing.Children.Clear();
                Painter.DrawRectangle((0, 0),
                                      (CanvasForDrawing.ActualWidth, CanvasForDrawing.ActualHeight),
                                      Colors.White, Colors.Black);
                if (selectedFractal != null)
                {
                    selectedFractal.DrawFractal();
                }
            }
        }

        /// <summary>
        /// Сохранение картинки с холста.
        /// </summary>
        /// <param name="sender">Объект, пославший событие.</param>
        /// <param name="e">Пользователь выбрал пункт в меню.</param>
        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)Canvas0.ActualWidth + 27,
                    (int)Canvas0.ActualHeight + 27, 96d, 96d, System.Windows.Media.PixelFormats.Default);
                rtb.Render(Canvas0);
                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(rtb));
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = selectedFractal != null ? selectedFractal.ToString() : "Fractal";
                dlg.DefaultExt = ".png";
                dlg.Filter = "Pictures |*.png";
                bool? result = dlg.ShowDialog();
                if (result == true)
                {
                    using (var fs = System.IO.File.OpenWrite(dlg.FileName))
                    {
                        pngEncoder.Save(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        /// <summary>
        /// Очищает холст для рисования.
        /// Меняет выбранный в меню фрактал.
        /// Показывает настройки для выбранного фрактала.
        /// </summary>
        private void ChangeSelectedFractal()
        {
            CanvasForDrawing.Children.Clear();
            Painter.DrawRectangle((0, 0),
                                      (CanvasForDrawing.ActualWidth, CanvasForDrawing.ActualHeight),
                                      Colors.White, Colors.Black);
            MenuItemTree.IsChecked = false; 
            MenuItemKochCurve.IsChecked = false;
            MenuItemSierpinskiCarpet.IsChecked = false;
            MenuItemSierpinskiTriangle.IsChecked = false;
            MenuItemCantorSet.IsChecked = false;
            for (int i = 2; i < 7; i++)
            {
                ((StackPanel)MainGrid.Children[i]).Visibility = Visibility.Hidden;
            }
            if (selectedFractal is Tree)
            {
                ((StackPanel)MainGrid.Children[6]).Visibility = Visibility.Visible;
                MenuItemTree.IsChecked = true;
            }
            if (selectedFractal is KochСurve)
            {
                ((StackPanel)MainGrid.Children[5]).Visibility = Visibility.Visible;
                MenuItemKochCurve.IsChecked = true;
            }
            if (selectedFractal is SierpinskiСarpet)
            {
                ((StackPanel)MainGrid.Children[4]).Visibility = Visibility.Visible;
                MenuItemSierpinskiCarpet.IsChecked = true;
            }
            if (selectedFractal is SierpinskiTriangle)
            {
                ((StackPanel)MainGrid.Children[3]).Visibility = Visibility.Visible;
                MenuItemSierpinskiTriangle.IsChecked = true;
            }
            if (selectedFractal is CantorSet)
            {
                ((StackPanel)MainGrid.Children[2]).Visibility = Visibility.Visible;
                MenuItemCantorSet.IsChecked = true;
            }
        }
    }
}
