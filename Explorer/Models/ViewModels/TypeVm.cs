using Microsoft.AspNetCore.Http;

namespace Explorer.Models.ViewModels
{
    public class TypeVm
    {
        public string Format { get; set; }
        public IFormFile FormData { get; set; }
    }
}
