using System.IO;
using GameHelper.Config;


namespace GameHelper.Helpers
{
    public class MessageService
    {
        private static readonly Lazy<MessageService> _instance = new Lazy<MessageService>(() => new MessageService());

        public static MessageService Instance => _instance.Value;

        public event Action<string> NotificationReceived;
        public event Action<double> ProgressUpdated;

        private readonly string _logFilePath;

        private MessageService()
        {
            _logFilePath = ConfigManager.GetLogFilePath();
        }

        public void ShowNotification(string message)
        {
            try
            {
                NotificationReceived?.Invoke(message);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public void LogMessage(string message)
        {
            try
            {
                WriteToLogFile(message);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public void UpdateProgress(double progress)
        {
            try
            {
                ProgressUpdated?.Invoke(progress);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void WriteToLogFile(string message)
        {
            File.AppendAllText(_logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }

        private void HandleException(Exception ex)
        {
            // Example: Log the exception to a file
            File.AppendAllText(_logFilePath, $"{DateTime.Now}: {ex.Message}{Environment.NewLine}");

            // Optionally, you can also log the exception to the UI
            NotificationReceived?.Invoke($"Error: {ex.Message}");
        }
    }
}
