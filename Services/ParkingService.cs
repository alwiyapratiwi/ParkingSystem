using System;
using System.Collections.Generic;
using System.Linq;
using ParkingSystem.Entities;

namespace ParkingSystem.Services
{
    public class ParkingService
    {
        private List<Slot> _slots;
        private const int HourlyRate = 5000;

        public ParkingService(int totalSlots)
        {
            _slots = new List<Slot> (totalSlots);
            for (int i = 1; i <= totalSlots; i++)
            {
                _slots.Add(new Slot(i));
            }
        }

        public string ParkVehicle(Vehicle vehicle) 
        {
            if(vehicle.Type != VehicleTypes.Mobil && vehicle.Type != VehicleTypes.Motor)
            {
                return "Sorry, only Car and Motor are allowed";
            }

            var emptySlot = _slots.FirstOrDefault(slot => slot.IsEmpty);
            if(emptySlot == null) 
            {
                return "Sorry, parking lot is full";
            }

            vehicle.CheckInTime = DateTime.Now;
            emptySlot.AssignVehicle(vehicle);
            return $"Allocated slot number: {emptySlot.Number}";
        }

        public string LeaveVehicle(int slotNumber)
        {
            var slot = _slots.FirstOrDefault(s => s.Number == slotNumber);
            if (slot == null || slot.IsEmpty)
            {
                return $"Slot number {slotNumber} is already empty";
            }

            var vehicle = slot.Vehicle;
            var hoursParked = (DateTime.Now - vehicle.CheckInTime).TotalHours;
            var cost = Math.Ceiling(hoursParked) * HourlyRate;
            slot.RemoveVehicle();
            return $"Slot number {slotNumber} is free. Parking cost: {cost:C}";
        }

        public string GetStatus()
        {
            var occupiedSlots = _slots.Where(slot => !slot.IsEmpty)
            .Select(slot => new
            {
                SlotNo = slot.Number,
                Type = slot.Vehicle.Type,
                RegistrationNo = slot.Vehicle.RegistrationNumber,
                Color = slot.Vehicle.Color
            });

            if(!occupiedSlots.Any())
            {
                return "Parking lot is empty";
            }

            var status = "Slot No. | Type | Registration No | Color \n ";
            foreach (var slot in occupiedSlots)
            {
                status += $"{slot.SlotNo} | {slot.Type} | {slot.RegistrationNo} | {slot.Color} \n";
            }

            return status;
        }

        public int CountOccupiedSlots()
        {
            return _slots.Count(slot => !slot.IsEmpty);
        }

        public int CountAvailableSlots()
        {
            return _slots.Count(slot => slot.IsEmpty);
        }

        public int CountVehiclesByType(VehicleTypes type)
        {
            return _slots.Count(slot => !slot.IsEmpty && slot.Vehicle.Type == type);
        }

        private bool IsOddPlate(string plateNumber)
        {
            var parts = plateNumber.Split('-');
            var middlePart = parts[1];

            var lastDigitChar = middlePart.Last();
            var lastDigit = lastDigitChar - '0';
            return lastDigit % 2 != 0;
        }

        public List<String> CountOddAndEvenPlates(bool isOdd)
        {
            var plates = _slots
            .Where(slot => !slot.IsEmpty)
            .Select(slot => slot.Vehicle.RegistrationNumber)
            .ToList();
            
            var oddPlates = plates.Where(plate => IsOddPlate(plate)).ToList();
            var evenPlates = plates.Where(plate => !IsOddPlate(plate)).ToList();
            if(isOdd){
                return oddPlates;
            } else {
                return evenPlates;
            }
            
        }

        public int CountVehiclesByColor(String color)
        {
            return _slots.Count(slot => ! slot.IsEmpty && slot.Vehicle != null && slot.Vehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
        }

        public List<string> GetRegistrationNumbersByColor(string color)
        {
            var plates = _slots
            .Where(slot => ! slot.IsEmpty && slot.Vehicle != null && slot.Vehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
            .Select(slot => slot.Vehicle.RegistrationNumber)
            .ToList();
            return plates;
        }

    }
    
}