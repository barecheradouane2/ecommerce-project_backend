using ecommerceApiBusinessLayer;
using ecommerceDataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        [HttpGet("All", Name = "GetAllOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<OrderDTO>> GetAllOrders()
        {
            List<OrderDTO> OrderList = ecommerceApiBusinessLayer.Order.GetAllOrders();

            if (OrderList.Count == 0)
            {
                return NotFound("No Order Found !");
            }
            return Ok(OrderList);
        }




        [HttpGet("{id}", Name = "GetOrderByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public ActionResult<OrderDTO> GetOrderByID(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Order ID");
            }
            // server side 
            ecommerceApiBusinessLayer.Order Order = ecommerceApiBusinessLayer.Order.Find(id);

            if (Order == null)
            {
                return NotFound($" Order  with ID {id} not found !");
            }
            
            OrderDTO OrderDTO = Order.ODTO;
            return Ok(OrderDTO);
        }


        [HttpPost(Name = "AddNewOrder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]

        public  ActionResult<OrderDTO2> AddNewOrder([FromBody] OrderDTO2 NewOrderDTO)
        {
            try
            {
                // Create a order object
                var order = new ecommerceApiBusinessLayer.Order(
                    new OrderDTO2(
                     NewOrderDTO.OrderID,
                     NewOrderDTO.OrderDate,
                     NewOrderDTO.TotalAmount,
                     NewOrderDTO.OrderStatus,
                     NewOrderDTO.FullName,
                     NewOrderDTO.Telephone,
                     NewOrderDTO.Wilaya,
                     NewOrderDTO.Commune,
                     NewOrderDTO.OrderAddress,
                     NewOrderDTO.DiscountCodeID,
                     NewOrderDTO.ShippingID,
                     NewOrderDTO.ShippingStatus,
                     NewOrderDTO.OrderItems2


                    )
                );

                // Save the product
                bool isSaved = order.Save();
                if (!isSaved)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save the order.");
                }

                // Set the generated OrderID back to the DTO
                NewOrderDTO.OrderID = order.OrderID;

                // Return a CreatedAtRoute response
                return CreatedAtRoute("GetOrderByID", new { id = NewOrderDTO.OrderID }, NewOrderDTO);
            }
            catch (Exception ex)
            {
                // Log the exception (replace with your logger)
                Console.WriteLine($"Error while adding order: {ex.Message}");

                // Return an error response
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }



        [HttpPut("{id}", Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<OrderDTO2> UpdateOrder(int id, OrderDTO2 UpdateOrder)
        {
            if (UpdateOrder == null )
            {
                return BadRequest("Invalid order data");
            }
            // server side 
            ecommerceApiBusinessLayer.Order order = ecommerceApiBusinessLayer.Order.Find(id);
            if (order == null)
            {
                return NotFound($" order  with ID {id} not found !");
            }

            order.OrderDate = UpdateOrder.OrderDate;
            order.TotalAmount = UpdateOrder.TotalAmount;
            order.OrderStatus = UpdateOrder.OrderStatus;
            order.FullName = UpdateOrder.FullName;
            order.Telephone = UpdateOrder.Telephone;
            order.Wilaya = UpdateOrder.Wilaya;
            order.Commune = UpdateOrder.Commune;
            order.OrderAddress = UpdateOrder.OrderAddress;
            order.DiscountCodeID = UpdateOrder.DiscountCodeID;
            order.ShippingID = UpdateOrder.ShippingID;
            order.ShippingStatus = UpdateOrder.ShippingStatus;
            order.OrderItems2 = UpdateOrder.OrderItems2;


            if ( order.Save())
            {
                return Ok(order.ODTO2);
            }
            else
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }


        }


        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public ActionResult<OrderDTO> DeleteOrder(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Order ID");
            }

            ecommerceApiBusinessLayer.Order order = ecommerceApiBusinessLayer.Order.Find(id);

            if (order == null)
            {
                return NotFound($" Order  with ID {id} not found !");
            }

            if (order.Delete())
            {
                return Ok(order.ODTO);
            }
            else
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }



        }





















    }
}
