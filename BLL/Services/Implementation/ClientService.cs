using BLL.ApiModels.Request.Client;
using BLL.ApiModels.Response.Client;
using BLL.DomainModels;
using BLL.Services.Abstraction;
using DAL.Converters.Abstraction;
using DAL.PersistModels;
using DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using BLL.ApiModels.Response.Inner;

namespace BLL.Services.Implementation
{
    public class ClientService : IClientService
    {
        private readonly IAssociateRepository<AssociateDomain> associateRepository;
        private readonly IOrderRepository<OrderDomain> orderRepository;
        private readonly ICarRepository<CarDomain> carRepository;

        private readonly IPersistToDomainConverter<AssociatePersist, AssociateDomain> associateConverter;
        private readonly IPersistToDomainConverter<OrderPersist, OrderDomain> orderConverter;
        private readonly IPersistToDomainConverter<CarPersist, CarDomain> carConverter;

        public ClientService(IAssociateRepository<AssociateDomain> _associateRepository,
                            IOrderRepository<OrderDomain> _orderRepository,
                            ICarRepository<CarDomain> _carRepository,

                            IPersistToDomainConverter<AssociatePersist, AssociateDomain> _associateConverter,
                            IPersistToDomainConverter<OrderPersist, OrderDomain> _orderConverter,
                            IPersistToDomainConverter<CarPersist, CarDomain> _carConverter)
        {
            this.associateRepository = _associateRepository;
            this.orderRepository = _orderRepository;
            this.carRepository = _carRepository;

            this.associateConverter = _associateConverter;
            this.orderConverter = _orderConverter;
            this.carConverter = _carConverter;
        }
        public async Task<GetClientInfoResponseModel> GetClientInfo(GetClientInfoRequestModel request)
        {
            GetClientInfoResponseModel response = new GetClientInfoResponseModel();
            var client = associateRepository.Get(x => x.Id == request.AssociateId).FirstOrDefault();
            if (client == null)
            {
                response.IsSuccess = false;
                response.ResponseMessage = "Пользователь не найден";
                return response;
            }

            var orders = orderRepository.Get(x => x.AssociateId == client.Id).ToList();
            if (orders != null && orders.Count != 0)
            {
                response.Orders = orders.Select(x => orderConverter.ConvertToDomain(x)).ToList();
            }
            else
            {
                response.Orders = new List<OrderDomain>();
            }

            response.Client = associateConverter.ConvertToDomain(client);
            response.IsSuccess = true;
            response.ResponseMessage = $"Пользователь #{response.Client.Id}. Найдено {response.Orders.Count} заказов";
            return response;
        }

        public async Task<GetAutoModelsResponseModel> GetAutoModels(GetAutoModelsRequestModel request)
        {
            GetAutoModelsResponseModel response = new GetAutoModelsResponseModel();
            var cars = carRepository.Get().ToList();

            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                try
                {
                    var orderByParam = typeof(CarDomain).GetProperty(request.OrderBy);
                    if (orderByParam != null)
                    {
                        var res = cars.OrderBy(x => $"{(string)orderByParam.GetValue(x, null)} asc") ;
                        if (res != null) cars = res.ToList();
                    }
                }
                catch (Exception er)
                {
                    response.IsSuccess = false;
                    response.ResponseMessage = er.Message;
                    return response;
                }
            }
            if (!string.IsNullOrEmpty(request.Type))
            {
                cars = cars.Where(x => x.Type == request.Type).ToList();
            }
            try
            {
                cars = cars.Skip(request.From).Take(request.BatchSize).ToList();
            }
            catch (Exception er)
            {
                response.IsSuccess = false;
                response.ResponseMessage = er.Message;
                return response;
            }
            response.Cars = cars.Select(x => carConverter.ConvertToDomain(x)).ToList();
            response.IsSuccess = true;
            response.ResponseMessage = $"Найдено {response.Cars.Count} авто";
            return response;
        }

        public async Task<GetOrderStatisticResponseModel> GetOrderStatistic(GetOrderStatisticRequestModel request)
        {
            GetOrderStatisticResponseModel response = new GetOrderStatisticResponseModel();
            try
            {
                var stat = (from order in orderRepository.Get()
                            where order.OrderDate >= request.From && order.OrderDate <= request.To
                            group order by order.AssociateId).ToList();

                response.Statistics = new List<OrderStatisticModel>();
                var clients = associateRepository.Get(x => stat.Select(y => y.Key).Contains(x.Id)).ToList();
                foreach (IGrouping<int, OrderPersist> clientOrders in stat)
                {
                    var client = associateConverter.ConvertToDomain(clients.Where(x => x.Id == clientOrders.Key).FirstOrDefault());
                    var orders = clientOrders.ToList().Select(x => orderConverter.ConvertToDomain(x)).ToList();

                    response.Statistics.Add(new OrderStatisticModel() { Client = client, Orders = orders });
                }
                response.IsSuccess = true;
                response.ResponseMessage = "Статистика получена";
            }
            catch (Exception er)
            {
                response.IsSuccess = false;
                response.ResponseMessage = er.Message;
                return response;
            }
            return response;
        }

        public async Task<CreateOrderResponseModel> CreateOrder(CreateOrderRequestModel request)
        {
            CreateOrderResponseModel response = new CreateOrderResponseModel();
            if (request.AssociateId == -1)
            {
                response.IsSuccess = false;
                response.ResponseMessage = "Пользователь не найден";
                return response;
            }
            if (request.Order == null)
            {
                response.IsSuccess = false;
                response.ResponseMessage = "Заказ пуст";
                return response;
            }
            var car = carRepository.Get(x => x.Id == request.Order.CarId).FirstOrDefault();
            if (car == null)
            {
                response.IsSuccess = false;
                response.ResponseMessage = "Авто не найдено";
                return response;
            }
            
            request.Order.CarModel = car.Model;
            request.Order.AssociateId = request.AssociateId;

            request.Order.SystemNumber = Guid.NewGuid().ToString();
            request.Order.OrderDate = DateTime.Now;

            response.Order = orderConverter.ConvertToDomain(orderRepository.Create(request.Order));
            if (response.Order == null)
            {
                response.IsSuccess = false;
                response.ResponseMessage = "Не удалось создать заказ";
                return response;
            }
            response.IsSuccess = true;
            response.ResponseMessage = $"Заказ №{response.Order.SystemNumber} создан";
            return response;
        }

        public async Task<CreateCarResponseModel> CreateCar(CreateCarRequestModel request)
        {
            CreateCarResponseModel response = new CreateCarResponseModel();
            if (request.Car == null)
            {
                response.IsSuccess = false;
                response.ResponseMessage = "Авто не может быть пустым";
                return response;
            }
            response.Car = carConverter.ConvertToDomain(carRepository.Create(request.Car));
            response.IsSuccess = true;
            response.ResponseMessage = "Авто добавлено";
            return response;
        }
    }
}
