using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp1
{
    public class SampleContext : DbContext
    {
        // Имя будущей базы данных можно указать через
        // вызов конструктора базового класса
        public SampleContext() : base("Flat_Rent")
        { }

        // Отражение таблиц базы данных на свойства с типом DbSet
        public DbSet<Flat> Flat { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Deal> Deal { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
