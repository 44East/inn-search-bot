using Newtonsoft.Json.Linq;

namespace Get_Info_by_INN_bot.Services
{
    internal class Extractor
    {
        private readonly HttpClient _httpClient;
        public Extractor()
        {
            _httpClient = new HttpClient();
        }
        public Queue<string> GetTheFormatSearchResult(string rawContent)
        {

            var jsonObject = JObject.Parse(rawContent);

            var companies = jsonObject["items"];

            string message = "Информация о компании:\n\n";

            var result = new Queue<string>();

            foreach (var company in companies)
            {
                var inn = company["ЮЛ"]["ИНН"];
                var ogrn = company["ЮЛ"]["ОГРН"];
                var fullCompanyName = company["ЮЛ"]["НаимПолнЮЛ"];
                var status = company["ЮЛ"]["Статус"];
                var fullAddress = company["ЮЛ"]["АдресПолн"];

                string companyInfo = $"ИНН: {inn}\nОГРН: {ogrn}\nНаименование: {fullCompanyName}\nСтатус: {status}\nАдрес: {fullAddress}\n\n";

                result.Enqueue(message += companyInfo);

            }
            return result;

        }
        public async Task<Queue<string>> GetCompanyInfoByINN(string inn, string api)
        {

            string apiUrl = $@"https://api-fns.ru/api/search?q={inn}&key={api}";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {

                var rawContent = await response.Content.ReadAsStringAsync();
                return GetTheFormatSearchResult(rawContent);
            }
            else
            {
                var notFound = new Queue<string>();
                notFound.Enqueue("ИНН не найден");
                return notFound;
            }
        }
    }
}
