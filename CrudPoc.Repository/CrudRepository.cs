using CrudPoc.Domain;
using System.Collections.Generic;
using System.Linq;

namespace CrudPoc.Repository
{
    public class CrudRepository
    {
        private readonly CrudDbContext _dbContext;

        public CrudRepository(CrudDbContext context)
        {
            _dbContext = context;
        }

        public void Add(AnnouncementWebMotors anuncioWebMotors)
        {
            _dbContext.Add(anuncioWebMotors);
            _dbContext.SaveChanges();
        }

        public AnnouncementWebMotors Find(int iD)
        {
            return _dbContext.AnnouncementWebMotors.FirstOrDefault(x => x.ID == iD);
        }

        public List<AnnouncementWebMotors> GetAll()
        {
            return _dbContext.AnnouncementWebMotors.ToList();
        }

        public void Remove(int iD)
        {
            AnnouncementWebMotors anuncioWebMotors = _dbContext.AnnouncementWebMotors.FirstOrDefault(x => x.ID == iD);
            _dbContext.AnnouncementWebMotors.Remove(anuncioWebMotors);
            _dbContext.SaveChanges();
        }

        public void Update(AnnouncementWebMotors anuncioWebMotors)
        {
            _dbContext.Update(anuncioWebMotors);
            _dbContext.SaveChanges();
        }
    }
}