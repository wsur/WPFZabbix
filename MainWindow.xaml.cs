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
			s.AddRange(new string[] { "Get", "BulkWalk" });
			//s.Add("Get");
			//s.Add("BulkWalk");
			combo1.Items.Add(s[0]);
			combo1.Items.Add(s[1]);
			combo2.Items.Add(s[0]);
			combo2.Items.Add(s[1]);
			combo3.Items.Add(s[0]);
			combo3.Items.Add(s[1]);
			combo4.Items.Add(s[0]);
			combo4.Items.Add(s[1]);
			combo5.Items.Add(s[0]);
			combo5.Items.Add(s[1]);
			combo1.SelectedIndex = 1;
			combo2.SelectedIndex = 1;
			combo3.SelectedIndex = 1;
			combo4.SelectedIndex = 1;
			combo5.SelectedIndex = 1;
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
/*			Controller controller = new Controller();
			if (combo1.SelectedIndex == 0)
			{
				controller.Get(text3.Text, text4.Text);
				text1.Text = controller.GetString();
			}
			else
			{
				controller.BulkWalk(text3.Text, text4.Text, text5.Text);
				List<Variable> r = controller.BulkWalkList();
				text1.Text = "Количество элементов: " + r.Count.ToString() + "\n";
				for (int i = 0; i < r.Count; i++)
				{
					text1.Text += $"{i + 1}-й: " + r[i].Data.ToString() + "\n";
				}
			}*/
			//вынес всё в отдельный метод, который можно запускать кучу раз
			DoSurvey(combo1, new Controller(), text3.Text, text4.Text, text5.Text, text1);
		}

		 void DoSurvey(ComboBox combo, Controller controller, string ip, string oid, string contextName, TextBox surveyAnswer)
		 {
			if (combo.SelectedIndex == 0)
			{
				controller.Get(ip, oid);
				surveyAnswer.Text = controller.GetString();
			}
			else
			{
				controller.BulkWalk(ip, oid, contextName);
				List<Variable> r = controller.BulkWalkList();
				surveyAnswer.Text = "Количество элементов: " + r.Count.ToString() + "\n";
				for (int i = 0; i < r.Count; i++)
				{
					surveyAnswer.Text += $"{i + 1}-й: " + r[i].Data.ToString() + "\n";
				}
			}
		 }

		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			DoSurvey(combo2, new Controller(), text7.Text, text8.Text, text6.Text, text9);
		}
		private void Button2_Click(object sender, RoutedEventArgs e)
		{
			DoSurvey(combo3, new Controller(), text12.Text, text13.Text, text11.Text, text14);
		}

		private void Button4_Click(object sender, RoutedEventArgs e)
		{
			DoSurvey(combo5, new Controller(), text17.Text, text18.Text, text16.Text, text19);

		}

		private void Button5_Click(object sender, RoutedEventArgs e)
		{
			DoSurvey(combo4, new Controller(), text22.Text, text23.Text, text21.Text, text24);
			///ааааааааааааааааааааааааааааааааааааааааааааааааааааааа
		}
	}
}