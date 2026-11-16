using System;
using System.Text;


class MediaDevice
{
    
    public virtual void ShowInfo()
    {
        Console.WriteLine("MediaDevice: Базовий медіапристрій");
    }

    
    public void DisplayType()
    {
        Console.WriteLine("MediaDevice: Метод DisplayType (база)");
    }
}


class SmartTv : MediaDevice
{
    
    public override void ShowInfo()
    {
        Console.WriteLine("SmartTv: Розумний телевізор (override)");
    }

    
    public new void DisplayType()
    {
        Console.WriteLine("SmartTv: Метод DisplayType (прихований через new)");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("--- 1. Демонстрація override  ---");
        MediaDevice devRef = new SmartTv(); 
        devRef.ShowInfo();                  
        Console.WriteLine($"Фактичний тип у купі (GetType): {devRef.GetType().Name}\n");

        SmartTv tvRef = new SmartTv();      
        tvRef.ShowInfo();
        Console.WriteLine();

        Console.WriteLine("--- 2. Демонстрація new  ---");
        
        devRef.DisplayType(); 
        tvRef.DisplayType();
    }
}