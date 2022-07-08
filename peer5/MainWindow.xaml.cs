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
using System.Reflection;

namespace Fractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Все классы фракталов из проекта.
        /// </summary>
        private Type[] allFractalsTypes =
            Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Fractal)).ToArray();

        /// <summary>
        /// Текущий фрактал.
        /// </summary>
        private Fractal selectedFractal;

        /// <summary>
        /// Холст для рисования фрактала.
        /// </summary>
        public static Canvas CanvasForDrawing { get; set; }

        /// <summary>
        /// Панель настройки текущего фрактала.
        /// </summary>
        public static StackPanel PanelWithFractalSettings => panelWithFractalSettings;
        private static StackPanel panelWithFractalSettings;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            CanvasForDrawing = Canvas0;
        }

        /// <summary>
        /// Добавляет в меню выбора фракталов все фракталы из проекта.
        /// </summary>
        private void MenuInitialization()
        {
            Dictionary<string, Type> constIDToType = new();
            int index = 1;
            foreach (var fractalType in allFractalsTypes)
            {
                var fractal = fractalType.GetConstructors()[0].Invoke(null) as Fractal;
                if (constIDToType.ContainsKey(fractal.ConstID))
                {
                    throw new Exception(
                        $"Классы {fractalType.Name} и {constIDToType[fractal.ConstID].Name} " +
                        $"имеют одинаковое значение в свойстве constID");
                }
                constIDToType.Add(fractal.ConstID, fractalType);
                var menuItem = new MenuItem
                {
                    Header = $"{index}. {fractal.Name}",
                    Name = fractal.ConstID
                };
                menuItem.Click += (_, _) =>
                {
                    selectedFractal = fractalType.GetConstructors()[0].Invoke(null) as Fractal;
                    ChangeSelectedFractal();
                    selectedFractal.DrawFractal();
                };
                MenuWithFractalSelection.Items.Add(menuItem);
                index++;
            }
        }

        /// <summary>
        /// Отрисовка границы холста для рисовния и вывод окошка с предупреждением.
        /// </summary>
        /// <param name="sender">Объект, пославший событие.</param>
        /// <param name="e">Пользователь выбрал пункт в меню.</param>
        private void Canvas0_Loaded(object sender, RoutedEventArgs e)
        {
            // Чертим рамку, где будут отрисовываться фракталы.
            Painter.DrawRectangle((0, 0),
                                  (CanvasForDrawing.ActualWidth, CanvasForDrawing.ActualHeight),
                                  Colors.White, Colors.Black);
            // Иницализация меню.
            MenuInitialization();
            // Вывод окошка с предупреждением.
            System.Windows.MessageBox.Show("При большой глубине рекурсии приложение будет работать долго.",
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
                dlg.FileName = selectedFractal != null ? selectedFractal.Name : "Fractal";
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
                System.Windows.MessageBox.Show(ex.Message, "Ошибка!");
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
            foreach (MenuItem menuItem in MenuWithFractalSelection.Items)
            {
                menuItem.IsChecked = false;
            }
            foreach (MenuItem menuItem in MenuWithFractalSelection.Items)
            {
                if (menuItem.Name == selectedFractal.ConstID)
                {
                    menuItem.IsChecked = true;
                }
            }
            foreach (StackPanel stackPanel in GridWithFractalSettings.Children)
            {
                stackPanel.Visibility = Visibility.Hidden;
            }
            foreach (StackPanel fractalSettings in GridWithFractalSettings.Children)
            {
                if (fractalSettings.Name == selectedFractal.ConstID)
                {
                    fractalSettings.Visibility = Visibility.Visible;
                    panelWithFractalSettings = fractalSettings;
                }
            }
        }

        private void palette2_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {

        }
    }
}
