using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class LoyaltyProgramServiceImpl : LoyaltyProgramService
    {
        private DatabaseContext databaseContext;
        public LoyaltyProgramServiceImpl(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public bool AddProgram(Models.Program program)
        {
            if (program != null)
            {
                databaseContext.Programs.Add(program);
                return databaseContext.SaveChanges() > 0;
            }
            return false;
        }

        public bool DeleteProgram(int id)
        {
            var program= databaseContext.Programs.FirstOrDefault(o => o.Id == id);
            if (program != null)
            {
                if (program.Status == 1)
                {
                    program.Status = 0;
                    return databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        
        }

        public int GetProgramCount()
        {
            return databaseContext.Programs.Where(p => p.Status == 1).Count();
        }

        public Models.Program GetProgramById(int id)
        {
            var program= databaseContext.Programs.FirstOrDefault(o => o.Id == id);
            if (program != null)
            {
                if (program.Status == 1)
                {
                    return program;
                }
            }

            return null;
        }

        public List<Models.Program> GetPrograms()
        {
            return databaseContext.Programs.Where(o => o.Status == 1).ToList();
        }

        public bool UpdateProgram(Models.Program program, int id)
        {
            if (program != null)
            {
                var programDb = databaseContext.Programs.FirstOrDefault(o => o.Id == id);

                if (programDb != null)
                {
                    if (programDb.Status == 1)
                    {
                        if (program.Name != null)
                            programDb.Name = program.Name;
                        if (program.Status != null)
                            programDb.Status = program.Status;
                        if (program.Description != null)
                            programDb.Description = program.Description;

                        return databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
