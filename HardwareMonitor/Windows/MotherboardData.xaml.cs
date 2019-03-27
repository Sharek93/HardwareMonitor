using HardwareMonitor.Resources;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HardwareMonitor.Windows
{
    /// <summary>
    /// Interaction logic for MotherboardData.xaml
    /// </summary>
    public partial class MotherboardData : Window
    {
        private bool _interfacePrepared = false;
        private int currentRowCount;

        public MotherboardData()
        {
            InitializeComponent();            
        }

        private void MotherboardDataWindow_Deactivated(object sender, EventArgs e)
        {
            if (AppInfo.TopMost)
            {
                ((Window)sender).Topmost = true;
                return;
            }
            ((Window)sender).Topmost = false;
        }

        public void PrepareInterface(int rowCount)
        {
            if (!_interfacePrepared || currentRowCount != rowCount)
            {
                currentRowCount = rowCount;
                Height = rowCount * 20 + 25;
                if (MainGrid.Children.Count > 1)
                {
                    MainGrid.Children.RemoveAt(1);
                }                

                var grid = new Grid
                {
                    Name = "DataGrid"
                };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                for (int i = 0; i < rowCount; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                    var keyLabel = new Label
                    {
                        Name = "KeyLabelName_" + i,
                        Content = "TestKey_" + i,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(5, 0, 5, 0),
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 11,
                        Foreground = Brushes.White
                    };
                    Grid.SetRow(keyLabel, i);
                    Grid.SetColumn(keyLabel, 0);
                    grid.Children.Add(keyLabel);

                    var valueLabel = new Label
                    {
                        Name = "ValueLabelName_" + i,
                        Content = "TestValue_" + i,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(5, 0, 5, 0),
                        FontFamily = new FontFamily("Calibri"),
                        FontSize = 11,
                        Foreground = Brushes.White
                    };
                    Grid.SetRow(valueLabel, i);
                    Grid.SetColumn(valueLabel, 1);
                    grid.Children.Add(valueLabel);
                }
                Grid.SetRow(grid, 1);

                MainGrid.Children.Add(grid);

                ApplyTemplate();
                _interfacePrepared = true;
            }            
        }
    }
}
