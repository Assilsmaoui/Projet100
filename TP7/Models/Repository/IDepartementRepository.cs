namespace TP7.Models.Repository
{
    public interface IDepartementRepository
    {
       Task<List<Departement>> GetDepartement();

       Task<Departement> AddDepartement(Departement dep);

        Task<Departement> GetDepartementbyID(int id);

        Task<Departement> GetDepartementbyName(string Name);

        Task UpdateDepartent(Departement dep);

        Task DeleteDepartement(int id);



    }
}
