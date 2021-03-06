﻿using System;
namespace CarService.Core.Entities
{
    public class Car
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public string PlateNumber { get; set; }
        public string Status { get; set; }
        public int TypeOfFuelID { get; set; }
        public int TypeOfCarID { get; set; }
        public int BrandID { get; set; }
        public int ModelID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string TypeOfCar { get; set; }
        public string TypeOfFuel { get; set; }
    }
}