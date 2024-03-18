using Microsoft.AspNetCore.Mvc.Rendering;

namespace obilet_Assignment.Models
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Origin = new List<SelectListItem>();
            Destination = new List<SelectListItem>();
        }
        public int? SelectedOrigin { get; set; }
        public int? SelectedDestination { get; set; }
        public DateTime DepatureDate { get; set; }
        public List<SelectListItem> Origin { get; set; }
        public List<SelectListItem> Destination { get; set; }

    }
}
