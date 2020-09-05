namespace SoltysDb
{
    internal abstract class Feature
    {
        protected DatabaseData DatabaseData;

        protected Feature(DatabaseData data)
        {
            this.DatabaseData = data;
        }
    }
}
