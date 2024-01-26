using Kendo.Mvc.UI.Fluent;

namespace Tacmin.Core.ViewExtensions
{
    public class TyFilterBuilder<T> : FilterBuilder<T> where T : class
    {
        public TyFilterBuilder(TyFilter<T> component) : base(component)
        {
            this.Name("masterfilter")
                .DataSource("masterds")
                .ApplyButton()
                .ExpressionPreview(false)
                .Messages(msg =>
                {
                    msg.AddExpression("Kriter Ekle");
                    msg.AddGroup("Grup Ekle");
                    msg.And("Ve");
                    msg.Apply("Uygula");
                    msg.Close("Kaldır");
                    msg.Or("Veya");
                    msg.Operators("Operatörler");
                    msg.Fields("Alanlar");
                })
                .Operators(opt =>
                {
                    opt.String(s => s.Contains("İçeren").Eq("Eşit").Isnullorempty("Boş Olanlar").Isnotnullorempty("Boş Olmayanlar").Startswith("İle Başlayan"));
                    opt.Date(d => d.Eq("=").Gt(">").Gte(">=").Lt("<").Lte("<="));
                    opt.Boolean(b => b.Eq("Eşit").Neq("Eşit Değil"));
                });
        }
    }
}