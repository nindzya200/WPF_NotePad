using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace WPF_NotePad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsTextEdit = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NotePadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotePadMainWindow.Title = !NotePadMainWindow.Title.Contains('*') ? "*" + NotePadMainWindow.Title : NotePadMainWindow.Title;
            IsTextEdit = true;
        }

        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsTextEdit)
            {
                if (System.Windows.MessageBox.Show("Do you want to save the changes to the file?", "NotePad",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
                    File.WriteAllText(saveFileDialog.FileName, NotePadTextBox.Text);
                    NotePadMainWindow.Title = Path.GetFileName(saveFileDialog.FileName) + " - NotePad";
                }
            }
            NotePadTextBox.Text = "";
            NotePadMainWindow.Title = "Noname - NotePad";
            IsTextEdit = false;
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsTextEdit)
            {
                if (System.Windows.MessageBox.Show("Do you want to save the changes to the file?", "NotePad",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
                    File.WriteAllText(saveFileDialog.FileName, NotePadTextBox.Text);
                    NotePadMainWindow.Title = Path.GetFileName(saveFileDialog.FileName) + " - NotePad";
                }
            }
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
            NotePadTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            NotePadMainWindow.Title = Path.GetFileName(openFileDialog.FileName) + " - NotePad";
            IsTextEdit = false;
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
            File.WriteAllText(saveFileDialog.FileName, NotePadTextBox.Text);
            NotePadMainWindow.Title = Path.GetFileName(saveFileDialog.FileName) + " - NotePad";
            IsTextEdit = false;
        }

        private void EditBackgroundButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();

            colorDialog.FullOpen = false;
            colorDialog.AllowFullOpen = true;

            System.Windows.Forms.DialogResult result = colorDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Media.Color color = new System.Windows.Media.Color();
                color.A = colorDialog.Color.A;
                color.R = colorDialog.Color.R;
                color.G = colorDialog.Color.G;
                color.B = colorDialog.Color.B;

                SolidColorBrush brush = new SolidColorBrush(color);
                NotePadTextBox.Background = brush;
            }
        }

        private void EditForegroundButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();

            colorDialog.FullOpen = false;
            colorDialog.AllowFullOpen = true;

            System.Windows.Forms.DialogResult result = colorDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Media.Color color = new System.Windows.Media.Color();
                color.A = colorDialog.Color.A;
                color.R = colorDialog.Color.R;
                color.G = colorDialog.Color.G;
                color.B = colorDialog.Color.B;

                SolidColorBrush brush = new SolidColorBrush(color);
                NotePadTextBox.Foreground = brush;
            }
        }

        private void EditFontButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();

            fontDialog.ShowEffects = true;
            fontDialog.Color = System.Drawing.Color.Black;

            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                NotePadTextBox.FontFamily = new System.Windows.Media.FontFamily(fontDialog.Font.Name);
                NotePadTextBox.FontSize = fontDialog.Font.Size * 96.0 / 72.0;
                NotePadTextBox.FontWeight = fontDialog.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                NotePadTextBox.FontStyle = fontDialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;

                System.Windows.Media.Color color = new System.Windows.Media.Color();
                color.A = fontDialog.Color.A;
                color.R = fontDialog.Color.R;
                color.G = fontDialog.Color.G;
                color.B = fontDialog.Color.B;
                SolidColorBrush brush = new SolidColorBrush(color);
                NotePadTextBox.Foreground = brush;
            }
        }

        private void FindAndReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            FindAndReplaceWindow findAndReplaceWindow = new FindAndReplaceWindow();
            findAndReplaceWindow.Owner = NotePadMainWindow;
            findAndReplaceWindow.Find += FindAndReplaceWindow_Find;
            findAndReplaceWindow.Replace += FindAndReplaceWindow_Replace;
            findAndReplaceWindow.ReplaceAll += FindAndReplaceWindow_ReplaceAll;
            findAndReplaceWindow.Show();
        }
        private int FindLastIndex = 0;
        private void FindAndReplaceWindow_Find(string FindWhat, string ReplaceWith, bool? IsCaseSensitive, bool? IsTextWrapping)
        {
            int findIndex = NotePadTextBox.Text.IndexOf(FindWhat, FindLastIndex);
            if (findIndex == -1)
            {
                System.Windows.MessageBox.Show("The text was not found.", "FindNext", MessageBoxButton.OK, MessageBoxImage.Information);
                FindLastIndex = 0;
                return;
            }
            NotePadTextBox.Select(findIndex, FindWhat.Length);
            NotePadTextBox.Focus();

            FindLastIndex = findIndex + FindWhat.Length;
        }
        private void FindAndReplaceWindow_Replace(string FindWhat, string ReplaceWith, bool? IsCaseSensitive, bool? IsTextWrapping)
        {
            int findIndex = NotePadTextBox.Text.IndexOf(FindWhat);
            if (findIndex == -1)
            {
                System.Windows.MessageBox.Show("The text was not found.", "Find", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            NotePadTextBox.Select(findIndex, FindWhat.Length);
            NotePadTextBox.SelectedText = ReplaceWith;
            NotePadTextBox.Focus();
        }

        private void FindAndReplaceWindow_ReplaceAll(string FindWhat, string ReplaceWith, bool? IsCaseSensitive, bool? IsTextWrapping)
        {
            int findIndex = NotePadTextBox.Text.IndexOf(FindWhat);
            if (findIndex == -1)
            {
                System.Windows.MessageBox.Show("The text was not found.", "Find", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            NotePadTextBox.Text = NotePadTextBox.Text.Replace(FindWhat, ReplaceWith);
        }
    }
}