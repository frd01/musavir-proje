namespace Tacmin.Core.Infrastructure
{
    public interface ISmsService
    {
        string Gonder(string number, string message);

        string BaslikKontrol(string header);

        string KrediKontrol();
    }
}