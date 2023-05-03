using System.ComponentModel.DataAnnotations;

namespace CRUD_C_.CreateViewModel
{
    public class CreateTodoViewModel
    {
        [Required (ErrorMessage = "One task is required at least!")]
        public string Title { get; set; }
        public bool Done { get; set; }
    }
}