using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Extensions;
using Tacmin.Model;

namespace Tacmin.Service.Maliye
{
    public class YeniBeyannameService
    {
        private static readonly string GIB_API_URL = "https://intvrg.gib.gov.tr/intvrg_server";
        private readonly string GIB_REQUEST_URL = $"{GIB_API_URL}/dispatch";
        private string BeyannameRequestData(string token, string arsiv, DateTime ilkTarih, DateTime sonTarih, string sayfaNo)
        {
            var data = "cmd=beyannameService_beyannameAra&" +
                      "callid=1fb75eb799465-19&" +
                      "token=" + token + "&" +
                      "jp=%7B%22arsivde%22%3A" + arsiv + "%2C%22" +
                      "sorguTipiN%22%3A0%2C%22" +
                      "vergiNo%22%3A%22%22%2C%22" +
                      "sorguTipiT%22%3A0%2C%22" +
                      "tcKimlikNo%22%3A%22%22%2C%22" +
                      "sorguTipiB%22%3A0%2C%22" +
                      "beyannameTanim%22%3A%22%22%2C%22" +
                      "sorguTipiP%22%3A0%2C%22" +
                      "donemBasAy%22%3A%2207%22%2C%22" +
                      "donemBasYil%22%3A%222018%22%2C%22" +
                      "donemBitAy%22%3A%2207%22%2C%22" +
                      "donemBitYil%22%3A%222018%22%2C%22" +
                      "sorguTipiV%22%3A0%2C%22" +
                      "vdKodu%22%3A%22%22%2C%22" +
                      "sorguTipiZ%22%3A1%2C%22" +
                      "tarihAraligi%22%3A%7B%22" +
                      "baslangicTarihi%22%3A%22" + ilkTarih.ToString("yyyyMMdd") + "%22%2C%22" +
                      "bitisTarihi%22%3A%22" + sonTarih.ToString("yyyyMMdd") + "%22%7D%2C%22" +
                      "sorguTipiD%22%3A1%2C%22" +
                      "durum%22%3A%7B%22" +
                      "radiob%22%3Afalse%2C%22" +
                      "radiob1%22%3Afalse%2C%22" +
                      "radiob2%22%3Atrue%2C%22" +
                      "radiob3%22%3Afalse%7D%2C%22" +
                      "pageNo%22%3A" + sayfaNo + "%7D";
            return data;
        }

        public int GibBeyannameSorgula(string gibToken, DateTime ilkTarih, DateTime sonTarih)
        {

            var beyannameIceriks = new List<BeyannameIcerik>();
            var rootBeyanname = new BeyannameResultModel();

            var arsiv = "false";
            if (ilkTarih.Year - DateTime.Now.Year > 1)
            {
                arsiv = "true";
            }

            var SayfaSayisi = 0;
            var toplamBeyannameAdedi = 0;

            var data = BeyannameRequestData(gibToken, arsiv, ilkTarih, sonTarih, "1");
            var response = GibRequestService.PostJsonRequest(GIB_REQUEST_URL, data);
            rootBeyanname = JsonConvert.DeserializeObject<BeyannameResultModel>(response);
            Console.WriteLine(rootBeyanname.Data);

            SayfaSayisi = (int)Math.Ceiling(Convert.ToDecimal(rootBeyanname.Data.Rowcount) / 25);
            toplamBeyannameAdedi = rootBeyanname.Data.Rowcount;

            return 0;


        }

        private BeyannameIcerik BeyannamePdfleriIndir(string token, string beyOid, string TahOid, string beyKodu)
        {
            var bulunanBilgiler = new BeyannameIcerik();

            try
            {

                /*while (bulunanBilgiler.islemDurum)
                {
                    bulunanBilgiler = _BeyannamePdfleriIndir(token, beyOid, TahOid, beyKodu);
                }*/
                bulunanBilgiler = _BeyannamePdfleriIndir(token, beyOid, TahOid, beyKodu);

            }
            catch
            {

                bulunanBilgiler = _BeyannamePdfleriIndir(token, beyOid, TahOid, beyKodu);
            }

            return bulunanBilgiler;

        }

