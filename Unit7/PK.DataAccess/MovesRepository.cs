using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace PK.DataAccess
{
    public class MovesRepository : IMovesRepository
    {
        //private readonly HttpClient _httpClient = new HttpClient
        //{
        //    BaseAddress = new Uri("https://pokeapi.co/api/v2/")
        //};

        private readonly HttpClient _httpClient = new HttpClient();
        private async Task<TypeFireStats> GetTypeStats()
        {

            try
            {
                //HttpResponseMessage response = await _httpClient.GetAsync(_uri);
                //response.EnsureSuccessStatusCode();
                //string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                string responseBody = await _httpClient.GetStringAsync("https://pokeapi.co/api/v2/type/fire");
                TypeFireStats typeFireStats = JsonConvert.DeserializeObject<TypeFireStats>(responseBody);
                return typeFireStats;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private async Task<MoveStats> GetMoveStats(string url)
        {

            try
            {
                string responseBody = await _httpClient.GetStringAsync(url);
                MoveStats moveStats = JsonConvert.DeserializeObject<MoveStats>(responseBody);
                return moveStats;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<Domain.Move>> GetMoveNames(int number)
        {
            int counter = 0;

            try
            {
                TypeFireStats typeStats = await GetTypeStats();
                List<Domain.Move> listOfMoves = new List<Domain.Move>();

                typeStats.Moves.ToList().ForEach(async x =>
                {
                    if (counter < number)
                    {
                        counter++;
                        MoveStats moveStats = await GetMoveStats(x.Url);
                        listOfMoves.Add(new Domain.Move
                        {
                            EnglisName = moveStats.Name,
                            SpanishName = moveStats.Names.FirstOrDefault(name => name.Language.Name == "es").Name,
                        });
                    }

                });

                //for (int i = 0; i < number; i++)
                //{
                //    MoveStats moveStats = await GetMoveStats(typeStats.Moves[i].Url);
                //    listOfMoves.Add(new Domain.Move
                //    {
                //        EnglisName = moveStats.Name,
                //        SpanishName = moveStats.Names.FirstOrDefault(x => x.Language.Name == "es").Name,
                //    });
                //}

                return listOfMoves;

            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
