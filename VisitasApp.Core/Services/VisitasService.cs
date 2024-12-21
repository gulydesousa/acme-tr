using System.Net.Http.Json;
using System.Text.Json;
using VisitasApp.Core.Domain.Entities;
using VisitasApp.Core.DTO;
using VisitasApp.Core.ServicesContract;

namespace VisitasApp.Core.Services
{
    public class VisitasService : IVisitasService
    {
        private readonly HttpClient _httpClient;

        public VisitasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Visita>> VisitasGetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/visitas");
                response.EnsureSuccessStatusCode(); // Lanza una excepción si el código de estado no es exitoso
                var content = await response.Content.ReadFromJsonAsync<IEnumerable<Visita>>();
                return content ?? Enumerable.Empty<Visita>();
            }
            catch (HttpRequestException ex)
            {
                // Manejar el error de solicitud HTTP
                Console.WriteLine($"Error de solicitud: {ex.Message}");
                return Enumerable.Empty<Visita>();
            }
            catch (NotSupportedException ex)
            {
                // Manejar el error de contenido no soportado
                Console.WriteLine($"El tipo de contenido no es compatible: {ex.Message}");
                return Enumerable.Empty<Visita>();
            }
            catch (JsonException ex)
            {
                // Manejar el error de JSON
                Console.WriteLine($"JSON inválido: {ex.Message}");
                return Enumerable.Empty<Visita>();
            }
        }

        public async Task<Visita> VisitasGetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/visitas/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Visita>() ?? throw new Exception("No se pudo obtener la visita por su ID.");
            }
            catch (HttpRequestException ex)
            {
                // Manejar la excepción aquí
                Console.WriteLine($"Ocurrió un error al obtener la visita por su ID: {ex.Message}");
                throw;
            }
        }

        public async Task<Visita> VisitasCreateAsync(CreateVisitaDto visita)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/visitas", visita);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Visita>() ?? throw new Exception("No ha sido posible deserializar la respuesta.");
            }
            catch (HttpRequestException ex)
            {
                // Manejar la excepción aquí
                Console.WriteLine($"Ocurrió un error al agregar la visita: {ex.Message}");
                throw;
            }
        }

        public async Task VisitasUpdateAsync(Visita visita)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/visitas/{visita.Id}", visita);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                // Manejar la excepción aquí
                Console.WriteLine($"Ocurrió un error al actualizar la visita: {ex.Message}");
                throw;
            }
        }

        public async Task VisitasDeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/visitas/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                // Manejar la excepción aquí
                Console.WriteLine($"Ocurrió un error al eliminar la visita: {ex.Message}");
                throw;
            }
        }
    }
}
