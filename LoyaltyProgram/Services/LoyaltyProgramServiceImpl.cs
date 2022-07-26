using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class LoyaltyProgramServiceImpl : LoyaltyProgramService
    {
        private DatabaseContext databaseContext;
        private ISortHelper<Models.Program> _sortHelper;
        public LoyaltyProgramServiceImpl(DatabaseContext databaseContext, ISortHelper<Models.Program> sortHelper)
        {
            this.databaseContext = databaseContext;
            this._sortHelper = sortHelper;
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

        public PagedList<Models.Program> GetPrograms(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<Models.Program> programs;
            if (filterString != null)
            {
                programs =databaseContext.Programs.Where(c => c.Status == 1 && c.Name.Contains(filterString));
            }
            else
            {
                programs = databaseContext.Programs.Where(c => c.Status == 1);
            }

            var sortedPrograms = _sortHelper.ApplySort(programs, pagingParameters.OrderBy);
            if (programs != null)
            {
                return PagedList<Models.Program>.ToPagedList(sortedPrograms, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
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
