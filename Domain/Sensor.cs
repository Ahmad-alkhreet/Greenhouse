using System.Collections.Generic;

namespace Domain
{
    public class Sensor
    {
        private int Id { get; set; }
        private string Location { get; set; }
        private string Type { get; set; }
        private List<Measurement> Measurements { get; set; } = new List<Measurement>();

        public Sensor(int id, string location, string type)
        {
            Id = id;
            Location = location;
            Type = type;
        }

        public int GetId() => Id;
        public string GetLocation() => Location;
        public string GetType() => Type;

        public void SetLocation(string location) => Location = location;
        public void SetType(string type) => Type = type;

        public void AddMeasurement(Measurement measurement)
        {
            Measurements.Add(measurement);
        }

        public List<Measurement> GetMeasurements()
        {
            return new List<Measurement>(Measurements);
        }



        private int BuildingId { get; set; }

        public int GetBuildingId() => BuildingId;

        public Sensor(int id, string location, string type, int buildingId)
        {
            Id = id;
            Location = location;
            Type = type;
            BuildingId = buildingId;
        }

    }
}
