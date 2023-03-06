using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using carsabm.Models;
using carsabm.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace carsabm.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        public IActionResult Index()
        {
            ViewBag.GuessedPrice = _carService.getIsGuessedPrice();
            return View(_carService.GetAll());
        }

        public IActionResult Restart()
        {
            _carService.SetIsGuessedPrice(false);
            ViewBag.GuessedPrice = false;
            return RedirectToAction("Index");
        }

        public IActionResult ShowPrices()
        {
            _carService.SetIsGuessedPrice(true);
            ViewBag.GuessedPrice = true;
            return RedirectToAction("Index");
        }

        public IActionResult Editor(int id)
        {
        var car = _carService.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }   

        [HttpPost]
        public IActionResult Create(Car car)
        {
            _carService.Add(car);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Car car)
        {
            _carService.Update(car);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            _carService.Delete(id);
            return RedirectToAction("Index");
        }

        public bool GuessPrice(int id, decimal guess)
        {
            return _carService.Guess(id, guess);
        }

}
    public static class SessionExtensions
    {
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void SetObjectAsJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }

}