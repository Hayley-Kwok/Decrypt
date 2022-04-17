namespace Privasight.Model.Shared.DataStructures.Dashboard
{
    public record CardSetting(string FileWrapperTypeName,
        DialogTypes Dialog = DialogTypes.Nothing,
        CardTypes CardType = CardTypes.PlainNumberCard)
    {
        public DialogTypes Dialog { get; set; } = Dialog;

        public CardTypes CardType { get; set; } = CardType;

        public string FileWrapperTypeName { get; set; } = FileWrapperTypeName;

        public override string ToString()
        {
            return $"Data Type: {FileWrapperTypeName}; CardType: {CardType}; PopUp: {Dialog}";
        }
    }
}