using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumeWebAPI
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            var repositories = await ProcessGitHubRepo();
            //repositories.ForEach(r =>  Console.WriteLine(r.Name));
            foreach (var repository in repositories)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(repository.Name.ToUpper());
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(repository.Description);
                Console.WriteLine(repository.GitHubHomeUrl);
                Console.WriteLine(repository.HomePage);
                Console.WriteLine(repository.Watchers);
                Console.WriteLine("\n");
                
            }
        }

        private static async Task<List<GitHubRepo>> ProcessGitHubRepo()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".Net Foundation Repository Reporter");

            var result = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos"); //Open repo, for personnal repo allow app in settings usin OAuth
            var repos = await JsonSerializer.DeserializeAsync<List<GitHubRepo>>(await result);
            return repos;
        }
    }
}
