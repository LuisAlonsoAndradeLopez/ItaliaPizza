using CrystalDecisions.CrystalReports.Engine;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.Resources;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ItalianPizza.XAMLViews.Articles
{
    /// <summary>
    /// Lógica de interacción para InventoryReportPreview.xaml
    /// </summary>
    public partial class InventoryReportPreview : Page
    {
        public InventoryReportPreview()
        {
            InitializeComponent();

            string incompleteRPTPath = Path.GetFullPath("Resources\\InventoryReport.rpt");
            string RPTpathPartToDelete = "bin\\Debug\\";
            string completeRPTPath = incompleteRPTPath.Replace(RPTpathPartToDelete, "");

            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(completeRPTPath);

            InventoryReportDataSet inventoryReportDataSet = new InventoryReportDataSet();
            InventoryReportDataSet.ArticleDataTable products = inventoryReportDataSet.Article;

            foreach(var product in new ProductDAO().GetProductsForInventoryReport())
            {
                products.AddArticleRow(
                    product.Name,
                    "Producto",
                    product.ProductTypeSet.Type,
                    "NA",
                    product.IdentificationCode,
                    product.PricePerUnit.ToString(),
                    product.Quantity.ToString()
                );
            }

            foreach (var supply in new SupplyDAO().GetSuppliesForInventoryReport())
            {
                products.AddArticleRow(
                    supply.Name,
                    "Insumo",
                    supply.SupplyTypeSet.Type,
                    supply.SupplyUnitSet.Unit,
                    supply.IdentificationCode,
                    supply.PricePerUnit.ToString(),
                    supply.Quantity.ToString()
                );
            }

            reportDocument.SetDataSource(inventoryReportDataSet);

            windowsFormsHost.Child = new CrystalDecisions.Windows.Forms.CrystalReportViewer { ReportSource = reportDocument };
        }

        private void ExitButtonOnClick(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService.Navigate(new GUI_InventoryReport());
        }
    }
}
