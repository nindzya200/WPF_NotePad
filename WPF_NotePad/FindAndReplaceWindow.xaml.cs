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
using System.Windows.Shapes;

namespace WPF_NotePad
{
    /// <summary>
    /// Логика взаимодействия для FindAndReplaceWindow.xaml
    /// </summary>
    public partial class FindAndReplaceWindow : Window
    {
        public delegate void Data(string FindWhat, string ReplaceWith, bool? IsCaseSensitive, bool? IsTextWrapping);
        public event Data Find;
        public event Data Replace;
        public event Data ReplaceAll;
        public FindAndReplaceWindow()
        {
            InitializeComponent();
        }

        private void FindNextButton_Click(object sender, RoutedEventArgs e)
        {
            Find?.Invoke(WhatTextBox.Text, "", CaseSensitiveCheckBox.IsChecked, TextWrappingCheckBox.IsChecked);
        }

        private void ReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            Replace?.Invoke(WhatTextBox.Text, WithTextBox.Text, CaseSensitiveCheckBox.IsChecked, TextWrappingCheckBox.IsChecked);
        }

        private void ReplaceAllButton_Click(object sender, RoutedEventArgs e)
        {
            ReplaceAll?.Invoke(WhatTextBox.Text, WithTextBox.Text, CaseSensitiveCheckBox.IsChecked, TextWrappingCheckBox.IsChecked);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WhatTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (WhatTextBox.Text.Length > 0)
            {
                FindNextButton.IsEnabled = true;
                ReplaceButton.IsEnabled = true;
                ReplaceAllButton.IsEnabled = true;
            }
            else
            {
                FindNextButton.IsEnabled = false;
                ReplaceButton.IsEnabled = false;
                ReplaceAllButton.IsEnabled = false;
            }
        }
    }
}
