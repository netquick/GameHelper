using System.Windows.Markup;
using GameHelper.Helpers;

namespace GameHelper.CustomMarkupExtensions
{
    public class AppInfoExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return AppInfo.AppTitleAndVersion;
        }
    }
}
