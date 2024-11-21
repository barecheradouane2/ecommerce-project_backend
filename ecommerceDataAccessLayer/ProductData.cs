using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerceDataAccessLayer
{

    public class ProductDTO
    {

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int Discount { get; set; }
        public int Stock { get; set; }
        public int CategoryID { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<IFormFile> ProductImage { get; set; }


        public ProductDTO()
        {
       

        }


        public ProductDTO(int ProductID, string ProductName, string Description, decimal Price, int Discount, int Stock, int CategoryID, DateTime CreatedAt, List<IFormFile> ProductImage)
        {
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.Description = Description;
            this.Price = Price;
            this.Discount = Discount;
            this.Stock = Stock;
            this.CategoryID = CategoryID;
            this.CreatedAt = CreatedAt;
            this.ProductImage = ProductImage;   


        }

            


        

      
        public  ProductDTO(int ProductID, string ProductName, string Description, decimal Price,int Discount,int Stock ,int CategoryID ,DateTime CreatedAt)
        {
          this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.Description = Description;
            this.Price = Price;
            this.Discount = Discount;
            this.Stock = Stock;
            this.CategoryID = CategoryID;
            this.CreatedAt = CreatedAt;


        }

       
    }
    public  class ProductData
    {

        private static string _ConnectionString = "Server=localhost;Database=EcommerceDB;User Id=sa;Password=sa123456;Encrypt=True;TrustServerCertificate=True;";


        /*  static string _ConnectionString = "Server=localhost;Database=EcommerceDB;Integrated Security=True;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;"; */


        public static List<ProductDTO> GetAllProducts()
        {
            List<ProductDTO> productsList = new List<ProductDTO>();
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from ProductCatalog", con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader()) {

                        while (reader.Read())
                        {


                            ProductDTO product = new ProductDTO(
                               reader.GetInt32(reader.GetOrdinal("ProductID")),
                                reader.GetString(reader.GetOrdinal("ProductName")),
                              reader.GetString(reader.GetOrdinal("Description")),
                           reader.GetDecimal(reader.GetOrdinal("Price")),  // Convert Decimal to Int
                             reader.GetInt16(reader.GetOrdinal("Discount")),  // Convert Decimal to Int
                                  reader.GetInt32(reader.GetOrdinal("Stock")),
                              reader.GetInt32(reader.GetOrdinal("CategoryID")),
                           reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                             );

                            productsList.Add( product);
                        }


                    }

                }
             }
            return productsList;
        }


        public static ProductDTO GetProductByID(int ProductID)
        {

            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from ProductCatalog where ProductID = @ProductID", con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //  return new ProductDTO(reader.GetInt32(reader.GetOrdinal("ProductID")), reader.GetString(reader.GetOrdinal("ProductName")), reader.GetString(reader.GetOrdinal("Description")), reader.GetInt32(reader.GetOrdinal("Price")), reader.GetInt32(reader.GetOrdinal("Discount")), reader.GetInt32(reader.GetOrdinal("Stock")), reader.GetInt32(reader.GetOrdinal("CategoryID")), reader.GetDateTime("CreatedAt"));

                            return new ProductDTO(
                               reader.GetInt32(reader.GetOrdinal("ProductID")),
                                reader.GetString(reader.GetOrdinal("ProductName")),
                              reader.GetString(reader.GetOrdinal("Description")),
                           reader.GetDecimal(reader.GetOrdinal("Price")),  // Convert Decimal to Int
                             reader.GetInt16(reader.GetOrdinal("Discount")),  // Convert Decimal to Int
                                  reader.GetInt32(reader.GetOrdinal("Stock")),
                              reader.GetInt32(reader.GetOrdinal("CategoryID")),
                           reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                             );

                        }
                    }

                }
            }
            return null;

        }


        public static int AddNewProduct(ProductDTO product)
        {
            int ProductID = -1;

            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into ProductCatalog(ProductName,Description,Price,Discount,Stock,CategoryID,CreatedAt) values(@ProductName,@Description,@Price,@Discount,@Stock,@CategoryID,@CreatedAt);SELECT SCOPE_IDENTITY();", con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Discount", product.Discount);
                    cmd.Parameters.AddWithValue("@Stock", product.Stock);
                    cmd.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                    cmd.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);
                    con.Open();
                    ProductID= Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return ProductID;
        }

        public static int AddNewProductImage(String ImageUrl, int ImageOrder,int ProductID)
        {
            int ID = -1;

            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into ProductImages(ImageUrl,ImageOrder,ProductID) values(@ImageUrl,@ImageOrder,@ProductID);SELECT SCOPE_IDENTITY();", con))
                {
                    cmd.Parameters.AddWithValue("@ImageUrl", ImageUrl);
                    cmd.Parameters.AddWithValue("@ImageOrder", ImageOrder);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                  
                    con.Open();
                    ID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return ID;
        }



      


        public static bool UpdateProduct(ProductDTO product)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update ProductCatalog set ProductName=@ProductName,Description=@Description,Price=@Price,Discount=@Discount,Stock=@Stock,CategoryID=@CategoryID,CreatedAt=@CreatedAt where ProductID=@ProductID", con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Discount", product.Discount);
                    cmd.Parameters.AddWithValue("@Stock", product.Stock);
                    cmd.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                    cmd.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return true;
        }

        public static bool DeleteProduct(int ProductID)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM ProductCatalog WHERE ProductID = @ProductID", con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery(); // Use ExecuteNonQuery for DELETE statement

                    return rowsAffected > 0; // Return true if at least one row was deleted
                }

            }


        }




    }
}
