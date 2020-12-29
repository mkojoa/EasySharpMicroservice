namespace CompanyService.Dtos.Companies
{
    public class CompanyCreateDto
    {
        public string CompanyName  { get; set; }
        public string Ceo          { get; set; }
        public string Logo         { get; set; }
        public bool   Status       { get; set; }
        public int    NoOfEmployee { get; set; }
    }
}