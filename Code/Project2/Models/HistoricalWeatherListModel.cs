using System.Collections.Generic;

namespace BGModern.Models
{
    public class HistoricalWeatherListModel : MasterModel
    {
        public List<HistoricalWeatherModel> HistoricalWeatherList { get; set; }

        public HistoricalWeatherListModel()
        {
            HistoricalWeatherList = new List<HistoricalWeatherModel>();
        }
    }
}