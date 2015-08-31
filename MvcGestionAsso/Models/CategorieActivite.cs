using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreeUtility;

namespace MvcGestionAsso.Models
{
	public class CategorieActivite : ITreeNode<CategorieActivite>
	{
		public int Id { get; set; }

		public int? ParentId { get; set; }

		public string CategorieActiviteNom { get; set; }

		public virtual List<Activite> Activites { get; set; }

		public CategorieActivite Parent { get; set; }

		public IList<CategorieActivite> Children { get; set; }
	}
}
