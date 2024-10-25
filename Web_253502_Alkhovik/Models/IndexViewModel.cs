using Microsoft.AspNetCore.Mvc.Rendering;
using Web_253502_Alkhovik.Helpers;

namespace Web_253502_Alkhovik.Models
{
    public class IndexViewModel
    {
        public int SelectedId { get; set; }
        public SelectList ListItems { get; set; }
    }
}