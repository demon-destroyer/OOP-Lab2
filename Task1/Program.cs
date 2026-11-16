using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

abstract class DeliveryVehicle
{
    public string Model { get; set; }
    public double LoadKg { get; set; }

    protected DeliveryVehicle(string model, double loadKg)
    {
        Model = model;
        LoadKg = loadKg > 0 ? loadKg : 1;
    }

    public abstract string EnvironmentType { get; }
    public abstract double CalculateCost(double distanceKm);
    public abstract string GetDescription();
    public virtual string GetStorageConditions() => "Стандартний відкритий паркінг";
}

class CourierDrone : DeliveryVehicle
{
    public CourierDrone(string model, double loadKg) : base(model, loadKg) { }

    public override string EnvironmentType => "Повітряний простір";
    public override double CalculateCost(double distanceKm) => distanceKm * 5.0 + LoadKg * 20.0;
    public override string GetDescription() => $"[Дрон] {Model}, макс. вага: {LoadKg} кг";
    public override string GetStorageConditions() => "Зарядна станція в сухому приміщенні";
}

class CargoVan : DeliveryVehicle
{
    public CargoVan(string model, double loadKg) : base(model, loadKg) { }

    public override string EnvironmentType => "Автомобільні дороги";
    public override double CalculateCost(double distanceKm) => distanceKm * 18.0 + LoadKg * 0.5;
    public override string GetDescription() => $"[Фургон] {Model}, тоннаж: {LoadKg} кг";
}

class ElectricScooter : DeliveryVehicle
{
    public ElectricScooter(string model, double loadKg) : base(model, loadKg) { }

    public override string EnvironmentType => "Велодоріжки та тротуари";
    public override double CalculateCost(double distanceKm) => distanceKm * 6.0;
    public override string GetDescription() => $"[Електроскутер] {Model}, рюкзак: {LoadKg} кг";
}


class CargoShip : DeliveryVehicle
{
    public CargoShip(string model, double loadKg) : base(model, loadKg) { }

    public override string EnvironmentType => "Водні шляхи";
    public override double CalculateCost(double distanceKm) => distanceKm * 3.5 + LoadKg * 0.1;
    public override string GetDescription() => $"[Судно] {Model}, дедвейт: {LoadKg} кг";
    public override string GetStorageConditions() => "Портовий причал";
}

class Program
{
    
    static double CalculateTotalFleetCost(IEnumerable<DeliveryVehicle> vehicles, double distanceKm)
    {
        double totalCost = 0;
        foreach (var vehicle in vehicles)
        {
            
            totalCost += vehicle.CalculateCost(distanceKm);
        }
        return totalCost;
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        
        List<DeliveryVehicle> fleet = new List<DeliveryVehicle>
        {
            new CargoVan("Renault Master", 1200),
            new CourierDrone("DJI FlyCart 30", 15),
            new ElectricScooter("Xiaomi Pro 2", 8),
            new CargoShip("Dnipro-1", 25000) // Новий об'єкт нового класу
        };

        double routeKm = 40.0;

        Console.WriteLine($"--- 1. Поліморфна узагальнена процедура (Пункт 1 & 3) ---");
        double totalCost = CalculateTotalFleetCost(fleet, routeKm);
        Console.WriteLine($"Загальна вартість доставки всього парку на {routeKm} км: {totalCost} грн\n");
        
        Console.WriteLine($"--- 2. Фільтрація за фактичним типом (Пункт 2) ---");
        var drones = fleet.OfType<CourierDrone>();
        foreach (var drone in drones)
        {
            Console.WriteLine($"Відібрано через OfType: {drone.GetDescription()} (Умови: {drone.GetStorageConditions()})");
        }
    }
}