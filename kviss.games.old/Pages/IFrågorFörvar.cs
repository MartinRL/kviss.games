using kviss.games.Spel.MerEllerMindre;

namespace kviss.games.Spel;

internal interface IFrågorFörvar
{
    ISet<Fråga> HämtaTioSlumpmässigt();
}
