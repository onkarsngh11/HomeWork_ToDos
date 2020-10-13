using AutoMapper;
using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HomeWork_ToDos.API.Controllers.v1
{
    /// <summary>
    /// TodoItems controller.
    /// </summary>
    [Authorize(Roles = "User,Admin")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/todo/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemContract _toDoItemContract;
        private readonly IMapper _mapper;

        public ToDoItemController(IToDoItemContract toDoItemContract, IMapper mapper)
        {
            _toDoItemContract = toDoItemContract;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all todoitem records.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Returns Action Result type based on Success or Failure. </returns>
        /// <response code="200"> Gets all todoitem reecords.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("AllToDoItems")]
        public async Task<IActionResult> GetAllToDoItems([FromQuery]PaginationParameters parameters)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            PagedList<ToDoItemDto> pagedToDoItemDto = await _toDoItemContract.GetToDoItems(parameters, userId);
            if (pagedToDoItemDto != null)
            {
                if (pagedToDoItemDto.Count > 0)
                {
                    var metadata = new
                    {
                        pagedToDoItemDto.TotalCount,
                        pagedToDoItemDto.PageSize,
                        pagedToDoItemDto.CurrentPage,
                        pagedToDoItemDto.TotalPages,
                        pagedToDoItemDto.HasNext,
                        pagedToDoItemDto.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(
                        new ApiResponse<PagedList<ToDoItemDto>>
                        {
                            IsSuccess = true,
                            Result = pagedToDoItemDto,
                            Message = "Items retrieval successful."
                        });
                }
                else
                {
                    return Ok(
                        new ApiResponse<string>
                        {
                            IsSuccess = false,
                            Result = "No ToDoItem records present.",
                            Message = " Please add few ToDoItems first."
                        });
                }
            }
            return NotFound(
                new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "No Results Found.",
                    Message = "No data exist. Please add todo items first."
                });
        }

        /// <summary>
        /// Get specific todoitem record.
        /// </summary> 
        /// <param name="toDoItemId"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Gets specific todoitem record.</response>
        /// <response code="404"> Error: 404 not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("SpecificToDoItem")]
        public async Task<IActionResult> GetToDoItemById([Required]long toDoItemId)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());

            ToDoItemDto ToDoItemDto = await _toDoItemContract.GetToDoItemById(toDoItemId, userId);
            if (ToDoItemDto != null)
            {
                return Ok(
                    new ApiResponse<ToDoItemDto>
                    {
                        IsSuccess = true,
                        Result = ToDoItemDto,
                        Message = "Item retrieval successful."
                    });
            }
            return NotFound(
                new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "Not found.",
                    Message = "No data exist for Id = " + toDoItemId + "."
                });
        }

        /// <summary>
        /// Create todoitem record.
        /// </summary>
        /// <param name="createToDoItem"></param>
        /// <param name="version"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="201"> Creates todoitem reecord and returns location where it is created.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> The provided todoitem id should be positive integer.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateToDoItem(CreateToDoItemModel createToDoItem, ApiVersion version)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            if (createToDoItem == null || string.IsNullOrWhiteSpace(createToDoItem.Notes)
                || createToDoItem.ToDoListId == 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "Not Updated.",
                    Message = "Please enter correct values. Description should not be empty."
                });
            }
            createToDoItem.CreatedBy = userId;
            CreateToDoItemDto createToDoItemDto = _mapper.Map<CreateToDoItemDto>(createToDoItem);
            ToDoItemDto createdToDoItem = await _toDoItemContract.AddToDoItem(createToDoItemDto);
            return CreatedAtAction("GetToDoItemById", new { createdToDoItem.ToDoItemId, version = $"{version}" }, createdToDoItem);
        }

        /// <summary>
        /// Update specific todoitem record.
        /// </summary>
        /// <param name="itemToUpdate"></param>
        /// <returns>Returns ActionResult type based on Success/Failure.</returns>
        /// <response code="200"> Updates specific todoitem reecord with details provided.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> The provided todoitem id should be positive integer.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("Item")]
        public async Task<IActionResult> PutToDoItem(UpdateToDoItemModel itemToUpdate)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            if (null == itemToUpdate || string.IsNullOrWhiteSpace(itemToUpdate.Notes))
            {
                return BadRequest(new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "Not Updated.",
                    Message = "Please enter correct values. Description should not be empty."
                });
            }
            UpdateToDoItemDto itemToUpdateDto = _mapper.Map<UpdateToDoItemDto>(itemToUpdate);
            ToDoItemDto updatedToDoItem = await _toDoItemContract.UpdateToDoItem(itemToUpdateDto);
            ToDoItemModel updatedToDoItemModel = _mapper.Map<ToDoItemModel>(updatedToDoItem);
            if (updatedToDoItem != null)
            {
                return Ok(
                    new ApiResponse<ToDoItemModel>
                    {
                        IsSuccess = true,
                        Result = updatedToDoItemModel,
                        Message = "ToDoItem with Id = " + updatedToDoItemModel.ToDoItemId + " is updated on " + updatedToDoItemModel.UpdationDate + " by UserId = " + userId + "."
                    });
            }
            return NotFound(
                new ApiResponse<object>
                {
                    IsSuccess = false,
                    Result = "Failed to update.",
                    Message = "No data exist for Id = " + itemToUpdate.ToDoItemId
                });
        }

        /// <summary>
        /// Delete specific todoItem record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Deletes specific todoitem reecord.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem([Required]long id)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            int deletedToDoItem = await _toDoItemContract.DeleteToDoItem(id, userId);
            if (deletedToDoItem == 1)
            {
                return Ok(
                    new ApiResponse<object>
                    {
                        IsSuccess = true,
                        Result = "Deleted",
                        Message = "ToDoItem with ID = " + id + "is deleted by UserId = " + userId + "."
                    });
            }
            return NotFound(
                new ApiResponse<string>
                {
                    IsSuccess = true,
                    Result = "Not found.",
                    Message = "No data exist for Id = " + id + "."
                });
        }

        /// <summary>
        /// Partial update todoitem record with JsonPatch document.
        /// </summary>
        /// <param name="toDoItemId"></param>
        /// <param name="itemToUpdatePatchDoc"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Updates specific todoitem record with details provided.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> The provided todoitem id should be positive integer.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("Item")]
        public async Task<IActionResult> Patch([Required] long toDoItemId, [FromBody]JsonPatchDocument<UpdateToDoItemModel> itemToUpdatePatchDoc)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            if (itemToUpdatePatchDoc == null)
            {
                return BadRequest(
                    new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Result = "Bad Request.",
                        Message = "Please try again withh correct input."
                    });
            }
            ToDoItemDto existingToDoItemDto = await _toDoItemContract.GetToDoItemById(toDoItemId, userId);
            if (existingToDoItemDto == null)
            {
                return NotFound(
                    new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Result = "No existing record found for provided input.",
                        Message = "No data exist for Id = " + toDoItemId
                    });
            }
            JsonPatchDocument<UpdateToDoItemDto> patchToItemDto = _mapper.Map<JsonPatchDocument<UpdateToDoItemDto>>(itemToUpdatePatchDoc);
            UpdateToDoItemDto itemToUpdateDto = _mapper.Map<UpdateToDoItemDto>(existingToDoItemDto);
            patchToItemDto.ApplyTo(itemToUpdateDto);
            bool isValid = TryValidateModel(itemToUpdateDto);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            ToDoItemDto updatedToDoItemDto = await _toDoItemContract.UpdateToDoItem(itemToUpdateDto);

            ToDoItemModel updatedToDoItem = _mapper.Map<ToDoItemModel>(updatedToDoItemDto);              //Dto to Model
            if (updatedToDoItem == null)
            {
                return NotFound(
                    new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Result = "No existing record found for provided input.",
                        Message = "No data exist for Id = " + itemToUpdateDto.ToDoItemId
                    });
            }
            else
            {
                return Ok(
                     new ApiResponse<ToDoItemModel>
                     {
                         IsSuccess = true,
                         Result = updatedToDoItem,
                         Message = "ToDoItem with Id = " + updatedToDoItem.ToDoItemId + " is updated on " + updatedToDoItem.UpdationDate + " by UserId = " + userId + "."
                     });
            }
        }
    }
}