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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System;

namespace WPFZabbix
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//Токены отмены для первой галочки
		//List<CancellationToken> cancellationTokens1 = new List<CancellationToken>();
		List<CancellationTokenSource> cts1 = new List<CancellationTokenSource>();
		List<CancellationTokenSource> cts2 = new List<CancellationTokenSource>();
		List<CancellationTokenSource> cts3 = new List<CancellationTokenSource>();
		List<CancellationTokenSource> cts4 = new List<CancellationTokenSource>();
		List<CancellationTokenSource> cts5 = new List<CancellationTokenSource>();
		public MainWindow()
		{
			InitializeComponent();
			var s =  new List<string>();
			s.AddRange(new string[] { "BulkWalk", "Get" });
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
			/*			while (true)
						{
							DoSurvey(combo1, new Controller(), text3.Text, text4.Text, text5.Text, text1);
							Thread.Sleep(2000);
						}*/
		}

		static async Task RunPeriodicAsync(int selectedIndex, Controller controller, string ip, int port, string oid, string contextName, TextBox surveyAnswer,
			Action<int,Controller, string, int, string, string, TextBox> action,
										   TimeSpan dueTime,
										   TimeSpan interval,
										   CancellationToken token)
		{
			if (dueTime > TimeSpan.Zero)
				await Task.Delay(dueTime, token);

			// Repeat this loop until cancelled.
			while (!token.IsCancellationRequested)
			{
				// Call our onTick function.
				action?.Invoke(selectedIndex, controller, ip, port, oid, contextName, surveyAnswer);

				// Wait to repeat again.
				if (interval > TimeSpan.Zero)
					await Task.Delay(interval, token);
			}
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

		void DoSurvey(int selectedIndex, Controller controller, string ip, int port, string oid, string contextName, TextBox surveyAnswer)
		{
			//string surveyAnswer = string.Empty;
			try
			{
				if (selectedIndex == 1)
				{
					controller.Get(ip, port, oid);
					surveyAnswer.Text = controller.GetString();
				}
				else
				{
					controller.BulkWalk(ip,port, oid, contextName);
					List<Variable> r = controller.BulkWalkList();
					surveyAnswer.Text = "Количество элементов: " + r.Count.ToString() + "\n";
					for (int i = 0; i < r.Count; i++)
					{
						surveyAnswer.Text += $"{i + 1}-й: " + r[i].Data.ToString() + "\n";
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			//return surveyAnswer;
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{

			DoSurvey(combo1.SelectedIndex, new Controller(), text3.Text, Convert.ToInt32(port1.Text), text4.Text, text5.Text, text1);
		}

		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			 DoSurvey(combo2.SelectedIndex, new Controller(), text7.Text, Convert.ToInt32(port2.Text), text8.Text, text6.Text, text9);
		}
		private void Button2_Click(object sender, RoutedEventArgs e)
		{
			DoSurvey(combo3.SelectedIndex, new Controller(), text12.Text, Convert.ToInt32(port3.Text), text13.Text, text11.Text, text14);
		}

		private void Button4_Click(object sender, RoutedEventArgs e)
		{
			DoSurvey(combo4.SelectedIndex, new Controller(), text17.Text, Convert.ToInt32(port4.Text), text18.Text, text16.Text, text19);
		}

		private void Button5_Click(object sender, RoutedEventArgs e)
		{
			DoSurvey(combo5.SelectedIndex, new Controller(), text22.Text, Convert.ToInt32(port5.Text), text23.Text, text21.Text, text24);
		}

		private void check1_Checked(object sender, RoutedEventArgs e)
		{
			
		}

		private void check2_Checked(object sender, RoutedEventArgs e)
		{
			//text9.Text = DoSurvey(combo2.SelectedIndex, new Controller(), text7.Text, text8.Text, text6.Text);
			//Thread.Sleep(2000);
		}

		private void check3_Checked(object sender, RoutedEventArgs e)
		{
			//text14.Text = DoSurvey(combo3.SelectedIndex, new Controller(), text12.Text, text13.Text, text11.Text);
			//Thread.Sleep(2000);
		}
		private void check4_Checked(object sender, RoutedEventArgs e)
		{
			//text19.Text = DoSurvey(combo5.SelectedIndex, new Controller(), text17.Text, text18.Text, text16.Text);
			//Thread.Sleep(2000);
		}
		private void check5_Checked(object sender, RoutedEventArgs e)
		{
/*			while ((bool)check5.IsChecked)
			{
			//text24.Text = DoSurvey(combo4.SelectedIndex, new Controller(), text22.Text, text23.Text, text21.Text);
			Thread.Sleep(2000);
			}*/

		}

		private void check1_Click(object sender, RoutedEventArgs e)
		{
			//Перед очередным вызовом метода, нужно вызвать токены отмены.
			foreach (var c in cts1)
			{
				c.Cancel();
			}
			//если галочка опроса нажата
			if (check1.IsChecked == true)
			{
				//отчистка токенов
				cts1.Clear();
				var dueTime = TimeSpan.FromSeconds(0);
				var interval = TimeSpan.FromSeconds(20);

				cts1.Add(new CancellationTokenSource());
				RunPeriodicAsync(combo1.SelectedIndex, new Controller(), text3.Text, Convert.ToInt32(port1.Text), text4.Text, text5.Text, text1, DoSurvey, dueTime, interval, cts1.Last().Token);
			}
		}

		private void check2_Click(object sender, RoutedEventArgs e)
		{
			//Перед очередным вызовом метода, нужно вызвать токены отмены.
			foreach (var c in cts2)
			{
				c.Cancel();
			}
			//если галочка опроса нажата
			if (check2.IsChecked == true)
			{
				//отчистка токенов
				cts2.Clear();
				var dueTime = TimeSpan.FromSeconds(0);
				var interval = TimeSpan.FromSeconds(20);

				cts2.Add(new CancellationTokenSource());
				RunPeriodicAsync(combo2.SelectedIndex, new Controller(), text7.Text, Convert.ToInt32(port2.Text), text8.Text, text6.Text, text9, DoSurvey, dueTime, interval, cts2.Last().Token);
			}
		}

		private void check3_Click(object sender, RoutedEventArgs e)
		{
			//Перед очередным вызовом метода, нужно вызвать токены отмены.
			foreach (var c in cts3)
			{
				c.Cancel();
			}
			//если галочка опроса нажата
			if (check3.IsChecked == true)
			{
				//отчистка токенов
				cts3.Clear();
				var dueTime = TimeSpan.FromSeconds(0);
				var interval = TimeSpan.FromSeconds(20);

				cts3.Add(new CancellationTokenSource());
				RunPeriodicAsync(combo3.SelectedIndex, new Controller(), text12.Text, Convert.ToInt32(port3.Text), text13.Text, text11.Text, text14, DoSurvey, dueTime, interval, cts3.Last().Token);
			}
		}

		private void check4_Click(object sender, RoutedEventArgs e)
		{
			//Перед очередным вызовом метода, нужно вызвать токены отмены.
			foreach (var c in cts4)
			{
				c.Cancel();
			}
			//если галочка опроса нажата
			if (check4.IsChecked == true)
			{
				//отчистка токенов
				cts4.Clear();
				var dueTime = TimeSpan.FromSeconds(0);
				var interval = TimeSpan.FromSeconds(20);

				cts4.Add(new CancellationTokenSource());
				RunPeriodicAsync(combo4.SelectedIndex, new Controller(), text17.Text, Convert.ToInt32(port4.Text), text18.Text, text16.Text, text19, DoSurvey, dueTime, interval, cts4.Last().Token);
			}
		}

		private void check5_Click(object sender, RoutedEventArgs e)
		{
			//Перед очередным вызовом метода, нужно вызвать токены отмены.
			foreach (var c in cts5)
			{
				c.Cancel();
			}
			//если галочка опроса нажата
			if (check5.IsChecked == true)
			{
				//отчистка токенов
				cts5.Clear();
				var dueTime = TimeSpan.FromSeconds(0);
				var interval = TimeSpan.FromSeconds(20);

				cts5.Add(new CancellationTokenSource());
				RunPeriodicAsync(combo5.SelectedIndex, new Controller(), text22.Text, Convert.ToInt32(port5.Text), text23.Text, text21.Text, text24, DoSurvey, dueTime, interval, cts5.Last().Token);
			}
		}

		private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
		{

		}
	}
}