﻿using Common;
using System.Collections;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RoutesViewUserControl.xaml
    /// </summary>
    public partial class RoutesViewUserControl : UserControl
    {
        public RoutesViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            if (await MainWindow.Client.Send(ClientRequest.GetRoutes))
                Routes.ItemsSource = await MainWindow.Client.ReceiveObject() as IEnumerable;
        }
    }
}
