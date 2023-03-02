using System;

namespace RmsApp.Services
{
    public interface IFlashMessageService
    {
        string SuccessMessage { get; set; }
        string FailureMessage { get; set; }
        int DurationInSeconds { get; set; }
        event Action OnChange;
        void SetSuccessMessage(string message);
        void SetFailureMessage(string message);
        void ClearMessages();

        Task ClearMessagesAsync();
    }
}
