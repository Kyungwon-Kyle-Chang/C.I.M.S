using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace C.I.M.S_WPF.Views
{
    /// <summary>
    /// Interaction logic for GridWithViewboxText.xaml
    /// </summary>
    public partial class GridWithViewboxText : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register
        ("TextValue", typeof(string), typeof(GridWithViewboxText), new PropertyMetadata(string.Empty, TextValueChanged));

        public static readonly DependencyProperty RowNumProperty = DependencyProperty.Register
        ("RowNumValue", typeof(string), typeof(GridWithViewboxText), new PropertyMetadata(string.Empty, RowNumValueChanged));

        public static readonly DependencyProperty ColNumProperty = DependencyProperty.Register
        ("ColNumValue", typeof(string), typeof(GridWithViewboxText), new PropertyMetadata(string.Empty, ColNumValueChanged));

        public static readonly DependencyProperty RowProperty = DependencyProperty.Register
        ("RowValue", typeof(string), typeof(GridWithViewboxText), new PropertyMetadata(string.Empty, RowValueChanged));

        public static readonly DependencyProperty ColumnProperty = DependencyProperty.Register
        ("ColumnValue", typeof(string), typeof(GridWithViewboxText), new PropertyMetadata(string.Empty, ColumnValueChanged));

        public static readonly DependencyProperty RowSpanProperty = DependencyProperty.Register
        ("RowSpanValue", typeof(string), typeof(GridWithViewboxText), new PropertyMetadata(string.Empty, RowSpanValueChanged));

        public static readonly DependencyProperty ColumnSpanProperty = DependencyProperty.Register
        ("ColumnSpanValue", typeof(string), typeof(GridWithViewboxText), new PropertyMetadata(string.Empty, ColumnSpanValueChanged));

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register
        ("ColorValue", typeof(string), typeof(GridWithViewboxText), new PropertyMetadata(string.Empty, ColorValueChanged));

        private static void TextValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GridWithViewboxText;
            control.textBlock.Text = control.TextValue;
        }

        private static void RowNumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GridWithViewboxText;
            for (int i = 0; i < int.Parse(control.RowNumValue); i++)
            {
                RowDefinition rowDef = new RowDefinition();
                control.grid.RowDefinitions.Add(rowDef);
            }
        }

        private static void ColNumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GridWithViewboxText;
            for (int i = 0; i < int.Parse(control.ColNumValue); i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                control.grid.ColumnDefinitions.Add(colDef);
            }
        }

        private static void RowValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GridWithViewboxText;
            Grid.SetRow(control.viewBox, int.Parse(control.RowValue));
        }

        private static void ColumnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GridWithViewboxText;
            Grid.SetColumn(control.viewBox, int.Parse(control.ColumnValue));
        }

        private static void RowSpanValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GridWithViewboxText;
            Grid.SetRowSpan(control.viewBox, int.Parse(control.RowSpanValue));
        }

        private static void ColumnSpanValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GridWithViewboxText;
            Grid.SetColumnSpan(control.viewBox, int.Parse(control.ColumnSpanValue));
        }

        private static void ColorValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GridWithViewboxText;
            BrushConverter converter = new BrushConverter();
            Brush newBrush = control.ColorValue.Equals("") ? null : (Brush)converter.ConvertFromString(control.ColorValue);
            control.textBlock.Foreground = newBrush;
        }

        public string TextValue
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public string RowNumValue
        {
            get
            {
                return (string)GetValue(RowNumProperty);
            }
            set
            {
                SetValue(RowNumProperty, value);
            }
        }

        public string ColNumValue
        {
            get
            {
                return (string)GetValue(ColNumProperty);
            }
            set
            {
                SetValue(ColNumProperty, value);
            }
        }

        public string RowValue
        {
            get
            {
                return (string)GetValue(RowProperty);
            }
            set
            {
                SetValue(RowProperty, value);
            }
        }

        public string ColumnValue
        {
            get
            {
                return (string)GetValue(ColumnProperty);
            }
            set
            {
                SetValue(ColumnProperty, value);
            }
        }

        public string RowSpanValue
        {
            get
            {
                return (string)GetValue(RowSpanProperty);
            }
            set
            {
                SetValue(RowSpanProperty, value);
            }
        }

        public string ColumnSpanValue
        {
            get
            {
                return (string)GetValue(ColumnSpanProperty);
            }
            set
            {
                SetValue(ColumnSpanProperty, value);
            }
        }

        public string ColorValue
        {
            get
            {
                return (string)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }

        public GridWithViewboxText()
        {
            InitializeComponent();
        }
    }
}
