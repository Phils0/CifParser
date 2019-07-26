namespace CifParser
{
    public interface IParserFactory
    {
        IParser CreateParser();
        IParser CreateParser(int ignoreLines);
    }
}