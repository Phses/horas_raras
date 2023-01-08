using AutoMapper;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Exceptions;
using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Interfaces.Service;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace HorasRaras.Service
{
    public class TogglService : ITogglService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly ITarefaService _tarefaService;
        

        public TogglService(IHttpClientFactory httpClientFactory, IMapper mapper, ITarefaService tarefaService)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _tarefaService = tarefaService;
        }
        public async Task ObterTarefasApiToggl(string workspaceId, string since, string until, string userAgent, string token)
        {
           const string url = "https://api.track.toggl.com/reports/api/v2/details";
           var param = new Dictionary<string, string>() { {"workspace_id", workspaceId}, { "since", since }, { "until", until }, { "user_agent", userAgent } };
           var newUrl = new Uri(QueryHelpers.AddQueryString(url, param));

           var httpRequestMessage = new HttpRequestMessage(
           HttpMethod.Get, newUrl)
            {
                Headers =
                {
                { HeaderNames.Authorization,$"Basic {Convert.ToBase64String(Encoding.Default.GetBytes(token + ":api_token"))}" },
            }
            };
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await TratarErroAsync(httpResponseMessage);
            }
            var content =
                  await httpResponseMessage.Content.ReadAsStringAsync();

            var entity = JsonConvert.DeserializeObject<TogglTaskEntity>(content);
            

            foreach (var task in entity.Data)
            {
                //Tarefas sem os atributos necessarios serao ignoradas
                if (task.Description == null || task.Project == null || task.Start == null)
                {
                    continue;
                }
                var tarefaReponse = _mapper.Map<TarefaTogglResponse>(task);
                var tarefaEntity = _mapper.Map<TarefaEntity>(tarefaReponse);
                await _tarefaService.AdicionarTarefaTogglAsync(tarefaEntity, task.Project);
            }
        }
        private async Task TratarErroAsync(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new InformacaoException(Domain.Enums.StatusException.NaoAutorizado, $"Usuário sem acesso. {body}");
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new InformacaoException(Domain.Enums.StatusException.NaoAutorizado,$"Requisição inválida.{body}");
            else
                throw new InformacaoException(Domain.Enums.StatusException.NaoAutorizado, $"Erro ao fazer a requisição. StatusCode: {response.StatusCode} {body}");
        }
    }

}

