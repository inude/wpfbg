using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// ExTextLookup.xaml 的交互逻辑
    /// </summary>
    public partial class ExTextLookup : UserControl, INotifyPropertyChanged
    {
        private void TargetControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!this.popup.IsKeyboardFocusWithin)
                this.popup.IsOpen = false;
        }

        private void TargetControl_GotFocus(object sender, RoutedEventArgs e)
        {
            if (itemsSelected)
                itemsSelected = false;
           // else if  this.popup.IsOpen = true;
        }

        private void TargetControl_KeyUp(object sender, KeyEventArgs e)
        {
                if (e.Key == Key.Up)
                {
                    if (this.listBox.SelectedIndex > 0)
                    {
                        this.listBox.SelectedIndex--;
                    }
                }
                if (e.Key == Key.Down)
                {
                    if (this.listBox.SelectedIndex < this.listBox.Items.Count - 1)
                    {
                        this.listBox.SelectedIndex++;
                    }
                }
                if (e.Key == Key.Enter)
                {
                    if (this.popup.IsOpen && this.listBox.Items.Count > 0)
                    {
                        RaiseSelectionChangedEvent();
                        SetTextAndHide();
                    }
                    else
                    {
                        RaiseSearchEvent();
                    }
                }
        }

        private void TargetControl_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.popup.IsOpen = false;
                this.listBox.SelectedItem = null;
                e.Handled = true;
            }
        }

        private void TargetControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && this.listBox.SelectedItem != null)
            {
                this.popup.IsOpen = false;
                TargetControl.Text = String.IsNullOrEmpty(DisplayMemberPath) ?
                                     this.listBox.SelectedItem.ToString() :
                                     this.listBox.SelectedItem.GetType().GetProperty(
                                     DisplayMemberPath).GetValue(this.listBox.SelectedItem, null).ToString();
                if (MovesFocus)
                    TargetControl.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }
        }

        private void TargetControl_TextChanged(object sender, TextChangedEventArgs e)
        {
//            if (DataProvider != null)
//            {
//                DataItems = from a in DataProvider where a.GetType().GetProperty(listBox.DisplayMemberPath).GetValue(a, null).ToString().IndexOf(TargetControl.Text) > -1 select a;
//            }
            if (DataItems != null && DataItems.Count() > 0)
            {
                listBox.ItemsSource = DataItems;
                listBox.SelectedIndex = 0;
                popup.IsOpen = true;
            }
            else
            {
                popup.IsOpen = false;
            }
        }

        #region MovesFocus
        /// <summary>
        /// Do we move focus to the next control, when we 
        /// have selected an item from the dropdown list?
        /// </summary>
        public bool MovesFocus
        {
            get { return (bool)GetValue(MovesFocusProperty); }
            set { SetValue(MovesFocusProperty, value); }
        }

        public static readonly DependencyProperty MovesFocusProperty =
            DependencyProperty.Register("MovesFocus", typeof(bool), typeof(ExTextLookup), new UIPropertyMetadata(true));

        #endregion

        public ExTextLookup()
        {
            InitializeComponent();
            this.grid.DataContext = this;
            
             TargetControl.LostFocus += new RoutedEventHandler(TargetControl_LostFocus);
             TargetControl.GotFocus += new RoutedEventHandler(TargetControl_GotFocus);
             TargetControl.KeyUp += new KeyEventHandler(TargetControl_KeyUp);
             TargetControl.PreviewKeyUp += new KeyEventHandler(TargetControl_PreviewKeyUp);
             TargetControl.PreviewKeyDown += new KeyEventHandler(TargetControl_PreviewKeyDown);
//             TargetControl.TextChanged += new TextChangedEventHandler(TargetControl_TextChanged);
            // ListBox inside the Popup
            this.listBox.SelectionChanged += new SelectionChangedEventHandler(listBox_SelectionChanged);
            this.listBox.SelectionMode = SelectionMode.Single;
            this.itemsSelected = false;
//
//            Binding bindTarget = new Binding("ActualWidth");
//            bindTarget.Source = TargetControl;
//            popup.SetBinding(Popup.WidthProperty, bindTarget);  
        }

        #region Control Properties

        public string DisplayMemberPath
        {
            get { return this.listBox.DisplayMemberPath; }
            set { this.listBox.DisplayMemberPath = value; }
        }

        public string SelectedValuePath
        {
            get { return this.listBox.SelectedValuePath; }
            set { this.listBox.SelectedValuePath = value; }
        }

        public DataTemplate ItemTemplate
        {
            get { return this.listBox.ItemTemplate; }
            set { this.listBox.ItemTemplate = value; }
        }

        public ItemsPanelTemplate ItemsPanel
        {
            get { return this.listBox.ItemsPanel; }
            set { this.listBox.ItemsPanel = value; }
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get { return this.listBox.ItemTemplateSelector; }
            set { this.listBox.ItemTemplateSelector = value; }
        }

        #region SelectedListBoxIndex
        public Int32 SelectedListBoxIndex
        {
            get { return (Int32)GetValue(SelectedListBoxIndexProperty); }
            set { SetValue(SelectedListBoxIndexProperty, value); }
        }

        public static readonly DependencyProperty SelectedListBoxIndexProperty = DependencyProperty.Register("SelectedListBoxIndex", typeof(Int32), typeof(ExTextLookup), new UIPropertyMetadata(0));
        #endregion

        #region SelectedListBoxItem
        public object SelectedListBoxItem
        {
            get { return (object)GetValue(SelectedListBoxItemProperty); }
            set { SetValue(SelectedListBoxItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedListBoxItemProperty = DependencyProperty.Register("SelectedListBoxItem", typeof(object), typeof(ExTextLookup), new UIPropertyMetadata(null));
        #endregion


        #region SelectedListBoxValue
        public object SelectedListBoxValue
        {
            get { return (object)GetValue(SelectedListBoxValueProperty); }
            set { SetValue(SelectedListBoxValueProperty, value); }
        }

        public static readonly DependencyProperty SelectedListBoxValueProperty = DependencyProperty.Register("SelectedListBoxValue", typeof(object), typeof(ExTextLookup), new UIPropertyMetadata(null));
        #endregion


        #region PopupWidth
        /// <summary>
        /// Set the width of the Popup
        /// </summary>
        public double PopupWidth
        {
            get { return (double)GetValue(PopupWidthProperty); }
            set { SetValue(PopupWidthProperty, value); }
        }

        public static readonly DependencyProperty PopupWidthProperty = DependencyProperty.Register("PopupWidth", typeof(double), typeof(ExTextLookup), new UIPropertyMetadata(0.0,new PropertyChangedCallback(PopupWidthChangeCallback)));


        private static void PopupWidthChangeCallback(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            var auto = d as ExTextLookup;
            if(auto != null)
                auto.popup.Width = (double)e.NewValue;
        }

        #endregion
        
        public object BindValue
        {
            get { return (object)GetValue(BindValueProperty); }
            set { SetValue(BindValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BindValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindValueProperty =
            DependencyProperty.Register("BindValue", typeof(object), typeof(ExTextLookup), new UIPropertyMetadata(null,new PropertyChangedCallback(BindValueChangeCallback)));

        private static void BindValueChangeCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ExTextLookup textAutoBox = obj as ExTextLookup;
            textAutoBox.popup.IsOpen = false;
        }

        public PopupAnimation PopupAnimation
        {
            get { return this.popup.PopupAnimation; }
            set { this.popup.PopupAnimation = value; }
        }

        #endregion

        #region Listbox Selection

        private bool itemsSelected;

        void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NotifyPropertyChanged("SelectedItem");
            NotifyPropertyChanged("SelectedValue");
            if (this.popup.IsKeyboardFocusWithin)
            {
                SetTextAndHide();
            }
        }

        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ExTextLookup));

        // Provide CLR accessors for the event
        public event RoutedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        private void RaiseSelectionChangedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ExTextLookup.SelectionChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly RoutedEvent SearchEvent = EventManager.RegisterRoutedEvent("Search", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ExTextLookup));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Search
        {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }

        private void RaiseSearchEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ExTextLookup.SearchEvent);
            RaiseEvent(newEventArgs);
        }

        private void SetTextAndHide()
        {
            this.popup.IsOpen = false;
            if (this.listBox.SelectedItem != null)
            {
                TargetControl.Text = String.IsNullOrEmpty(DisplayMemberPath) ?
                                     this.listBox.SelectedItem.ToString() :
                                     this.listBox.SelectedItem.GetType().GetProperty(
                                        DisplayMemberPath).GetValue(this.listBox.SelectedItem, null).ToString();
                itemsSelected = true;
                BindValue = String.IsNullOrEmpty(SelectedValuePath)
                                ? this.listBox.SelectedItem
                                : this.listBox.SelectedItem.GetType().GetProperty(
                                    SelectedValuePath).GetValue(this.listBox.SelectedItem, null);
                if (MovesFocus)
                    TargetControl.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        #endregion

        public IEnumerable<object> DataItems
        {
            get { return (IEnumerable<object>)GetValue(DataItemsProperty); }
            set
            {
                SetValue(DataItemsProperty, value);
                var dataItems = value as IEnumerable<object>;
                listBox.ItemsSource = dataItems;
                if (dataItems != null && dataItems.Count() > 0)
                {
                    popup.IsOpen = true;
                    listBox.SelectedIndex = 0;
                }
                else
                {
                    popup.IsOpen = false;
                    listBox.SelectedIndex = -1;
                }
            }
        }

        // Using a DependencyProperty as the backing store for DataItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataItemsProperty =
            DependencyProperty.Register("DataItems", typeof(IEnumerable<object>), typeof(ExTextLookup), new UIPropertyMetadata(null,new PropertyChangedCallback(DataItemsChangedCallback)));
        
        
        private static void DataItemsChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
//            var look = obj as ExTextLookup;
//            if (look != null)
//            {
//                var dataItems = e.NewValue as IEnumerable<object>;
//                look.listBox.ItemsSource = dataItems;
//                if (dataItems != null && dataItems.Count() > 0)
//                {
//                    look.popup.IsOpen = true;
//                    look.listBox.SelectedIndex = 0;
//                }
//                else
//                {
//                    look.popup.IsOpen = false;
//                    look.listBox.SelectedIndex = -1;
//                }
//            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
