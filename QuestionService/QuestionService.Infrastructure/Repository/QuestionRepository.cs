
namespace QuestionService.Infrastructure.Repository
{

    public class QuestionRepository(AppDbContext context) : IQuestionRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> CreateQuestion(Question question)
        {

            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Question?> GetFullQuestionById(string questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            return question;
        }
        public async Task<List<Question>> GetAllQuestions()
        {
            var question = await _context.Questions.ToListAsync();
            return question;
        }

        public async Task UpdateQuestion(Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestion(string id)
        {
            var question = await _context.Questions.FindAsync(id) ?? throw new KeyNotFoundException("Given Question Id is not valid");
            question.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionPermanently(string id)
        {
            var question = await _context.Questions.IgnoreQueryFilters().FirstOrDefaultAsync(q => q.QuestionId == id && q.IsDeleted)
                ?? throw new KeyNotFoundException("constrain of given id not found");
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ValidateQuestion(string id)
        {
            var question = await _context.Questions.FindAsync(id);
            return question != null;
        }


    }
}
