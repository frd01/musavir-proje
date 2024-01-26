namespace Tacmin.Core.Model
{
    public class SistemYetkiModel
    {
        public string NAME { get; set; }

        public string DESCRIPTION { get; set; }

        public SistemYetkiModel Name(string name)
        {
            NAME = name;
            return this;
        }

        public SistemYetkiModel Desc(string desc)
        {
            DESCRIPTION = desc;
            return this;
        }
    }
}
