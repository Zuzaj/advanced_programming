using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program{
    public static void Main(string[] args)  {
        bool isRunning = true;
        
    IPHostEntry host = Dns.GetHostEntry("localhost");
//wybieramy pierwszy adres z listy
IPAddress ipAddress = host.AddressList[0];
IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

Socket socket = new(
    localEndPoint.AddressFamily, 
    SocketType.Stream, 
    ProtocolType.Tcp);
//łączenie się z serwerem
socket.Connect(localEndPoint);

while(isRunning){
string wiadomosc = Console.ReadLine();
//wysyłanie wiadomości na serwer enkodowanej w UTF8
//string wiadomosc = "Wiadomość od klienta";
byte[] wiadomoscBajty = Encoding.UTF8.GetBytes(wiadomosc);
int len = wiadomoscBajty.Length;
byte[] lenBajty =  BitConverter.GetBytes(len);
socket.Send(lenBajty, SocketFlags.None);
socket.Send(wiadomoscBajty, SocketFlags.None);
if (wiadomosc == "!end"){
    isRunning = false;
} 
var bufor_len = new byte[4];
socket.Receive(bufor_len, SocketFlags.None);
int messageLen = BitConverter.ToInt32(bufor_len,0);
//bufor na odbieranie danych
var bufor_message = new byte[messageLen];
//odebranie wiadomosci z serwera
int liczbaBajtów = socket.Receive(bufor_message, SocketFlags.None);
String odpowiedzSerwera = Encoding.UTF8.GetString(bufor_message, 0, liczbaBajtów);
Console.WriteLine($"Dostałem odpowiedź: \"{odpowiedzSerwera}\"");
}
try {
    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
}
catch{}
}
}