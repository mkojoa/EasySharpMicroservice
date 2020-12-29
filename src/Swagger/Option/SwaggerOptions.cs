namespace EasySharp.Swagger.Option
{
    public class SwaggerOptions
    {
        public bool Enabled { get; set; }
        public bool ReDocEnabled { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string RoutePrefix { get; set; }
        public bool IncludeSecurity { get; set; }
        public string Description { get; set; }
        public string TermsOfService { get; set; }
        public SecurityOptions SecurityOptions { get; set; }
    }
}