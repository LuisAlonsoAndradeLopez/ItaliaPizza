using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
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

            PriceDecimalUpDown.Text = "$0.00";
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

                if (new ImageManager().GetBitmapImageBytes(imageSource).Length <= 10 * 1024 * 1024)
                {
                    ArticleImage.Source = imageSource;
                }
                else
                {
                    new AlertPopup("¡Tamaño de imágen excedido!", "La imágen no debe pesar más de 10MB", AlertPopupTypes.Error);
                }
            }
        }

        private void ArticleTypesComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox articleTypesComboBox = (ComboBox)sender;
            string selectedOption = articleTypesComboBox.SelectedItem?.ToString();

            SupplyOrProductTypesComboBox.Items.Clear();
            SupplyUnitsComboBox.Items.Clear();

            List<SupplyTypeSet> supplyCategories = new SupplyTypeDAO().GetAllSupplyTypes();
            List<ProductTypeSet> productCategories = new ProductTypeDAO().GetAllProductTypes();


            if(selectedOption == ArticleTypes.Insumo.ToString())
            {
                foreach (var supply in supplyCategories)
                {
                    SupplyOrProductTypesComboBox.Items.Add(supply.Type);
                }

                NameAndArticleTypeStackPanel.Margin = new Thickness(0, 74, 0, 0);
                SupplyUnitsStackPanel.Visibility = Visibility.Visible;
                DescriptionStackPanel.Visibility = Visibility.Collapsed;
            }

            if (selectedOption == ArticleTypes.Producto.ToString())
            {
                foreach (var product in productCategories)
                {
                    SupplyOrProductTypesComboBox.Items.Add(product.Type);
                }

                NameAndArticleTypeStackPanel.Margin = new Thickness(0, 10, 0, 0);
                SupplyUnitsStackPanel.Visibility = Visibility.Collapsed;
                DescriptionStackPanel.Visibility = Visibility.Visible;
            }

            SupplyOrProductTypesComboBox.SelectedItem = SupplyOrProductTypesComboBox.Items[0];

            List<SupplyUnitSet> supplyUnits = new SupplyUnitDAO().GetAllSupplyUnits();

            foreach (var supplyUnit in supplyUnits)
            {
                SupplyUnitsComboBox.Items.Add(supplyUnit.Unit);
            }

            SupplyUnitsComboBox.SelectedItem = SupplyUnitsComboBox.Items[0];
        }

        private void QuantityIntegerUpDownPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
                if (SupplyOrProductTypesComboBox.SelectedItem != null || SupplyOrProductTypesComboBox.SelectedItem?.ToString() != "")
                {
                    if (SupplyUnitsComboBox.SelectedItem != null || SupplyUnitsComboBox.SelectedItem?.ToString() != "")
                    {
                        if (InvalidValuesInTextFieldsTextGenerator() == "")
                        {
                            if (new ImageManager().GetBitmapImageBytes((BitmapImage)ArticleImage.Source) != null)
                            {
                                if (!new SupplyDAO().TheNameIsAlreadyRegistred(ArticleNameTextBox.Text) &&
                                    !new ProductDAO().TheNameIsAlreadyRegistred(ArticleNameTextBox.Text))
                                {
                                    if (!new SupplyDAO().TheCodeIsAlreadyRegistred(CodeTextBox.Text) &&
                                        !new ProductDAO().TheCodeIsAlreadyRegistred(CodeTextBox.Text))
                                    {

                                        double price;

                                        if (PriceDecimalUpDown.Value != null)
                                        {
                                            price = (double)PriceDecimalUpDown.Value;
                                        }
                                        else
                                        {
                                            price = 0;
                                        }

                                        if (ArticleTypesComboBox.SelectedItem?.ToString() == ArticleTypes.Insumo.ToString())
                                        {
                                            SupplySet supply = new SupplySet
                                            {
                                                Name = ArticleNameTextBox.Text,
                                                Quantity = QuantityIntegerUpDown.Value ?? 0,
                                                PricePerUnit = price,
                                                Picture = new ImageManager().GetBitmapImageBytes((BitmapImage)ArticleImage.Source),
                                                SupplyUnitId = new SupplyUnitDAO().GetSupplyUnitByName(SupplyUnitsComboBox.SelectedItem?.ToString()).Id,
                                                ProductStatusId = new ProductStatusDAO().GetProductStatusByName(ArticleStatus.Activo.ToString()).Id,
                                                SupplyTypeId = new SupplyTypeDAO().GetSupplyTypeByName(SupplyOrProductTypesComboBox.SelectedItem?.ToString()).Id,
                                                EmployeeId = 2,
                                                IdentificationCode = CodeTextBox.Text
                                            };

                                            new SupplyDAO().AddSupply(supply);
                                        }

                                        if (ArticleTypesComboBox.SelectedItem?.ToString() == ArticleTypes.Producto.ToString())
                                        {
                                            ProductSaleSet product = new ProductSaleSet
                                            {
                                                Name = ArticleNameTextBox.Text,
                                                Quantity = QuantityIntegerUpDown.Value ?? 0,
                                                PricePerUnit = price,
                                                Picture = new ImageManager().GetBitmapImageBytes((BitmapImage)ArticleImage.Source),
                                                ProductStatusId = new ProductStatusDAO().GetProductStatusByName(ArticleStatus.Activo.ToString()).Id,
                                                ProductTypeId = new ProductTypeDAO().GetProductTypeByName(SupplyOrProductTypesComboBox.SelectedItem?.ToString()).Id,
                                                EmployeeId = 2,
                                                IdentificationCode = CodeTextBox.Text,
                                                Description = DescriptionTextBox.Text
                                            };

                                            new ProductDAO().AddProduct(product);
                                        }

                                        new AlertPopup("¡Muy bien!", "Artículo registrado con éxito", AlertPopupTypes.Success);
                                    }
                                    else
                                    {
                                        new AlertPopup("¡Código ya usado!", "El código ya está usado, por favor introduzca otro", AlertPopupTypes.Error);
                                    }
                                }
                                else
                                {
                                    new AlertPopup("¡Nombre ya usado!", "El nombre ya está usado, por favor introduzca otro", AlertPopupTypes.Error);
                                }
                            }
                            else
                            {
                                new AlertPopup("¡Falta la imágen!", "Falta que selecciones la imágen", AlertPopupTypes.Error);
                            }
                        }
                        else
                        {
                            new AlertPopup("¡Campos Incorrectos!", InvalidValuesInTextFieldsTextGenerator(), AlertPopupTypes.Error);
                        }
                    }
                    else
                    {
                        new AlertPopup("¡Falta el tipo de insumo!", "Falta que selecciones el tipo de insumo", AlertPopupTypes.Error);
                    }
                }
                else
                {
                    new AlertPopup("¡Falta el tipo de artículo!", "Falta que selecciones el tipo de artículo", AlertPopupTypes.Error);
                }
            }
            catch (DbEntityValidationException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);

                string incompletePath = Path.GetFullPath("ValidationErrors.txt");
                string pathPartToDelete = "ItalianPizza\\bin\\Debug\\";
                string completePath = incompletePath.Replace(pathPartToDelete, "");

                using (StreamWriter writer = new StreamWriter(completePath))
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            writer.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                }
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }


        private void InitializeComboBoxes()
        {
            string[] articleTypes = { ArticleTypes.Insumo.ToString(), ArticleTypes.Producto.ToString() };

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
            string quantityPattern = "^\\d+";
            string pricePerUnitPattern = "^\\d+(\\.\\d{2})?$";
            string codePattern = "^[A-Za-z0-9áéíóúÁÉÍÓÚ\\s]+$";
            string descriptionPattern = "^[A-Za-z0-9áéíóúÁÉÍÓÚ\\s]+$";

            Regex articleNameRegex = new Regex(articleNamePattern);
            Regex quantityRegex = new Regex(quantityPattern);
            Regex pricePerUnitRegex = new Regex(pricePerUnitPattern);
            Regex codeRegex = new Regex(codePattern);
            Regex descriptionRegex = new Regex(descriptionPattern);         

            Match articleNameMatch = articleNameRegex.Match(ArticleNameTextBox.Text);
            Match quantityMatch = quantityRegex.Match(QuantityIntegerUpDown.Value.ToString());
            Match pricePerUnitMatch = pricePerUnitRegex.Match(PriceDecimalUpDown.Value.ToString());
            Match codeMatch = codeRegex.Match(CodeTextBox.Text);

            Match descriptionMatch = null;

            if (ArticleTypesComboBox.SelectedItem.ToString() == ArticleTypes.Insumo.ToString())
            {
                descriptionMatch = descriptionRegex.Match("A");
            }

            if (ArticleTypesComboBox.SelectedItem.ToString() == ArticleTypes.Producto.ToString())
            {
                descriptionMatch = descriptionRegex.Match(DescriptionTextBox.Text);
            }


            if (!articleNameMatch.Success || !quantityMatch.Success || !pricePerUnitMatch.Success || !codeMatch.Success || !descriptionMatch.Success)
            {
                finalText += "Los campos con valores inválidos son: ";
            }

            if (!articleNameMatch.Success)
            {
                finalText = finalText + "Nombre del Artículo" + ".";
                textFieldsWithIncorrectValues++;
            }

            if (!quantityMatch.Success)
            {
                if (textFieldsWithIncorrectValues >= 1)
                {
                    finalText = finalText.Substring(0, finalText.Length - 1);
                    finalText = finalText + ", " + "Cantidad" + ".";
                }
                else
                {
                    finalText = finalText + "Cantidad" + ".";
                }

                textFieldsWithIncorrectValues++;
            }

            if (!pricePerUnitMatch.Success)
            {
                if (textFieldsWithIncorrectValues >= 1)
                {
                    finalText = finalText.Substring(0, finalText.Length - 1);
                    finalText = finalText + ", " + "Precio" + ".";
                }
                else
                {
                    finalText = finalText + "Precio" + ".";
                }

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
