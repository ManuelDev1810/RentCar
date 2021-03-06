﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Core.Entities;
using CarService.Core.Interfaces;
using MassTransit;
using Serilog;

namespace CarService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CarController(ICarRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cars = await _repository.GetAll();
                return Ok(cars);
            }
            catch (Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var car = await _repository.Get(id);
                return Ok(car);
            }
            catch (Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport(int? id = null, string description = null, string status = null,
            int? brandID = null, int? modelID = null, int? typeOfCarID = null, int? typeOfFuelID = null,
            string PlateNumber = null, string EngineNumber = null, string ChassisNumber = null)
        {
            try
            {
                var cars = await _repository.GetReport(id, description, status, brandID, modelID, typeOfCarID,
                    typeOfFuelID, PlateNumber, EngineNumber, ChassisNumber);
                return Ok(cars);
            }
            catch (Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Car car)
        {
            try
            {
                var response = await _repository.Add(car);
                if(response != 0)
                {
                    return Ok("Added successfully");
                }
                else
                {
                    return BadRequest("An error ocurred, contact IT Staff");
                }

            }
            catch (Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Car car)
        {
            try
            {
                var response = await _repository.Update(car);
                if (response != 0)
                {
                    //Sending the object to the car exchange
                    await _publishEndpoint.Publish<Car>(car);

                    return Ok("Updated successfully");
                }
                else
                {
                    return BadRequest("An error ocurred, contact IT Staff");
                }
            }
            catch (Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _repository.Delete(id);
                if (response != 0)
                {
                    return Ok("Deleted successfully");
                }
                else
                {
                    return BadRequest("An error ocurred, contact IT Staff");
                }
            }
            catch (Exception e)
            {
                //Log error
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return BadRequest("An error ocurred, contact IT Staff");
            }
        }
    }
}
