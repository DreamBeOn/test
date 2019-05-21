using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace VehicleRental.Models
{
    public class VehicleRepository : IVehicleRepository
    {
        public IQueryable<string> getAllBodyTypes()
        {
            List<string> result = new List<string>();

            SqlConnection sqlConnection = DBUtils.GetSQLConnection();

            string SQLQuery = "SELECT Name FROM BodyTypes";
            sqlConnection.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(SQLQuery, sqlConnection);
                using (DbDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(reader.GetOrdinal("Name")));
                        }
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return result.AsQueryable<string>();
        }

        public IQueryable<string> getAllFuelTypes()
        {
            List<string> result = new List<string>();

            SqlConnection sqlConnection = DBUtils.GetSQLConnection();

            string SQLQuery = "SELECT Name FROM FuelTypes";
            sqlConnection.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(SQLQuery, sqlConnection);
                using (DbDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(reader.GetOrdinal("Name")));
                        }
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return result.AsQueryable<string>();
        }

        public IQueryable<string> getAllTransmissionTypes()
        {
            List<string> result = new List<string>();

            SqlConnection sqlConnection = DBUtils.GetSQLConnection();

            string SQLQuery = "SELECT Name FROM TransmissionTypes";
            sqlConnection.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(SQLQuery, sqlConnection);
                using (DbDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(reader.GetOrdinal("Name")));
                        }
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return result.AsQueryable<string>();
        }

        public IQueryable<Vehicle> getVehicles(DateTime? from = null, DateTime? to = null, IEnumerable<int> bodyTypes = null, IEnumerable<int> fuelTypes = null, IEnumerable<int> transmissionTypes = null, int? passangersCount = null)
        {
            List<Vehicle> result = new List<Vehicle>();

            SqlConnection sqlConnection = DBUtils.GetSQLConnection();

            /*
                SELECT Vehicles.Id
	                , ModelName
	                , BodyTypes.Name as BodyType
	                , FuelTypes.Name as FuelType
	                , TransmissionTypes.Name as TransmissionType
	                , PassangersCount
	                , AirCondition
	                , EngineVolume
	                , FuelConsumptionPer100km
	                , Drives.Name as Drive
	                , PricePerDay
	                , Deposit
	                , Image
                FROM Vehicles
                LEFT JOIN BodyTypes ON BodyTypes.Id = BodyType
                LEFT JOIN FuelTypes ON FuelTypes.Id = FuelType
                LEFT JOIN TransmissionTypes ON TransmissionTypes.Id = TransmissonType
                LEFT JOIN Drives ON Drives.Id = Drive 
             */
            string SQLQuery = "SELECT Vehicles.Id, ModelName, BodyTypes.Name as BodyType, FuelTypes.Name as FuelType, TransmissionTypes.Name as TransmissionType, PassangersCount, AirCondition, EngineVolume, FuelConsumptionPer100km, Drives.Name as Drive, PricePerDay, Deposit, Image FROM Vehicles LEFT JOIN BodyTypes ON BodyTypes.Id = BodyType LEFT JOIN FuelTypes ON FuelTypes.Id = FuelType LEFT JOIN TransmissionTypes ON TransmissionTypes.Id = TransmissonType LEFT JOIN Drives ON Drives.Id = Drive ";

            if ((bodyTypes != null && bodyTypes.Count() > 0)
                || (fuelTypes != null && fuelTypes.Count() > 0)
                || (transmissionTypes != null && transmissionTypes.Count() > 0)
                || (passangersCount != null))
            {
                SQLQuery += "WHERE ";

                bool isANDneeded = false;


                if (bodyTypes != null && bodyTypes.Count() > 0)
                {
                    string t = string.Format("BodyTypes.Id in ({0}) ", string.Join(", ", bodyTypes));
                    SQLQuery += t;
                    isANDneeded = true;
                }

                if (fuelTypes != null && fuelTypes.Count() > 0)
                {
                    if (isANDneeded)
                    {
                        SQLQuery += "AND ";
                    }
                    string t = string.Format("FuelTypes.Id in ({0}) ", string.Join(", ", fuelTypes));
                    SQLQuery += t;
                    isANDneeded = true;
                }

                if (transmissionTypes != null && transmissionTypes.Count() > 0)
                {
                    if (isANDneeded)
                    {
                        SQLQuery += "AND ";
                    }
                    string t = string.Format("FuelTypes.Id in ({0}) ", string.Join(", ", transmissionTypes));
                    SQLQuery += t;
                    isANDneeded = true;
                }

                if (passangersCount != null)
                {
                    if (isANDneeded)
                    {
                        SQLQuery += "AND ";
                    }
                    SQLQuery += " PassangersCount >= " + passangersCount;
                    isANDneeded = true;
                }
            }

            sqlConnection.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(SQLQuery, sqlConnection);
                using (DbDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(new Vehicle
                            {
                                Id = reader.GetInt16(reader.GetOrdinal("Id")),
                                ModelName = reader.GetString(reader.GetOrdinal("ModelName")),
                                BodyType = reader.GetString(reader.GetOrdinal("BodyType")),
                                FuelType = reader.GetString(reader.GetOrdinal("FuelType")),
                                AirCondition = reader.GetBoolean(reader.GetOrdinal("AirCondition")),
                                Drive = reader.GetString(reader.GetOrdinal("Drive")),
                                EngineVolume = reader.GetDouble(reader.GetOrdinal("EngineVolume")),
                                FuelConsuptionPer100km = reader.GetDouble(reader.GetOrdinal("FuelConsumptionPer100km")),
                                PricePerDay = reader.GetDecimal(reader.GetOrdinal("PricePerDay")),
                                Deposit = reader.GetDecimal(reader.GetOrdinal("Deposit")),
                                PassangersCount = reader.GetInt16(reader.GetOrdinal("PassangersCount")),
                                TransmissionType = reader.GetString(reader.GetOrdinal("TransmissionType")),
                                Image = reader.GetValue(reader.GetOrdinal("Image")) is System.DBNull ? null : (byte[])reader.GetValue(reader.GetOrdinal("Image"))
                            });
                        }
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return result.AsQueryable<Vehicle>();
        }

        public IQueryable<Vehicle> getVehicles(string nameContains, IEnumerable<int> bodyTypes = null, IEnumerable<int> fuelTypes = null, IEnumerable<int> transmissionTypes = null, int? passangersCount = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Vehicle> getVehicles()
        {
            List<Vehicle> result = new List<Vehicle>();

            SqlConnection sqlConnection = DBUtils.GetSQLConnection();

            /*
                SELECT Vehicles.Id
	                , ModelName
	                , BodyTypes.Name as BodyType
	                , FuelTypes.Name as FuelType
	                , TransmissionTypes.Name as TransmissionType
	                , PassangersCount
	                , AirCondition
	                , EngineVolume
	                , FuelConsumptionPer100km
	                , Drives.Name as Drive
	                , PricePerDay
	                , Deposit
	                , Image
                FROM Vehicles
                LEFT JOIN BodyTypes ON BodyTypes.Id = BodyType
                LEFT JOIN FuelTypes ON FuelTypes.Id = FuelType
                LEFT JOIN TransmissionTypes ON TransmissionTypes.Id = TransmissonType
                LEFT JOIN Drives ON Drives.Id = Drive 
             */
            string SQLQuery = "SELECT Vehicles.Id, ModelName, BodyTypes.Name as BodyType, FuelTypes.Name as FuelType, TransmissionTypes.Name as TransmissionType, PassangersCount, AirCondition, EngineVolume, FuelConsumptionPer100km, Drives.Name as Drive, PricePerDay, Deposit, Image FROM Vehicles LEFT JOIN BodyTypes ON BodyTypes.Id = BodyType LEFT JOIN FuelTypes ON FuelTypes.Id = FuelType LEFT JOIN TransmissionTypes ON TransmissionTypes.Id = TransmissonType LEFT JOIN Drives ON Drives.Id = Drive ";

            sqlConnection.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(SQLQuery, sqlConnection);
                using (DbDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(new Vehicle
                            {
                                Id = reader.GetInt16(reader.GetOrdinal("Id")),
                                ModelName = reader.GetString(reader.GetOrdinal("ModelName")),
                                BodyType = reader.GetString(reader.GetOrdinal("BodyType")),
                                FuelType = reader.GetString(reader.GetOrdinal("FuelType")),
                                AirCondition = reader.GetBoolean(reader.GetOrdinal("AirCondition")),
                                Drive = reader.GetString(reader.GetOrdinal("Drive")),
                                EngineVolume = reader.GetDouble(reader.GetOrdinal("EngineVolume")),
                                FuelConsuptionPer100km = reader.GetDouble(reader.GetOrdinal("FuelConsumptionPer100km")),
                                PricePerDay = reader.GetDecimal(reader.GetOrdinal("PricePerDay")),
                                Deposit = reader.GetDecimal(reader.GetOrdinal("Deposit")),
                                PassangersCount = reader.GetInt16(reader.GetOrdinal("PassangersCount")),
                                TransmissionType = reader.GetString(reader.GetOrdinal("TransmissionType")),
                                Image = reader.GetValue(reader.GetOrdinal("Image")) is System.DBNull ? null : (byte[])reader.GetValue(reader.GetOrdinal("Image"))
                            });
                        }
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return result.AsQueryable<Vehicle>();
        }

        public IQueryable<string> getAllDrives()
        {
            List<string> result = new List<string>();

            SqlConnection sqlConnection = DBUtils.GetSQLConnection();

            string SQLQuery = "SELECT Name FROM Drives";
            sqlConnection.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(SQLQuery, sqlConnection);
                using (DbDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(reader.GetOrdinal("Name")));
                        }
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return result.AsQueryable<string>();
        }
    }
}
