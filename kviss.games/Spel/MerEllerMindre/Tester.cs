namespace kviss.games.Spel.MerEllerMindre.Omgång;

using FluentAssertions;
using Xunit;
using static SystemGuid;
using Frågor = ISet<Fråga>;
using Ställning = Dictionary<string, ushort>;

public class OmgångSkall
{
    public readonly Frågor EnFråga = new HashSet<Fråga>() { new("musik", "flest", "miljoner", "Vilken grupp har sålt flest album, {alt1_namn} eller {alt2_namn}?", "{alt1_namn} har sålt flest album, {alt1_tal} {enhet}. Hur många färre {enhet} album har { alt2_namn } sålt?", "ABBA", 400, "Roxette", 75)};

    public readonly Spelare Spelmästare = new("Martin");

    public OmgångSkall() => NewGuid = () => Guid.Empty;

    [Fact]
    public void Startas()
    {
        var händelser = Tillstånd.Initialt
        .Besluta(new Skapa(Spelmästare, EnFråga));

        händelser
        .Should()
        .Equal(new Skapad(NewGuid(),this.Spelmästare, this.EnFråga).ToArray());

        händelser
        .Aggregera()
        .Should()
        .Be(new Tillstånd([], true, false));
    }

    ~OmgångSkall() => NewGuid = () => Guid.NewGuid();
}
