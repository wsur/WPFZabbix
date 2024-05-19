using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFZabbix
{
	internal class Privacy : IPrivacyProvider
	{
		public OctetString Salt => throw new NotImplementedException();

		public IAuthenticationProvider AuthenticationProvider => throw new NotImplementedException();

		public ICollection<OctetString>? EngineIds => throw new NotImplementedException();

		public ISnmpData Decrypt(ISnmpData data, SecurityParameters parameters)
		{
			throw new NotImplementedException();
		}

		public ISnmpData Encrypt(ISnmpData data, SecurityParameters parameters)
		{
			throw new NotImplementedException();
		}

		public byte[] PasswordToKey(byte[] secret, byte[] engineId)
		{
			throw new NotImplementedException();
		}
	}
}
