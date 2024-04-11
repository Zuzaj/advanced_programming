using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FileMonitoring
    {
     public static void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Added file: {e.Name}");
    }

    public static void OnFileDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Deleted file: {e.Name}");
    }
    

    public static void SearchFiles(string directoryPath, string searchString, BlockingQueue<string> fileQueue)
    {
        try
        {
            foreach (string filePath in Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories))
            {
                if (Path.GetFileName(filePath).Contains(searchString))
                {
                    // Matching file found, add to queue
                    fileQueue.Enqueue(filePath);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while searching: {ex.Message}");
        }
    }
}


// Class representing blocking queue
public class BlockingQueue<T>
{
    private readonly object syncLock = new object();
    private readonly Queue<T> queue = new Queue<T>();

    public void Enqueue(T item)
    {
        lock (syncLock)
        {
            queue.Enqueue(item);
            Monitor.Pulse(syncLock);
        }
    }

    public T Dequeue()
    {
        lock (syncLock)
        {
            while (queue.Count == 0)
            {
                Monitor.Wait(syncLock);
            }
            return queue.Dequeue();
        }
    }
}
}