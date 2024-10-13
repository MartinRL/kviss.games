namespace kviss.games.Spel.MerEllerMindre.Omgång;

using static SystemGuid;
using Händelser = HashSet<IHändelse>;
using Frågor = ISet<Fråga>;
using Ställning = Dictionary<string, ushort>; // todo - global usings

public static class Medlare // in/ut I/O (händelser)
{
    // tar emot kommandon (alla utom första (Skapa) har id 

    // hämtar och bifogar händesler till händelseförrådet

    // vyer...
}

public record Spelare(string Namn);

public record Fråga(string Kategori, string Jämförelse, string Enhet, string Jämförelsefråga, string Deltafråga, string Alt1Namn, int Alt1Tal, string Alt2Namn, int Alt2Tal);

// händelser
public interface IHändelse { Guid Id { get; } } // "used to mimic a discriminated union", dvs returnera olika typer

public record Skapad(Guid Id, Spelare Spelmästare, Frågor Frågor) : IHändelse;

// kommandon
public interface IKommando { }
public record Skapa(Spelare Spelmästare, Frågor Frågor) : IKommando; // guid som en del av url:en bra då det blir omöjligt att hoppa in i andras omgångar medelst testa url:er

// vyer
public interface IVy { }

public record Tillstånd(Ställning Ställning, bool ÄrSkapad, bool ÄrAvslutad)
{
    public static readonly Tillstånd Initialt = new([], false, false);
}

// todo: många spelare --> trådsäkerhet
public static class Beslutare // functionell kärna (functional core)
{
    public static Tillstånd Aggregera(this Händelser @this, Tillstånd tillstånd) =>
        @this.Aggregate(tillstånd, Utveckla);
   
    public static Tillstånd Aggregera(this Händelser @this) =>
        @this.Aggregera(Tillstånd.Initialt);

    public static Händelser Besluta(this Tillstånd @this, IKommando kommando) =>
        kommando switch
        {
            Skapa k => Skapa(k),
            _ => throw new InvalidOperationException()
        };

    private static Tillstånd Utveckla(Tillstånd tillstånd, IHändelse händelse) => 
        händelse switch
        {
            Skapad skapad => tillstånd with { ÄrSkapad = true },
            _ => tillstånd
        };
    
    private static Händelser Skapa(Skapa k) =>
        new Skapad(NewGuid(), k.Spelmästare, k.Frågor).TillHändelser();
}

public static class Vyer // in-memory DB (till att börja med)
{
    public static Guid OmgångId(this Händelser @this) =>
        @this.First().Id;
}

// händelseförråd

public static class HändelseFörråd // in-memory DB (till att börja med)
{
    private static Dictionary<Guid /* stream/omgång-id */, Händelser> händelseFörråd = [];

    public static void Bifoga(IHändelse händelse) => 
        Bifoga(händelse.TillHändelser());
    
    public static void Bifoga(Händelser händelser) => throw new NotImplementedException();

    public static Händelser Hämta(Guid id)
    {
        if (!händelseFörråd.ContainsKey(id))
            händelseFörråd[id] = [];

        return händelseFörråd[id];
    }
}

// hjälpare

public static class Extensions
{
    public static Händelser TillHändelser(this IHändelse h) => [h];
}

public static class SystemGuid
{
    public static Func<Guid> NewGuid = Guid.NewGuid;
}
