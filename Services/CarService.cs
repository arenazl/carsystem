using System;
using System.Collections.Generic;
using carsabm.Controllers;
using carsabm.Models;

namespace carsabm.Services
{
    public class CarService : ICarService
    {

        private static List<Car> _cars = new List<Car>();
        private readonly ISession _session;

        public CarService(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;

            _cars = getCarList();

            if (_cars == null)
            {
                _cars = new List<Car>()
                {
                    new Car { Id = 1, Make = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Color = "Red" },
                    new Car { Id = 2, Make = "Ford", Model = "Mustang", Year = 2021, Price = 35000, Color = "Blue" },
                    new Car { Id = 3, Make = "Chevrolet", Model = "Camaro", Year = 2022, Price = 45000, Color = "Black" }
                };

                SetCarList(_cars);
            }
        }

        public IEnumerable<Car> GetAll()
        {
            return _cars;
        }

        public Car GetById(int id)
        {
            if (id == 0)
            {
                return new Car();
            }
            else
            {
                Car car = _cars.Find(c => c.Id == id);
                return car;
            }
        }

        public void Add(Car car)
        {
            car.Id = _cars.Count + 1;
            _cars.Add(car);
            SetCarList(_cars);
        }

        public void Update(Car car)
        {
            int index = _cars.FindIndex(c => c.Id == car.Id);
            if (index == -1)
            {
                _cars[index] = car;
                SetCarList(_cars);
            }
        }

        public void Delete(int id)
        {
            int index = _cars.FindIndex(c => c.Id == id);
            _cars.RemoveAt(index);
            SetCarList(_cars);

        }

        public bool Guess(int id, decimal guess)
        {
            Car car = _cars.FirstOrDefault(c => c.Id == id);

            var guessed = Math.Abs(guess - car.Price) < 5000;

            SetIsGuessedPrice(guessed);

            return guessed;

        }

        #region session Management

        public void SetIsGuessedPrice(bool value)
        {
            _session.SetObjectAsJson<bool>("GuessedPrice", value);
        }

        public bool getIsGuessedPrice()
        {
            return _session.GetObjectFromJson<bool>("GuessedPrice");
        }

        public void SetCarList(List<Car> cars)
        {
            _session.SetObjectAsJson("Cars", cars);
        }

        public List<Car> getCarList()
        {
            List<Car> cars = _session.GetObjectFromJson<List<Car>>("Cars");
            return cars;
        }
        #endregion
    }
}

