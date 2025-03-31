namespace SchoolManagementApp.MVC.Models
{
    public class ClassViewModel
    {
        public int Id { get; set; }
        public string CourseName {get;set;}
        public string Time { get; set; }
        public string LecturerName {get;set;}
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Created"; // Default status
    }
}