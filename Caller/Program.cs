const string apiUrl = "http://localhost:5096/api/Product?pageNumber=1&pageSize=10";

using var client = new HttpClient();
try
{
    var response = await client.GetAsync(apiUrl);

    if (response.IsSuccessStatusCode)
    {
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);
    }
    else
    {
        Console.WriteLine($"An error has occurred: {response.StatusCode} - {response.ReasonPhrase}");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}