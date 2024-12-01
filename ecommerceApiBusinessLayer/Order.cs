using ecommerceDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerceApiBusinessLayer
{

   
    public class Order
    {

        public enum enMode { AddNew = 0, Update = 1 }

        public enMode Mode = enMode.AddNew;

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


        public List<OrderItemDTO> OrderItems { get; set; }

        public List <OrderItemDTO2> OrderItems2 { get; set; }





        public OrderDTO ODTO
        {
            get { return (new OrderDTO(this.OrderID, this.OrderDate, this.TotalAmount, this.OrderStatus, this.FullName, this.Telephone, this.Wilaya, this.Commune, this.OrderAddress, this.ShippingStatus,this.OrderItems)); }
        }

        public OrderDTO2 ODTO2
        {
            get { return (new OrderDTO2(this.OrderID, this.OrderDate, this.TotalAmount, this.OrderStatus, this.FullName, this.Telephone, this.Wilaya, this.Commune, this.OrderAddress, this.DiscountCodeID, this.ShippingID, this.ShippingStatus,this.OrderItems2));
            
            
            }
        }



        //  int OrderID, DateTime OrderDate, decimal TotalAmount, short OrderStatus, string FullName, string Telephone, string Wilaya, string Commune, string OrderAddress, int DiscountCodeID, int ShippingID, short shippingStatus
        /*  public OrderDTO ODTO2
          {
              get { return (new OrderDTO(this.OrderID, this.OrderDate, this.TotalAmount, this.OrderStatus, this.FullName, this.Telephone, this.Wilaya, this.Commune, this.OrderAddress,this.DiscountCodeID,this.ShippingID, this.ShippingStatus)); }
          }
        */



        public Order(OrderDTO oDTO, enMode cMode = enMode.AddNew)
        {
          this.OrderID = oDTO.OrderID;
            this.OrderDate = oDTO.OrderDate;
            this.TotalAmount = oDTO.TotalAmount;
            this.OrderStatus = oDTO.OrderStatus;
            this.FullName = oDTO.FullName;
            this.Telephone = oDTO.Telephone;
            this.Wilaya = oDTO.Wilaya;
            this.Commune = oDTO.Commune;
            this.OrderAddress = oDTO.OrderAddress;
            this.DiscountCodeID = oDTO.DiscountCodeID;
            this.ShippingID = oDTO.ShippingID;
            this.OrderItems = oDTO.OrderItems;
            this.ShippingStatus = oDTO.ShippingStatus;




            Mode = cMode;
        }



        public Order(OrderDTO2 oDTO, enMode cMode = enMode.AddNew)
        {
            this.OrderID = oDTO.OrderID;
            this.OrderDate = oDTO.OrderDate;
            this.TotalAmount = oDTO.TotalAmount;
            this.OrderStatus = oDTO.OrderStatus;
            this.FullName = oDTO.FullName;
            this.Telephone = oDTO.Telephone;
            this.Wilaya = oDTO.Wilaya;
            this.Commune = oDTO.Commune;
            this.OrderAddress = oDTO.OrderAddress;
            this.DiscountCodeID = oDTO.DiscountCodeID;
            this.ShippingID = oDTO.ShippingID;
            this.ShippingStatus = oDTO.ShippingStatus;
         this.OrderItems2 = oDTO.OrderItems2;
         




            Mode = cMode;
        }


        public  static List<OrderDTO> GetAllOrders()
        {
            return OrderData.GetAllOrders();
        }

        public static Order Find (int OrderID)
        {
            OrderDTO oDTO = OrderData.GetOrderById(OrderID);
            if (oDTO != null)
            {
                return new Order(oDTO, enMode.Update);
            }
            else
                return null;
        }


        public bool Delete()
        {
            foreach(var item in this.OrderItems)
            {
                
                OrderData.DeleteOrderItem(item.OrderID);
            }

            return OrderData.DeleteOrder(this.OrderID);

        }


        private bool _AddNewOrder()
        {
            // it works i should try it in ODTO2
            this.OrderID = OrderData.AddNewOrder(this.ODTO2);


              foreach (var item in this.OrderItems2)
              {
                  item.OrderID = this.OrderID;    
                  OrderData.AddNewOrderItem(item);
              }
          

            return OrderID != -1;
        }

        private bool _UpdateOrder()
        {
            /* OrderDTO neworder = new OrderDTO();

              neworder.OrderID = this.OrderID;
              neworder.OrderDate = this.OrderDate;
              neworder.TotalAmount = this.TotalAmount;
              neworder.OrderStatus = this.OrderStatus;
              neworder.FullName = this.FullName;
              neworder.Telephone = this.Telephone;
              neworder.Wilaya = this.Wilaya;
              neworder.Commune = this.Commune;
              neworder.OrderAddress = this.OrderAddress;
              neworder.DiscountCodeID = this.DiscountCodeID;
              neworder.ShippingID = this.ShippingID;
              neworder.ShippingStatus = this.ShippingStatus;
              neworder.OrderItems = this.OrderItems; */

            foreach (var item in this.OrderItems2)
            {
             
                OrderData.UpdateOrderItem(item);
            }

            return OrderData.UpdateOrder(this.ODTO2);
        }


        public bool Save()
        {

            if (Mode == enMode.AddNew)
            {

                if (_AddNewOrder())
                {
                    Mode = enMode.Update;
                    return true;

                }
                else
                {
                    return false;
                }
              
            }
            else if(Mode == enMode.Update)
            {
                return _UpdateOrder();
            }

            return false;

        }




    }
}
