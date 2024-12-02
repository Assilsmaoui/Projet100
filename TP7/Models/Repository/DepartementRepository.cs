
using Microsoft.EntityFrameworkCore;

namespace TP7.Models.Repository
{
    public class DepartementRepository : IDepartementRepository
    {
        private readonly TP7Context context;

        public DepartementRepository(TP7Context context)
        {
            this.context = context;
        }
        public async Task<Departement> AddDepartement(Departement dep)
        {
            var result = await context.Departements.AddAsync(dep);
            await context.SaveChangesAsync();//aaaaaa
            return result.Entity; 
        }

        public async Task DeleteDepartement(int id)
        {
            var Dep= await context.Departements.FindAsync(id);
             context.Departements.Remove(Dep);
            await context.SaveChangesAsync();
        }

        public async Task<List<Departement>> GetDepartement()
        {
            List<Departement> departements =
                await context.Departements.ToListAsync();
            return departements;
        }

        public async Task<Departement> GetDepartementbyID(int id)
        {
            return await context.Departements.FindAsync(id);
        }

        public Task<Departement> GetDepartementbyName(string Name)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateDepartent(Departement dep)
        {
            context.Departements.Update(dep);
            await context.SaveChangesAsync();
        }
    }
}
