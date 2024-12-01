using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ecommerceDataAccessLayer
{

    public class OrderDTO
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

    
        public short OrderStatus { get; set; }

        public string OrderStatusString { get; set; }
      



        public string FullName { get; set; }

        public string Telephone { get; set; }
        public string Wilaya { get; set; }
        public string Commune { get; set; }
        public string OrderAddress { get; set; }

     
        public int DiscountCodeID { get; set; }

   
        public int ShippingID { get; set; }

        public short ShippingStatus { get; set; }


        public string ShippingStatusString { get; set; }

        public List <OrderItemDTO> OrderItems { get; set; }



        public OrderDTO()
        {

        }


        public OrderDTO(int OrderID, DateTime OrderDate, decimal TotalAmount, short OrderStatus, string FullName, string Telephone, string Wilaya, string Commune, string OrderAddress, short shippingStatus , List<OrderItemDTO> OrderItems)
        {
            this.OrderID = OrderID;
            this.OrderDate = OrderDate;
            this.TotalAmount = TotalAmount;

            

                this.OrderStatusString = OrderStatus switch
            {
                0 => "Pending",
                1 => "Confirmed",
                2=> "Shipped",
                3 => "Delivered",
                4 => "Canceled",
            }; 

            this.FullName = FullName;
            this.Telephone = Telephone;
            this.Wilaya = Wilaya;
            this.Commune = Commune;
            this.OrderAddress = OrderAddress;
          

            this.ShippingStatusString = ShippingStatus switch
            {
                0 => "Home",
                 1=> "Office",
                _ => "Home"
            };

            this.OrderItems = OrderItems;


        }

        public OrderDTO(int OrderID, DateTime OrderDate, decimal TotalAmount, short OrderStatus, string FullName, string Telephone, string Wilaya, string Commune, string OrderAddress, int DiscountCodeID, int ShippingID, short shippingStatus)
        {
            this.OrderID = OrderID;
            this.OrderDate = OrderDate;
            this.TotalAmount = TotalAmount;
            this.OrderStatus = OrderStatus;
            this.FullName = FullName;
            this.Telephone = Telephone;
            this.Wilaya = Wilaya;
            this.Commune = Commune;
            this.OrderAddress = OrderAddress;
            this.DiscountCodeID = DiscountCodeID;
            this.ShippingID = ShippingID;
            this.ShippingStatus = shippingStatus;
        }

        public OrderDTO(int OrderID, DateTime OrderDate, decimal TotalAmount, short OrderStatus, string FullName, string Telephone, string Wilaya, string Commune, string OrderAddress, int DiscountCodeID, int ShippingID, short ShippingStatus, List<OrderItemDTO> OrderItems)
        {
            this.OrderID = OrderID;
            this.OrderDate = OrderDate;
            this.TotalAmount = TotalAmount;
            this.OrderStatus = OrderStatus;
            this.FullName = FullName;
            this.Telephone = Telephone;
            this.Wilaya = Wilaya;
            this.Commune = Commune;
            this.OrderAddress = OrderAddress;
            this.DiscountCodeID = DiscountCodeID;
            this.ShippingID = ShippingID;
            this.OrderItems = OrderItems;
            this.ShippingStatus = ShippingStatus;
        }


    }


    public class OrderItemDTO
    {
        public int OrderItemID { get; set; }
    
        public int OrderID { get; set; }
      
        public int ProductID { get; set; }

        public string ProductName { get; set; } 
        public int Quantity { get; set; }
        public decimal Price { get; set; }
      
        public decimal TotalItemPrice { get; set; }

        public OrderItemDTO()
        {

        }

        public OrderItemDTO(int OrderItemID, int OrderID, int ProductID, string ProductName, int Quantity, decimal Price, decimal TotalItemPrice)
        {
            this.OrderItemID = OrderItemID;
            this.OrderID = OrderID;
            this.ProductID = ProductID;
            this.Quantity = Quantity;
            this.Price = Price;
            this.TotalItemPrice = TotalItemPrice;
            this.ProductName = ProductName;
        }

    }


    public class OrderDTO2 {



        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }


        public short OrderStatus { get; set; }

        public string FullName { get; set; }

        public string Telephone { get; set; }
        public string Wilaya { get; set; }
        public string Commune { get; set; }
        public string OrderAddress { get; set; }


        public int DiscountCodeID { get; set; }


        public int ShippingID { get; set; }

        public short ShippingStatus { get; set; }



        public List<OrderItemDTO2> OrderItems2 { get; set; }



        public OrderDTO2(int OrderID, DateTime OrderDate, decimal TotalAmount, short OrderStatus, string FullName, string Telephone, string Wilaya, string Commune, string OrderAddress, int DiscountCodeID, int ShippingID, short shippingStatus, List<OrderItemDTO2> OrderItems2)
        {
            this.OrderID = OrderID;
            this.OrderDate = OrderDate;
            this.TotalAmount = TotalAmount;
            this.OrderStatus = OrderStatus;

            this.FullName = FullName;
            this.Telephone = Telephone;
            this.Wilaya = Wilaya;
            this.Commune = Commune;
            this.OrderAddress = OrderAddress;
            this.DiscountCodeID = DiscountCodeID;
            this.ShippingID = ShippingID;
            this.ShippingStatus = shippingStatus;


            this.OrderItems2 = OrderItems2;


        }


    }


    public class OrderItemDTO2
    {
        public int OrderItemID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

     
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal TotalItemPrice { get; set; }

        public OrderItemDTO2()
        {

        }

        public OrderItemDTO2(int OrderItemID, int OrderID, int ProductID, string ProductName, int Quantity, decimal Price, decimal TotalItemPrice)
        {
            this.OrderItemID = OrderItemID;
            this.OrderID = OrderID;
            this.ProductID = ProductID;
            this.Quantity = Quantity;
            this.Price = Price;
            this.TotalItemPrice = TotalItemPrice;
          
        }

    }

    public class OrderData
    {
        private static string _ConnectionString = "Server=localhost;Database=EcommerceDB;User Id=sa;Password=sa123456;Encrypt=True;TrustServerCertificate=True;";

        public static List<OrderDTO> GetAllOrders()
        {
            List<OrderDTO> Orders = new List<OrderDTO>();

            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("select * from OrdersInfo", con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                         

                            OrderDTO order = new OrderDTO(

                                reader.GetInt32(reader.GetOrdinal("OrderID")),
                                reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                                reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                                reader.GetInt16(reader.GetOrdinal("OrderStatus")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.GetString(reader.GetOrdinal("TelephoneNumber")),
                                reader.GetString(reader.GetOrdinal("Wilaya")),
                                reader.GetString(reader.GetOrdinal("Commune")),
                                reader.GetString(reader.GetOrdinal("OrderAddress")),

                               (short)(reader.GetBoolean(reader.GetOrdinal("ShippingStatus")) ? 1 : 0), GetOrderItemsByOrderId(reader.GetInt32(reader.GetOrdinal("OrderID")))

                             );

                            Orders.Add(order);










                        }


                    }






                }
            }


            return Orders;

        }


        public static OrderDTO GetOrderById(int OrderID)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from OrdersInfo where OrderID = @OrderID", con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", OrderID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new OrderDTO(
                                reader.GetInt32(reader.GetOrdinal("OrderID")),
                                reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                                reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                                reader.GetInt16(reader.GetOrdinal("OrderStatus")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.GetString(reader.GetOrdinal("TelephoneNumber")),
                                reader.GetString(reader.GetOrdinal("Wilaya")),
                                reader.GetString(reader.GetOrdinal("Commune")),
                                reader.GetString(reader.GetOrdinal("OrderAddress")),
                                (short)(reader.GetBoolean(reader.GetOrdinal("ShippingStatus")) ? 1 : 0),
                                GetOrderItemsByOrderId(OrderID) // Include the order items here
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }




        public static List<OrderItemDTO> GetOrderItemsByOrderId(int OrderID)
        {
            List<OrderItemDTO> OrderItems = new List<OrderItemDTO>();

            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("select * from OrderItemsDetails where OrderID = @OrderID", con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", OrderID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            OrderItemDTO orderItem = new OrderItemDTO(

                                reader.GetInt32(reader.GetOrdinal("OrderItemsID")),
                                reader.GetInt32(reader.GetOrdinal("OrderID")),
                                reader.GetInt32(reader.GetOrdinal("ProductID")),
                                reader.GetString(reader.GetOrdinal("ProductName")),
                                reader.GetInt32(reader.GetOrdinal("Quantity")),
                                reader.GetDecimal(reader.GetOrdinal("Price")),
                                reader.GetDecimal(reader.GetOrdinal("TotalItemsPrice"))

                             );

                            OrderItems.Add(orderItem);


                        }

                    }

                }
            }

            return OrderItems;

        }



        public static int  AddNewOrder(OrderDTO2 order)
        {
            int OrderID = -1;
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into Orders (OrderDate, TotalAmount, OrderStatus, FullName, TelephoneNumber, Wilaya, Commune, OrderAddress, DiscountCodeID, ShippingID,ShippingStatus) values (@OrderDate, @TotalAmount, @OrderStatus, @FullName, @Telephone, @Wilaya, @Commune, @OrderAddress, @DiscountCodeID, @ShippingID,@ShippingStatus);SELECT SCOPE_IDENTITY();", con))
                {
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
                    cmd.Parameters.AddWithValue("@FullName", order.FullName);
                    cmd.Parameters.AddWithValue("@Telephone", order.Telephone);
                    cmd.Parameters.AddWithValue("@Wilaya", order.Wilaya);
                    cmd.Parameters.AddWithValue("@Commune", order.Commune);
                    cmd.Parameters.AddWithValue("@OrderAddress", order.OrderAddress);
                    cmd.Parameters.AddWithValue("@DiscountCodeID", order.DiscountCodeID);
                    cmd.Parameters.AddWithValue("@ShippingID", order.ShippingID);
                    cmd.Parameters.AddWithValue("@ShippingStatus", order.ShippingStatus);
                    con.Open();


                    OrderID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return OrderID;
        }


        public static int AddNewOrderItem(OrderItemDTO2 orderItem)
        {
            int OrderItemID = -1;
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into OrderItems (OrderID, ProductID, Quantity, Price, TotalItemsPrice) values (@OrderID, @ProductID, @Quantity, @Price, @TotalItemPrice)", con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderItem.OrderID);
                    cmd.Parameters.AddWithValue("@ProductID", orderItem.ProductID);
                    cmd.Parameters.AddWithValue("@Quantity", orderItem.Quantity);
                    cmd.Parameters.AddWithValue("@Price", orderItem.Price);
                    cmd.Parameters.AddWithValue("@TotalItemPrice", orderItem.TotalItemPrice);
                    con.Open();




                    OrderItemID = Convert.ToInt32(cmd.ExecuteScalar());


                }
            }

            return OrderItemID;

        }


        public static bool UpdateOrder(OrderDTO2 order)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update Orders set OrderDate = @OrderDate, TotalAmount = @TotalAmount, OrderStatus = @OrderStatus, FullName = @FullName, TelephoneNumber = @Telephone, Wilaya = @Wilaya, Commune = @Commune, OrderAddress = @OrderAddress, DiscountCodeID = @DiscountCodeID, ShippingID = @ShippingID  , ShippingStatus =@ShippingStatus where OrderID = @OrderID", con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", order.OrderID);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
                    cmd.Parameters.AddWithValue("@FullName", order.FullName);
                    cmd.Parameters.AddWithValue("@Telephone", order.Telephone);
                    cmd.Parameters.AddWithValue("@Wilaya", order.Wilaya);
                    cmd.Parameters.AddWithValue("@Commune", order.Commune);
                    cmd.Parameters.AddWithValue("@OrderAddress", order.OrderAddress);
                    cmd.Parameters.AddWithValue("@DiscountCodeID", order.DiscountCodeID);
                    cmd.Parameters.AddWithValue("@ShippingID", order.ShippingID);
                    cmd.Parameters.AddWithValue("@ShippingStatus", order.ShippingStatus);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }



        public static bool UpdateOrderItem(OrderItemDTO2 orderItem)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update OrderItems set OrderID = @OrderID, ProductID = @ProductID, Quantity = @Quantity, Price = @Price, TotalItemsPrice = @TotalItemPrice where OrderItemsID = @OrderItemID", con))
                {
                    cmd.Parameters.AddWithValue("@OrderItemID", orderItem.OrderItemID);
                    cmd.Parameters.AddWithValue("@OrderID", orderItem.OrderID);
                    cmd.Parameters.AddWithValue("@ProductID", orderItem.ProductID);
                    cmd.Parameters.AddWithValue("@Quantity", orderItem.Quantity);
                    cmd.Parameters.AddWithValue("@Price", orderItem.Price);
                    cmd.Parameters.AddWithValue("@TotalItemPrice", orderItem.TotalItemPrice);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }


        public static bool DeleteOrder(int OrderID)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("delete from Orders where OrderID = @OrderID", con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", OrderID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        

        public static bool DeleteOrderItem(int OrderID)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("delete from OrderItems where  OrderID = @OrderID", con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", OrderID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }


        /*    select * from OrdersInfo

            */





    }

}
