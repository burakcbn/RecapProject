using Business.Abstract;
using Business.Validation.FluentValidation;
using Core.Aspects.Validation;
using Core.CrossCuttingConcerns;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService;
        public CarManager(ICarDal carDal, IBrandService brandService)
        {
            _brandService = brandService;
            _carDal = carDal;
        }
        public IDataResult<List<Car>> GetList()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(null, "Sistem Bakımda");
            }
            return new SuccesDataResult<List<Car>>(_carDal.GetList());
        }
        public IDataResult<List<CarDetail>> GetCarDetail()
        {
            return new SuccesDataResult<List<CarDetail>>(_carDal.GetCarDetails());
        }

        public IDataResult<Car> Get(int id)
        {
            return new SuccesDataResult<Car>(_carDal.Get(c => c.CarId == id));
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            var result = BusinessRules.Run(CheckIfCarCountOfBrandCorrect(car.BrandId),CarLimitCheck());
            if (!result.Success)
            {
                return result;
            }
            _carDal.Add(car);
            return new SuccessResult();
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult();
        }

        private IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            var result = _carDal.GetList(c => c.BrandId == brandId);
            if (result.Count>=5)
            {
                return new ErrorResult("Bu marka için stok dolu ");
            }
            return new SuccessResult();
        }
        private IResult CarLimitCheck()
        {
            var result = _carDal.GetList();
            if (result.Count>=45)
            {
                return new ErrorResult("Araba stoğumuz  dolu");
            }
            return new SuccessResult();
        }
    }
}
