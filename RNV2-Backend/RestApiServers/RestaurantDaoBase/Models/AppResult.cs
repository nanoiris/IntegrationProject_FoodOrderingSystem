namespace RestaurantDaoBase.Models
{
    public class AppResult
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public AppResult() { }
        public AppResult(string message,bool isSuccess) 
        {
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}
