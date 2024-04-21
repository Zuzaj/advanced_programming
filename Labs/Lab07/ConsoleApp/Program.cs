using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;  
using System.Text;   
class Program  
{  
    static void Main()  
    {  
           
        //Task_1(1);
         Task_2("somedata.txt","somedata_Hash.txt", algotMD5);
        // Task_3("somedata.txt", "somedataSign.dat");
        // Task_4("somedata.txt", "somedataAES.dat", "pass", 1);
    }  

    static String algoSHA256(String napis)
{
    Encoding enc = Encoding.UTF8;
    var hashBuilder = new StringBuilder();
    using var hash = SHA256.Create();
    byte[] result = hash.ComputeHash(enc.GetBytes(napis));
    foreach (var b in result)
        hashBuilder.Append(b.ToString("x2"));
    return hashBuilder.ToString();
}

static String algoSHA512(String napis)
{
    Encoding enc = Encoding.UTF8;
    var hashBuilder = new StringBuilder();
    using var hash = SHA512.Create();
    byte[] result = hash.ComputeHash(enc.GetBytes(napis));
    foreach (var b in result)
        hashBuilder.Append(b.ToString("x2"));
    return hashBuilder.ToString();
}

static String algotMD5(String napis)
{
    Encoding enc = Encoding.UTF8;
    var hashBuilder = new StringBuilder();
    using var hash = MD5.Create();
    byte[] result = hash.ComputeHash(enc.GetBytes(napis));
    foreach (var b in result)
        hashBuilder.Append(b.ToString("x2"));
    return hashBuilder.ToString();
}

    // Utwórz metodę szyfrowania inputFileu i zapisywania go do określonego pliku przy użyciu klucza publicznego algorytmu RSA   
    static void EncryptText(string filePublicKey ,string inputFile, string outputFile)  
    {  
        string publicKey = File.ReadAllText(filePublicKey);
        string text = File.ReadAllText(inputFile);
        // Zmień text na tablicę bajtów   
        UnicodeEncoding byteConverter = new UnicodeEncoding();  
        byte[] daneDoZaszyfrowania = byteConverter.GetBytes(text);  

        // Utwórz tablicę bajtów, aby przechowywać w niej zaszyfrowane dane   
        byte[] zaszyfrowaneDane;   
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())  
        {  
            // Ustaw publiczny klicz RSA   
            rsa.FromXmlString(publicKey);  
            // Zaszyfruj dane in wstaw je do tabicy zaszyfrowaneDane
            zaszyfrowaneDane = rsa.Encrypt(daneDoZaszyfrowania, false);   
        }  
        // Zapisz zaszyfrowaną tablicę danych do pliku   
        File.WriteAllBytes(outputFile, zaszyfrowaneDane);  

        Console.WriteLine("Dane zostały zaszyfrowane");   
    }  

    static void SaveKeys(string filePublicKey, string filePrivateKey){
        // Stworzenie instancji klasy implementującej algorytm RSA z losową
        // inicjalizacją klucza prywatnego i publicznego
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        // piliki, w których będą trzymane klucze
        
        string ?publicKey = null;
        string ?privateKey = null;
        // jeśli plik istnieje, wczytujemy go z pliku
        if (File.Exists(filePublicKey))
        {
            publicKey = File.ReadAllText(filePublicKey);
        }
        // jeżeli plik nie istnieje, tworzymy plik z kluczem
        else {
            publicKey = rsa.ToXmlString(false); // false aby wziąć klucz publiczny
            File.WriteAllText(filePublicKey, publicKey);
        }
        if (File.Exists(filePrivateKey))
        {
            privateKey = File.ReadAllText(filePrivateKey);
        }
        else {
            privateKey = rsa.ToXmlString(true); // true aby wziąć klucz prywatny
            File.WriteAllText(filePrivateKey, privateKey);   
        }
    }
    // Metoda odszyfrowania danych w określonym pliku przy użyciu klucza prywatnego algorytmu RSA   
    static void DecryptData(string filePrivateKey,string inputFile, string outputFile)  
    {  
        string privateKey = File.ReadAllText(filePrivateKey);
        // odczytanie zaszyfrowanych bajtów z pliku   
        byte[] daneDoOdszyfrowania = File.ReadAllBytes(inputFile);  

        // Create an array to store the decrypted data in it   
        byte[] odszyfrowaneDane;  
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())  
        {  
            // Set the private key of the algorithm   
            rsa.FromXmlString(privateKey);  
            odszyfrowaneDane = rsa.Decrypt(daneDoOdszyfrowania, false);   
        }  

        // Get the string value from the decryptedData byte array   
        UnicodeEncoding byteConverter = new UnicodeEncoding();  
        File.WriteAllText(outputFile, byteConverter.GetString(odszyfrowaneDane));   
    }

            
