namespace ConsoleApp;
public class Program {


    public static void Task_1(int n, int m){

        Random random = new Random(Environment.TickCount);
        List<PThread>pthreads = new List<PThread>();
        List<CThread>cthreads = new List<CThread>();
        Queue<Data> data = new Queue<Data>();
        Mutex mutex = new Mutex();
        for (int a = 0; a < n; a++){
            PThread pt = new PThread(a,random.Next(10000), mutex, data);   
            pthreads.Add(pt);                     
           Thread t = new Thread(new ThreadStart(pt.Start));
           t.Start();
        }

        for (int a = 0; a < m; a++){
            CThread ct = new CThread(a, random.Next(10000), mutex, data);   
            cthreads.Add(ct);                     
           Thread t = new Thread(new ThreadStart(ct.Start));
           t.Start();
        }

        Console.WriteLine("Press 'q' to stop the process");
        bool end = false;

        while (!end)
        {
            if (Console.ReadKey().KeyChar == 'q')
            {
                end = true;
            }
        }

        foreach (PThread t in pthreads)
        {
            t.end = true;
        }
        foreach (CThread t in cthreads)
        {
            t.end = true;
        }

        Console.WriteLine("## Koniec programu ##");


    }

    public static void Task_2(string mycatalog){
        bool running = true;

        string directoryPath = mycatalog; 
        FileSystemWatcher watcher = new FileSystemWatcher(directoryPath);
        watcher.IncludeSubdirectories = false;
        watcher.Created += FileMonitoring.OnFileCreated;
        watcher.Deleted += FileMonitoring.OnFileDeleted;
        watcher.EnableRaisingEvents = true;

        Thread monitorThread = new Thread(() =>
        {
            Console.WriteLine($"Started monitoring folder: {directoryPath}");
            while (running)
            {
                
                Thread.Sleep(1000); //monitor every 1s
            }
        });
        monitorThread.Start();

        //checking input
        Thread inputThread = new Thread(() =>
        {
            Console.WriteLine("Press 'q' to exit.");
            while (running)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar == 'q')
                {
                    running = false;
                }
            }
        });
        inputThread.Start();

        // Waiting for program to finish
        monitorThread.Join();
        inputThread.Join();

}

public static void Task_3(string catalog, string txt){
        string directoryPath = catalog;

        var fileQueue = new BlockingQueue<string>();
        var exitSignal = new ManualResetEvent(false);
        

        Thread searchThread = new Thread(() =>
        {
            FileMonitoring.SearchFiles(directoryPath, txt, fileQueue);
            exitSignal.Set(); // exit signal set after searching
        });
        searchThread.Start();

        Thread writeThread = new Thread(() =>
        {
            while (true)
            {
                string filePath = fileQueue.Dequeue();
                if (filePath == null) break; // end thread when exit signal sent 
                Console.WriteLine($"Found matching file: {filePath}");
            }
        });
        writeThread.Start();

        // wait for searching thread to end
        exitSignal.WaitOne();

        fileQueue.Enqueue(null); // Finish writing thread
        writeThread.Join();

        Console.WriteLine("Searching completed.");
    }

    public static void Task_4(int n){
        int numThreads = n;
        Semaphore semThreads = new Semaphore(initialCount: n, maximumCount: n);

        List<Thread_4>threads = new List<Thread_4>();
        List<Thread_4>threads_copy = new List<Thread_4>();
        for (int a = 0; a < n; a++)
        {
            Thread_4 t_4 = new Thread_4(a, semThreads);
            threads.Add(t_4);
            Thread t = new Thread(new ThreadStart(t_4.Start));
            t.Start();

        }
        bool allStarted = false;
        while(!allStarted){
        foreach (Thread_4 t in threads){
            if (t.Started == false){
                allStarted = false;
                continue;
            }
            else{
                allStarted = true;
            }
        }
        }
        Console.WriteLine("All threads started.");
        
        foreach(Thread_4 t_4 in threads){
            t_4.end = true;
        }
        bool allEnded = false;
        while(!allEnded){
        foreach (Thread_4 t in threads){
            if (t.Ended == false){
                allEnded = false;
                continue;
            }
            else{
                allEnded = true;
            }
        }
        }
    Console.WriteLine("All threads ended.");
    }

    


    public static void Main(string[]argv){
        
    //  Task_1(3,3);
    Task_2("testfolder");
    //Task_3("testfolder", "ile");
    //Task_4(5);


      

    }
}