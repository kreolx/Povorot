using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Povorot.DAL.Models;

namespace Povorot.DAL.Contexts
{
    public class AppDbContext: IdentityDbContext<User, IdentityRole<long>, long>
    {
        /// <summary>
        /// Машины
        /// </summary>
        public DbSet<Car> Cars { get; set; }
        /// <summary>
        /// Марка машин
        /// </summary>
        public DbSet<CarBrand> CarBrands { get; set; }
        /// <summary>
        /// Модели машин
        /// </summary>
        public DbSet<CarModel> CarModels { get; set; }
        /// <summary>
        /// Сервисы
        /// </summary>
        public DbSet<CarStation> CarStations { get; set; }
        /// <summary>
        /// Типы машин - легковая, грузовая...
        /// </summary>
        public DbSet<CarType> CarTypes { get; set; }
        /// <summary>
        /// Клиентские автомобили
        /// </summary>
        public DbSet<ClientCar> ClientCars { get; set; }
        /// <summary>
        /// Двигатели
        /// </summary>
        public DbSet<Engine> Engines { get; set; }
        /// <summary>
        /// Мастера
        /// </summary>
        public DbSet<Mechanic> Mechanics { get; set; }
        /// <summary>
        /// Прайс
        /// </summary>
        public DbSet<Price> Prices { get; set; }
        /// <summary>
        /// Боксы
        /// </summary>
        public DbSet<RepairPost> RepairPosts { get; set; }
        /// <summary>
        /// Заявки на ремонт
        /// </summary>
        public DbSet<Request> Requests { get; set; }
        /// <summary>
        /// Коробки передач
        /// </summary>
        public DbSet<Transmission> Transmissions { get; set; }
        /// <summary>
        /// Работы
        /// </summary>
        public DbSet<Work> Works { get; set; }
        /// <summary>
        /// Категории работ
        /// </summary>
        public DbSet<WorkCategory> WorkCategories { get; set; }
        
        
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
    }
}