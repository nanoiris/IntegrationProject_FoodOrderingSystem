using System;

namespace RmsApp.Services
{
    public class FlashMessageService : IFlashMessageService
    {
        private int _durationInSeconds = 5;
        public string SuccessMessage { get; set; }

        public string FailureMessage { get; set; }

        public event Action OnChange;
        public int DurationInSeconds
        {
            get => _durationInSeconds;
            set => _durationInSeconds = value;
        }

        public async Task SetSuccessMessage(string message)
        {
            SuccessMessage = message;
            NotifyStateChanged();
            await Task.Delay(_durationInSeconds * 1000); // add a delay using the duration specified by the property
            SuccessMessage = null; // clear the message after the delay
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
            Console.WriteLine("clear from service");
            ClearMessages();
            NotifyStateChanged(); // add this line to notify the UI that messages have been cleared
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
