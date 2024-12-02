namespace TP7.Models.Data
{
    public class SeederData
    {

        public static void Seeder(IApplicationBuilder application)
        {
            var scopeservice = application.ApplicationServices.CreateScope();
            var context= scopeservice.ServiceProvider.GetService<TP7Context>();
            context.Database.EnsureCreated();
            if(context.Departements.Any())
            {
                context.Departements.AddRange(
                    new List<Departement>()
                    {
                        new Departement() {Name="Production"},
                        new Departement() {Name="Logistique"}
                    }
                    
                    );
                context.SaveChanges();
            }
        }
    }
}
