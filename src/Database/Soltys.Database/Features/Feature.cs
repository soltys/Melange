namespace Soltys.Database
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
