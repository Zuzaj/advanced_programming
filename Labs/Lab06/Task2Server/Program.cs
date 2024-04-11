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
// bufor na długość wiadomości, 4 bajty
    byte []bufor_len = new byte[4];
    socketKlienta.Receive(bufor_len, SocketFlags.None);
    int messageLenght = BitConverter.ToInt32(bufor_len, 0);
    byte []bufor_message = new byte[messageLenght];
//instrukcja blokująca, czeka na połączenie
    int receivedBytes = socketKlienta.Receive(bufor_message, SocketFlags.None);
    String wiadomoscKlienta = Encoding.UTF8.GetString(bufor_message, 0, receivedBytes);
    Console.WriteLine($"Dostałem wiadomość od klienta: \"{wiadomoscKlienta}\" o długości: {messageLenght}");
    string odpowiedz = $"odczytałem: {wiadomoscKlienta}, długość: {receivedBytes}";
    var echoBytes = Encoding.UTF8.GetBytes(odpowiedz);
    int echo_len = echoBytes.Length;
    var echo_len_bytes =  BitConverter.GetBytes(echo_len);
    socketKlienta.Send(echo_len_bytes, 0);
    socketKlienta.Send(echoBytes, 0);
    try {
        socketSerwera.Shutdown(SocketShutdown.Both);
        socketSerwera.Close();
    }
    catch{}
}
}