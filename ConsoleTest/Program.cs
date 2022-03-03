// See https://aka.ms/new-console-template for more information

using System;
using System.Net.Http;
using System.Net.Http.Json;
using Glory.Domain;
using ShopClient;

HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Add("UserAgent", "Windows");
string uri = "http://localhost:5188";
MyClient myClient = new MyClient(uri, client);

var responseMessage = await myClient.GetResponseMessage();
Console.WriteLine($"{responseMessage}");

//Print products
var products = await myClient.GetProducts();
myClient.Print(products);

//Add product
var productAddMessage = await myClient.AddProduct("Razor", 550, 1);
Console.WriteLine(productAddMessage);

//Print products
var productsAfterAdd = await myClient.GetProducts();
myClient.Print(productsAfterAdd);

//Print products
//var res2 = await client.GetStringAsync(uri);
//Console.WriteLine(res2);

