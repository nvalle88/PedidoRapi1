using MvcPedidos.Entity.DTO;
using MvcPedidos.Services.Interface;
using MvcPrestadores.Entity.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utilidades;
using Utilidades.Extensores;

namespace MvcPedidos.Services
{
    public class ServiceZoho : IServiceZoho
    {
        private string GetIdLog()
        {

            try
            {
                return Guid.NewGuid().ToString();
            }
            catch (Exception ex)
            {
                LoggerBase.WriteLog("GetIdLog", $"{ex.Serializar()}", TypeError.Error);
                return DateTime.Now.Ticks.ToString();
            }
        }

        private async Task<T> InterpretarRespuesta<T>(HttpResponseMessage httpResponse, string idLog)
        {
            try
            {
                var respuesta = await httpResponse.Content.ReadAsAsync<T>();

                switch (httpResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        LoggerBase.WriteLog(GetType().ToString(),
                            $"{idLog} - RespuestaServicio: ({respuesta.Serializar()})",
                            TypeError.Trace);
                        return respuesta;

                    case HttpStatusCode.BadRequest:
                        LoggerBase.WriteLog(GetType().ToString(),
                            $"{idLog} - RespuestaServicio: ({respuesta.Serializar()})",
                            TypeError.Error);
                        return respuesta;

                    case HttpStatusCode.InternalServerError:
                        LoggerBase.WriteLog(GetType().ToString(),
                            $"{idLog} - Exception: ({respuesta.Serializar()})",
                            TypeError.Error);
                        throw new Exception("Error interno del servidor");

                    default:
                        LoggerBase.WriteLog(GetType().ToString(),
                            $"{idLog} - Exception: ({respuesta.Serializar()})",
                            TypeError.Error);
                        throw new Exception("No se puede obtener respuesta del servicio");
                }
            }
            catch (Exception e)
            {
                LoggerBase.WriteLog(GetType().ToString(),
                    $"{idLog} - Exception: ({e?.Message})",
                    TypeError.Error);
                throw new Exception("Error interno del servidor");
            }
        }

        private string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(value)}")
                .ToArray();
            return "?" + string.Join("&", array);
        }

        public async Task<List<ReturnBizTypes>> GetBizTypes(int tryCallService)
        {
            var idLog = GetIdLog();
            try
            {
                using (var client = new HttpClient())
                {
                    var request = $"{LoggerBase.UrlBase}{LoggerBase.ApiGetBizTypes}";
                    LoggerBase.WriteLog($"{idLog} - GetBizTypes - tryCallService: {tryCallService}", $"{request}", TypeError.Trace);
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    return await InterpretarRespuesta<List<ReturnBizTypes>>(
                        await client.GetAsync(request), idLog);
                }
            }
            catch (Exception ex)
            {
                LoggerBase.WriteLog($"{idLog} - GetBizTypes - tryCallService: {tryCallService}", $"{ex.Serializar()}", TypeError.Error);
                if (tryCallService > LoggerBase.TryCallService)
                    return new List<ReturnBizTypes>();

                tryCallService += 1;
                return await GetBizTypes(tryCallService);
            }
        }

        public async Task<List<ReturnSpeciality>> GetSpecialities(int idBizType, int tryCallService)
        {
            var idLog = GetIdLog();
            try
            {
                using (var client = new HttpClient())
                {
                    var request = $"{LoggerBase.UrlBase}{LoggerBase.ApiGetSpecialities}?idBizType={idBizType}";
                    LoggerBase.WriteLog($"{idLog} - GetSpecialities - tryCallService: {tryCallService}", $"{request}", TypeError.Trace);
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    return await InterpretarRespuesta<List<ReturnSpeciality>>(
                        await client.GetAsync(request), idLog);
                }
            }
            catch (Exception ex)
            {
                LoggerBase.WriteLog($"{idLog} - GetSpecialities - tryCallService: {tryCallService}", $"{ex.Serializar()}", TypeError.Error);
                if (tryCallService > LoggerBase.TryCallService)
                    return new List<ReturnSpeciality>();


                tryCallService += 1;
                return await GetSpecialities(idBizType, tryCallService);
            }
        }

        public async Task<List<ReturnVendor>> GetVendorsBySpecialityAndCustomer(string specialityName, string customerId, int tryCallService)
        {
            var idLog = GetIdLog();
            try
            {
                using (var client = new HttpClient())
                {
                    var request = $"{LoggerBase.UrlBase}{LoggerBase.ApiGetVendorsBySpecialityAndCustomer}?speciality={specialityName}&customerId={customerId}";
                    LoggerBase.WriteLog($"{idLog} - GetVendorsBySpecialityAndCustomer - tryCallService: {tryCallService}", $"{request}", TypeError.Trace);
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    return await InterpretarRespuesta<List<ReturnVendor>>(
                        await client.GetAsync(request), idLog);
                }
            }
            catch (Exception ex)
            {
                LoggerBase.WriteLog($"{idLog} - GetVendorsBySpecialityAndCustomer - tryCallService: {tryCallService}", $"{ex.Serializar()}", TypeError.Error);
                if (tryCallService > LoggerBase.TryCallService)
                    return new List<ReturnVendor>();

                tryCallService += 1;
                return await GetVendorsBySpecialityAndCustomer(specialityName, customerId, tryCallService);
            }
        }

        public async Task<List<ReturnItem>> GetItemsByVendor(string vendorId, int tryCallService)
        {
            var idLog = GetIdLog();
            try
            {
                using (var client = new HttpClient())
                {
                    var request = $"{LoggerBase.UrlBase}{LoggerBase.ApiGetItemsByVendor}?vendorId={vendorId}";
                    LoggerBase.WriteLog($"{idLog} - GetItemsByVendor - tryCallService: {tryCallService}", $"{request}", TypeError.Trace);
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    return await InterpretarRespuesta<List<ReturnItem>>(
                        await client.GetAsync(request), idLog);
                }
            }
            catch (Exception ex)
            {
                LoggerBase.WriteLog($"{idLog} - GetItemsByVendor - tryCallService: {tryCallService}", $"{ex.Serializar()}", TypeError.Error);
                if (tryCallService > LoggerBase.TryCallService)
                    return new List<ReturnItem>();

                tryCallService += 1;
                return await GetItemsByVendor(vendorId, tryCallService++);
            }
        }

        public async Task<bool> ProcessOrder(Order order, int tryCallService)
        {
            var idLog = GetIdLog();
            try
            {
                using (var client = new HttpClient())
                {
                    var request = $"{LoggerBase.UrlBase}{LoggerBase.ApiProcessOrder}";
                    var requestObject = JsonConvert.SerializeObject(order);
                    var content = new StringContent(requestObject, Encoding.UTF8, "application/json");
                    LoggerBase.WriteLog($"{idLog} - ProcessOrder - tryCallService: {tryCallService}", $"{request} : order: {order.Serializar()}", TypeError.Trace);
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    return await InterpretarRespuesta<bool>(await client.PostAsync(request, content),idLog);
                }
            }
            catch (Exception ex)
            {
                LoggerBase.WriteLog($"{idLog} - ProcessOrder - tryCallService: {tryCallService}", $"{ex.Serializar()}", TypeError.Error);
                if (tryCallService > LoggerBase.TryCallService)
                    return false;

                tryCallService += 1;
                return await ProcessOrder(order, tryCallService);
            }
        }
    }


}
