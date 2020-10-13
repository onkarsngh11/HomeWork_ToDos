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
    /// Todolists controller.
    /// </summary>
    [Authorize(Roles = "User,Admin")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/todo/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListContract _toDoListContract;
        private readonly IMapper _mapper;

        public ToDoListController(IToDoListContract toDoListContract, IMapper mapper)
        {
            _toDoListContract = toDoListContract;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all todolist records.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Returns Action Result type based on Success or Failure. </returns>
        /// <response code="200"> Gets all todolist records.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("AllToDoLists")]
        public async Task<IActionResult> GetAllToDoLists([FromQuery]PaginationParameters parameters)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            PagedList<ToDoListDto> pagedToDoListDto = await _toDoListContract.GetToDoLists(parameters, userId);
            if (pagedToDoListDto != null)
            {
                if (pagedToDoListDto.Count > 0)
                {
                    var metadata = new
                    {
                        pagedToDoListDto.TotalCount,
                        pagedToDoListDto.PageSize,
                        pagedToDoListDto.CurrentPage,
                        pagedToDoListDto.TotalPages,
                        pagedToDoListDto.HasNext,
                        pagedToDoListDto.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(
                        new ApiResponse<PagedList<ToDoListDto>>
                        {
                            IsSuccess = true,
                            Result = pagedToDoListDto,
                            Message = "Lists retrieval successful."
                        });
                }
                else
                {
                    return Ok(
                        new ApiResponse<string>
                        {
                            IsSuccess = false,
                            Result = "No ToDoList records present.",
                            Message = " Please add few ToDoLists first."
                        });
                }
            }
            return NotFound(
                new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "No Results Found.",
                    Message = "Please add items to list first."
                });
        }

        /// <summary>
        /// Get specific todolist record.
        /// </summary> 
        /// <param name="toDoListId" example="1"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Gets specific todolist record.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("SpecificToDoList")]
        public async Task<IActionResult> GetToDoListById([Required]long toDoListId)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            ToDoListDto toDoListDto = await _toDoListContract.GetToDoListById(toDoListId, userId);
            if (toDoListDto != null)
            {
                return Ok(
                    new ApiResponse<ToDoListDto>
                    {
                        IsSuccess = true,
                        Result = toDoListDto,
                        Message = "ToDoList records retrieval successful."
                    });
            }
            return NotFound(
                new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "Not found.",
                    Message = "No data exist for Id = " + toDoListId + "."
                });
        }

        /// <summary>
        /// Create todolist record.
        /// </summary>
        /// <param name="createToDoList"></param>
        /// <param name="version"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="201"> Creates todolist record and returns the location where created.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> The provided todolistid should be positive integer.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateToDoList(CreateToDoListModel createToDoList, ApiVersion version)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            if (createToDoList == null || string.IsNullOrWhiteSpace(createToDoList.Description))
            {
                return BadRequest(new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "Not Updated.",
                    Message = "Please enter correct values. Description should not be empty."
                });
            }
            createToDoList.CreatedBy = userId;

            CreateToDoListDto createToDoListDto = _mapper.Map<CreateToDoListDto>(createToDoList);
            ToDoListDto createdToDoList = await _toDoListContract.CreateToDoList(createToDoListDto);
            return CreatedAtAction(nameof(GetToDoListById), new { createdToDoList.ToDoListId, version = $"{version}" }, createdToDoList);
        }

        /// <summary>
        /// Update specific todolist record.
        /// </summary>
        /// <param name="listToUpdate"></param>
        /// <returns>Returns ActionResult type based on Success/Failure.</returns>
        /// <response code="200"> Updates specific todolist record with details provided.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> The provided todolistid should be positive integer.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("List")]
        public async Task<IActionResult> PutToDoList(UpdateToDoListModel listToUpdate)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            if (null == listToUpdate || string.IsNullOrWhiteSpace(listToUpdate.Description))
            {
                return BadRequest(new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "Not Updated.",
                    Message = "Please enter correct values. Description should not be empty."
                });
            }

            UpdateToDoListDto listToUpdateDto = _mapper.Map<UpdateToDoListDto>(listToUpdate);
            ToDoListDto updatedToDoListDto = await _toDoListContract.UpdateToDoList(listToUpdateDto);
            ToDoListModel updatedToDoList = _mapper.Map<ToDoListModel>(updatedToDoListDto);

            if (updatedToDoList != null)
            {
                return Ok(
                    new ApiResponse<ToDoListModel>
                    {
                        IsSuccess = true,
                        Result = updatedToDoList,
                        Message = "ToDoList with Id = " + updatedToDoList.ToDoListId + " is updated on " + updatedToDoList.UpdationDate + " by UserId = " + userId + "."
                    });
            }
            return NotFound(
                new ApiResponse<object>
                {
                    IsSuccess = false,
                    Result = "Item to be updated not found.",
                    Message = "No data exist for ToDoListId = " + listToUpdate.ToDoListId
                });
        }
        /// <summary>
        /// Delete specific todolist record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Deletes specific todolist record.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoList([Required]long id)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            int deletedItem = await _toDoListContract.DeleteToDoList(id, userId);
            if (deletedItem == 1)
            {
                return Ok(
                    new ApiResponse<object>
                    {
                        IsSuccess = true,
                        Result = "Deleted",
                        Message = "ToDoList with ID = " + id + "is deleted by UserId = " + userId + "."
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
        /// Partial update specific todolist record with JsonPatch document.
        /// </summary>
        /// <param name="toDoListId"></param>
        /// <param name="listToUpdatePatchDoc">Partial updated data.</param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Updates specific todolist record with details provided.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> The provided todolistid should be positive integer.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("List")]
        public async Task<IActionResult> PatchToDoList([Required]long toDoListId, [FromBody]JsonPatchDocument<UpdateToDoListModel> listToUpdatePatchDoc)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            if (listToUpdatePatchDoc == null)
            {
                return BadRequest(
                    new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Result = "Bad Request.",
                        Message = "Please try again with correct input."
                    });
            }
            ToDoListDto existingToDoListDto = await _toDoListContract.GetToDoListById(toDoListId, userId);
            if (existingToDoListDto == null)
            {
                return NotFound(
                    new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Result = "No existing record found for provided input.",
                        Message = "No data exist for Id = " + toDoListId
                    });
            }
            JsonPatchDocument<UpdateToDoListDto> PatchToListDto = _mapper.Map<JsonPatchDocument<UpdateToDoListDto>>(listToUpdatePatchDoc);
            UpdateToDoListDto listToUpdateDto = _mapper.Map<UpdateToDoListDto>(existingToDoListDto);
            PatchToListDto.ApplyTo(listToUpdateDto);
            bool isValid = TryValidateModel(listToUpdateDto);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            ToDoListDto updatedToDoListDto = await _toDoListContract.UpdateToDoList(listToUpdateDto);

            ToDoListModel updatedToDoList = _mapper.Map<ToDoListModel>(updatedToDoListDto);        // Dto to Model
            if (updatedToDoList == null)
            {
                return NotFound(
                    new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Result = "No existing record found for provided input.",
                        Message = "No data exist for Id = " + listToUpdateDto.ToDoListId
                    });
            }
            else
            {
                return Ok(
                    new ApiResponse<ToDoListModel>
                    {
                        IsSuccess = true,
                        Result = updatedToDoList,
                        Message = "ToDoList record with id =" + updatedToDoList.ToDoListId + " is updated on " + updatedToDoList.UpdationDate + " by UserId = " + userId
                    });
            }
        }
    }
}