using System;
using System.Threading.Tasks;
using Povorot.DAL.Models;

namespace Povorot.DAL.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Car> Cars { get; }
        IGenericRepository<CarBrand> CarBrands { get; }
        IGenericRepository<CarModel> CarModels { get; }
        IGenericRepository<CarStation> CarStations { get; }
        IGenericRepository<CarType> CarType { get; }
        IGenericRepository<ClientCar> ClientCar { get; }
        IGenericRepository<Engine> Engines { get; }
        IGenericRepository<Mechanic> Mechanics { get; }
        IGenericRepository<Price> Prices { get; }
        IGenericRepository<RepairPost> RepairPosts { get; }
        IGenericRepository<Request> Requests { get; }
        IGenericRepository<Transmission> Tranmissions { get; }
        IGenericRepository<Work> Works { get; }
        IGenericRepository<WorkCategory> WorkCategories { get; }
        Task Save(long userId);
    }
}