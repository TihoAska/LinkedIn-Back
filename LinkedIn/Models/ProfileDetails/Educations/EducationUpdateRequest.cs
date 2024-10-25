﻿namespace LinkedIn.Models.ProfileDetails.Educations
{
    public class EducationUpdateRequest
    {
        public int Id { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Grade { get; set; }
        public string ActivitiesAndSocieties { get; set; }
        public string Description { get; set; }
    }
}
