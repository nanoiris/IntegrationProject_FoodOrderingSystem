using System;

namespace RmsApp.Services
{
    public class FlashMessageService : IFlashMessageService
    {
        private int _durationInSeconds = 15;
        public string SuccessMessage { get; set; }

        public string FailureMessage { get; set; }

        public event Action OnChange;
        public int DurationInSeconds
        {
            get => _durationInSeconds;
            set => _durationInSeconds = value;
        }

        public async void SetSuccessMessage(string message)
        {
            SuccessMessage = message;
            NotifyStateChanged();
            await Task.Delay(500); // add a 500ms delay
            NotifyStateChanged();
        }


        public void SetFailureMessage(string message)
        {
            FailureMessage = message;
            NotifyStateChanged();
        }

        public void ClearMessages()
        {
            SuccessMessage = null;
            FailureMessage = null;
            NotifyStateChanged();
        }
        public async Task ClearMessagesAsync()
        {
            await Task.Delay(_durationInSeconds * 1000);
            ClearMessages();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
