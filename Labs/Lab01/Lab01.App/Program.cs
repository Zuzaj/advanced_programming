OsobaFizyczna osoba = new OsobaFizyczna("Jan", "Kowalski", "Adam", "44", "44");
List<PosiadaczRachunku> posiadacze = new List<PosiadaczRachunku>();
posiadacze.Add(osoba);

RachunekBankowy rachunekBankowy = new RachunekBankowy("gs7", 1000, true, posiadacze);
RachunekBankowy rachunekDocelowy = new RachunekBankowy("gs8", 1000, true, posiadacze);

RachunekBankowy.DokonajTransakcji(rachunekBankowy, rachunekDocelowy, 1001, "przelew");
Console.WriteLine(rachunekDocelowy.StanRachunku);
Console.WriteLine(rachunekBankowy.StanRachunku);

return;


public abstract class PosiadaczRachunku
{
    public abstract override string ToString();
}

public class OsobaFizyczna : PosiadaczRachunku
{
    private string imie;
    private string nazwisko;
    private string drugieImie;
    private string PESEL;
    private string numerPaszportu;

    public string Imie
    {
        get { return imie; }
        set { imie = value; }
    }

    public string Nazwisko
    {
        get { return nazwisko; }
        set { nazwisko = value; }
    }

    public string NumerPaszportu
    {
        get { return numerPaszportu; }
        set { numerPaszportu = value; }
    }

    public string DrugieImie
    {
        get { return drugieImie; }
        set { drugieImie = value; }
    }

    public string Pesel
    {
        get { return PESEL; }
        set
        {
            if (value == null || value.Length != 11)
                throw new ArgumentException("PESEL musi składać się z 11 cyfr.");

            PESEL = value;
        }
    }


    public OsobaFizyczna(string imie, string nazwisko, string drugieImie, string PESEL, string numerPaszportu)
    {
        if (PESEL == null && numerPaszportu == null)
            throw new Exception("PESEL albo numer paszportu muszą być nie null");
        this.imie = imie;
        this.nazwisko = nazwisko;
        this.drugieImie = drugieImie;
        this.PESEL = PESEL;
        this.numerPaszportu = numerPaszportu;
    }

    public override string ToString()
    {
        return $"Osoba fizyczna: {imie} {nazwisko}";
    }
}

public class OsobaPrawna : PosiadaczRachunku
{
    private string nazwa;
    private string siedziba;

    public string Nazwa
    {
        get { return nazwa; }
    }

    public string Siedziba
    {
        get { return siedziba; }
    }

    public OsobaPrawna(string nazwa, string siedziba)
    {
        this.nazwa = nazwa;
        this.siedziba = siedziba;
    }

    public override string ToString()
    {
        return $"Osoba prawna: {nazwa}, siedziba: {siedziba}";
    }
}

public class RachunekBankowy
{
    private string numer;
    private decimal stanRachunku;
    private bool czyDozwolonyDebet;

    List<PosiadaczRachunku> _PosiadaczeRachunku = new List<PosiadaczRachunku>();
    List<Transakcja> _Transakcje = new List<Transakcja>();

