using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib;
using System.Net;
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
using ZabbixApi;

namespace WPFZabbix
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			var s =  new List<string>();
			s.Add("Get");
			s.Add("BulkWalk");
			combo1.Items.Add(s[0]);
			combo1.Items.Add(s[1]);
			combo1.SelectedIndex = 1;
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void text4_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void combo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Controller controller = new Controller();
			if (combo1.SelectedIndex == 0)
			{
				controller.Get(text3.Text, text4.Text);
				text1.Text = controller.GetString();
			}
			else
			{
				controller.BulkWalk("127.0.0.1", text4.Text, "Huawei S5700 switch");
				List<Variable> r = controller.BulkWalkList();
				text1.Text = "Количество элементов: " + r.Count.ToString() + "\n";
				for (int i = 0; i < r.Count; i++)
				{
					text1.Text += $"{i + 1}-й: " + r[i].Data.ToString() + "\n";
				}
			}
		}
	}
}