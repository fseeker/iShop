using iShopServerSide.Model;
using Microsoft.Data.SqlClient;
using System;

namespace iShopServerSide.DAL
{
    public class ProductRepository
    {
        private SqlConnection sqlConn;

        public ProductRepository(SqlConnection sqlConn) {
            this.sqlConn = sqlConn;
        }

        public List<Product> GetAllProducts() //check what access modifier internal means - restricts access within a single ASSEMBLY, whatever the eff assembly means.
        {
            try
            {
                var ProductList = new List<Product>();
                string sql = @"
                    Select * from ProductTbl
                    ";
                sqlConn.Open();
                var cmd = new SqlCommand(sql, sqlConn); 
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductList.Add(
                        new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            ImageName = reader.GetString(3),
                            Category = reader.GetString(4),
                            Price = Convert.ToSingle(reader.GetDouble(5)),
                            Discount = Convert.ToSingle(reader.GetDouble(6)),
                            Stock = reader.GetInt32(7)
                        });
                }

                return ProductList;
            }
            catch
            {
                return new List<Product>();
            }
        }
        public int AddProduct(Product product)
        {
            try
            {
                string sql = @"
                    INSERT INTO [dbo].[ProductTbl]
                        ([ProductName]
                        ,[Description]
                        ,[ImageName]
                        ,[Category]
                        ,[Price]
                        ,[Discount]
                        ,[Stock])
                    VALUES
                        (@param1, @param2, @param3, @param4, @param5, @param6, @param7);
                    SELECT SCOPE_IDENTITY();
                    ";

                using (var cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.Parameters.AddWithValue("@param1", product.Name);
                    cmd.Parameters.AddWithValue("@param2", product.Description);
                    cmd.Parameters.AddWithValue("@param3", product.ImageName);
                    cmd.Parameters.AddWithValue("@param4", product.Category);
                    cmd.Parameters.AddWithValue("@param5", product.Price);
                    cmd.Parameters.AddWithValue("@param6", product.Discount);
                    cmd.Parameters.AddWithValue("@param7", product.Stock);

                    sqlConn.Open();

                    int generatedId = Convert.ToInt32(cmd.ExecuteScalar());
                    return generatedId;

                    //int rowsAffected = cmd.ExecuteNonQuery();
                    // 'rowsAffected' contains the number of rows affected by the INSERT query
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public bool AddToCart(int productId, int userId, int quantity)
        {
            try
            {
                var ProductList = new List<Product>();
                string sql = @"
                    IF EXISTS (SELECT * FROM CartTbl WHERE ProductId = @productId AND CustomerId = @userId)
                    BEGIN
                        -- If the record exists, update it
                        UPDATE CartTbl
                        SET Quantity = @quantity + Quantity
                        WHERE productId = @productId AND CustomerId = @userId;
                    END
                    ELSE
                    BEGIN
                        -- If the record doesn't exist, insert a new entry
                        INSERT INTO CartTbl (productId, CustomerId, Quantity)
                        VALUES (@productId, @userId, @quantity);
                    END
                    ";
                using (var cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.Parameters.AddWithValue("@productId", productId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);

                    sqlConn.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        internal bool RemoveFromCart(int productId, int userId, int quantity)
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetCartItems(int userId)
        {
            try
            {
                var CartList = new List<CartItem>();
                string sql = @"
                    Select pt.Id, pt.ProductName, pt.Category, pt.Description, pt.ImageName, pt.Price, pt.Discount, ct.Quantity from CartTbl ct
                    JOIN ProductTbl pt
                    on ct.ProductId = pt.Id
                    WHERE ct.CustomerId = @userId
                    ";
                sqlConn.Open();
                var cmd = new SqlCommand(sql, sqlConn);
                cmd.Parameters.AddWithValue("@userId", userId);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                { //only productId in CartTbl, need to get all data from productTbl against all Ids.
                    CartList.Add(
                        new CartItem
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Category = reader.GetString(2),
                            Description = reader.GetString(3),
                            ImageName = reader.GetString(4),
                            Price = Convert.ToSingle(reader.GetDouble(5)),
                            Discount = Convert.ToSingle(reader.GetDouble(6)),
                            Quantity = reader.GetInt32(7)
                        });
                }

                return CartList;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
