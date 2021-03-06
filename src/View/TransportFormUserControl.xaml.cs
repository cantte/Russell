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
    /// Interaction logic for TransportFormUserControl.xaml
    /// </summary>
    public partial class TransportFormUserControl : UserControl
    {
        public TransportFormUserControl()
        {
            InitializeComponent();
        }

        private void RegisterTransportForm_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new RegisterTransportFormUserControl(AfterRegisterTransportForm));
        }

        private void AfterRegisterTransportForm()
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new CurrentTransportFormUserControl());
        }

        private void SeeCurrentTransportFrom_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new CurrentTransportFormUserControl());
        }

        private void SearchTransportForm_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new SearchTransportFormUserControl());
        }
    }
}