        private BeyannameIcerik _BeyannamePdfleriIndir(string token, string beyOid, string TahOid, string beyKodu)
        {
            var bulunanBilgiler = new BeyannameIcerik();
            bulunanBilgiler.islemDurum = true;

            try
            {
                var tahakkukUrl = $"{GIB_API_URL}/goruntuleme?cmd=IMAJ&subcmd=TAHAKKUKGORUNTULE&tahakkukOid=" + TahOid +
                            "&beyannameOid=" + beyOid + "&USERID=&inline=true&goruntuTip=1&token=" + token + "";

                var tahakkukFile = new byte[] { };
                var sayac = 0;

                while (tahakkukFile.Length < 50 && sayac < 10)
                {
                    tahakkukFile = GetFile(tahakkukUrl);
                    sayac += 1;
                    Task.Delay(3000);
                }

                bulunanBilgiler.tahfile = tahakkukFile;

                if (beyKodu != "OTV4")
                {
                    var beyannameUrl = $"{GIB_API_URL}/goruntuleme?cmd=IMAJ&subcmd=BEYANNAMEGORUNTULE&beyannameOid=" + beyOid +
                                "&USERID=&inline=true&goruntuTip=1&token=" + token + "";

                    var beyannameFile = new byte[] { };
                    sayac = 0;

                    while (beyannameFile.Length < 50 && sayac < 10)
                    {
                        beyannameFile = GetFile(beyannameUrl);
                        sayac += 1;
                        Task.Delay(3000);
                    }

                    bulunanBilgiler.beyfile = beyannameFile;

                    var beyannameOku = new PdfReader(bulunanBilgiler.beyfile);
                    var beyanString = new StringBuilder();

                    for (var i = 1; i <= beyannameOku.NumberOfPages; i++)
                    {
                        beyanString.Append(PdfTextExtractor.GetTextFromPage(beyannameOku, i));
                    }

                    var beyanTxt = beyanString.ToString().Replace("\n", "");

                    bulunanBilgiler.YuklenmeTarihi = Convert.ToDateTime(beyanTxt.SafeSubstring(beyanTxt.IndexOf("Onay Zamanı : ") + 14, 10));
                }
                else
                {
                    bulunanBilgiler.beyfile = bulunanBilgiler.tahfile;
                    bulunanBilgiler.YuklenmeTarihi = DateTime.Now;
                }

                if (beyKodu != "FORMBS" && beyKodu != "FORMBA")
                {
                    var PdfReader = new PdfReader(bulunanBilgiler.tahfile);
                    var text = new StringBuilder();

                    for (var i = 1; i <= PdfReader.NumberOfPages; i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(PdfReader, i));
                    }

                    var tahakkukTxt = text.ToString().Replace("\n", "");

                    if (beyKodu == "OTV4")
                    {
                        bulunanBilgiler.YuklenmeTarihi = Convert.ToDateTime(tahakkukTxt.SafeSubstring(tahakkukTxt.IndexOf("kabul tarihi Tarihi") + 19, 10));
                    }

                    if (tahakkukTxt.Contains("İhtirazi Kayıt"))
                    {
                        bulunanBilgiler.Tutar = (tahakkukTxt.SafeSubstring(tahakkukTxt.IndexOf("TOPLAM") + 7, tahakkukTxt.IndexOf("İhtirazi Kayıt") - (tahakkukTxt.IndexOf("TOPLAM") + 7))).ToDecimal();
                    }
                    else if (tahakkukTxt.Contains("TRZM"))
                    {
                        bulunanBilgiler.Tutar = 0;
                        var tutarBilgiArr = tahakkukTxt.Split("TOPLAM ");
                        if (tutarBilgiArr.Length > 1)
                        {
                            var toplamAlani = tutarBilgiArr[1];
                            var tutar = toplamAlani.SafeSubstring(0, toplamAlani.IndexOf(',') + 3);
                            bulunanBilgiler.Tutar = tutar.ToDecimal();
                        }
                    }
                    else
                    {
                        bulunanBilgiler.Tutar = (tahakkukTxt.SafeSubstring(tahakkukTxt.IndexOf("TOPLAM") + 7, tahakkukTxt.IndexOf("İşlem Türü") - (tahakkukTxt.IndexOf("TOPLAM") + 7))).ToDecimal();
                    }

                    bulunanBilgiler.fisNo = tahakkukTxt.SafeSubstring(tahakkukTxt.IndexOf("BAŞKANLIĞI") + 10, 20);
                    if (tahakkukTxt.IndexOf("BAKANLIĞI") <= 0)
                    {
                        bulunanBilgiler.fisNo = tahakkukTxt.SafeSubstring(tahakkukTxt.IndexOf("DEFTERDARLIĞI") + 13, 20);
                    }
                    bulunanBilgiler.Vade = Convert.ToDateTime(tahakkukTxt.Substring(tahakkukTxt.IndexOf("TOPLAM") - 10, 10));
                }

                bulunanBilgiler.islemDurum = false;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                var beyannameFile = new byte[] { };
                var sayac = 0;
                var beyannameUrl = $"{GIB_API_URL}/goruntuleme?cmd=IMAJ&subcmd=BEYANNAMEGORUNTULE&beyannameOid=" + beyOid +
                                "&USERID=&inline=true&goruntuTip=1&token=" + token + "";

                while (beyannameFile.Length < 50 && sayac < 10)
                {
                    beyannameFile = GetFile(beyannameUrl);
                    sayac += 1;
                    Task.Delay(3000);
                }

                bulunanBilgiler.beyfile = beyannameFile;

                var tahakkukUrl = $"{GIB_API_URL}/goruntuleme?cmd=IMAJ&subcmd=TAHAKKUKGORUNTULE&tahakkukOid=" + TahOid +
                           "&beyannameOid=" + beyOid + "&USERID=&inline=true&goruntuTip=1&token=" + token + "";

                var tahakkukFile = new byte[] { };
                sayac = 0;

                while (tahakkukFile.Length < 50 && sayac < 10)
                {
                    tahakkukFile = GetFile(tahakkukUrl);
                    sayac += 1;
                    Task.Delay(3000);
                }

                bulunanBilgiler.tahfile = tahakkukFile;
            }



            return bulunanBilgiler;
        }

        private byte[] GetFile(string url)
        {
            var memStream = new MemoryStream();
            var stream = GibRequestService.GetResponseStream(url);
            stream.CopyTo(memStream, 16384);
            return memStream.ToArray();
        }
    }
}
