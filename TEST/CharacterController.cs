using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace TEST
{
    [Route("api/character")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        public ListCharacter listCharacters = new ListCharacter();
        public void GetDataFromAPI()
        {
            WebRequest request = WebRequest.Create("https://rickandmortyapi.com/api/character");
            WebResponse response = request.GetResponse();
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string line;
                if ((line = stream.ReadLine()) != null)
                {
                    listCharacters = JsonConvert.DeserializeObject<ListCharacter>(line);
                }
            }
        }

        public CharacterController()
        {
            GetDataFromAPI();
        }

        [HttpGet]
        public void Get()
        {
            base.Response.WriteAsJsonAsync(listCharacters.Results);
        }

        [HttpGet("{id}")]
        public void Get(int id)
        {
            Character? user = listCharacters.Results.FirstOrDefault((u) => u.Id == id);
            if (user != null)
            {
                base.Response.WriteAsJsonAsync(user);
            }
            else
            {
                base.Response.StatusCode = 404;
                base.Response.WriteAsJsonAsync(new { message = "Актёр не найден" });
            }
        }

        [HttpGet("{id}/check-person/{episode}")]
        public void Get(int id, string episode)
        {
            bool exits = false;
            Character? user = listCharacters.Results.FirstOrDefault((u) => u.Id == id);
            if (user != null)
            {
                HttpClient client = new HttpClient();
                string str = "https://rickandmortyapi.com/api/episode/" + episode;
                foreach (var item in user.Episode)
                {
                    if (item == str) { exits = true; break; }
                    else { exits = false; }
                }
                if (exits == true)
                    base.Response.WriteAsJsonAsync(new { message = "Актёр брал участь в этом эпизоде" });
                else
                    base.Response.WriteAsJsonAsync(new { message = "Актёр не участвывал в этом эпизоде" });
            }
            else
            {
                base.Response.StatusCode = 404;
                base.Response.WriteAsJsonAsync(new { message = "Актёр не найден" });
            }
        }
    }
}
