namespace EasySharp.Swagger.Option
{
    public class SecurityOptions
    {
        public bool XmlDoc { get; set; }
        public string ApiName { get; set; }
        public string ApiId { get; set; }
        public string TokenUrl { get; set; }
        public string AuthorityURL { get; set; }
        public string Authority { get; set; }
        public string ClientSecret { get; set; }
        public string IssuerUri { get; set; }
        public string RequireHttpsMetadata { get; set; }
        public string Folder { get; set; }
        public string FilterClassName { get; set; } 
        public Contact Contact { get; set; }
        public License License { get; set; }
        public Scope Scope { get; set; }
    }
}
