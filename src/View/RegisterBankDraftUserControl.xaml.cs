﻿using Common;
using Entity;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterBankDraftUserControl.xaml
    /// </summary>
    public partial class RegisterBankDraftUserControl : UserControl
    {
        public RegisterBankDraftUserControl()
        {
            InitializeComponent();
        }

        private async void RegisterBankDraftButton_Click(object sender, RoutedEventArgs e)
        {
            if (DeliveryFields.Sender is null)
            {
                if (await MainWindow.Client.Send(TypeCommand.Search, TypeData.Person, DeliveryFields.SenderField.Text))
                    DeliveryFields.Sender = await MainWindow.Client.ReceiveObject() as Person;
            }

            if (DeliveryFields.Receiver is null)
            {
                if (await MainWindow.Client.Send(TypeCommand.Search, TypeData.Person, DeliveryFields.ReceiverField.Text))
                    DeliveryFields.Receiver = await MainWindow.Client.ReceiveObject() as Person;
            }

            if (DeliveryFields.Sender is null)
            {
                MessageBoxResult result = MessageBox.Show("Remitente no registrado. ¿Desea registrarlo?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                    DeliveryFields.ShowRegisterPerson(DeliveryFields.SenderField.Text, this);

                return;
            }

            if (DeliveryFields.Receiver is null)
            {
                MessageBoxResult result = MessageBox.Show("Destinario no registrado. ¿Desea registrarlo?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                    DeliveryFields.ShowRegisterPerson(DeliveryFields.ReceiverField.Text, this);

                return;
            }

            if (!int.TryParse(ValueToSendField.Text, out int valueToSend))
            {
                MessageBox.Show("Valor a enviar invalido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(CostField.Text, out int cost))
            {
                MessageBox.Show("Costo invalido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (await MainWindow.Client.Send(ClientRequest.GetDeliveriesCount))
            {
                string deliveryNumber = ((int)await MainWindow.Client.ReceiveObject() + 1).ToString("00000");

                BankDraft bankDraft = new BankDraft(deliveryNumber, DeliveryFields.Sender, DeliveryFields.Receiver, MainWindow.AdministrativeEmployee,
                                                    DeliveryFields.DestinationComboBox.SelectedItem as string, valueToSend, cost);

                if (await MainWindow.Client.Send(TypeCommand.Save, TypeData.BankDraft, bankDraft))
                    HandleServerAnswer();
            }
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer == ServerAnswer.SaveSuccessful)
            {
                MessageBox.Show("Datos guardadados", "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (answer == ServerAnswer.DataAlreadyRegistered)
            {
                MessageBox.Show("Error al guardar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
