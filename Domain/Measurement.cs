using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Measurement
    {
        private int Id { get; set; }
        private double Temperature { get; set; }
        private double Humidity { get; set; }
        private DateTime Timestamp { get; set; }
        private int SensorId { get; set; }

        public Measurement(int id, double temperature, double humidity, DateTime timestamp, int sensorId)
        {
            Id = id;
            Temperature = temperature;
            Humidity = humidity;
            Timestamp = timestamp;
            SensorId = sensorId;
        }

        public int GetId() => Id;
        public double GetTemperature() => Temperature;
        public double GetHumidity() => Humidity;
        public DateTime GetTimestamp() => Timestamp;
        public int GetSensorId() => SensorId;

        public void UpdateMeasurement(float temperature, float humidity)
        {
            Temperature = temperature;
            Humidity = humidity;
            Timestamp = DateTime.Now; // Auto-update timestamp
        }
    }

}
