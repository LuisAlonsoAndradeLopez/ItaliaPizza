using ItalianPizza.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItalianPizza.XAMLViews.Finances
{
    /// <summary>
    /// Lógica de interacción para DailyBalanceJustification.xaml
    /// </summary>
    public partial class DailyBalanceJustification : UserControl
    {

        public event EventHandler<string> JustificationRegistered;
        public event EventHandler Cancelled;


        public DailyBalanceJustification()
        {
            InitializeComponent();
        }

        private void RegisterDailyBalance_Clic(object sender, RoutedEventArgs e)
        {
            string justification = txtJustification.Text;

            if (string.IsNullOrEmpty(justification))
            {
                new AlertPopup("La justificación no puede estar vacía", "Error", AlertPopupTypes.Error);
            }else if (new AlertPopup("Confirmar justificación", "¿Deseas continuar?", AlertPopupTypes.Decision).GetDecisionOfDecisionPopup())
            {
                JustificationRegistered?.Invoke(this, justification);

                Panel parentPanel = this.Parent as Panel;
                if (parentPanel != null)
                {
                    parentPanel.Children.Remove(this);
                }
            }
        }

        private void CancelDailyBalanceRegistration_Clic(object sender, RoutedEventArgs e)
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}
