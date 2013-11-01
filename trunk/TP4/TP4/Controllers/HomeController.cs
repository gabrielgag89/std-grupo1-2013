using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml;
using System.Web;
using System.Web.Mvc;
using TP4.Models;

namespace TP4.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "TP 4";

			ViewData["Refresco"] = ConfigurationManager.AppSettings["Refresco"];

			return View();
		}

		[HttpPost]
		public JsonResult GetEMails(string remitente = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
		{
			try
			{
				List<EMailModel> emails = new List<EMailModel>();
				EMailModel email;
				string fechaHora;
				int count = 1;

				XmlReader xmlReader = XmlReader.Create(ConfigurationManager.AppSettings["DirArchivo"]);

				xmlReader.ReadToFollowing("RECEPCION");
				xmlReader.MoveToFirstAttribute();
				string[] fechaRecepcion = xmlReader.Value.Split('/');

				DateTime dtFechaRecepcion = DateTime.Parse(string.Format("{0}/{1}/20{2}",
																		 fechaRecepcion[0],
																		 fechaRecepcion[1],
																		 fechaRecepcion[2]));

				xmlReader.ReadToFollowing("EMAILS");

				while (xmlReader.ReadToFollowing("EMAIL"))
				{
					email = new EMailModel { Numero = count++ };

					xmlReader.ReadToFollowing("Remitente");
					email.Remitente = xmlReader.ReadElementContentAsString();

					xmlReader.ReadToFollowing("FechaHora");
					fechaHora = xmlReader.ReadElementContentAsString();
					email.FechaHora = string.Format("{0}-{1}-{2} {3}:{4}:{5}",
													fechaHora.Substring(0, 4),
													fechaHora.Substring(4, 2),
													fechaHora.Substring(6, 2),
													fechaHora.Substring(8, 2),
													fechaHora.Substring(10, 2),
													fechaHora.Substring(12, 2));

					xmlReader.ReadToFollowing("Corriente");
					email.Corriente = xmlReader.ReadElementContentAsString();

					xmlReader.ReadToFollowing("Potencia");
					email.Potencia = xmlReader.ReadElementContentAsString();

					xmlReader.ReadToFollowing("Presion");
					email.Presion = xmlReader.ReadElementContentAsString();

					xmlReader.ReadToFollowing("Temperatura");
					email.Temperatura = xmlReader.ReadElementContentAsString();

					xmlReader.ReadToFollowing("Tension");
					email.Tension = xmlReader.ReadElementContentAsString();

					emails.Add(email);
				}

				xmlReader.Close();

				return Json(new { Result = "OK", Records = emails.Where(e => e.Remitente.Contains(remitente)).ToList(), TotalRecordCount = emails.Count });
			}
			catch (Exception ex)
			{
				return Json(new { Result = "ERROR", Message = ex.Message });
			}
		}
	}
}