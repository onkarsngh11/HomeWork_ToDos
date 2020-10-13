using AutoMapper;
using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HomeWork_ToDos.API.Controllers.v1
{
    /// <summary>
    /// Labels controller.
    /// </summary>
    [Authorize(Roles = "User,Admin")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/todo/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelContract _labelContract;
        private readonly IMapper _mapper;

        public LabelController(ILabelContract labelContract, IMapper mapper)
        {
            _labelContract = labelContract;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all label records.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> Action result based on Success/Failure.</returns>
        /// <response code="200"> Gets all  Labels. </response>
        /// <response code="404"> Error: 404 not found </response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("AllLabels")]
        public async Task<IActionResult> GetAllLabels([FromQuery]PaginationParameters parameters)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            PagedList<LabelDto> pagedLabel = await _labelContract.GetAllLabels(parameters, userId);
            if (pagedLabel != null)
            {
                if (pagedLabel.Count > 0)
                {
                    var metadata = new
                    {
                        pagedLabel.TotalCount,
                        pagedLabel.PageSize,
                        pagedLabel.CurrentPage,
                        pagedLabel.TotalPages,
                        pagedLabel.HasNext,
                        pagedLabel.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(
                        new ApiResponse<PagedList<LabelDto>>
                        {
                            IsSuccess = true,
                            Result = pagedLabel,
                            Message = "Items retrieval successful."
                        });
                }
                else
                {
                    return Ok(
                        new ApiResponse<string>
                        {
                            IsSuccess = true,
                            Result = "No Label records present.",
                            Message = " Please add few labels first."
                        });
                }
            }
            return NotFound(
                new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "No Results Found.",
                    Message = "No data found."
                });
        }


        /// <summary>
        /// Get specific label record.
        /// </summary> 
        /// <param name="labelId" example="1"></param>
        /// <returns>Returns Action result" type based on Success/Failure.</returns>
        /// <response code="200"> Gets specified label.</response>
        /// <response code="404"> A label with the specified label ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("SpecificLabel")]
        public async Task<IActionResult> GetLabelById([Required]long labelId)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            LabelDto LabelModel = await _labelContract.GetLabelById(labelId, userId);
            if (LabelModel != null)
            {
                return Ok(
                    new ApiResponse<LabelDto>
                    {
                        IsSuccess = true,
                        Result = LabelModel,
                        Message = "Label records retrieved successfully."
                    });
            }
            return NotFound(
                new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "Not found.",
                    Message = "No data exist for Id = " + labelId + "."
                });
        }
        /// <summary>
        /// Create Label record.
        /// </summary>
        /// <param name="createLabelModel"></param>
        /// <param name="version"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="201"> Creates Label and returns location where it is created.</response>
        /// <response code="400"> Invalid request format.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateLabel(CreateLabelModel createLabelModel, ApiVersion version)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            if (createLabelModel == null || string.IsNullOrWhiteSpace(createLabelModel.Description))
            {
                return BadRequest(new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "Not Updated.",
                    Message = "Please enter correct values. Description should not be empty."
                });
            }
            createLabelModel.CreatedBy = userId;
            CreateLabelDto createLabelDto = _mapper.Map<CreateLabelDto>(createLabelModel);
            LabelDto createdLabel = await _labelContract.AddLabel(createLabelDto);
            return CreatedAtAction(nameof(GetLabelById), new { createdLabel.LabelId, version = $"{version}" }, createdLabel);
        }

        /// <summary>
        /// Delete specific label record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Deletes specified label record.</response>
        /// <response code="404"> A label with the specified label ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabel([Required]long id)
        {

            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            int deletedItem = await _labelContract.DeleteLabel(id, userId);
            if (deletedItem == 1)
            {
                return Ok(
                    new ApiResponse<object>
                    {
                        IsSuccess = true,
                        Result = "Deleted successful",
                        Message = "Label with ID = " + id + " is deleted by UserId = " + userId + "."
                    });
            }
            return NotFound(
                new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "Not found.",
                    Message = "No data exist for Id = " + id
                });
        }

        /// <summary>
        /// Assign label/s to todolist record.
        /// </summary>
        /// <param name="assignLabelToListModel"></param>
        /// <returns>Ok if successful else not found.</returns>
        /// <response code="200"> Assigns specified label/s to todolist record.</response>
        /// <response code="404"> Error: 404 not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("AssignLabelToList")]
        public async Task<IActionResult> AssignLabelToList(AssignLabelToListModel assignLabelToListModel)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            assignLabelToListModel.CreatedBy = userId;

            AssignLabelToListDto assignLabelToListDto = _mapper.Map<AssignLabelToListDto>(assignLabelToListModel);
            bool isAssigned = await _labelContract.AssignLabelToList(assignLabelToListDto);
            if (isAssigned)
            {
                return Ok(
                    new ApiResponse<object>
                    {
                        IsSuccess = true,
                        Result = "Assignment to ToDoItem successful."
                    });
            }
            return NotFound(
                new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "Assignment to ToDoItem failed."
                });
        }
        /// <summary>
        /// Assign label/s to todoitem record.
        /// </summary>
        /// <param name="assignLabelToItemModel"></param>
        /// <returns>Ok if successful else not found.</returns>
        /// <response code="200"> Assigns specified label/s to todoitem record.</response>
        /// <response code="404"> Error: 404 not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("AssignLabelToItem")]
        public async Task<IActionResult> AssignLabelToItem(AssignLabelToItemModel assignLabelToItemModel)
        {
            long userId = long.Parse(HttpContext.Items["UserId"].ToString());
            assignLabelToItemModel.CreatedBy = userId;

            AssignLabelToItemDto assignLabelToItemDto = _mapper.Map<AssignLabelToItemDto>(assignLabelToItemModel);
            bool isAssigned = await _labelContract.AssignLabelToItem(assignLabelToItemDto);
            if (isAssigned)
            {
                return Ok(
                    new ApiResponse<object>
                    {
                        IsSuccess = true,
                        Result = "Assignment to ToDoItem successful."
                    });
            }
            return NotFound(
                new ApiResponse<string>
                {
                    IsSuccess = false,
                    Result = "Assignment to ToDoItem failed."
                });
        }
    }
}