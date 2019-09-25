using System.Windows.Controls;

namespace QuestionAnswerViewLib
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class QuestionAnswerView : UserControl
    {
        QuestionAnswerViewModelLib.QuestionAnswerViewModel _vmRef = new QuestionAnswerViewModelLib.QuestionAnswerViewModel();
        public QuestionAnswerView()
        {
            InitializeComponent();

            this.DataContext = _vmRef;
            


            // <TextBox Text="{Binding Source=_vmRef,Path=Result,Mode=OneWay}"/>

            //Binding

            //Binding _connector_result = new Binding();
            ////source Object
            //_connector_result.Source = _vmRef;
            ////Source Property
            //_connector_result.Path = new PropertyPath("Result");
            ////Mode
            //_connector_result.Mode = BindingMode.OneWay;
            ////Set Target Property
            //this.resultTextBox.SetBinding(TextBox.TextProperty, _connector_result);

        }
    }
}
