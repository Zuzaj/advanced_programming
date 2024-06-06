using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;  
using System.Text; 
using System.Reactive.Disposables;  
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
class Program  
{  

    public static void Task1(){
        var sinSource = Observable
            .Interval(TimeSpan.FromSeconds(1))
            .Select(i => Math.Sin(i * 0.01))
            .Where(i => i >= 0 && i <= 0.3)
            .Do(_ => Console.WriteLine("Sinus source emitting"))
            .Finally(() => Console.WriteLine("Sinus source completed"));

        var random = new Random();
        var randomSource = Observable
            .Interval(TimeSpan.FromSeconds(1))
            .Select(_ => random.NextDouble() * 2 - 1)
            .Scan((maxValue, currentValue) => Math.Max(maxValue, currentValue))
            .Do(_ => Console.WriteLine("Random source emitting"))
            .Finally(() => Console.WriteLine("Random source completed"));

        // Subskrybuj oba źródła i wypisuj wartości na konsoli
        // sinSource.Subscribe(value => Console.WriteLine($"Sin: {value}"));
        // randomSource.Subscribe(value => Console.WriteLine($"Random: {value}"));


        var mergedSource = sinSource.Merge(randomSource);

        var limitedSource = mergedSource.TakeUntil(Observable.Timer(TimeSpan.FromSeconds(20)));

        // Subskrybujemy połączone źródło i wypisujemy wartości na konsoli
        limitedSource.Subscribe(
            value => Console.WriteLine($"Value: {value}"),
            () => Console.WriteLine("Observation completed"));


        // Zapobiegaj zakończeniu aplikacji natychmiast
        Thread.Sleep(Timeout.Infinite);
    }
    static void Main()  
    {  
        Task1();
    }  


}