namespace codecrafters_http_server
{
    public static class HttpClientTestCaller
    {

        public static async Task CallLocalHostServerBro()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:4221");

                Console.WriteLine(await response.Content.ReadAsStringAsync());
                

            }
        }

    }
}
