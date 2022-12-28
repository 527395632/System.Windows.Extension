using System.Windows.Media;

namespace System.Windows.Extension.Expression.Media
{
    public interface IGeometrySourceParameters
    {
        Stretch Stretch { get; }

        Brush Stroke { get; }

        double StrokeThickness { get; }
    }
}