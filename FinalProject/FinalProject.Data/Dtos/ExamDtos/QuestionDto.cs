namespace FinalProject.Data.Dtos.ExamDtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int ExamTemplateId { get; set; }
        public required string QuestionText { get; set; }
        public required string AnswerA { get; set; }
        public required string AnswerB { get; set; }
        public required string AnswerC { get; set; }
        public required string AnswerD { get; set; }
        public required string CorrectAnwser { get; set; }
        public required string CandidateAnswer { get; set; } = "";


    }
}
