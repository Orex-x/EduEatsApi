using Microsoft.AspNetCore.Mvc;

namespace EduEatsApi.ViewModels;

public class UploadDataViewModel
{
    [BindProperty]
    public BufferedSingleFileUploadDb FileUpload { get; set; }
}