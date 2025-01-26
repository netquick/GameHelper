using System;
using System.Reflection;
using System.Windows.Markup;

namespace GameHelper.CustomMarkupExtensions
{
    public class AssemblyNameExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }
    }
}
