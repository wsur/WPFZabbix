using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ZabbixApi.Entities;
using ZabbixApi;

namespace WPFZabbix
{
	internal class ZabbixController
	{
		public string? Func()
		{
			using (var context = new Context("192.168.1.101", "newUser", "abc12345"))
			{
				var host = context.Items.Get();
				Console.WriteLine(host);
				return host.ToString();
			}
		}

	}
}
