using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace WPFZabbix
{
	internal class Controller
	{
		public string firstElement = "None";

		//буффер для выдачи результата с методов
		private StringBuilder sb = new StringBuilder("");
		private static List<Variable> BulkWalkResult = new List<Variable>();

		private ISnmpMessage? report;
		OctetString octetString = new OctetString("Huawei S5700 switch");//по умолчанию
		VersionCode version = VersionCode.V2;
		string community = "public";
		ObjectIdentifier test = new ObjectIdentifier("1.3.6.1.2.1.2.2.1.1");//по умолчанию
		int timeout = 1000;
		WalkMode mode = WalkMode.WithinSubtree;
		int maxRepetitions = 10;

		/// <summary>
		/// Выполнение Snmp-метода GET. Результат можно получить, вызвав метод GetString()
		/// </summary>
		/// <param name="ip"> ip-адрес подключения</param>
		/// <param name="oid"> опрашиваемый OID</param>
		public void Get(string ip, string oid)
		{
			try
			{
				var result = Messenger.Get(VersionCode.V1,
							   new IPEndPoint(IPAddress.Parse(ip), 161),
							   new OctetString("public"),
							   new List<Variable> { new Variable(new ObjectIdentifier(oid)) },
							   6000);
				int count = result.Count;//количество переданных элементов
				for (int i = 0; i < count; i++)
				{
					sb.Append(result[i].ToString());
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Выполнение SNMP-метода BulkWalk
		/// </summary>
		/// <param name="ipAddr">ip-адрес</param>
		/// <param name="oid">опрашиваемый OID</param>
		/// <param name="contextName">Имя устройства (контекста)</param>
		public void BulkWalk(string ipAddr, string oid, string contextName)
		{
			try
			{
				IPAddress ip = IPAddress.Parse(ipAddr);
				IPEndPoint receiver = new IPEndPoint(ip, 161);
				test = new ObjectIdentifier(oid);//задание oid
				octetString = new OctetString(contextName);//задание имени контекста

				//Messenger.Walk(version, receiver, new OctetString("public"), new ObjectIdentifier("1.3.6.1.2.1.2.1.0"), BulkWalkResult, 10000, WalkMode.WithinSubtree);
				if (version == VersionCode.V1)
				{
					Messenger.Walk(version, receiver, new OctetString(community), test, BulkWalkResult, timeout, mode);
				}
				else if (version == VersionCode.V2)
				{
					Messenger.BulkWalk(version, receiver, new OctetString(community), octetString, test, BulkWalkResult, timeout, maxRepetitions, mode, null, null);
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}	
		}

		/// <summary>
		/// Выдаёт результат метода Get и отчищает буффер
		/// </summary>
		/// <returns></returns>
		public string GetString()
		{
			string result = sb.ToString();
			sb.Clear();
			return result;
		}
		
		/// <summary>
		/// Выдаёт результат работы SNMP-метода BulkWalk()
		/// </summary>
		/// <returns></returns>
		public List<Variable> BulkWalkList()
		{
			List<Variable> result = BulkWalkResult.ToList();//копирую содержимое, а не ссылку на содержимое, т.к. это классы и мне нужно работать с ними отдельно
			BulkWalkResult.Clear();
			return result;
		}
	}
}
