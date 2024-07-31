using System;
using ParkingSystem.Entities;
using ParkingSystem.Services;

class Program
{
    static void Main(string[] args)
    {

        ParkingService parkingService = null;

        ShowInstructions();

        while (true)
        {
            Console.Write("Masukkan perintah : ");
            var input = Console.ReadLine();
            var command = input?.Split(' ');

            if(command == null || command.Length == 0) continue;
            switch(command[0].ToLower())
            {
                case "create_parking_lot":
                    int totalSlots = int.Parse(command[1]);
                    parkingService = new ParkingService(totalSlots);
                    Console.WriteLine($"Created a parking lot with {totalSlots} slots");
                    break;

                case "park":
                    var vehicle = new Vehicle
                    {
                        RegistrationNumber = command[1],
                        Color = command[2],
                        Type = command[3] == "Mobil" ? VehicleTypes.Mobil : VehicleTypes.Motor
                    };
                    Console.WriteLine(parkingService.ParkVehicle(vehicle));
                    break;

                case "leave":
                    int slotNumber = int.Parse(command[1]);
                    Console.WriteLine(parkingService.LeaveVehicle(slotNumber));
                    break;

                case "status":
                    if(parkingService == null) {
                        Console.WriteLine("Please create a parking lot first!");
                        break;
                    }
                    Console.WriteLine(parkingService.GetStatus());
                    break;

                case "occupied_slots":
                    if(parkingService == null) {
                        Console.WriteLine("Please create a parking lot first!");
                        break;
                    }
                    Console.WriteLine(parkingService.CountOccupiedSlots());
                    break;

                case "available_slots":
                    if(parkingService == null) {
                        Console.WriteLine("Please create a parking lot first!");
                        break;
                    }
                    Console.WriteLine(parkingService.CountAvailableSlots());
                    break;

                case "type_of_vehicles":
                    if(parkingService == null) {
                        Console.WriteLine("Please create a parking lot first!");
                        break;
                    }
                    var type = command[1] == "Motor" ? VehicleTypes.Motor : VehicleTypes.Mobil;
                    Console.WriteLine(parkingService.CountVehiclesByType(type));
                    break;

                case "count_vehicles_by_color":
                    if(parkingService == null) {
                        Console.WriteLine("Please create a parking lot first!");
                        break;
                    }
                    var color = command[1];
                    Console.WriteLine($"Jumlah plat dengan warna {color} : " + parkingService.CountVehiclesByColor(color));
                    var platesByColor = parkingService.GetRegistrationNumbersByColor(color);
                    Console.WriteLine($"Plat with {color} : " + string.Join(", ", platesByColor));
                    break;

                case "registration_numbers_for_vehicles_with_odd_plate":
                    if(parkingService == null) {
                        Console.WriteLine("Please create a parking lot first!");
                        break;
                    }

                    var oddPlates = parkingService.CountOddAndEvenPlates(true);
                    Console.WriteLine("Jumlah Plat Ganjil: " + oddPlates.Count);
                    Console.WriteLine("Plat Ganjil: " + string.Join(", ", oddPlates));
                    break;

                case "registration_numbers_for_vehicles_with_even_plate":
                    if(parkingService == null) {
                        Console.WriteLine("Please create a parking lot first!");
                        break;
                    }
                    var evenPlates = parkingService.CountOddAndEvenPlates(false);
                    Console.WriteLine("Jumlah Plat Genap: " + evenPlates.Count);
                    Console.WriteLine("Plat Genap: " + string.Join(", ", evenPlates));
                    break;

                case "exit":
                    return;

                default:
                    Console.WriteLine("Unknown Command");
                    break;
            }
        }
    }

    private static void ShowInstructions()
    {
        Console.WriteLine("Selamat datang di sistem parkir!");
        Console.WriteLine("Berikut adalah perintah-perintah yang dapat Anda gunakan: ");
        Console.WriteLine("1. create_parking_lot {jumlah_slot} -> Membuat lot parkir dengan jumlahh tertentu");
        Console.WriteLine("2. park {nomorPolisi} {warna} {jenis_kendaraan} -> Memarkir Kendaraan");
        Console.WriteLine("3. leave {nomor_slot} -> Mengeluarkan kendaraan dari slot parkir");
        Console.WriteLine("4. status -> Menampilkan status parkir");
        Console.WriteLine("5. exit -> Keluar dari program");
        Console.WriteLine("========================== Reports ==========================");
        Console.WriteLine("1. occupied_slots -> Menampilkan laporan jumlah lot yang terisi");
        Console.WriteLine("2. available_slots -> Menampilkan laporan jumlah lot yang tersedia");
        Console.WriteLine("3. type_of_vehicles {type} -> Menampilkan laporan jumlah kendaraan berdasarkan jenis kendaraan");
        Console.WriteLine("4. slot_numbers_for_vehicles_with_color {color} -> Menampilkan laporan jumlah kendaraan berdasarkan warna kendaraan");
        Console.WriteLine("5. registration_numbers_for_vehicles_with_odd_plate -> Menampilkan laporan jumlah kendaraan berdasarkan nomor kendaraan ganjil");
        Console.WriteLine("6. registration_numbers_for_vehicles_with_even_plate -> Menampilkan laporan jumlah kendaraan berdasarkan nomor kendaraan genap");
        
    }
}