using MvcPedidos.Entity.DTO;
using MvcPedidos.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        //Obtiene la información completa del prestador médico según su código

        private string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(value)}")
                .ToArray();
            return "?" + string.Join("&", array);
        }

        public async Task<List<ReturnBizTypes>> GetBizTypes(int tryCallService = 1)
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
                LoggerBase.WriteLog($"{idLog} - GetBizTypes", $"{ex.Serializar()}", TypeError.Error);
                if (tryCallService > LoggerBase.TryCallService)
                    return new List<ReturnBizTypes>();
                return await GetBizTypes(tryCallService++);
            }
        }

        public async Task<List<ReturnSpeciality>> GetSpecialities(int idBizType, int tryCallService = 1)
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
                LoggerBase.WriteLog($"{idLog} - GetSpecialities", $"{ex.Serializar()}", TypeError.Error);
                if (tryCallService > LoggerBase.TryCallService)
                    return new List<ReturnSpeciality>();
                return await GetSpecialities(idBizType, tryCallService++);
            }
        }
    }


}
