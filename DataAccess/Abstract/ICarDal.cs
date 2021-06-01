using Core.DataAccess.EntityFramework;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal:IRepositoryBase<Car>
    {
        List<CarDetail> GetCarDetails();
    }
}
