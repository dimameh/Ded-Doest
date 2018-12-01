using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace VkBot
{
	class Spot
	{
		public long Id { get; set; }

		public string Description { get; set; }

		public string City { get; set; }

		public string Adress { get; set; }

		public Bitmap Photo { get; set; }
	}
}
