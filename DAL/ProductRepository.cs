using MahindraCrud.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace MahindraCrud.DAL
{
    public class ProductRepository
    {
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        State = reader["State"].ToString(),
                        District = reader["District"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        ImagePath = reader["ImagePath"].ToString(),
                        Date = Convert.ToDateTime(reader["Date"]),
                        Email = reader["Email"].ToString(),
                        Mobile = reader["Mobile"].ToString()
                    });
                }
            }
            return products;
        }
        public void InsertProduct(Product product)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("InsertProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@State", product.State);
                cmd.Parameters.AddWithValue("@District", product.District);
                cmd.Parameters.AddWithValue("@Gender", product.Gender);
                cmd.Parameters.AddWithValue("@ImagePath", product.ImagePath ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Date", product.Date);
                cmd.Parameters.AddWithValue("@Email", product.Email);
                cmd.Parameters.AddWithValue("@Mobile", product.Mobile);

                cmd.ExecuteNonQuery();
            }
        }
        // Get product by ID
        public Product GetProductById(int id)
        {
            Product product = new Product();
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetProductById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product.Id = Convert.ToInt32(reader["Id"]);
                    product.Name = reader["Name"].ToString();
                    product.State = reader["State"].ToString();
                    product.District = reader["District"].ToString();
                    product.Gender = reader["Gender"].ToString();
                    product.ImagePath = reader["ImagePath"].ToString();
                    product.Date = Convert.ToDateTime(reader["Date"]);
                    product.Email = reader["Email"].ToString();
                    product.Mobile = reader["Mobile"].ToString();
                }
            }
            return product;
        }

        // Update
        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UpdateProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@State", product.State);
                cmd.Parameters.AddWithValue("@District", product.District);
                cmd.Parameters.AddWithValue("@Gender", product.Gender);
                cmd.Parameters.AddWithValue("@ImagePath", product.ImagePath ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Date", product.Date);
                cmd.Parameters.AddWithValue("@Email", product.Email);
                cmd.Parameters.AddWithValue("@Mobile", product.Mobile);

                cmd.ExecuteNonQuery();
            }
        }

        // Delete
        public void DeleteProduct(int id)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DeleteProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public bool IsMobileExists(string mobile)
        {
            bool exists = false;

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("CheckDuplicateMobile", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mobile", mobile);
                SqlParameter output = new SqlParameter("@Exists", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery();
                exists = Convert.ToBoolean(output.Value);
            }

            return exists;
        }



    }
}
