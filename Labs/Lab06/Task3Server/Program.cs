using System.Data.Common;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program{

public static void ReturnFilesDirectories(Socket socketKlienta, string myDir){
    string message = "";
        string[] files = Directory.GetFiles(myDir);
        string[] directories = Directory.GetDirectories(myDir);
        message += "Files: ";
        foreach (string file in files)
                            {
                                message += $"{Path.GetFileName(file)} ";
                            }
                            message += "\nDirectories: ";
                            foreach (string directory in directories)
                            {
                                message +=  $"{Path.GetFileName(directory)} ";
                            }
                            message += "\n[End of list]";
    
   SendMessage(socketKlienta, message);
    }



public static void End(Socket socketSerwera){
    try {
        socketSerwera.Shutdown(SocketShutdown.Both);
        socketSerwera.Close();
    }
    catch{}
}

public static void SendMessage(Socket client, string message){
var echoBytes = Encoding.UTF8.GetBytes(message);
    int echo_len = echoBytes.Length;
    var echo_len_bytes =  BitConverter.GetBytes(echo_len);
    client.Send(echo_len_bytes, 0);
    client.Send(echoBytes, 0);
}

public static void Main(string[]argv){
    string myDir = Directory.GetCurrentDirectory();
    bool isRunning = true;

    IPHostEntry host = Dns.GetHostEntry("localhost");
//wybieramy pierwszy adres z listy
    IPAddress ipAddress = host.AddressList[0];
    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
//socket nasłuchujący na porcie TCP/IP
    Socket socketSerwera = new(
        localEndPoint.AddressFamily,
        SocketType.Stream,
        ProtocolType.Tcp);
//rezerwacja portu
    socketSerwera.Bind(localEndPoint);
//rozpoczęcie nasłuchiwania
    socketSerwera.Listen(100);
    Socket socketKlienta = socketSerwera.Accept();

    while(isRunning){
//oczekiwanie na połączenie z klientem

// bufor na długość wiadomości, 4 bajty
    byte []bufor_len = new byte[4];
    socketKlienta.Receive(bufor_len, SocketFlags.None);
    int messageLenght = BitConverter.ToInt32(bufor_len, 0);
    byte []bufor_message = new byte[messageLenght];
//instrukcja blokująca, czeka na połączenie
    int receivedBytes = socketKlienta.Receive(bufor_message, SocketFlags.None);
    String wiadomoscKlienta = Encoding.UTF8.GetString(bufor_message, 0, receivedBytes);
    // Console.WriteLine($"Dostałem wiadomość od klienta: \"{wiadomoscKlienta}\" o długości: {messageLenght}");
    if (wiadomoscKlienta == "!end"){
        isRunning = false;
        SendMessage(socketKlienta, "Program ends");
    }

    if (wiadomoscKlienta == "list"){
        ReturnFilesDirectories(socketKlienta,myDir);
    }

    if (wiadomoscKlienta.StartsWith("in ")){
        string subDir = wiadomoscKlienta.Substring(3);
        string newPath;
        if ( subDir == ".."){
            newPath = Path.GetDirectoryName(myDir);
            ReturnFilesDirectories(socketKlienta, newPath);
        }
        else{
            newPath = Path.Combine(myDir,subDir);
        
            if(Directory.Exists(newPath)){
            ReturnFilesDirectories(socketKlienta,newPath);
            }
        
            else{
            SendMessage(socketKlienta, "nieznane polecenie");
        }
        // socketKlienta.Close();
    }   
    }
    else{
        SendMessage(socketKlienta, "nieznane polecenie");
    }
    }
    End(socketSerwera);
}
}
