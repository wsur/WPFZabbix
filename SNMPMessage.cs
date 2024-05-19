using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFZabbix
{
	internal class SNMPMessage : ISnmpMessage
	{
		public Header Header => throw new NotImplementedException();

		public SecurityParameters Parameters => throw new NotImplementedException();

		public Scope Scope => throw new NotImplementedException();

		public VersionCode Version => throw new NotImplementedException();

		public IPrivacyProvider Privacy => throw new NotImplementedException();

		public byte[] ToBytes()
		{
			throw new NotImplementedException();
		}
	}
}
