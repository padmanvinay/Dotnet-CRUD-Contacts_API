namespace ContactsAPI.Models
{
    public class AddcontactRequest
    {
        public string fullName { get; set; }
        public string email { get; set; }    
        public long phone { get; set; }
        public string address { get; set; }
    }
}