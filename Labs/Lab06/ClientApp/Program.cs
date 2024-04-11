using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program{
    public static void Main(string[] args)  {
        
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
//wysyłanie wiadomości na serwer enkodowanej w UTF8
string wiadomosc = "Wiadomość od klienta";
byte[] wiadomoscBajty = Encoding.UTF8.GetBytes(wiadomosc);
socket.Send(wiadomoscBajty, SocketFlags.None);
//bufor na odbieranie danych
var bufor = new byte[1_024];
//odebranie wiadomosci z serwera
int liczbaBajtów = socket.Receive(bufor, SocketFlags.None);
String odpowiedzSerwera = Encoding.UTF8.GetString(bufor, 0, liczbaBajtów);
Console.WriteLine(odpowiedzSerwera);
try {
    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
}
catch{}
    }
}