    public static void DokonajTransakcji(RachunekBankowy rachunekZrodlowy, RachunekBankowy rachunekDocelowy, decimal kwota,
        string opis)
    {
        if (kwota < 0)
            throw new ArgumentException("Kwota transakcji nie moze byc ujemna");
        if (rachunekZrodlowy == null && rachunekDocelowy == null)
            throw new ArgumentNullException("Oba rachunki nie moga byc rowne null");
        if (!rachunekZrodlowy.CzyDozwolonyDebet && kwota > rachunekZrodlowy.StanRachunku)
            throw new InvalidOperationException("Brak wystarczających środków na rachunku źródłowym.");
        if (rachunekZrodlowy == null)
        {
            rachunekDocelowy.stanRachunku += kwota;
            Transakcja transakcja = new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis);
            rachunekZrodlowy._Transakcje.Add(transakcja);
        }
        else
        {
            rachunekZrodlowy.stanRachunku -= kwota;
            rachunekDocelowy.stanRachunku += kwota;
            Transakcja transakcja = new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis);
            rachunekZrodlowy._Transakcje.Add(transakcja);
            rachunekDocelowy._Transakcje.Add(transakcja);
        }
    }

    public string Numer
    {
        get { return numer; }
    }

    public decimal StanRachunku
    {
        get { return stanRachunku; }
    }

    public bool CzyDozwolonyDebet
    {
        get { return czyDozwolonyDebet; }
    }

    public List<PosiadaczRachunku> PosiadaczeRachunku
    {
        get { return _PosiadaczeRachunku; }
    }

    public RachunekBankowy(string numer, decimal stanRachunku, bool czyDozwolonyDebet, List<PosiadaczRachunku> posiadaczeRachunku)
    {
        if (posiadaczeRachunku == null || posiadaczeRachunku.Count == 0)
            throw new ArgumentException("Lista posiadaczy musi zawierać co najmniej jedną pozycję");

        this.numer = numer;
        this.stanRachunku = stanRachunku;
        this.czyDozwolonyDebet = czyDozwolonyDebet;
        this._PosiadaczeRachunku = posiadaczeRachunku;
    }

    public static RachunekBankowy operator +(RachunekBankowy rachunek, PosiadaczRachunku posiadacz)
    {
        if (posiadacz == null)
            throw new ArgumentNullException(nameof(posiadacz), "Posiadacz nie może być null.");

        if (rachunek._PosiadaczeRachunku.Contains(posiadacz))
            throw new InvalidOperationException("Posiadacz jest już na liście posiadaczy rachunku.");

        rachunek._PosiadaczeRachunku.Add(posiadacz);
        return rachunek;
    }

    public static RachunekBankowy operator -(RachunekBankowy rachunek, PosiadaczRachunku posiadacz)
    {
        if (posiadacz == null)
            throw new ArgumentNullException(nameof(posiadacz), "Posiadacz nie może być null.");

        if (!rachunek._PosiadaczeRachunku.Contains(posiadacz))
            throw new InvalidOperationException("Posiadacz nie jest na liście posiadaczy rachunku.");

        if (rachunek._PosiadaczeRachunku.Count - 1 < 1)
            throw new InvalidOperationException("Nie można usunąć posiadacza, ponieważ liczba posiadaczy spadłaby poniżej 1.");

        rachunek._PosiadaczeRachunku.Remove(posiadacz);
        return rachunek;
    }

    public override string ToString()
    {
        string posiadaczeInfo = "Posiadacze rachunku:";
        foreach (var posiadacz in _PosiadaczeRachunku)
        {
            posiadaczeInfo += $"\n - {posiadacz}";
        }

        string transakcjeInfo = "Transakcje rachunku:";
        foreach (var transakcja in _Transakcje)
        {
            transakcjeInfo += $"\n - {transakcja}";
        }

        return $"Numer rachunku: {numer}\nStan rachunku: {stanRachunku}\n{posiadaczeInfo}\n{transakcjeInfo}";
    }
}

public class Transakcja
{
    private RachunekBankowy rachunekZrodlowy;
    private RachunekBankowy rachunekDocelowy;
    private decimal kwota;
    private string opis;

    public RachunekBankowy RachunekZrodlowy
    {
        get { return rachunekZrodlowy; }
    }

    public RachunekBankowy RachunekDocelowy
    {
        get { return rachunekDocelowy; }
    }

    public decimal Kwota
    {
        get { return kwota; }
    }

    public string Opis
    {
        get { return opis; }
    }

    public Transakcja(RachunekBankowy rachunekZrodlowy, RachunekBankowy rachunekDocelowy, decimal kwota, string opis)
    {
        if (rachunekDocelowy == null || rachunekZrodlowy == null)
            throw new ArgumentException("Rachunek nie moze być równy null");
        this.rachunekZrodlowy = rachunekZrodlowy;
        this.rachunekDocelowy = rachunekDocelowy;
        this.kwota = kwota;
        this.opis = opis;
    }

    public override string ToString()
    {
        return
            $"Rachunek docelowy: {this.rachunekDocelowy.Numer}, rachunek zrodlowy: {this.rachunekZrodlowy.Numer}, kwota: {this.Kwota}, opis: {this.Opis}";
    }
}