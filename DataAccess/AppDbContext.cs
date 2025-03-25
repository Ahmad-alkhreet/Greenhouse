//using Domain;


//namespace DataAccess
//{
//    public class AppDbContext : DbContext
//    {
//        public DbSet<Sensor> Sensors { get; set; }
//        public DbSet<Measurement> Measurements { get; set; }
//        public DbSet<User> Users { get; set; }

//        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<User>()
//                .Property(u => u.Role)
//                .HasConversion<string>(); // Convert Enum to String in DB

//            base.OnModelCreating(modelBuilder);
//        }
//    }
//}