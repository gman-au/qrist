namespace Qrist.Domain.Todoist
{
    public static class TodoistConstants
    {
        public const string Provider = "Todoist";

        public const string ActionTypeInitial = "initial";
        public const string ActionTypeSubmit = "submit";

        public const string ConfirmClearBundle = "Confirm.ClearBundle";
        public const string ConfirmGenerateQrCode = "Confirm.GenerateQrCode";
        public const string CancelConfirmation = "Confirm.Cancel";

        public const string ActionAddToBundle = "Action.AddToBundle";
        public const string ActionClearBundle = "Action.ClearBundle";
        public const string ActionGenerateQrCode = "Action.GenerateQrCode";

        public const string TransparentBase64SinglePixel =
            "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNkYAAAAAYAAjCB0C8AAAAASUVORK5CYII=";
    }
}