namespace Privasight.Model.Shared.Display.Dashboard
{
    public record DashboardSetting(HashSet<CardSetting> CardSettings, string Name = "")
    {
        public string Name { get; set; } = Name;
        public HashSet<CardSetting> CardSettings { get; set; } = CardSettings;
    }
}