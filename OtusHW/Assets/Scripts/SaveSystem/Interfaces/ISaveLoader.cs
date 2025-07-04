namespace SaveSystem
{
    public interface ISaveLoader
    {
        void Save();
        void Load();
    }
    
    public interface ISaveService: ISaveLoader {}
}