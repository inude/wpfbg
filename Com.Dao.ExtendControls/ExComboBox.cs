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

namespace Com.Dao.ExtendControls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Com.Dao.ExtendControls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Com.Dao.ExtendControls;assembly=Com.Dao.ExtendControls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:ExComboBox/>
    ///
    /// </summary>
    public class ExComboBox : ComboBox
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

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (SelectedItem is ListBoxItem)
            {
                BindValue = (SelectedItem as ListBoxItem).Content;
            }
            else
            {
                BindValue = String.IsNullOrEmpty(SelectedValuePath)
                               ? this.SelectedItem
                               : this.SelectedItem.GetType().GetProperty(
                                   SelectedValuePath).GetValue(this.SelectedItem, null);
            }
           
        }

        public static readonly DependencyProperty GoNextProperty = DependencyProperty.Register("GoNext", typeof(bool), typeof(ExComboBox), new PropertyMetadata(true));
        public bool GoNext
        {
            get { return (bool)GetValue(GoNextProperty); }
            set { SetValue(GoNextProperty, value); }
        }

        public object BindValue
        {
            get { return (object)GetValue(BindValueProperty); }
            set { SetValue(BindValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BindValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindValueProperty =
            DependencyProperty.Register("BindValue", typeof(object), typeof(ExComboBox), new UIPropertyMetadata(null, new PropertyChangedCallback(BindValueChangeCallback)));

        private static void BindValueChangeCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ExComboBox textAutoBox = obj as ExComboBox;
            if (textAutoBox.Items != null && e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                for (var i = 0; i < textAutoBox.Items.Count; i++)
                {
                    var item = textAutoBox.Items[i];
                    if (item is ListBoxItem)
                    {
                        if ((item as ListBoxItem).Content.Equals(e.NewValue))
                        {
                            textAutoBox.SelectedItem = item;
                            return;
                        }
                    }
                    else if(!string.IsNullOrEmpty(textAutoBox.SelectedValuePath))
                    {
                        if (
                            item.GetType()
                                .GetProperty(textAutoBox.SelectedValuePath)
                                .GetValue(item, null)
                                .Equals(e.NewValue))
                        {
                            textAutoBox.SelectedItem = item;
                            return;
                        }
                    }else if(!string.IsNullOrEmpty(textAutoBox.DisplayMemberPath))
                    {
                        if (
                            item.GetType()
                                .GetProperty(textAutoBox.DisplayMemberPath)
                                .GetValue(item, null)
                                .Equals(e.NewValue))
                        {
                            textAutoBox.SelectedItem = item;
                            return;
                        }
                    }
                    else if (item.Equals(e.NewValue))
                    {
                        textAutoBox.SelectedItem = item;
                        return;
                    }
                }
            }
        }
    }
}
