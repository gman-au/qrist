namespace Qrist.Adapters.Todoist.Options
{
    public class TodoistConfigurationOptions
    {
        public string AuthRequestEndpoint { get; set; }

        public string TokenRequestEndpoint { get; set; }

        public string CreateTaskEndpoint { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string VerificationToken { get; set; }

        public string BackgroundImageUrl { get; set; }
    }
}