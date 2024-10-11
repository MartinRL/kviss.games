﻿namespace kviss.games.Spel.MerEllerMindre.Omgång;

using FluentAssertions;
using Xunit;
using static SystemGuid;
using Frågor = ISet<Fråga>;

public class OmgångSkall
{
    public readonly Frågor EnFråga = new HashSet<Fråga>() { new("musik", "flest", "miljoner", "Vilken grupp har sålt flest album, {alt1_namn} eller {alt2_namn}?", "{alt1_namn} har sålt flest album, {alt1_tal} {enhet}. Hur många färre {enhet} album har { alt2_namn } sålt?", "ABBA", 400, "Roxette", 75)};

    public readonly Spelare Spelmästare = new("Martin");

    public OmgångSkall() => NewGuid = () => Guid.Empty;

    [Fact]
    public void Startas()
    {
        Beslutare
        .Besluta(new Skapa(NewGuid(), Spelmästare, EnFråga))
        .Should()
        .Equal(new Skapad(new Skapa(NewGuid(), Spelmästare, EnFråga).Id, Spelmästare, EnFråga).ToArray());

        Beslutare
        .Tillstånd
        .Should()
        .Be(new Tillstånd(new Dictionary<string, ushort>(), true, false));
    }

    ~OmgångSkall() => NewGuid = () => Guid.NewGuid();
}
