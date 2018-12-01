﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using AngleSharp;

namespace VkBot
{
	class Spot
	{
		public long Id { get; set; }

		public string Description { get; set; }

		public string City { get; set; }

		/// <summary>
		/// широта; долгота
		/// </summary>
		public Point Adress { get; set; }

		public Url Photo { get; set; }

		#region Overrides of Object

		public override string ToString()
		{
			return "ID: "+ Id + "\nГород: " + City + "\nАдрес: "+Adress+"\nОписание: "+Description+"\nФотка: "+Photo;
		}

		#endregion
	}
}
