using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

using ComboBox = System.Windows.Controls.ComboBox;


namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_AddArticle.xaml
    /// </summary>
    public partial class GUI_AddArticle : Page
    {
        public GUI_AddArticle()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void UsersButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void InventoryButtonOnClick(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService.Navigate(new GUI_Inventory());
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

        private void SelectImageButtonOnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png)|*.png",
                Title = "Selecciona una imágen"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                BitmapImage imageSource = new BitmapImage(new Uri(openFileDialog.FileName));

                if (new ImageManager().GetBitmapImageBytes(imageSource).Length <= 1048576)
                {
                    ArticleImage.Source = imageSource;
                }
                else
                {
                    new AlertPopUpGenerator().OpenErrorPopUp("¡Tamaño de imágen excedido!", "La imágen no debe pesar más de 1MB");
                }
            }
        }

        private void ArticleTypesComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox articleTypesComboBox = (ComboBox)sender;
            string selectedOption = articleTypesComboBox.SelectedItem?.ToString();

            IngredientOrProductTypesComboBox.Items.Clear();

            string[] ingredients = { "Queso", "Carne", "Fruta", "Verdura", "Harina" };
            string[] products = { "Bebida", "Dedos de Queso", "Hamburguesa", "Pizza" };


            if(selectedOption == "Insumo")
            {
                foreach (var ingredient in ingredients)
                {
                    IngredientOrProductTypesComboBox.Items.Add(ingredient);
                }
            }

            if (selectedOption == "Producto")
            {
                foreach (var product in products)
                {
                    IngredientOrProductTypesComboBox.Items.Add(product);
                }
            }

            IngredientOrProductTypesComboBox.SelectedItem = IngredientOrProductTypesComboBox.Items[0];
        }

        private void BackButtonOnClick(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService.Navigate(new GUI_Inventory());
        }

        private void AddArticleButtonOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if(InvalidValuesInTextFieldsTextGenerator() == "")
                {
                    if(ArticleTypesComboBox.SelectedItem?.ToString() == "Insumo") 
                    {
                        Insumo ingredient = new Insumo
                        {
                            Nombre = ArticleNameTextBox.Text,
                            Costo = (double)PriceDecimalUpDown.Value,
                            Descripcion = DescriptionTextBox.Text,
                            Tipo = IngredientOrProductTypesComboBox.SelectedItem?.ToString(),
                            Cantidad = QuantityIntegerUpDown.Value ?? 0,
                            Foto = new ImageManager().GetBitmapImageBytes((BitmapImage)ArticleImage.Source).ToString(),
                            Estado = ArticleStatus.Activo.ToString()
                        };

                            new IngredientDAO().AddIngredient(ingredient);                    
                    }

                    if (ArticleTypesComboBox.SelectedItem?.ToString() == "Producto")
                    {
                        Producto product = new Producto
                        {
                            Nombre = ArticleNameTextBox.Text,
                            Costo = (double)PriceDecimalUpDown.Value,
                            Descripcion = DescriptionTextBox.Text,
                            Categoria = IngredientOrProductTypesComboBox.SelectedItem?.ToString(),
                            //Cantidad = QuantityIntegerUpDown.Value ?? 0,
                            Foto = new ImageManager().GetBitmapImageBytes((BitmapImage)ArticleImage.Source).ToString(),
                            Estado = ArticleStatus.Activo.ToString()
                        };

                        new ProductDAO().AddProduct(product);
                    }

                    new AlertPopUpGenerator().OpenSuccessPopUp("¡Muy Bien!", "¡Artículo registrado con éxito!");
                }
                else
                {
                    new AlertPopUpGenerator().OpenErrorPopUp("¡Campos Incorrectos!", InvalidValuesInTextFieldsTextGenerator());
                }
            }
            catch (Exception ex)
            {
                new AlertPopUpGenerator().OpenSuccessPopUp("¡Error!", "¡Inténtelo más tarde!");
                new ExceptionLogger().LogException(ex);
            }
        }


        public void InitializeComboBoxes()
        {
            string[] articleTypes = { "Insumo", "Producto" };

            foreach (var articleType in articleTypes)
            {
                ArticleTypesComboBox.Items.Add(articleType);
            }

            ArticleTypesComboBox.SelectedItem = ArticleTypesComboBox.Items[0];
        }

        private string InvalidValuesInTextFieldsTextGenerator()
        {
            int textFieldsWithIncorrectValues = 0;

            string finalText = "";

            string articleNamePattern = "^[A-Za-z0-9áéíóúÁÉÍÓÚ\\s]+$";
            string codePattern = "^[A-Za-z0-9áéíóúÁÉÍÓÚ\\s]+$";
            string descriptionPattern = "^[A-Za-z0-9áéíóúÁÉÍÓÚ\\s]+$";

            Regex articleNameRegex = new Regex(articleNamePattern);
            Regex codeRegex = new Regex(codePattern);
            Regex descriptionRegex = new Regex(descriptionPattern);         

            Match articleNameMatch = articleNameRegex.Match(ArticleNameTextBox.Text);
            Match codeMatch = codeRegex.Match(CodeTextBox.Text);
            Match descriptionMatch = descriptionRegex.Match(DescriptionTextBox.Text);

            if (!articleNameMatch.Success || !codeMatch.Success || !descriptionMatch.Success)
            {
                finalText += "Los campos con valores inválidos son: ";
            }

            if (!articleNameMatch.Success)
            {
                finalText = finalText + "Nombre del Artículo" + ".";
                textFieldsWithIncorrectValues++;
            }

            if (!codeMatch.Success)
            {
                if (textFieldsWithIncorrectValues >= 1)
                {
                    finalText = finalText.Substring(0, finalText.Length - 1);
                    finalText = finalText + ", " + "Código" + ".";
                }
                else
                {
                    finalText = finalText + "Código" + ".";
                }

                textFieldsWithIncorrectValues++;
            }

            if (!descriptionMatch.Success)
            {
                if (textFieldsWithIncorrectValues >= 1)
                {
                    finalText = finalText.Substring(0, finalText.Length - 1);
                    finalText = finalText + ", " + "Descripción" + ".";
                }
                else
                {
                    finalText = finalText + "Descripción" + ".";
                }
            }

            return finalText;
        }
    }
}
