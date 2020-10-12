using AutoMapper;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using HomeWork_ToDos.CommonLib.Models.DbModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace HomeWork_ToDos.CommonLib.Helpers
{
    /// <summary>
    /// Mapping class used by automapper.
    /// </summary>
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            //User mapping  
            CreateMap<UserModel, UserDto>();
            CreateMap<CreateUserModel, CreateUserDto>();
            CreateMap<CreateUserDto, UserDbModel>();
            CreateMap<UserDbModel, UserDto>();

            //Labels mapping
            CreateMap<LabelDbModel, LabelDto>();
            CreateMap<LabelDto, LabelModel>();

            CreateMap<CreateLabelModel, CreateLabelDto>();
            CreateMap<CreateLabelDto, LabelDbModel>();

            CreateMap<AssignLabelToListModel, AssignLabelToListDto>();
            CreateMap<AssignLabelToListDto, MapLabelsToListDbModel>();

            CreateMap<AssignLabelToItemModel, AssignLabelToItemDto>();
            CreateMap<AssignLabelToItemDto, MapLabelsToItemDbModel>();

            CreateMap<DeleteLabelModel, DeleteLabelDto>();

            //ToDoList mapping
            CreateMap<ToDoListDto, ToDoListModel>();
            CreateMap<JsonPatchDocument<UpdateToDoListModel>, JsonPatchDocument<UpdateToDoListDto>>();
            CreateMap<Operation<UpdateToDoListModel>, Operation<UpdateToDoListDto>>();
            CreateMap<ToDoListDto, UpdateToDoListDto>();
            CreateMap<ToDoListDbModel, ToDoListDto>();
            CreateMap<MapLabelsToListDbModel, MapLabelToListDto>();

            CreateMap<CreateToDoListModel, CreateToDoListDto>();
            CreateMap<CreateToDoListDto, ToDoListDbModel>();

            CreateMap<UpdateToDoListModel, UpdateToDoListDto>();
            CreateMap<UpdateToDoListDto, ToDoListDbModel>().ReverseMap();
            CreateMap<UpdateToDoListDto, ToDoListModel>();

            CreateMap<DeleteToDoListModel, DeleteToDoListDto>();

            //ToDoItem mapping
            CreateMap<ToDoItemDto, ToDoItemModel>();
            CreateMap<ToDoItemDbModel, ToDoItemDto>();
            CreateMap<MapLabelsToItemDbModel, MapLabelToItemDto>();
            CreateMap<JsonPatchDocument<UpdateToDoItemModel>, JsonPatchDocument<UpdateToDoItemDto>>();
            CreateMap<Operation<UpdateToDoItemModel>, Operation<UpdateToDoItemDto>>();
            CreateMap<ToDoItemDto, UpdateToDoItemDto>();

            CreateMap<CreateToDoItemModel, CreateToDoItemDto>();
            CreateMap<CreateToDoItemDto, ToDoItemDbModel>();

            CreateMap<UpdateToDoItemModel, UpdateToDoItemDto>();
            CreateMap<UpdateToDoItemDto, ToDoItemDbModel>().ReverseMap();
            CreateMap<UpdateToDoItemDto, ToDoItemModel>();

            CreateMap<DeleteToDoItemModel, DeleteToDoItemDto>();



        }
    }
}
