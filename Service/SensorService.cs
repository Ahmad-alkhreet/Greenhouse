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

        public async Task AddSensorAsync(string location, string type)
        {
            var sensor = new Sensor(0, location, type);
            await _sensorRepository.AddAsync(sensor);
        }

        public async Task UpdateSensorAsync(int id, string location, string type)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id);
            if (sensor != null)
            {
                sensor.SetLocation(location);
                sensor.SetType(type);
                await _sensorRepository.UpdateAsync(sensor);
            }
        }

        public async Task DeleteSensorAsync(int id)
        {
            await _sensorRepository.DeleteAsync(id);
        }
    }
}
