using System;
using System.Collections.Generic;
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

    
    public string LaunchDroneInspection() => "Запуск камер високої роздільної здатності та діагностика гвинтів виконана успішно.";
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

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        
        List<DeliveryVehicle> fleet = new List<DeliveryVehicle>
        {
            new CargoVan("Renault Master", 1200),
            new CourierDrone("DJI FlyCart 30", 15),
            new ElectricScooter("Xiaomi Pro 2", 8),
            new CourierDrone("Autel Titan", 20)
        };

        Console.WriteLine("--- Обхід колекції з використанням is та as ---\n");

        foreach (DeliveryVehicle vehicle in fleet)
        {
            Console.WriteLine($"Об'єкт: {vehicle.GetDescription()}");

            
            if (vehicle is CourierDrone drone)
            {
                
                Console.WriteLine($"  -> [is вибірка] Знайдено дрон! Виконуємо специфічну операцію:");
                Console.WriteLine($"     {drone.LaunchDroneInspection()}");
            }

            
            CargoVan van = vehicle as CargoVan;
            if (van != null)
            {
                
                Console.WriteLine($"  -> [as вибірка] Знайдено вантажний фургон. Потребує ліцензії категорії B.");
            }

            Console.WriteLine(new string('-', 50));
        }
    }
}