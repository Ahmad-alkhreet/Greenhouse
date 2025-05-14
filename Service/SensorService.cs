using DataAccess.Repositories;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class SensorService
    {
        private readonly SensorRepository _sensorRepository;

        public SensorService(SensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public async Task<List<Sensor>> GetAllSensorsAsync()
        {
            return await _sensorRepository.GetAllAsync();
        }

        public async Task<Sensor> GetSensorByIdAsync(int id)
        {
            return await _sensorRepository.GetByIdAsync(id);
        }

        public async Task AddSensorAsync(string location, string type, int buildingId)
        {
            var sensor = new Sensor(0, location, type, buildingId);
            await _sensorRepository.AddAsync(sensor);
        }

        public async Task UpdateSensorAsync(int id, string location, string type, int buildingId)
        {
            var existingSensor = await _sensorRepository.GetByIdAsync(id);
            if (existingSensor != null)
            {
                var updatedSensor = new Sensor(id, location, type, buildingId);
                await _sensorRepository.UpdateAsync(updatedSensor);
            }
        }

        public async Task DeleteSensorAsync(int id)
        {
            await _sensorRepository.DeleteAsync(id);
        }
    }
}
