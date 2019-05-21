using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.Models
{
    public class HomeViewModel
    {
        public IQueryable<Vehicle> Vehicles
        {
            get;
            set;
        }

        public IQueryable<string> Drives
        {
            get;
            set;
        }

        public IQueryable<string> BodyTypes
        {
            get;
            set;
        }

        public IQueryable<string> FuelTypes
        {
            get;
            set;
        }

        public IQueryable<string> TransmissionTypes
        {
            get;
            set;
        }

    }
}
