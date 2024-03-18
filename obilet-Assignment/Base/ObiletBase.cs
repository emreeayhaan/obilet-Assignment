using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using obilet_Assignment.Interface;
using obilet_Assignment.Models;
using ObiletBusiness.Interfaces;
using ObiletData.Dto;
using System.Globalization;

namespace obilet_Assignment.Base
{
    public class ObiletBase : IObiletBase
    {
        private readonly IRedisCacheManager _cacheBusiness;
        private readonly IBrowserInteractionService _iSessionService;
        private readonly ITravelService _travelService;
        private readonly IMapper _mapper;
        public ObiletBase(IRedisCacheManager cacheBusiness, IBrowserInteractionService iSessionService, ITravelService travelService, IMapper mapper)
        {
            _cacheBusiness = cacheBusiness;
            _iSessionService = iSessionService;
            _travelService = travelService;
            _mapper = mapper;
        }
        public async Task<IndexViewModel> GetBusLocations(bool isReturnPage)
        {
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.Origin = await _cacheBusiness.Get<List<SelectListItem>>("origion");
            viewModel.Destination = await _cacheBusiness.Get<List<SelectListItem>>("destination");

            if (viewModel.Origin == null || viewModel.Destination == null)
            {
                await GetLocations(viewModel);
            }

            return await UpdateIndexViewModel(viewModel, isReturnPage);
        }
        public async Task<IndexViewJourneyModel> GetJourney(IndexViewModel model)
        {
            SessionDataDto deviceSession = await GetSession() ?? new SessionDataDto();
            if (deviceSession != null && model.SelectedDestination.HasValue && model.SelectedOrigin.HasValue)
            {
                var response = await _travelService.GetBusJourneys(new JourneysRequestDto
                {
                    DeviceSession = deviceSession,
                    Data = new Data { DepartureDate = model.DepatureDate, DestinationId = model.SelectedDestination.Value, OriginId = model.SelectedOrigin.Value }
                });

                IndexViewJourneyModel viewModel = _mapper.Map<IndexViewJourneyModel>(response);

                await SetJourneyViewHeader(viewModel, model);

                return viewModel;
            }

            return new IndexViewJourneyModel();
        }

        public async Task SetJourneyDetailCache(IndexViewModel model)
        {
            await _cacheBusiness.Set("selectedOrigion", model.SelectedOrigin);
            await _cacheBusiness.Set("selectedDestination", model.SelectedDestination);
            await _cacheBusiness.Set("selectedDate", model.DepatureDate);
        }
        private async Task<IndexViewModel> UpdateIndexViewModel(IndexViewModel viewModel, bool isReturnPage)
        {
            if (isReturnPage)
            {
                viewModel.SelectedOrigin = await _cacheBusiness.Get<int>("selectedOrigion");
                viewModel.SelectedDestination = await _cacheBusiness.Get<int>("selectedDestination");
                viewModel.DepatureDate = await _cacheBusiness.Get<DateTime>("selectedDate");
            }
            else
            {
                viewModel.DepatureDate = DateTime.Now.AddDays(1);
            }

            return viewModel;
        }
        private async Task<SessionDataDto> GetSession()
        {
            SessionDataDto result = await _cacheBusiness.Get<SessionDataDto>("session");

            if (result != null) return result;

            SessionResponseDto session = await _iSessionService.GetSesion();

            if (session.Status == "Success")
            {
                result = new SessionDataDto();

                result.DeviceId = session.Data.DeviceId;
                result.SessionId = session.Data.SessionId;
            }

            return result;
        }
        private async Task GetLocations(IndexViewModel viewModel)
        {
            SessionDataDto deviceSession = await GetSession() ?? new SessionDataDto();
            if (deviceSession != null)
            {
                LocationsResponse response = await _travelService.GetBusLocations(new LocationRequestDto
                {
                    DeviceSession = deviceSession
                });

                List<SelectListItem> origion = await GetLocaitonOrigion(response.Data);
                List<SelectListItem> destination = await GetLocaitonDestination(response.Data);

                await _cacheBusiness.Set("origion", origion);
                await _cacheBusiness.Set("destination", destination);

                viewModel.Origin = origion;
                viewModel.Destination = destination;
            }
        }
        private async Task SetJourneyViewHeader(IndexViewJourneyModel viewModel, IndexViewModel model)
        {
            List<SelectListItem> originLocation = await _cacheBusiness.Get<List<SelectListItem>>("origion");
            if (originLocation != null)
            {
                viewModel.OriginLocation = originLocation.Find(item => item.Value == model.SelectedOrigin.ToString())?.Text;
            }

            List<SelectListItem> destinationLocation = await _cacheBusiness.Get<List<SelectListItem>>("destination");
            if (destinationLocation != null)
            {
                viewModel.DestinationLocation = destinationLocation.Find(item => item.Value == model.SelectedDestination.ToString())?.Text;
            }

            viewModel.SelectedDate = model.DepatureDate.ToString("d MMMM dddd", CultureInfo.CreateSpecificCulture("tr-TR"));
        }
        private async Task<List<SelectListItem>> GetLocaitonOrigion(List<LocationData> data)
        {
            if (data != null)
            {
                return data.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString(), Selected = s.Name.Contains("Avrupa") }).ToList();
            }
            else
            {
                return null;
            }
        }
        private async Task<List<SelectListItem>> GetLocaitonDestination(List<LocationData> data)
        {
            if (data != null)
            {

                return data.Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString(), Selected = s.Name.Equals("Ankara") }).ToList();
            }
            else
            {
                return null;
            }
        }
        public async Task GetBrowserInformation(string userAgent)
        {
            try
            {
                BrowserDataDto browserData = await _cacheBusiness.Get<BrowserDataDto>("browser");

                if (browserData != null) return;

                if (string.IsNullOrEmpty(userAgent)) return;

                string[] userAgentInfo = userAgent.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (userAgentInfo.Length >= 3)
                {
                    browserData = new BrowserDataDto();

                    browserData.Name = userAgentInfo[2];

                    if (userAgentInfo.Length >= 4)
                    {
                        browserData.Version = userAgentInfo[3];
                    }
                }

                await _cacheBusiness.Set("browser", browserData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
