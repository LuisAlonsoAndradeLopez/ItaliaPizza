using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_Inventory.xaml
    /// </summary>
    public partial class GUI_Inventory : Page
    {
        public GUI_Inventory()
        {
            InitializeComponent();
        }

        private void UsersButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void OrdersButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void FinanceButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ProvidersButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void CloseSessionButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddArticleButtonOnClick(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService.Navigate(new GUI_AddArticle());
        }

        private void GenerateInventoryReportOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ArticleButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void SelectImageButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyArticleButtonOnClick1(object sender, RoutedEventArgs e)
        {

        }

        private void ConsultArticleRecipeButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void BackButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void DisableArticleButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyArticleButtonOnClick2(object sender, RoutedEventArgs e)
        {

        }
    }
}
