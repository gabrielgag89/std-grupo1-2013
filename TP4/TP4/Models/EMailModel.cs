using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP4.Models
{
	public class EMailModel
	{
		public int Numero { get; set; }
		public string Remitente { get; set; }
		public string FechaHora { get; set; }
		public string Corriente { get; set; }
		public string Potencia { get; set; }
		public string Presion { get; set; }
		public string Temperatura { get; set; }
		public string Tension { get; set; }
	}
}