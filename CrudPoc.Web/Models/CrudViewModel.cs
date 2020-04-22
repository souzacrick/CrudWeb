using CrudPoc.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CrudPoc.Web.Models
{
    public class CrudViewModel
    {
        public CrudViewModel()
        {
            Makes = new List<SelectListItem>();
            Models = new List<SelectListItem>();
            Versions = new List<SelectListItem>();
            Announcement = new AnnouncementWebMotors();

        }

        public bool Saved { get; set; }

        public AnnouncementWebMotors Announcement { get; set; }

        public List<SelectListItem> Makes { get; set; }
        public List<SelectListItem> Models { get; set; }
        public List<SelectListItem> Versions { get; set; }

        public List<AnnouncementWebMotors> LstAnnouncements { get; set; }
    }
}