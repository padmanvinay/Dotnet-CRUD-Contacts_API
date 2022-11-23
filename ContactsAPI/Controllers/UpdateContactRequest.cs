namespace ContactsAPI.Controllers
{
    public class UpdateContactRequest
    {
        public string fullName { get; set; }
        public string email { get; set; }    
        public long phone { get; set; }
        public string address { get; set; }
    }
}