namespace kviss.games.Spel.MerEllerMindre.Omgång;

using FluentAssertions;
using Xunit;
using static SystemGuid;
using Frågor = ISet<Fråga>;
using Händelser = HashSet<IHändelse>;
using Ställning = Dictionary<string, ushort>;

public class OmgångSkall
{
    public readonly Frågor EnFråga = new HashSet<Fråga>() { new("musik", "flest", "miljoner", "Vilken grupp har sålt flest album, {alt1_namn} eller {alt2_namn}?", "{alt1_namn} har sålt flest album, {alt1_tal} {enhet}. Hur många färre {enhet} album har { alt2_namn } sålt?", "ABBA", 400, "Roxette", 75)};

    public readonly Spelare Spelmästare = new("Martin");

    public OmgångSkall() => NewGuid = () => Guid.Empty;

    [Fact]
    public void givet_initialt_tillstånd_när_skapa_så_händer_skapad() => 
        Tillstånd.Initialt
        .Besluta(new Skapa(Spelmästare, EnFråga))
        .Should()
        .Equal(new Skapad(NewGuid(), this.Spelmästare, this.EnFråga).TillHändelser());

    [Fact]
    public void givet_skapad_så_är_tillståndet_skapad() => 
        new Skapad(NewGuid(), this.Spelmästare, this.EnFråga).TillHändelser()
        .Aggregera()
        .Should()
        .BeEquivalentTo(new Tillstånd([], true, false));

    [Fact]
    public void givet_skapad_så_är_omgångs_id_tillgängligt() =>
        new Skapad(NewGuid(), this.Spelmästare, this.EnFråga).TillHändelser()
        .OmgångId()
        .Should()
        .Be(Guid.Empty);

    ~OmgångSkall() => NewGuid = () => Guid.NewGuid();
}
