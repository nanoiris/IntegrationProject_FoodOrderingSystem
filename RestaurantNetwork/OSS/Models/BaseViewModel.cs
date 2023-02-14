namespace OSS.Models
{
    public class BaseViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; }
        public BaseViewModel(){}
        public BaseViewModel(RoutePath[] Paths)
        {
            this.Paths = Paths;
        }
    }
}
