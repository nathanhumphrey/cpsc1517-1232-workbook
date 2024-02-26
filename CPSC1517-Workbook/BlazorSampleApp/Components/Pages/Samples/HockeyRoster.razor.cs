using Hockey.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorSampleApp.Components.Pages.Samples
{
    public partial class HockeyRoster
    {
        private string? FeedbackMessage { get; set; }
        private List<HockeyPlayer> Players { get; set; } = new();

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; } = default!;

        protected override void OnInitialized()
        {
            string csvFilePath = $@"{WebHostEnvironment.ContentRootPath}\Data\players.csv";
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                // Skip the first line as it contains column headings only
                reader.ReadLine();
                // Read one line at a time until the end of file (null) is read.
                string? currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    try
                    {
                        Players.Add(HockeyPlayer.Parse(currentLine));
                    }
                    catch (Exception ex)
                    {
                        FeedbackMessage = ex.Message;
                    }
                }

                reader.Close();
            }
        }
    }
}
