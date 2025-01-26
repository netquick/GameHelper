


Theme Colors are definded for the following colors:
	ThemeBackgroundColor
	ThemeBackground2Color
	ThemeBackground3Color
	ThemeForegroundColor
	ThemeAccentColor
	ThemeDisabledColor
	ThemeDisabledForegroundColor

MessageService, use as follows:
    public void DoWork()
    {
        var messageService = MessageService.Instance;
        messageService.ShowNotification("Work started");
        try
        {
            // Simulate work
            messageService.UpdateProgress(50);
            messageService.LogMessage("Work is at 50%");
        }
        catch (Exception ex)
        {
            messageService.ShowNotification($"Error during work: {ex.Message}");
            messageService.LogMessage($"Error during work: {ex.Message}");
        }
        messageService.ShowNotification("Work completed");
        messageService.LogMessage("Work completed");
    }
ConfigManager saves config to Appdata

ColorDefinitions to set Theme colors trough ThemeManager

LanguageManager to set language trough LanguageManager
    use like: 
        <TextBlock Text="{x:Static properties:Resources.WelcomeMessage}" />
        or
        <TextBlock Text="{Binding Source={x:Static properties:Resources.WelcomeMessage}}" />
        or in MainWindow.xaml.cs
        Title = LanguageManager.Instance.GetResourceString("WelcomeMessage");

Sidebarmenu:
    add Icons (Google Material Icons) to the project and set the Build Action to Resource)
    add the Icon to the SidebarmenuItem like this:
		<controls:SidebarmenuItem Icon="menu" Text="Menu" />
    add Navigation items like this:
        new NavigationItem { Icon = "Icons/home.svg", Text = "Home" },

