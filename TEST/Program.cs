namespace TEST
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.MapGet("/", () => "Hello World!\n\nAll characters - api/character\nSelected charecter - api/character/{id}\nCheck person - api/character/{id}/check-person/{episodeId}");

            app.Run();
        }
    }
}