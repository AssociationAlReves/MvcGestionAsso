using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TreeUtility;

namespace MvcGestionAsso.Models
{
	public class CategorieActivite : ITreeNode<CategorieActivite>
	{
		public int Id { get; set; }

		private int? _parentId;
		[Display(Name = "Catégorie parente")]
		public int? ParentId
		{
			get { return _parentId; }
			set
			{
				if (Id == value)
					throw new InvalidOperationException("Une catégorie ne peut être son propre parent.");

				_parentId = value;
			}
		}

		[Required(ErrorMessage = "Vous devez saisir un nom de catégorie.")]
		[StringLength(50, ErrorMessage = "Le nom de la catégorie doit comporter moins de 50 caractères.")]
		[Display(Name = "Catégorie")]    
		public string CategorieActiviteNom { get; set; }

		public virtual List<Activite> Activites { get; set; }

		public virtual CategorieActivite Parent { get; set; }

		public IList<CategorieActivite> Children { get; set; }
	}
}
