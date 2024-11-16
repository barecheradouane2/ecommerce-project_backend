﻿using ecommerceDataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {

        [HttpGet("All",Name="GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult <IEnumerable<ProductDTO>> GetAllProducts()
        {
            List < ProductDTO > ProductList = ecommerceApiBusinessLayer.Product.GetAllProducts();

            if (ProductList.Count==0)
            {
                return NotFound("No Product  Found !");
            }
            return Ok(ProductList);
        }



        [HttpGet("{id}", Name = "GetProductByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<ProductDTO> GetProductByID(int id)
        {
            if(id<1)
            {
                return BadRequest("Invalid Product ID");
            }
            // server side 
           ecommerceApiBusinessLayer.Product product = ecommerceApiBusinessLayer.Product.Find(id);

            if (product == null)
            {
                return NotFound($" Product  with ID {id} not found !");
            }
            // return pdto because the relation is stateless we should return ProductDTO
            ProductDTO productDTO = product.PDTO;
            return Ok(productDTO);
        }


        [HttpPost( Name = "AddNewProduct")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<ProductDTO> AddNewProduct(ProductDTO NewProductDTO)
        {
            if (NewProductDTO==null || string.IsNullOrEmpty(NewProductDTO.ProductName) ||NewProductDTO.Stock<=0)
            {
                return BadRequest("Invalid Product data");
            }
            // server side 
            ecommerceApiBusinessLayer.Product product = new ecommerceApiBusinessLayer.Product(new ProductDTO(NewProductDTO.ProductID, NewProductDTO.ProductName, NewProductDTO.Description,NewProductDTO.Price,NewProductDTO.Discount,NewProductDTO.Stock, NewProductDTO.CategoryID, DateTime.Now));
              
            product.Save();
            NewProductDTO.ProductID= product.ProductID;
            return CreatedAtRoute("GetProductByID", new { id = NewProductDTO.ProductID }, NewProductDTO);


         
        }


        [HttpPut("{id}",Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<ProductDTO> UpdateProduct(int id,ProductDTO UpdateProduct)
        {
            if (UpdateProduct == null || string.IsNullOrEmpty(UpdateProduct.ProductName) )
            {
                return BadRequest("Invalid Product data");
            }
            // server side 
            ecommerceApiBusinessLayer.Product product =  ecommerceApiBusinessLayer.Product.Find(id);
            if(product == null)
            {
                return NotFound($" Product  with ID {id} not found !");
            }   

            product.ProductName = UpdateProduct.ProductName;
            product.Description = UpdateProduct.Description;
            product.Price = UpdateProduct.Price;
            product.Discount = UpdateProduct.Discount;
            product.Stock = UpdateProduct.Stock;
            product.CategoryID = UpdateProduct.CategoryID;
            product.CreatedAt = UpdateProduct.CreatedAt;

           
            if (product.Save())
            {
                return Ok(product.PDTO);
            }
            else
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }

           
        }


        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<ProductDTO> DeleteProduct(int id)
        {
            if (id<1)
            {
                return BadRequest("Invalid Product ID");
            }

            ecommerceApiBusinessLayer.Product product = ecommerceApiBusinessLayer.Product.Find(id);

            if(product == null)
            {
                return NotFound($" Product  with ID {id} not found !");
            }

            if(product.Delete())
            {
                return Ok(product.PDTO);
            }
            else
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }



        }




        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            // Check if no file is uploaded
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No file uploaded.");

            // Directory where files will be uploaded
            var uploadDirectory = @"C:\MyUploads";

            // Generate a unique filename
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Ensure the uploads directory exists, create if it doesn't
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return the file path as a response
            return Ok(new { filePath });
        }


        // Endpoint to retrieve image from the server
        [HttpGet("GetImage/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            // Directory where files are stored
            var uploadDirectory = @"C:\MyUploads";
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
                return NotFound("Image not found.");

            // Open the image file for reading
            var image = System.IO.File.OpenRead(filePath);
            var mimeType = GetMimeType(filePath);

            // Return the file with the correct MIME type
            return File(image, mimeType);
        }

        // Helper method to get the MIME type based on file extension
        /*
         This code defines a  method called GetMimeType that takes a file path as a parameter 
         and returns the corresponding MIME type as a string. 
         MIME types are used to indicate the nature and format of a file, 
         especially in web contexts where you need to specify the type of content you're sending, 
         like images, text, etc.

        MIME type stands for Multipurpose Internet Mail Extensions type. 
        It's a standard way to indicate the nature and format of a file or content. 
        MIME types are used to tell browsers, email clients, and 
        other software about the type of data they're handling, so they can process it correctly.
         */
        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }





    }
}