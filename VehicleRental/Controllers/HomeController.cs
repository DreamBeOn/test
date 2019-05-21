using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleRental.Models;

namespace VehicleRental.Controllers
{
    public class HomeController : Controller
    {
        private IVehicleRepository repository;

        public HomeController(IVehicleRepository repo)
        {
            repository = repo;
        }

        public IActionResult Management(string modelName, string[] bodyTypes, string[] fuelTypes, string[] transmissionTypes, int? places, string orderBy, bool descending, string add, string btnADD)
        {
            if (add != null && add == "ADD")
            {
                ViewData["IsAddPresed"] = true;
            }
            if (btnADD != null && btnADD == "X")
            {
                ViewData["IsAddPresed"] = false;
            }
            foreach (string s in bodyTypes)
            {
                ViewData[s] = true;
            }
            foreach (string s in fuelTypes)
            {
                ViewData[s] = true;
            }
            foreach (string s in transmissionTypes)
            {
                ViewData[s] = true;
            }
            ViewData["ModelName"] = modelName;
            ViewData["Places"] = places;
            ViewData["OrderBy"] = orderBy;
            ViewData["Descending"] = descending;
            IQueryable<Vehicle> vehicles = repository.getVehicles();
            if (orderBy == "Name")
            {
                if (descending)
                {
                    vehicles = vehicles.OrderByDescending(x => x.ModelName);
                }
                else
                {
                    vehicles = vehicles.OrderBy(x => x.ModelName);
                }
            }
            else
            {
                if (orderBy == "Price")
                {
                    if (descending)
                    {
                        vehicles = vehicles.OrderByDescending(x => x.PricePerDay);
                    }
                    else
                    {
                        vehicles = vehicles.OrderBy(x => x.PricePerDay);
                    }
                }
            }
            HomeViewModel vm = new HomeViewModel()
            {
                Drives = repository.getAllDrives()
                ,
                BodyTypes = repository.getAllBodyTypes()
                ,
                FuelTypes = repository.getAllFuelTypes()
                ,
                TransmissionTypes = repository.getAllTransmissionTypes()
                ,
                Vehicles = vehicles
            };
            return View(vm);
        }

        public IActionResult Index(string dateRange, string[] bodyTypes, string[] fuelTypes, string[] transmissionTypes, int? places, string orderBy, bool descending)
        {
            foreach (string s in bodyTypes)
            {
                ViewData[s] = true;
            }
            foreach (string s in fuelTypes)
            {
                ViewData[s] = true;
            }
            foreach (string s in transmissionTypes)
            {
                ViewData[s] = true;
            }
            ViewData["Places"] = places;
            ViewData["DateRange"] = dateRange;
            ViewData["OrderBy"] = orderBy;
            ViewData["Descending"] = descending;
            IQueryable<Vehicle> vehicles = repository.getVehicles();
            if (orderBy == "Name")
            {
                if (descending)
                {
                    vehicles = vehicles.OrderByDescending(x => x.ModelName);
                }
                else
                {
                    vehicles = vehicles.OrderBy(x => x.ModelName);
                }
            }
            else
            {
                if (orderBy == "Price")
                {
                    if (descending)
                    {
                        vehicles = vehicles.OrderByDescending(x => x.PricePerDay);
                    }
                    else
                    {
                        vehicles = vehicles.OrderBy(x => x.PricePerDay);
                    }
                }
            }
            HomeViewModel vm = new HomeViewModel()
            {
                BodyTypes = repository.getAllBodyTypes()
                , FuelTypes = repository.getAllFuelTypes()
                , TransmissionTypes = repository.getAllTransmissionTypes()
                , Vehicles = vehicles
            };
            return View(vm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
