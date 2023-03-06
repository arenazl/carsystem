using System;

namespace carsabm.Models;

    public class Car
    {
    public int Id { get; set; } = 0;
        public string Make { get; set; }
    public string Model { get; set; }
        public int Year { get; set; }
        public int Doors { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
    }


