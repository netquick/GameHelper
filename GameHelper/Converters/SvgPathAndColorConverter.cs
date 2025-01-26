using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Linq;
using GameHelper.Helpers;

namespace GameHelper.Converters
{
    public class SvgPathAndColorConverter : IMultiValueConverter
    {
        private readonly MessageService _messageService = MessageService.Instance;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                _messageService.LogMessage("Invalid number of values passed to converter.");
                return DependencyProperty.UnsetValue;
            }

            if (!(values[0] is string path))
            {
                _messageService.LogMessage("First value is not a string.");
                return DependencyProperty.UnsetValue;
            }

            if (!(values[1] is SolidColorBrush colorBrush))
            {
                _messageService.LogMessage("Second value is not a SolidColorBrush.");
                return DependencyProperty.UnsetValue;
            }
            // Debug statements
            _messageService.LogMessage($"Path: {path}");
            _messageService.LogMessage($"ColorBrush: {colorBrush.Color}");

            // Get the project name dynamically from the assembly
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var resourcePath = $"pack://application:,,,/{assemblyName};component/{path}";
            try
            {
                var uri = new Uri(resourcePath, UriKind.RelativeOrAbsolute);
                var streamResourceInfo = Application.GetResourceStream(uri);
                if (streamResourceInfo != null)
                {
                    using (var stream = streamResourceInfo.Stream)
                    {
                        return LoadSvgFromStream(stream, colorBrush);
                    }
                }
                else
                {
                    _messageService.LogMessage("Stream resource info is null.");
                }
            }
            catch (Exception ex)
            {
                _messageService.LogMessage($"Error loading SVG as resource: {ex.Message}");
            }

            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = System.IO.Path.Combine(basePath, path);
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    using (var stream = System.IO.File.OpenRead(filePath))
                    {
                        return LoadSvgFromStream(stream, colorBrush);
                    }
                }
                catch (Exception ex)
                {
                    _messageService.LogMessage($"Error loading SVG from file: {ex.Message}");
                }
            }
            else
            {
                _messageService.LogMessage("File does not exist.");
            }

            return DependencyProperty.UnsetValue;
        }

        private object LoadSvgFromStream(System.IO.Stream stream, SolidColorBrush colorBrush)
        {
            var svgDocument = XDocument.Load(stream);
            if (svgDocument.Root == null) return DependencyProperty.UnsetValue;

            var ns = svgDocument.Root.GetDefaultNamespace();
            var paths = svgDocument.Descendants(ns + "path");

            var geometryGroup = new GeometryGroup();
            foreach (var pathElement in paths)
            {
                var dataAttribute = pathElement.Attribute("d");
                if (dataAttribute != null)
                {
                    var geometry = Geometry.Parse(dataAttribute.Value);
                    geometryGroup.Children.Add(geometry);
                }
            }

            var drawing = new GeometryDrawing
            {
                Geometry = geometryGroup,
                Brush = colorBrush,
                Pen = new Pen(colorBrush, 1)
            };

            // Debug statement
            _messageService.LogMessage($"Drawing created with {geometryGroup.Children.Count} paths");

            return new DrawingImage(drawing);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
