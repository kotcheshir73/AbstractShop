using AbstractShopService;
using AbstractShopService.BindingModels;
using AbstractShopService.Interfaces;
using AbstractShopService.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AbstractShopServer
{
    public class TSPServer
    {
        private readonly IClientService _serverClient;

        private readonly IComponentService _serverComponent;

        private readonly IImplementerService _serverImplementer;

        private readonly IProductService _serverProduct;

        private readonly IStockService _serverStock;

        private readonly IMainService _serverMain;

        private readonly IReportService _serverReport;

        public TSPServer(IClientService serverClient, IComponentService serverComponent, IImplementerService serverImplementer,
            IProductService serverProduct, IStockService serverStock, IMainService serverMain, IReportService serverReport)
        {
            _serverClient = serverClient;
            _serverComponent = serverComponent;
            _serverImplementer = serverImplementer;
            _serverProduct = serverProduct;
            _serverStock = serverStock;
            _serverMain = serverMain;
            _serverReport = serverReport;
        }

        public void RunServer(string ipAddress, int port)
        {
            TcpListener tcpListener = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse(ipAddress);
                tcpListener = new TcpListener(localAddr, port);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен");

                while (true)
                {
                    Console.WriteLine("Ожидание подключений... ");

                    // получаем входящее подключение
                    TcpClient client = tcpListener.AcceptTcpClient();
                    Console.WriteLine("Подключился клиент");

                    // получаем сетевой поток для чтения и записи
                    NetworkStream stream = client.GetStream();
                    Console.WriteLine("Получен поток");

                    var request = GetMessage(stream);

                    GetResponse(request, stream);

                    // закрываем подключение
                    client.Close();
                    Console.WriteLine("Отключился клиент");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            finally
            {
                if (tcpListener != null)
                {
                    tcpListener.Stop();
                }
            }
        }

        public RequestModel GetMessage(NetworkStream stream)
        {
            Byte[] bytes = new Byte[1024];
            string bytesAsString = "";

            do
            {
                stream.Read(bytes, 0, bytes.Length);
                bytesAsString += Encoding.UTF8.GetString(bytes);
            }
            while (stream.DataAvailable);
            Console.WriteLine("Получен запрос");

            return JsonConvert.DeserializeObject<RequestModel>(bytesAsString);
        }

        private void GetResponse(RequestModel model, NetworkStream stream)
        {
            switch (model.InterfaceName)
            {
                case InterfacesName.IClientService:
                    GetResponseForClientService(model, stream);
                    break;
                case InterfacesName.IComponentService:
                    GetResponseForComponentService(model, stream);
                    break;
                case InterfacesName.IImplementerService:
                    GetResponseForImplementerService(model, stream);
                    break;
                case InterfacesName.IProductService:
                    GetResponseForProductService(model, stream);
                    break;
                case InterfacesName.IStockService:
                    GetResponseForStockService(model, stream);
                    break;
                case InterfacesName.IMainService:
                    GetResponseForMainService(model, stream);
                    break;
                case InterfacesName.IReportService:
                    GetResponseForReportService(model, stream);
                    break;
                default:
                    throw new Exception("Неизвестный интерфейс");
            }
        }

        private void GetResponseForClientService(RequestModel request, NetworkStream stream)
        {
            DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<ClientViewModel>));
            ResponseModel<ClientViewModel> response = new ResponseModel<ClientViewModel> { Success = true };
            try
            {
                switch (request.MethodName)
                {
                    case MethodsName.GetList:
                        response.ResponseList = _serverClient.GetList();
                        break;
                    case MethodsName.GetElement:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        response.Response = _serverClient.GetElement(Convert.ToInt32(request.Request));
                        break;
                    case MethodsName.AddElement:
                        {
                            var model = JsonConvert.DeserializeObject<ClientBindingModel>(request.Request.ToString());
                            _serverClient.AddElement(model);
                        }
                        break;
                    case MethodsName.UpdElement:
                        {
                            var model = JsonConvert.DeserializeObject<ClientBindingModel>(request.Request.ToString());
                            _serverClient.UpdElement(model);
                        }
                        break;
                    case MethodsName.DelElement:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        _serverClient.DelElement(Convert.ToInt32(request.Request));
                        break;
                    default:
                        throw new Exception("Неизвестный метод");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            finally
            {
                jsonSerResponse.WriteObject(stream, response);
                Console.WriteLine("Отправлен ответ {0}", response.Success);
            }
        }

        private void GetResponseForComponentService(RequestModel request, NetworkStream stream)
        {
            DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<ComponentViewModel>));
            ResponseModel<ComponentViewModel> response = new ResponseModel<ComponentViewModel> { Success = true };
            try
            {
                switch (request.MethodName)
                {
                    case MethodsName.GetList:
                        response.ResponseList = _serverComponent.GetList();
                        break;
                    case MethodsName.GetElement:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        response.Response = _serverComponent.GetElement(Convert.ToInt32(request.Request));
                        break;
                    case MethodsName.AddElement:
                        {
                            var model = JsonConvert.DeserializeObject<ComponentBindingModel>(request.Request.ToString());
                            _serverComponent.AddElement(model);
                        }
                        break;
                    case MethodsName.UpdElement:
                        {
                            var model = JsonConvert.DeserializeObject<ComponentBindingModel>(request.Request.ToString());
                            _serverComponent.UpdElement(model);
                        }
                        break;
                    case MethodsName.DelElement:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        _serverComponent.DelElement(Convert.ToInt32(request.Request));
                        break;
                    default:
                        throw new Exception("Неизвестный метод");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            finally
            {
                jsonSerResponse.WriteObject(stream, response);
                Console.WriteLine("Отправлен ответ {0}", response.Success);
            }
        }

        private void GetResponseForImplementerService(RequestModel request, NetworkStream stream)
        {
            DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<ImplementerViewModel>));
            ResponseModel<ImplementerViewModel> response = new ResponseModel<ImplementerViewModel> { Success = true };
            try
            {
                switch (request.MethodName)
                {
                    case MethodsName.GetList:
                        response.ResponseList = _serverImplementer.GetList();
                        break;
                    case MethodsName.GetElement:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        response.Response = _serverImplementer.GetElement(Convert.ToInt32(request.Request));
                        break;
                    case MethodsName.AddElement:
                        {
                            var model = JsonConvert.DeserializeObject<ImplementerBindingModel>(request.Request.ToString());
                            _serverImplementer.AddElement(model);
                        }
                        break;
                    case MethodsName.UpdElement:
                        {
                            var model = JsonConvert.DeserializeObject<ImplementerBindingModel>(request.Request.ToString());
                            _serverImplementer.UpdElement(model);
                        }
                        break;
                    case MethodsName.DelElement:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        _serverImplementer.DelElement(Convert.ToInt32(request.Request));
                        break;
                    default:
                        throw new Exception("Неизвестный метод");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            finally
            {
                jsonSerResponse.WriteObject(stream, response);
                Console.WriteLine("Отправлен ответ {0}", response.Success);
            }
        }

        private void GetResponseForProductService(RequestModel request, NetworkStream stream)
        {
            DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<ProductViewModel>));
            ResponseModel<ProductViewModel> response = new ResponseModel<ProductViewModel> { Success = true };
            try
            {
                switch (request.MethodName)
                {
                    case MethodsName.GetList:
                        response.ResponseList = _serverProduct.GetList();
                        break;
                    case MethodsName.GetElement:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        response.Response = _serverProduct.GetElement(Convert.ToInt32(request.Request));
                        break;
                    case MethodsName.AddElement:
                        {
                            var model = JsonConvert.DeserializeObject<ProductBindingModel>(request.Request.ToString());
                            _serverProduct.AddElement(model);
                        }
                        break;
                    case MethodsName.UpdElement:
                        {
                            var model = JsonConvert.DeserializeObject<ProductBindingModel>(request.Request.ToString());
                            _serverProduct.UpdElement(model);
                        }
                        break;
                    case MethodsName.DelElement:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        _serverProduct.DelElement(Convert.ToInt32(request.Request));
                        break;
                    default:
                        throw new Exception("Неизвестный метод");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            finally
            {
                jsonSerResponse.WriteObject(stream, response);
                Console.WriteLine("Отправлен ответ {0}", response.Success);
            }
        }

        private void GetResponseForStockService(RequestModel request, NetworkStream stream)
        {
            DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<StockViewModel>));
            ResponseModel<StockViewModel> response = new ResponseModel<StockViewModel> { Success = true };
            try
            {
                switch (request.MethodName)
                {
                    case MethodsName.GetList:
                        response.ResponseList = _serverStock.GetList();
                        break;
                    case MethodsName.GetElement:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        response.Response = _serverStock.GetElement(Convert.ToInt32(request.Request));
                        break;
                    case MethodsName.AddElement:
                        {
                            var model = JsonConvert.DeserializeObject<StockBindingModel>(request.Request.ToString());
                            _serverStock.AddElement(model);
                        }
                        break;
                    case MethodsName.UpdElement:
                        {
                            var model = JsonConvert.DeserializeObject<StockBindingModel>(request.Request.ToString());
                            _serverStock.UpdElement(model);
                        }
                        break;
                    case MethodsName.DelElement:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        _serverStock.DelElement(Convert.ToInt32(request.Request));
                        break;
                    default:
                        throw new Exception("Неизвестный метод");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            finally
            {
                jsonSerResponse.WriteObject(stream, response);
                Console.WriteLine("Отправлен ответ {0}", response.Success);
            }
        }

        private void GetResponseForMainService(RequestModel request, NetworkStream stream)
        {
            DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<OrderViewModel>));
            ResponseModel<OrderViewModel> response = new ResponseModel<OrderViewModel> { Success = true };
            try
            {
                switch (request.MethodName)
                {
                    case MethodsName.GetList:
                        response.ResponseList = _serverMain.GetList();
                        break;
                    case MethodsName.CreateOrder:
                        {
                            var model = JsonConvert.DeserializeObject<OrderBindingModel>(request.Request.ToString());
                            _serverMain.CreateOrder(model);
                        }
                        break;
                    case MethodsName.TakeOrderInWork:
                        {
                            var model = JsonConvert.DeserializeObject<OrderBindingModel>(request.Request.ToString());
                            _serverMain.TakeOrderInWork(model);
                        }
                        break;
                    case MethodsName.FinishOrder:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        _serverMain.FinishOrder(Convert.ToInt32(request.Request));
                        break;
                    case MethodsName.PayOrder:
                        if (!(request.Request is Int64))
                        {
                            throw new Exception("Неверный тип данных");
                        }
                        _serverMain.PayOrder(Convert.ToInt32(request.Request));
                        break;
                    case MethodsName.PutComponentOnStock:
                        {
                            var model = JsonConvert.DeserializeObject<StockComponentBindingModel>(request.Request.ToString());
                            _serverMain.PutComponentOnStock(model);
                        }
                        break;
                    default:
                        throw new Exception("Неизвестный метод");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            finally
            {
                jsonSerResponse.WriteObject(stream, response);
                Console.WriteLine("Отправлен ответ {0}", response.Success);
            }
        }

        private void GetResponseForReportService(RequestModel request, NetworkStream stream)
        {
            ResponseModel<OrderViewModel> response = new ResponseModel<OrderViewModel> { Success = true };
            try
            {
                switch (request.MethodName)
                {
                    case MethodsName.GetClientOrders:
                        {
                            DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<ClientOrdersModel>));
                            ResponseModel<ClientOrdersModel> responseLocal = new ResponseModel<ClientOrdersModel> { Success = true };
                            try
                            {
                                var model = JsonConvert.DeserializeObject<ReportBindingModel>(request.Request.ToString());
                                responseLocal.ResponseList = _serverReport.GetClientOrders(model);
                            }
                            catch (Exception ex)
                            {
                                responseLocal.Success = false;
                                responseLocal.ErrorMessage = ex.Message;
                            }
                            finally
                            {
                                jsonSerResponse.WriteObject(stream, responseLocal);
                                Console.WriteLine("Отправлен ответ {0}", responseLocal.Success);
                            }
                        }
                        return;
                    case MethodsName.GetStocksLoad:
                        {
                            DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<StocksLoadViewModel>));
                            ResponseModel<StocksLoadViewModel> responseLocal = new ResponseModel<StocksLoadViewModel> { Success = true };
                            try
                            {
                                responseLocal.ResponseList = _serverReport.GetStocksLoad();
                            }
                            catch (Exception ex)
                            {
                                responseLocal.Success = false;
                                responseLocal.ErrorMessage = ex.Message;
                            }
                            finally
                            {
                                jsonSerResponse.WriteObject(stream, responseLocal);
                                Console.WriteLine("Отправлен ответ {0}", responseLocal.Success);
                            }
                        }
                        return;
                    case MethodsName.SaveClientOrders:
                        {
                            DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<OrderViewModel>));
                            ResponseModel<OrderViewModel> responseLocal = new ResponseModel<OrderViewModel> { Success = true };
                            try
                            {
                                var model = JsonConvert.DeserializeObject<ReportBindingModel>(request.Request.ToString());
                                _serverReport.SaveClientOrders(model);
                            }
                            catch (Exception ex)
                            {
                                responseLocal.Success = false;
                                responseLocal.ErrorMessage = ex.Message;
                            }
                            finally
                            {
                                jsonSerResponse.WriteObject(stream, responseLocal);
                                Console.WriteLine("Отправлен ответ {0}", responseLocal.Success);
                            }
                        }
                        return;
                    case MethodsName.SaveProductPrice:
                        {
                            DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<OrderViewModel>));
                            ResponseModel<OrderViewModel> responseLocal = new ResponseModel<OrderViewModel> { Success = true };
                            try
                            {
                                var model = JsonConvert.DeserializeObject<ReportBindingModel>(request.Request.ToString());
                                _serverReport.SaveProductPrice(model);
                            }
                            catch (Exception ex)
                            {
                                responseLocal.Success = false;
                                responseLocal.ErrorMessage = ex.Message;
                            }
                            finally
                            {
                                jsonSerResponse.WriteObject(stream, responseLocal);
                                Console.WriteLine("Отправлен ответ {0}", responseLocal.Success);
                            }
                        }
                        return;
                    case MethodsName.SaveStocksLoad:
                        {
                            DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<OrderViewModel>));
                            ResponseModel<OrderViewModel> responseLocal = new ResponseModel<OrderViewModel> { Success = true };
                            try
                            {
                                var model = JsonConvert.DeserializeObject<ReportBindingModel>(request.Request.ToString());
                                _serverReport.SaveStocksLoad(model);
                            }
                            catch (Exception ex)
                            {
                                responseLocal.Success = false;
                                responseLocal.ErrorMessage = ex.Message;
                            }
                            finally
                            {
                                jsonSerResponse.WriteObject(stream, responseLocal);
                                Console.WriteLine("Отправлен ответ {0}", responseLocal.Success);
                            }
                        }
                        return;
                    default:
                        throw new Exception("Неизвестный метод");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                DataContractJsonSerializer jsonSerResponse = new DataContractJsonSerializer(typeof(ResponseModel<OrderViewModel>));
                jsonSerResponse.WriteObject(stream, response);
                Console.WriteLine("Отправлен ответ {0}", response.Success);
            }
        }
    }
}
