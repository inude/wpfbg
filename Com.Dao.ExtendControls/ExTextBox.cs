using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Com.Dao.ExtendControls.Core;

namespace Com.Dao.ExtendControls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1;assembly=WpfCustomControlLibrary1"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class ExTextBox : TextBox
    {
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.Key == Key.Enter && GoNext)
            {
                e.Handled = true;
                this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (Upper.Equals(UpperOrLower.Upper.ToString()))
            {
                this.Text = this.Text.ToUpper();
            }else if (Upper.Equals(UpperOrLower.Lower.ToString()))
            {
                this.Text = this.Text.ToLower();
            }
            this.SelectionStart = this.Text.Length;
        }

        protected override void  OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
 	         base.OnGotKeyboardFocus(e);
            if (FocusAll)
            {
                this.SelectAll();
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (FocusAll)
            {
                this.SelectAll();
            }
        }

        public static readonly DependencyProperty GoNextProperty;
        public bool GoNext
        {
            get { return (bool)GetValue(GoNextProperty); }
            set { SetValue(GoNextProperty, value); }
        }

        public static readonly DependencyProperty UpperProperty;
        public string Upper
        {
            get { return GetValue(UpperProperty).ToString(); }
            set { SetValue(UpperProperty, value); }
        }

        public static readonly DependencyProperty FocusAllProperty;
        public bool FocusAll
        {
            get { return (bool)GetValue(FocusAllProperty); }
            set { SetValue(FocusAllProperty, value); }
        }
        static ExTextBox()
        {
            GoNextProperty = DependencyProperty.Register("GoNext", typeof(bool), typeof(ExTextBox), new PropertyMetadata(true));
            UpperProperty = DependencyProperty.Register("Upper", typeof(UpperOrLower), typeof(ExTextBox),
                                                        new PropertyMetadata(UpperOrLower.Default));
            FocusAllProperty = DependencyProperty.Register("FocusAll", typeof(bool), typeof(ExTextBox), new PropertyMetadata(true));
        }

        
    }
}
