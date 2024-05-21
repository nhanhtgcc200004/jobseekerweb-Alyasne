namespace finalyearproject.Models.ViewModel
{
    public class data_chart
    {
        public data_chart(int total_post, int total_candidate, int total_recrutier, int total_report, List<Chart_total_rating> chart_total_rating, List<chart_total_receving_candidate> chart_total_receving_candidate, List<chart_total_receiving_report> chart_total_receiving_report)
        {
            this.total_post = total_post;
            this.total_candidate = total_candidate;
            this.total_recrutier = total_recrutier;
            this.total_report = total_report;
            this.chart_total_rating = chart_total_rating;
            this.chart_total_receving_candidate = chart_total_receving_candidate;
            this.chart_total_receiving_report = chart_total_receiving_report;
        }

        public int total_post {get; set; }
        public int total_candidate {get; set; }
        public int total_recrutier {get; set; }
        public int total_report {get; set; }
        public List<Chart_total_rating> chart_total_rating {get; set; }
        public List<chart_total_receving_candidate> chart_total_receving_candidate {get; set; }
        public List<chart_total_receiving_report> chart_total_receiving_report {get; set; }

    }
}
