using ItalianPizza.Auxiliary;
using System;
using System.Windows;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para AlertPopup.xaml
    /// </summary>
    public partial class AlertPopup : Window
    {
        private bool DecisionOfDecisionPopup = false;

        public AlertPopup(string header, string message, AlertPopupTypes alertPopupType)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.NoResize;
            this.Title = header;

            InitializeComponent();

            MessageTextBlock.Text = message;


            switch (alertPopupType)
            {
                case AlertPopupTypes.Decision:
                    DecisionAlertPopupButtonsPane.Visibility = Visibility.Visible;
                    AlertPopupImage.Source = new ImageManager().GetImageByItaliaPizzaStoragedImagePath("Resources\\Pictures\\AlertPopupImages\\ICON-Decision.png");
                    break;

                case AlertPopupTypes.Error:
                    ErrorSuccessOrWarningAlertPopupButtonsPane.Visibility = Visibility.Visible;
                    AlertPopupImage.Source = new ImageManager().GetImageByItaliaPizzaStoragedImagePath("Resources\\Pictures\\AlertPopupImages\\ICON-Error.png");
                    break;

                case AlertPopupTypes.Success:
                    ErrorSuccessOrWarningAlertPopupButtonsPane.Visibility = Visibility.Visible;
                    AlertPopupImage.Source = new ImageManager().GetImageByItaliaPizzaStoragedImagePath("Resources\\Pictures\\AlertPopupImages\\ICON-Success.png");
                    break;

                case AlertPopupTypes.Warning:
                    ErrorSuccessOrWarningAlertPopupButtonsPane.Visibility = Visibility.Visible;
                    AlertPopupImage.Source = new ImageManager().GetImageByItaliaPizzaStoragedImagePath("Resources\\Pictures\\AlertPopupImages\\ICON-Warning.png");
                    break;

                default:
                    this.Title = "Pene";
                    break;

            }

            this.ShowDialog();
        }
        private void AcceptButtonOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void YesButtonOnClick(object sender, RoutedEventArgs e)
        {
            DecisionOfDecisionPopup = true;
            this.Close();
        }

        private void NoButtonOnClick(object sender, RoutedEventArgs e)
        {
            DecisionOfDecisionPopup = false;
            this.Close();
        }


        //Use only if the DecisionPopupType is used
        public bool GetDecisionOfDecisionPopup()
        {
            return DecisionOfDecisionPopup;
        }
    }
}
