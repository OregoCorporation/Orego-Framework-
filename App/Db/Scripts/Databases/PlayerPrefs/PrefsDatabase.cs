namespace OregoFramework.App
{
    public interface IPrefsDatabase : IDatabase
    {
    }

    public abstract class PrefsDatabase : Database<IPrefsDao>, IPrefsDatabase
    {
    }
}