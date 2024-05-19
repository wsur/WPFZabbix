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
			/*ZabbixController controller = new ZabbixController();
			text1.Text = controller.Func();*/
			Controller controller = new Controller();
			var result = Messenger.Get(VersionCode.V1,
						   new IPEndPoint(IPAddress.Parse("127.0.0.1"), 161),
						   new OctetString("public"),
						   new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.1.0")) },
						   60000);
			int count = result.Count;//количество переданных элементов
			for(int i = 0; i < count; i++)
			{
				controller.sb.Append(result[i].ToString());
			}
			text1.Text = controller.sb.ToString();

			var r = controller.BulkWalk();
			text2.Text = r.Count.ToString();
			for (int i = 0; i < r.Count; i++)
			{
				text2.Text += " " + r[i].ToString();
			}
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}
	}
}