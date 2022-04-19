namespace Privasight.Model.Shared.DataStructures.Dashboard
{
    public record CardSetting(string FileWrapperTitle,
        string FileWrapperTypeName,
        DialogTypes Dialog = DialogTypes.NoDialog,
        CardTypes CardType = CardTypes.PlainNumberCard)
    {
        public DialogTypes Dialog { get; set; } = Dialog;

        public CardTypes CardType { get; set; } = CardType;

        public string FileWrapperTypeName { get; set; } = FileWrapperTypeName;

        public string FileWrapperTitle { get; set; } = FileWrapperTitle;

        public override string ToString()
        {
            return $"Data Type: {FileWrapperTitle}; CardType: {CardType}; PopUp: {Dialog};";
        }
    }
}