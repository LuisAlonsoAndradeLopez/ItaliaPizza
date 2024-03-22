using System.Windows;
using System;

namespace ItalianPizza.Auxiliary
{
    public class AlertPopUpGenerator
    {
        public void OpenErrorPopUp(String header, String message)
        {
            MessageBox.Show(
                message,
                header,
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }

        public  void OpenSuccessPopUp(String header, String message)
        {
            MessageBox.Show(
                message,
                header,
                MessageBoxButton.OK,
                MessageBoxImage.None
            );
        }
    }
}
