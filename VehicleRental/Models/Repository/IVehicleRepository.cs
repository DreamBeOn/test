using System;
using System.Collections.Generic;
using System.Linq;

namespace VehicleRental.Models
{
    public interface IVehicleRepository
    {
        IQueryable<Vehicle> getVehicles(DateTime? from = null
                                        , DateTime? to = null
                                        , IEnumerable<int> bodyTypes = null
                                        , IEnumerable<int> fuelTypes = null
                                        , IEnumerable<int> transmissionTypes = null
                                        , int? passangersCount = null);

        IQueryable<Vehicle> getVehicles(string nameContains
                                        , IEnumerable<int> bodyTypes = null
                                        , IEnumerable<int> fuelTypes = null
                                        , IEnumerable<int> transmissionTypes = null
                                        , int? passangersCount = null);

        IQueryable<Vehicle> getVehicles();

        IQueryable<string> getAllBodyTypes();
        IQueryable<string> getAllFuelTypes();
        IQueryable<string> getAllTransmissionTypes();
        IQueryable<string> getAllDrives();
    }
}
