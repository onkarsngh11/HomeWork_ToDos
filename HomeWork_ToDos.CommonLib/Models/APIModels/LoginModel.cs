using Swashbuckle.AspNetCore.Annotations;

namespace HomeWork_ToDos.CommonLib.Models.APIModels
{

    [SwaggerSchemaFilter(typeof(LoginModel))]
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
