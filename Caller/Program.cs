const string firstTenProducts = "http://localhost:5096/api/Product?pageNumber=1&pageSize=10";
const string productsByCategory = "http://localhost:5096/api/Product?pageNumber=1&pageSize=10&categoryId=5";
const string CategoryById = "http://localhost:5096/api/Category/5";

using var client = new HttpClient();
try
{
    var products = await client.GetAsync(firstTenProducts);

    if (products.IsSuccessStatusCode)
    {
        var result = await products.Content.ReadAsStringAsync();
        Console.WriteLine($" first ten products: \n {result} \n");
    }
    else
    {
        Console.WriteLine($"An error has occurred: {products.StatusCode} - {products.ReasonPhrase}");
    }

    Console.ReadKey();

    var productsFiltered = await client.GetAsync(productsByCategory);

    if (productsFiltered.IsSuccessStatusCode)
    {
        var result = await productsFiltered.Content.ReadAsStringAsync();
        Console.WriteLine($" Products filtered by category: \n {result} \n");
    }
    else
    {
        Console.WriteLine($"An error has occurred: {productsFiltered.StatusCode} - {productsFiltered.ReasonPhrase}");
    }

    Console.ReadKey();

    var category = await client.GetAsync(CategoryById);

    if (category.IsSuccessStatusCode)
    {
        var result = await category.Content.ReadAsStringAsync();
        Console.WriteLine($" Category by id: \n {result} \n");
    }
    else
    {
        Console.WriteLine($"An error has occurred: {category.StatusCode} - {category.ReasonPhrase}");
    }

    Console.ReadKey();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}