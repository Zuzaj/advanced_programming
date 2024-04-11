using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class PThread
    {
        public int Number;
        public bool end = false;
        public int Delay;
        ThreadStart ?ThreadStart = null;
        public Mutex ?mutex = null;
        public Queue<Data> ?data = null;

        public PThread(int Number, int Delay, Mutex m, Queue<Data> data){
            this.Number = Number;
            this.Delay = Delay;
            this.mutex = m;
            this.data = data;
        }
        
        public void Start(){

            while(!end){
                Thread.Sleep(Delay);
                UseResource();
    
                }
                //Console.WriteLine("## Koniec wątku " + Number + " ##");
            }
        public void UseResource()
    {
       // Console.WriteLine($"Wątek {Number} chce dostęp do mutex");
        //zarządzaj mutexa
        mutex?.WaitOne();  
       // Console.WriteLine($"Wątek {Number} jest w sekcji krytycznej");      
        data?.Enqueue(new Data(Number));
        //zwolniej mutex
        mutex?.ReleaseMutex();
      //  Console.WriteLine($"Wątek {Number} zwalnia mutex");
    }
}
        

            




    public class CThread{
        public int Number;
        public bool end = false;
        public int Delay;
        ThreadStart ?ThreadStart = null;

        public Mutex ?mutex = null;
        public Queue<Data> ?data = null;
        Dictionary<int, int> producer_num;

        public CThread(int Number, int Delay, Mutex m, Queue<Data> data){
            this.Number = Number;
            this.Delay = Delay;
            this.mutex = m;
            this.data = data;
            producer_num = new Dictionary<int, int>();
        }
        
        public void Start(){

            while (!end)
        {
            Thread.Sleep(Delay);
            UseResource();
        }
        if(producer_num != null){
        Console.WriteLine($"For thread {Number}:");
        foreach (KeyValuePair<int,int> entry in producer_num){
            Console.WriteLine($"Producent {entry.Key} - {entry.Value}");
        }
        }
    }
        public void UseResource()
    {
       // Console.WriteLine($"Wątek C {Number} chce dostęp do mutex");
        //zarządzaj mutexa
        mutex?.WaitOne();  
       // Console.WriteLine($"Wątek C {Number} jest w sekcji krytycznej");
        //Console.WriteLine(data.Count);
        if(data?.Count >= 2){     
        var id = data.Dequeue();
        if (!producer_num.ContainsKey(id.ProducerId)){
            producer_num.Add(id.ProducerId,1);
        } 
        else{
            producer_num[id.ProducerId] += 1;
        }
        }

        mutex?.ReleaseMutex();
      //  Console.WriteLine($"Wątek C {Number} zwalnia mutex");
    }
    
    }


    public class Data{
        public int ProducerId;
        public Data(int producerId)
    {
        ProducerId = producerId;
    }
    }


class Thread_4 {

    public bool end = false;
    public Semaphore ?semThreads = null;
    public bool Started = false;

    public bool Ended = false;
    int number;
    public Thread_4(int number, Semaphore sem)
    {
        this.number = number;
        this.semThreads = sem;
        
    }
    public void Start()
    {
        while (!end)
        {
            Console.WriteLine("Thread " + number + " waits");
            semThreads.WaitOne();
            this.Started = true;
            semThreads.Release();        
        }
        Console.WriteLine("Thread " + number + " ends");
        this.Ended = true;
    }
}

}