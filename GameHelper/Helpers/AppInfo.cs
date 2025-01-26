namespace GameHelper.Helpers
{
    public static class AppInfo
    {
        public static string AppTitleAndVersion
        {
            get
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var version = assembly.GetName().Version;
                var title = assembly.GetName().Name;
                return $"{title} v{version}";
            }
        }
    }
}
