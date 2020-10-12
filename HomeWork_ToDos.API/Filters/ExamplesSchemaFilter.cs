using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace HomeWork_ToDos.Filters
{
    public class ExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            schema.Example = GetExampleOrNullFor(context.Type);
        }

        private IOpenApiAny GetExampleOrNullFor(Type type)
        {
            return type.Name switch
            {
                "CreateLabelModel" => new OpenApiObject
                {
                    ["Description"] = new OpenApiString("Phones")
                },
                "DeleteLabelModel" => new OpenApiObject
                {
                    ["LabelId"] = new OpenApiLong(3)
                },
                "AssignLabelToListModel" => new OpenApiObject
                {
                    ["LabelId"] = new OpenApiArray(),
                    ["ToDoListId"] = new OpenApiLong(2)
                },
                "AssignLabelToItemModel" => new OpenApiObject
                {
                    ["LabelId"] = new OpenApiArray(),
                    ["ToDoItemId"] = new OpenApiLong(3)
                },
                "CreateToDoItemModel" => new OpenApiObject
                {
                    ["ToDoListId"] = new OpenApiLong(1),
                    ["Notes"] = new OpenApiString("Review Iphone Se")
                },
                "UpdateToDoItemModel" => new OpenApiObject
                {
                    ["ToDoItemId"] = new OpenApiLong(2),
                    ["Notes"] = new OpenApiString("Review IPhone SE 2")
                },
                "DeleteToDoItemModel" => new OpenApiObject
                {
                    ["ToDoItemId"] = new OpenApiLong(1)
                },
                "CreateToDoListModel" => new OpenApiObject
                {
                    ["Description"] = new OpenApiString("List of IPhones")
                },
                "UpdateToDoListModel" => new OpenApiObject
                {
                    ["ToDoListId"] = new OpenApiLong(2),
                    ["Description"] = new OpenApiString("List of Google phones")
                },
                "DeleteToDoListModel" => new OpenApiObject
                {
                    ["ToDoListId"] = new OpenApiLong(1)
                },
                "CreateUserModel" => new OpenApiObject
                {
                    ["FirstName"] = new OpenApiString("Onkar"),
                    ["LastName"] = new OpenApiString("Singh"),
                    ["UserName"] = new OpenApiString("Onkar"),
                    ["Password"] = new OpenApiString("123"),
                },
                "LoginModel" => new OpenApiObject
                {
                    ["UserName"] = new OpenApiString("Onkar"),
                    ["Password"] = new OpenApiString("123"),
                },
                _ => null,
            };
        }
    }
}
