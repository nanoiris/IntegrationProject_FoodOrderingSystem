namespace OSS.Models
{
    public class RoutePath
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Titile { get; set; }

        public RoutePath() { }
        public RoutePath(string controllerName, string actionName,string title)
        {
            this.ControllerName= controllerName;
            this.ActionName= actionName;
            this.Titile= title;
        }
    }
}
