using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Threading;

namespace Lab12
{
//     public class Sinus : IObservable<int>
// {
//     // public IDisposable Subscribe(IObserver<int> observer)
//     // {
//     //     observer.OnNext(1);
//     //     observer.OnNext(2);
//     //     observer.OnNext(3);
//     //     observer.OnNext(4);
//     //     observer.OnNext(5);
//     //     observer.OnCompleted();
//     //    // return Disposable.Empty;
//     // }
// }

public class Obserwator : IObserver<int>
{
    string nazwa = "";
    public Obserwator(string nazwa)
    {
        this.nazwa = nazwa;
    }
    public void OnCompleted()
    {
        Console.WriteLine("Obserwator " + nazwa + ": obiekt obserwowany zakończył wysyłać dane.");
    }

    public void OnError(Exception error)
    {
        Console.WriteLine("Obserwator " + nazwa + ": obiekt obserwowany zwrócił błąd " + error.Message);
    }

    public void OnNext(int value)
    {
        Console.WriteLine("Obserwator " + nazwa + ": obiekt obserwowany wyemitował " + value);
    }
}

}