namespace VehicleRental.Models
{
    public class Vehicle
    {
        public short Id
        {
            get;
            set;
        }


        public string ModelName
        {
            get;
            set;
        }


        public string BodyType
        {
            get;
            set;
        }


        public string FuelType
        {
            get;
            set;
        }

        public string TransmissionType
        {
            get;
            set;
        }


        public short PassangersCount
        {
            get;
            set;
        }


        public bool? AirCondition
        {
            get;
            set;
        }


        public double EngineVolume
        {
            get;
            set;
        }


        public double FuelConsuptionPer100km
        {
            get;
            set;
        }


        public string Drive
        {
            get;
            set;
        }


        public decimal PricePerDay
        {
            get;
            set;
        }


        public decimal Deposit
        {
            get;
            set;
        }


        public byte[] Image
        {
            get;
            set;
        }
    }
}
