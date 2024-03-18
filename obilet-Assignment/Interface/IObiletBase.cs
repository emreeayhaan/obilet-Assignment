using obilet_Assignment.Models;

namespace obilet_Assignment.Interface
{
    public interface IObiletBase
    {
        public Task<IndexViewModel> GetBusLocations(bool isReturnPage);
        public Task<IndexViewJourneyModel> GetJourney(IndexViewModel model);
        public Task SetJourneyDetailCache(IndexViewModel model);
        public Task GetBrowserInformation(string userAgent);
    }
}
