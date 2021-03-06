﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for RoutesUserControl.xaml
    /// </summary>
    public partial class RoutesUserControl : UserControl
    {
        public RoutesUserControl()
        {
            InitializeComponent();
        }

        private void AddRouteButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new RegisterRouteUserControl());
        }

        private void ListRoutesButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new RoutesViewUserControl());
        }
    }
}
