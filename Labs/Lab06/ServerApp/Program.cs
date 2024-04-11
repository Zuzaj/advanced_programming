using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program{


public static void Main(string[]argv){
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
//oczekiwanie na połączenie z klientem
    Socket socketKlienta = socketSerwera.Accept();
// bufor na wiadomość, max 1024 bajty
    byte []bufor = new byte[1_024];
//instrukcja blokująca, czeka na połączenie
    int received = socketKlienta.Receive(bufor, SocketFlags.None);
    String wiadomoscKlienta = Encoding.UTF8.GetString(bufor, 0, received);
    Console.WriteLine(wiadomoscKlienta);
    string odpowiedz = $"odczytałem: {wiadomoscKlienta}";
    var echoBytes = Encoding.UTF8.GetBytes(odpowiedz);
    socketKlienta.Send(echoBytes, 0);
    try {
        socketSerwera.Shutdown(SocketShutdown.Both);
        socketSerwera.Close();
    }
    catch{}
}
}
