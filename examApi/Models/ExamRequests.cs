using System.ComponentModel.DataAnnotations;

namespace examApi.Models
{
    public class ExamRequest
    {
        [Required]
        public string examType { get; set; }

        [Required]
        public string examName { get; set; }

        [Required]
        public int questionCount { get; set; }

    }

    public class QuestionRequest
    {
        [Required]
        public string questionText { get; set; }

        [Required]

        public string[] options { get; set; }

        [Required]
        public int answerIndex { get; set; }

    }
}
