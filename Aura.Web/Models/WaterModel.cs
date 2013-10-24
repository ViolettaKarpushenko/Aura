namespace Aura.Web.Models
{
    public class WaterModel
    {
        public int RegionId { get; set; }

        public string RegionName { get; set; }

        public double RechnoiStok { get; set; }

        public double PodzemnyeVody { get; set; }

        public double ObemVody { get; set; }

        // Use.
        public double VodyIzato { get; set; }

        public double VodyIspolzovano { get; set; }

        public double HbPotreblenie { get; set; }

        public double PPotreblenie { get; set; }

        public double ShPotreblenie { get; set; }

        public double RhPotreblenie { get; set; }
    }
}