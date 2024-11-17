using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerceDataAccessLayer
{

     public class CategoryDTO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }


        public CategoryDTO(int CategoryID, string CategoryName, string Description)
        {
            this.CategoryID = CategoryID;
            this.CategoryName = CategoryName;
            this.Description = Description;
        }

    }
    public class CategoryData
    {

        private static string _ConnectionString = "Server=localhost;Database=EcommerceDB;User Id=sa;Password=sa123456;Encrypt=True;TrustServerCertificate=True;";
        public static List<CategoryDTO> GetAllCategories()
        {
            List<CategoryDTO> categoriesList = new List<CategoryDTO>();
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from ProductCategory", con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            
                            CategoryDTO category = new CategoryDTO(reader.GetInt32(reader.GetOrdinal("CategoryID")), reader.GetString(reader.GetOrdinal("CategoryName")), reader.GetString(reader.GetOrdinal("Description")));
                            categoriesList.Add(category);
                        }
                    }
                }
            }
            return categoriesList;
        }

        public static CategoryDTO GetCategoryByID(int CategoryID)
        {

            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from ProductCategory where CategoryID=@CategoryID", con))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //  return new ProductDTO(reader.GetInt32(reader.GetOrdinal("ProductID")), reader.GetString(reader.GetOrdinal("ProductName")), reader.GetString(reader.GetOrdinal("Description")), reader.GetInt32(reader.GetOrdinal("Price")), reader.GetInt32(reader.GetOrdinal("Discount")), reader.GetInt32(reader.GetOrdinal("Stock")), reader.GetInt32(reader.GetOrdinal("CategoryID")), reader.GetDateTime("CreatedAt"));

                            return new CategoryDTO(
                               reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                reader.GetString(reader.GetOrdinal("CategoryName")),
                              reader.GetString(reader.GetOrdinal("Description"))
                             );

                        }
                    }

                }
            }
            return null;

        }


        public static int AddNewCategory(CategoryDTO category)
        {
            int CategoryID = -1;
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into ProductCategory(CategoryName, Description) values(@CategoryName, @Description);SELECT SCOPE_IDENTITY();", con))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    cmd.Parameters.AddWithValue("@Description", category.Description);
                    con.Open();
                    /*cmd.ExecuteNonQuery(); */

                    CategoryID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return CategoryID;
        }


        public static bool UpdateCategory(CategoryDTO category)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update ProductCategory set CategoryName=@CategoryName, Description=@Description where CategoryID=@CategoryID", con))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    cmd.Parameters.AddWithValue("@Description", category.Description);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }


        public static bool DeleteCategory(int CategoryID)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("delete from ProductCategory where CategoryID=@CategoryID", con))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        
        }







    }

}
