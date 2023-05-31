using System.ComponentModel.DataAnnotations;

namespace EduEatsApi.ViewModels;

public class BufferedSingleFileUploadDb
{
    [Required]
    [Display(Name="File")]
    public IFormFile FormFile { get; set; }
}