using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Povorot.DAL.Contexts;
using Povorot.DAL.Models;

namespace Povorot.DAL.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<Car> _cars;
        private IGenericRepository<CarBrand> _carBrands;
        private IGenericRepository<CarModel> _carModels;
        private IGenericRepository<CarStation> _carStations;
        private IGenericRepository<CarType> _carType;
        private IGenericRepository<ClientCar> _clientCar;
        private IGenericRepository<Engine> _engines;
        private IGenericRepository<Mechanic> _mechanics;
        private IGenericRepository<Price> _prices;
        private IGenericRepository<RepairPost> _repairPosts;
        private IGenericRepository<Request> _requests;
        private IGenericRepository<Transmission> _tranmissions;
        private IGenericRepository<Work> _works;
        private IGenericRepository<WorkCategory> _workCategories;
        

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
       

        public IGenericRepository<Car> Cars => _cars ?? new GenericRepository<Car>(_context);
        public IGenericRepository<CarBrand> CarBrands => _carBrands ?? new GenericRepository<CarBrand>(_context);
        public IGenericRepository<CarModel> CarModels => _carModels ?? new GenericRepository<CarModel>(_context);

        public IGenericRepository<CarStation> CarStations =>
            _carStations ?? new GenericRepository<CarStation>(_context);

        public IGenericRepository<CarType> CarType => _carType ?? new GenericRepository<CarType>(_context);
        public IGenericRepository<ClientCar> ClientCar => _clientCar ?? new GenericRepository<ClientCar>(_context);
        public IGenericRepository<Engine> Engines => _engines ?? new GenericRepository<Engine>(_context);
        public IGenericRepository<Mechanic> Mechanics => _mechanics ?? new GenericRepository<Mechanic>(_context);
        public IGenericRepository<Price> Prices => _prices ?? new GenericRepository<Price>(_context);

        public IGenericRepository<RepairPost> RepairPosts =>
            _repairPosts ?? new GenericRepository<RepairPost>(_context);

        public IGenericRepository<Request> Requests => _requests ?? new GenericRepository<Request>(_context);

        public IGenericRepository<Transmission> Tranmissions =>
            _tranmissions ?? new GenericRepository<Transmission>(_context);

        public IGenericRepository<Work> Works => _works ?? new GenericRepository<Work>(_context);

        public IGenericRepository<WorkCategory> WorkCategories =>
            _workCategories ?? new GenericRepository<WorkCategory>(_context);
        
        public async Task Save(long userId)
        {
            foreach (var entity in _context.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted))
            {
                var trackable = entity.Entity as BaseRecord;
                if(trackable == null) continue;

                switch(entity.State)
                {
                    case EntityState.Added:
                        trackable.CreatedUserId = userId;
                        trackable.CreationDateTime = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        trackable.ModifiedUserId = userId;
                        trackable.ModifiedDateTime = DateTime.UtcNow;
                        break;
                }
            }
            await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}