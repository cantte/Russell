﻿using Common;
using System.Collections;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for CommendsViewUserControl.xaml
    /// </summary>
    public partial class CommendsViewUserControl : UserControl
    {
        public CommendsViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            if (await MainWindow.Client.Send(ClientRequest.GET_ALL_COMMENDS))
                Commends.ItemsSource = await MainWindow.Client.RecieveObject() as IEnumerable;
        }
    }
}
