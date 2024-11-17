using ecommerceDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerceApiBusinessLayer
{
    public class Category
    {


        public enum enMode { AddNew = 0, Update = 1 }

        public enMode Mode = enMode.AddNew;
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

      

        public CategoryDTO CDTO
        {
            get { return (new CategoryDTO(this.CategoryID, this.CategoryName, this.Description)); }
        }



        public Category(CategoryDTO CDTO, enMode cMode = enMode.AddNew)
        {
            this.CategoryID = CDTO.CategoryID;
            this.CategoryName = CDTO.CategoryName;
            this.Description = CDTO.Description;

            Mode = cMode;
        }


        private bool _AddNewCategory()
        {
            this.CategoryID = CategoryData.AddNewCategory(this.CDTO);
            return CategoryID!=-1;
        }

        private bool _UpdateNewCategory()
        {
            return CategoryData.UpdateCategory(this.CDTO);
        }

        public static List<CategoryDTO> GetAllCategories()
        {
            return CategoryData.GetAllCategories();
        }

        public static Category Find (int CategoryID)
        {
          CategoryDTO CDTO =CategoryData.GetCategoryByID(CategoryID);

            if (CDTO != null)
            {
                return new Category(CDTO, enMode.Update);
            }
            else
                return null;
         
        }


        public bool Delete()
        {
            return CategoryData.DeleteCategory(this.CategoryID);
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if (_AddNewCategory())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (Mode == enMode.Update)
            {
                return _UpdateNewCategory();
            }

            return false;

        }


    }
}