public static byte[] AESDecrypt(string password, byte[] salt, byte[]initVector,int iterations, byte[] input_data)
{
    Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            Aes decAlg = Aes.Create();
            decAlg.Key = k1.GetBytes(16);
            decAlg.IV = initVector;
            MemoryStream decryptionStreamBacking = new MemoryStream();
            CryptoStream decrypt = new CryptoStream(
                decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write);
            decrypt.Write(input_data, 0, input_data.Length);
             decrypt.Flush();
             k1.Reset();
            return decryptionStreamBacking.ToArray();
}
    public static byte[]? AESEncrypt(String password, byte[]salt, byte[]initVector,
                    int iterations, byte[]input_data)
{

            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            Aes encAlg = Aes.Create();
            encAlg.IV = initVector;
            encAlg.Key = key.GetBytes(16);
            MemoryStream encryptionStream = new MemoryStream();
            CryptoStream encrypt = new CryptoStream(encryptionStream,
                encAlg.CreateEncryptor(), CryptoStreamMode.Write);
            encrypt.Write(input_data, 0, input_data.Length);
            encrypt.FlushFinalBlock();
            encrypt.Close();
            key.Reset();
            return encryptionStream.ToArray();
}


    public static void  Task_1(int command){
        string? filePublicKey = "publicKey.dat";
        string? filePrivateKey = "privateKey.dat";
        SaveKeys(filePublicKey, filePrivateKey);
        
        // szyfrowanie danych
        if (command == 1){
            Console.WriteLine("Enter input file name:");
            string? fileWithInput = Console.ReadLine();
            Console.WriteLine("Enter output file name:");
            string? fileWithOutput = Console.ReadLine();
            EncryptText(filePublicKey,fileWithInput , fileWithOutput);  
        }
        if (command == 2){
            Console.WriteLine("Enter input file name:");
            string? fileWithInput = Console.ReadLine();
            Console.WriteLine("Enter output file name:");
            string? fileWithOutput = Console.ReadLine();
            DecryptData(filePrivateKey, fileWithInput, fileWithOutput);
        }
    
    } 
    public static void Task_2(string fileName, string fileHash, Func<string,string> algoHash){
        if (!File.Exists(fileHash)){
            string newHash = algoHash(fileName);
            File.WriteAllText(fileHash,newHash);
        }
        else{
            string realOuput = algoHash(fileName);
            string givenOutput = File.ReadAllText(fileHash);
            if ( realOuput == givenOutput){
                Console.WriteLine("Matching Hashes!");
            }
            else{
                Console.WriteLine("Hashes don't match.");
            }
        }
    }

    public static void Task_3(string fileA, string fileB){
        //file a (public)
        string publicKey = File.ReadAllText("publicKey.dat");
        string privateKey = File.ReadAllText("privateKey.dat");
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);
        // Wczytaj dane z pliku A
        string data = File.ReadAllText(fileA);
        byte[] bytesToSign = Encoding.UTF8.GetBytes(data);

                //file b (private)
        if (File.Exists(fileB)){
         // Weryfikacja podpisu
            string signature = File.ReadAllText(fileB);
            byte[] signatureBytes = Convert.FromBase64String(signature); // Przekształć string z podpisem na tablicę bajtów
            bool verified = rsa.VerifyData(bytesToSign, CryptoConfig.MapNameToOID("SHA256"), signatureBytes);
            Console.WriteLine("Weryfikacja podpisu: " + verified);
        }
    else{
            byte[] trueSignature = rsa.SignData(bytesToSign, CryptoConfig.MapNameToOID("SHA256"));
            string signatureString = Convert.ToBase64String(trueSignature); // Konwertuj podpis na string
            Console.WriteLine("Wygenerowany podpis: " + signatureString);
            File.WriteAllText(fileB, signatureString);
    }
}  

public static void Task_4(string fileA, string fileB, string password, int type){
    string text = File.ReadAllText(fileA);
    //czyli losowymi danymi dodanymi do hasła przed obliczeniem skrótu
    byte[] salt = Encoding.UTF8.GetBytes("CwuuJx/7");
    byte[] initVector = Encoding.UTF8.GetBytes("uyZG5sl561Wo2ZTE");
    int liczbaIteracji = 5;

if (type == 0){

    string file = File.ReadAllText(fileA);
            byte[] input_data = Encoding.UTF8.GetBytes(file);
            byte[] encrypted = AESEncrypt(password, salt, initVector, liczbaIteracji, input_data);
            File.WriteAllBytes(fileB, encrypted);
}
if (type == 1){
    byte[] file = File.ReadAllBytes(fileB);
     byte[] decrypted = AESDecrypt(password, salt, initVector, liczbaIteracji, file);
     string decryptedText =  Encoding.UTF8.GetString(decrypted);
     File.WriteAllBytes(fileB,decrypted);
     Console.WriteLine($"Original text: {text}");
     Console.WriteLine($"Decrypted text: {decryptedText}");


}
}
}