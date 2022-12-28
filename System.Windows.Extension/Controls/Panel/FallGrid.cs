using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace System.Windows.Extension.Controls
{
    public class FallGrid : Panel
    {
        #region ColumnCount
        public int ColumnCount
        {
            get { return (int)this.GetValue(ColumnCountProperty); }
            set { this.SetValue(ColumnCountProperty, value); }
        }
        public static readonly DependencyProperty ColumnCountProperty = DependencyProperty.Register("ColumnCount", typeof(int), typeof(FallGrid), new PropertyMetadata(3, (s, e) =>
         {
             if (s is FallGrid f)
             {
                 f.InvalidateArrange();
             }
         }));
        #endregion

        #region ColumnMargin
        public int ColumnMargin
        {
            get { return (int)GetValue(ColumnMarginProperty); }
            set { SetValue(ColumnMarginProperty, value); }
        }
        public static readonly DependencyProperty ColumnMarginProperty =
            DependencyProperty.Register("ColumnMargin", typeof(int), typeof(FallGrid), new PropertyMetadata(10, (s, e) =>
            {
                if (s is FallGrid f)
                {
                    f.InvalidateArrange();
                }
            }));
        #endregion

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count <= 0)
                return new Size(0, 0);

            Size childrenAvailableSize = new Size(double.IsPositiveInfinity(availableSize.Width) ? double.PositiveInfinity : availableSize.Width / ColumnCount, double.PositiveInfinity);
            Size[] childrenDesiredSizes = new Size[Children.Count];

            var columnH = new ColumnInfo[ColumnCount].Select(q => new ColumnInfo()).ToArray();
            var columnW = new double[ColumnCount];//列最宽元素的宽度
            for (int i = 0; i < Children.Count; i++)
            {
                int index = 0;//那一列目前高度最低
                for (int j = 1; j < ColumnCount; j++)
                {
                    if (columnH[j].Height < columnH[index].Height)
                        index = j;
                }
                Children[i].Measure(childrenAvailableSize);
                childrenDesiredSizes[i] = Children[i].DesiredSize;
                columnH[index].Height += childrenDesiredSizes[i].Height;
                columnH[index].Count++;

                if (columnW[index] < childrenDesiredSizes[i].Width)
                    columnW[index] = childrenDesiredSizes[i].Width;
            }

            //返回尺寸
            Size resultSize = new Size(0, 0);

            resultSize.Height = this.Height = columnH.Select(q => q.Height + (q.Count - 1) * ColumnMargin).Max();
            resultSize.Width = columnW.Max() * ColumnCount;//等宽

            resultSize.Width = double.IsPositiveInfinity(availableSize.Width) ? resultSize.Width : availableSize.Width;
            resultSize.Height = double.IsPositiveInfinity(availableSize.Height) ? resultSize.Height : availableSize.Height;

            return resultSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count <= 0)
                return new Size(0, 0);

            Size childrenFinalSize = new Size(finalSize.Width / ColumnCount, finalSize.Height);

            var columnH = new ColumnInfo[ColumnCount].Select(q => new ColumnInfo()).ToArray();
            for (int i = 0; i < Children.Count; i++)
            {
                int index = 0;//那一列目前高度最低
                for (int j = 1; j < ColumnCount; j++)
                {
                    if (columnH[j].Height < columnH[index].Height)
                        index = j;
                }

                // 容器的原始X坐标                                 - 容器因ColumnMargin导致的长度缩小位置                                   + 当前对象所处的ColumnMargin
                var x = childrenFinalSize.Width * index - ((ColumnCount - 1) * ColumnMargin / ColumnCount * index) + (index * ColumnMargin);
                // 容器的原始Y坐标                     + 当前容器所处的当前对象所处的ColumnMargin
                var y = columnH[index].Height + columnH[index].Count * ColumnMargin;
                // 容器原始宽度                                  - 容器均分ColumnMargin后得到的均摊宽度
                var width = childrenFinalSize.Width - (ColumnCount - 1) * ColumnMargin / ColumnCount;
                // 容器的高度
                var height = Children[i].DesiredSize.Height;

                Children[i].Arrange(new Rect(x, y, width, height));
                columnH[index].Height += Children[i].DesiredSize.Height;
                columnH[index].Count++;
            }

            return new Size(finalSize.Width, columnH.Select(q => q.Height).Max());
        }

        private class ColumnInfo
        {
            public ColumnInfo()
            {
                Height = 0;
                Count = 0;
            }

            public double Height { get; set; }
            public int Count { get; set; }
        }
    }
}
