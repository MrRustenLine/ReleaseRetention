using ReleaseRetention.InterfaceAdaptors.Managers;

namespace ReleaseRetention
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);       

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var dataFilesSettings = new DataFileSettings();
            builder.Configuration.GetSection(nameof(DataFileSettings)).Bind(dataFilesSettings);

            DataFileManager.ReleasesFile = dataFilesSettings.ReleasesJSON;
            DataFileManager.DeploymentsFile = dataFilesSettings.DeploymentsJSON;
            DataFileManager.LoadData();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}