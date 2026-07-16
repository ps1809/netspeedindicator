using System;
using System.Windows;
using System.Windows.Controls;

namespace NetSpeedWidget
{
    public partial class CustomFontSizeDialog : Window
    {
        public double SelectedFontSize { get; private set; }
        public bool IsCancelled { get; private set; } = false;

        public CustomFontSizeDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected value from the ComboBox
            var selectedItem = FontSizeComboBox.SelectedItem;
            if (selectedItem != null && selectedItem is System.Windows.Controls.ComboBoxItem comboBoxItem)
            {
                if (double.TryParse(comboBoxItem.Content.ToString(), out double fontSize))
                {
                    if (fontSize >= 6 && fontSize <= 24)
                    {
                        SelectedFontSize = fontSize;
                        DialogResult = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Font size must be between 6 and 24.", "Invalid Input", 
                                      MessageBoxButton.OK, MessageBoxImage.Warning);
                        FontSizeComboBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid font size.", "Invalid Input", 
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    FontSizeComboBox.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please select a font size.", "Invalid Input", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                FontSizeComboBox.Focus();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            IsCancelled = true;
            DialogResult = false;
            Close();
        }
    }
}
