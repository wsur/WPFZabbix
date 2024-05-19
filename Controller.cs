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
using static System.Net.Mime.MediaTypeNames;

namespace WPFZabbix
{
	internal class Controller
	{
		public string firstElement = "None";
		public StringBuilder sb = new StringBuilder("");
		private ISnmpMessage? report;
		OctetString octetString = new OctetString("Huawei S5700 switch");
		VersionCode version = VersionCode.V2;
		string community = "public";
		ObjectIdentifier test = new ObjectIdentifier("1.3.6.1.2.1.2.2.1.1");
		int timeout = 1000;
		WalkMode mode = WalkMode.WithinSubtree;
		int maxRepetitions = 10;
		public List<Variable> BulkWalk()
		{
			var priv = new Privacy();
			var report = new SNMPMessage();
			var result = new List<Variable>();
			IPAddress ip = IPAddress.Parse("127.0.0.1");
			IPEndPoint receiver = new IPEndPoint(ip, 161);
			//Messenger.BulkWalk(VersionCode.V3, IPAddress.Parse("192.168.1.2"), "public", octetString, new ObjectIdentifier("1.3.6.1.2.1.1"), result, 60000, 10, WalkMode.WithinSubtree, null, report);
			/*Messenger.BulkWalk(VersionCode.V1,
							  new IPEndPoint(IPAddress.Parse("192.168.1.2"), 161),
							  new OctetString("public"),
							  octetString, // context name
							  new ObjectIdentifier("1.3.6.1.2.1.1"),
							  result,
							  60000,
							  10,
							  WalkMode.WithinSubtree,
							  new MD5AuthenticationProvider(new OctetString("admin45")),
							  report);*/
			Messenger.Walk(version, receiver, new OctetString("public"), new ObjectIdentifier("1.3.6.1.2.1.2.1.0"), result, 10000, WalkMode.WithinSubtree);
			if (version == VersionCode.V1)
			{
				Messenger.Walk(version, receiver, new OctetString(community), test, result, timeout, mode);
			}
			else if (version == VersionCode.V2)
			{
				Messenger.BulkWalk(version, receiver, new OctetString(community), octetString, test, result, timeout, maxRepetitions, mode, null, null);
			}
			return result;
		}
	}
}
