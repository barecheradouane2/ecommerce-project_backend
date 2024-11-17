using ecommerceDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerceApiBusinessLayer
{
    public class Product
    {

        public enum enMode { AddNew=0,Update=1}

        public enMode Mode  =enMode.AddNew;

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int Discount { get; set; }
        public int Stock { get; set; }
        public int CategoryID { get; set; }

        public DateTime CreatedAt { get; set; }

        public ProductDTO PDTO { 
            get { return  (new ProductDTO(this.ProductID,this.ProductName,this.Description,this.Price,this.Discount,this.Stock,this.CategoryID,this.CreatedAt)); }
        }

        public Product(ProductDTO pDTO ,enMode cMode =enMode.AddNew)
        {
            this.ProductID = pDTO.ProductID;
            this.ProductName = pDTO.ProductName;
            this.Description = pDTO.Description;
            this.Price = pDTO.Price;
            this.Discount = pDTO.Discount;
            this.Stock = pDTO.Stock;
            this.CategoryID = pDTO.CategoryID;
            this.CreatedAt = pDTO.CreatedAt;

            Mode = cMode;
        }


        private bool _AddNewProduct()
        {
            this.ProductID = ProductData.AddNewProduct(this.PDTO);
            return ProductID!=-1;
        }

        private bool _UpdateNewProduct()
        {
            return ProductData.UpdateProduct(this.PDTO);
        }

        public static List<ProductDTO> GetAllProducts()
        {
            return ProductData.GetAllProducts();
        }

        public static Product Find(int ID)
        {
            ProductDTO PDTO = ProductData.GetProductByID(ID);
            if (PDTO != null)
            {
                return new Product(PDTO, enMode.Update);

            }
            else
                return null;

        }


        public bool Delete()
        {
          return  ProductData.DeleteProduct(this.ProductID);
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if (_AddNewProduct())
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
                return _UpdateNewProduct();
            }

            return false;
        }

        //getProduct images 
        // change status

        // i shoud take care and mange also the size of the product s xxl xxl xxxl l or 39  40 41 42 43 44 45 and there stock

    }
}
