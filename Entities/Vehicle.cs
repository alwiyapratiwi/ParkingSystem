using System;

namespace ParkingSystem.Entities
{
    public class Vehicle
    {
        public string RegistrationNumber {get; set;}
        public string Color {get; set;}
        public VehicleTypes Type {get; set;}
        public DateTime CheckInTime {get; set;}
    }
}