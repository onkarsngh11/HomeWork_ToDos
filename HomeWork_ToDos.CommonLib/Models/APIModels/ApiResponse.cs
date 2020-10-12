namespace HomeWork_ToDos.CommonLib.Models.APIModels
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
    }
}
