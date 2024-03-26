using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Data.Entity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;
using ItalianPizza.Auxiliary;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class IngredientDAO
    {
        public int AddIngredient(Insumo ingredient)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.InsumoSet.Add(ingredient);
                    context.SaveChanges();
                }

                result = 1;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return result;
        }

        public int DisableIngredient(string ingredientName)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    Insumo ingredientToDisable = context.InsumoSet.Where(i => i.Nombre == ingredientName).FirstOrDefault();
                    if (ingredientToDisable != null)
                    {
                        ingredientToDisable.Estado = ArticleStatus.Inactivo.ToString();
                        context.SaveChanges();
                    }
                }

                result = 1;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return result;
        }

        public BitmapImage GetImageByIngredientName(string ingredientName)
        {
            string imageDataInString;

            using (var context = new ItalianPizzaServerBDEntities())
            {
                imageDataInString = context.InsumoSet.Where(i => i.Nombre == ingredientName).First().Foto;
            }

            byte[] imageData = Convert.FromBase64String(imageDataInString);

            BitmapImage imageSource = new BitmapImage();

            if (imageData != null)
            {
                imageSource.BeginInit();
                imageSource.StreamSource = new MemoryStream(imageData);
                imageSource.EndInit();
            }

            return imageSource;
        }

        public Insumo GetIngredientByName(string ingredientName)
        {
            Insumo ingredient = new Insumo();
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    ingredient = context.InsumoSet.Where(i => i.Nombre == ingredientName).FirstOrDefault();
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return ingredient;
        }

        public List<Insumo> GetSpecifiedIngredientsByNameOrCode(string textForFindingArticle, string findByType)
        {
            List<Insumo> specifiedIngredients = new List<Insumo>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                if (findByType == "Nombre")
                {
                    specifiedIngredients = context.InsumoSet.Where(p => p.Nombre.StartsWith(textForFindingArticle)).ToList();
                }

                if (findByType == "Código")
                {
                    //specifiedIngredients = context.InsumoSet.Where(p => p.Código.StartsWith(textForFindingArticle)).ToList();
                }
            }

            return specifiedIngredients;
        }

        public int ModifyIngredient(Insumo modifiedIngredient)
        {
            int generatedID = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    Insumo ingredientToDisable = context.InsumoSet.Where(i => i.Nombre == modifiedIngredient.Nombre).FirstOrDefault();
                    if (ingredientToDisable != null)
                    {
                        ingredientToDisable.Id = modifiedIngredient.Id;
                        ingredientToDisable.Nombre = modifiedIngredient.Nombre;
                        ingredientToDisable.Costo = modifiedIngredient.Costo;
                        ingredientToDisable.Descripcion = modifiedIngredient.Descripcion;
                        //ingredientToDisable.Categoria = modifiedIngredient.Categoria;
                        ingredientToDisable.Foto = modifiedIngredient.Foto;
                        ingredientToDisable.Estado = modifiedIngredient.Estado;
                        ingredientToDisable.Empleado = modifiedIngredient.Empleado;
                        context.SaveChanges();
                        generatedID = (int)ingredientToDisable.Id;
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return generatedID;
        }
    }
}
