using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Models
{
    public interface LoyaltyProgramService
    {
        public PagedList<Program> GetPrograms(PagingParameters pagingParameters);
        public Program GetProgramById(int id);
        public int GetProgramCount();
        public bool DeleteProgram(int id);
        public bool UpdateProgram(Program program, int id);
        public bool AddProgram(Program program);
    }
}
