using DataAccess.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class MeasurementService
    {
        private readonly MeasurementRepository _measurementRepository;

        public MeasurementService(MeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
        }

        public async Task<List<Measurement>> GetAllMeasurementsAsync()
        {
            return await _measurementRepository.GetAllAsync();
        }

        public async Task<List<Measurement>> GetMeasurementsBySensorIdAsync(int sensorId)
        {
            return await _measurementRepository.GetMeasurementsBySensorIdAsync(sensorId);
        }

        public async Task AddMeasurementAsync(int sensorId, double temperature, double humidity)
        {
            var measurement = new Measurement(0, temperature, humidity, DateTime.Now, sensorId);
            await _measurementRepository.AddAsync(measurement);
        }
    }
}
