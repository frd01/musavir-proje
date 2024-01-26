namespace Tacmin.Service
{
    public class KdvHesaplama
    {
        Tacmin.Data.DbModel.GELEN_FATURALAR model;

        public KdvHesaplama(Tacmin.Data.DbModel.GELEN_FATURALAR model)
        {

            this.model = model;
        }

        public Tacmin.Data.DbModel.GELEN_FATURALAR GetFaturaModel()
        {
            if (this.Kdv_1())
            {
                this.model.KDV_1 = this.model.VERGI;
                this.model.TOPLAM_1 = this.model.TOPLAM;
            }
            if (this.Kdv_10())
            {
                this.model.KDV_10 = this.model.VERGI;
                this.model.KDV_10 = this.model.TOPLAM;
            }

            if (this.Kdv_20())
            {
                this.model.KDV_20 = this.model.VERGI;
                this.model.KDV_20 = this.model.TOPLAM;
            }

            return this.model;

        }

        private bool Kdv_1()
        {

            var hesaplanan = this.model.TOPLAM * 1.01M;

            var fark = hesaplanan - this.model.ODENECEK;

            if (fark > 0)
            {
                if (fark < 0.05M)
                {
                    return true;
                }
            }

            if (fark < 0)
            {
                if (fark >= -0.05M)
                {
                    return true;
                }
            }

            return false;
        }

        private bool Kdv_8()
        {

            var hesaplanan = this.model.TOPLAM * 1.08M;

            var fark = hesaplanan - this.model.ODENECEK;

            if (fark > 0)
            {
                if (fark < 0.05M)
                {
                    return true;
                }
            }

            if (fark < 0)
            {
                if (fark >= -0.05M)
                {
                    return true;
                }
            }

            return false;
        }

        private bool Kdv_10()
        {

            var hesaplanan = this.model.TOPLAM * 1.10M;

            var fark = hesaplanan - this.model.ODENECEK;

            if (fark > 0)
            {
                if (fark < 0.05M)
                {
                    return true;
                }
            }

            if (fark < 0)
            {
                if (fark >= -0.05M)
                {
                    return true;
                }
            }

            return false;
        }

        private bool Kdv_18()
        {

            var hesaplanan = this.model.TOPLAM * 1.18M;

            var fark = hesaplanan - this.model.ODENECEK;

            if (fark > 0)
            {
                if (fark < 0.05M)
                {
                    return true;
                }
            }

            if (fark < 0)
            {
                if (fark >= -0.05M)
                {
                    return true;
                }
            }

            return false;
        }

        private bool Kdv_20()
        {

            var hesaplanan = this.model.TOPLAM * 1.20M;

            var fark = hesaplanan - this.model.ODENECEK;

            if (fark > 0)
            {
                if (fark < 0.05M)
                {
                    return true;
                }
            }

            if (fark < 0)
            {
                if (fark >= -0.05M)
                {
                    return true;
                }
            }

            return false;
        }


    }
}
