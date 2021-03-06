﻿using Common;
using Entity;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterTicketUserControl.xaml
    /// </summary>
    public partial class RegisterTicketUserControl : UserControl
    {
        private Passenger _passenger;
        readonly IAfterRegister _afterRegister;
        readonly CloseAction _closeAction;
        public RegisterTicketUserControl()
        {
            InitializeComponent();
            TicketCode.Text += $"{CurrentTransportFormUserControl.CurrentTransportForm.Number}-{CurrentTransportFormUserControl.CurrentTransportForm.Tickets.Count + 1}";
            TicketDate.Text += DateTime.Now.ToShortDateString();
            TicketDispatcher.Text += MainWindow.AdministrativeEmployee.Name;
        }

        public RegisterTicketUserControl(IAfterRegister afterRegister, CloseAction closeAction) : this()
        {
            _afterRegister = afterRegister;
            _closeAction = closeAction;
        }

        private void SearhPassenger_Click(object sender, RoutedEventArgs e)
        {
            SelectPerson.Child = new PeopleViewUserControl(SetPassenger, CloseSelectPerson);
            SelectPerson.IsOpen = true;
        }

        private void CloseSelectPerson()
        {
            SelectPerson.IsOpen = false;
        }

        private void SetPassenger(Person person)
        {
            SelectPerson.IsOpen = false;
            _passenger = person.ToPassenger();
            PassenderID.Text = _passenger.ID;
        }

        private async void AddNewPassenger_Click(object sender, RoutedEventArgs e)
        {
            if (_passenger is null)
            {
                if (await MainWindow.Client.Send(TypeCommand.Search, TypeData.Person, PassenderID.Text))
                    _passenger = (await MainWindow.Client.ReceiveObject() as Person).ToPassenger();

                if (_passenger is null)
                    return;
            }

            CurrentTransportFormUserControl.CurrentTransportForm?.AddTicket(_passenger, int.Parse(SeatsField.Text));

            if (await MainWindow.Client.Send(TypeCommand.Save, TypeData.Ticket, CurrentTransportFormUserControl.CurrentTransportForm.Tickets.Last()))
                HandleServerAnswer();
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer != ServerAnswer.SaveSuccessful)
            {
                MessageBox.Show("Pasajero no registrado. Revise el estado de su conexion.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CurrentTransportFormUserControl.CurrentTransportForm.Tickets.Remove(CurrentTransportFormUserControl.CurrentTransportForm.Tickets.Last());
            }

            _afterRegister?.AfterRegister();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            _closeAction?.Invoke();
        }
    }
}
