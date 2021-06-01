using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete
{
    public class CarDal : EntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetail> GetCarDetails()
        {
            using (var  context = new ReCapProjectContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.BrandId
                             select new CarDetail()
                             {
                                 BrandName = brand.BrandName,
                                 CarName = car.CarName,
                                 DailyPrice = car.DailyPrice
                             };
                return result.ToList();
            }
        }
    }
}
