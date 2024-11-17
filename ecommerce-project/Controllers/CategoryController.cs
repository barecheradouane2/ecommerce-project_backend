using ecommerceDataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        [HttpGet("All", Name = "GetAllCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public ActionResult<IEnumerable<CategoryDTO>> GetAllCategory()
        {
           
            List<CategoryDTO> CategoryList = ecommerceApiBusinessLayer.Category.GetAllCategories();

            if (CategoryList.Count == 0)
            {
                return NotFound("No Product  Found !");
            }
            return Ok(CategoryList);
        }

        [HttpGet("{id}", Name = "GetCategoryByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<CategoryDTO> GetCategoryByID(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Product ID");
            }
            // server side 
            ecommerceApiBusinessLayer.Category Category = ecommerceApiBusinessLayer.Category.Find(id);

            if (Category == null)
            {
                return NotFound($" Product  with ID {id} not found !");
            }
            // return pdto because the relation is stateless we should return ProductDTO
            CategoryDTO CategoryDTO = Category.CDTO;
            return Ok(CategoryDTO);
        }


        [HttpPost(Name = "AddNewCategory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<CategoryDTO> AddNewCategory(CategoryDTO NewCategoryDTO)
        {
            if (NewCategoryDTO == null || string.IsNullOrEmpty(NewCategoryDTO.CategoryName) )
            {
                return BadRequest("Invalid Product data");
            }
            // server side 
            ecommerceApiBusinessLayer.Category Category = new ecommerceApiBusinessLayer.Category(new CategoryDTO(NewCategoryDTO.CategoryID, NewCategoryDTO.CategoryName, NewCategoryDTO.Description));

            Category.Save();
            NewCategoryDTO.CategoryID = Category.CategoryID;
            return CreatedAtRoute("GetCategoryByID", new { id = NewCategoryDTO.CategoryID }, NewCategoryDTO);



        }

        [HttpPut("{id}", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public ActionResult<CategoryDTO> UpdateCategory(int id, CategoryDTO UpdateCategory)
        {
            if (UpdateCategory == null || string.IsNullOrEmpty(UpdateCategory.CategoryName))
            {
                return BadRequest("Invalid Category data");
            }
            // server side 
            ecommerceApiBusinessLayer.Category Category = ecommerceApiBusinessLayer.Category.Find(id);
            if (Category == null)
            {
                return NotFound($" Category  with ID {id} not found !");
            }

            Category.CategoryName = UpdateCategory.CategoryName;
            Category.Description = UpdateCategory.Description;
            


            if (Category.Save())
            {
                return Ok(Category.CDTO);
            }
            else
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }


        }


        [HttpDelete("{id}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public ActionResult<ProductDTO> DeleteCategory(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Category ID");
            }

            ecommerceApiBusinessLayer.Category Category = ecommerceApiBusinessLayer.Category.Find(id);

            if (Category == null)
            {
                return NotFound($" Category  with ID {id} not found !");
            }

            if (Category.Delete())
            {
                return Ok(Category.CDTO);
            }
            else
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }



        }







    }
}
