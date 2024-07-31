namespace ParkingSystem.Entities
{
    public class Slot
    {
        public int Number {get; private set;}
        public Vehicle Vehicle {get; private set;}
        public bool IsEmpty => Vehicle == null;

        public Slot(int number)
        {
            Number = number;
        }

        public void AssignVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }

        public void RemoveVehicle()
        {
            Vehicle = null;
        }
    }
}