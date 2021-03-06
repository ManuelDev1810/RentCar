﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Core.Entities;
using CarService.Core.Interfaces;
using Serilog;

namespace CarService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeOfFuelController : ControllerBase
    {
        private readonly ITypeOfFuelRepository _repository;

        public TypeOfFuelController(ITypeOfFuelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var typeOfFuels = await _repository.GetAll();
                return Ok(typeOfFuels);
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
                var typeOfFuel = await _repository.Get(id);
                return Ok(typeOfFuel);
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
        public async Task<IActionResult> GetReport(int? id = null, string description = null, string status = null)
        {
            try
            {
                var typeOfFuels = await _repository.GetReport(id, description, status);
                return Ok(typeOfFuels);
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
        public async Task<IActionResult> Post(TypeOfFuel typeOfFuel)
        {
            try
            {
                var response = await _repository.Add(typeOfFuel);
                if (response != 0)
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
        public async Task<IActionResult> Put(TypeOfFuel typeOfFuel)
        {
            try
            {
                var response = await _repository.Update(typeOfFuel);
                if (response != 0)
                {
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
