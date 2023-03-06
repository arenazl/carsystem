using System;
using carsabm.Models;

namespace carsabm.Services
{
    public interface ICarService
    {
        IEnumerable<Car> GetAll();
        Car GetById(int id);
        void Add(Car auto);
        void Update(Car auto);
        void Delete(int id);
        bool Guess(int id, decimal guess);
        void SetIsGuessedPrice(bool value);
        bool getIsGuessedPrice();
        void SetCarList(List<Car> cars);
        List<Car> getCarList();
    }

}

