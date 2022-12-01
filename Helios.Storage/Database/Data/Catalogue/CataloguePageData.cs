using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helios.Storage.Database.Data
{
    public class CataloguePageData
    {
        public virtual int Id { get; set; }
        public virtual int ParentId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual string Caption { get; set; }
        public virtual string PageLink { get; set; }
        public virtual int MinRank { get; set; }
        public virtual int IconColour { get; set; }
        public virtual int IconImage { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual bool IsNavigatable { get; set; }
        public virtual bool IsClubOnly { get; set; }
        public virtual string Layout { get; set; }
        public virtual string ImagesData { get; set; }
        public virtual string TextsData { get; set; }

        [NotMapped]
        public virtual string Images { get; set; }

        [NotMapped]
        public virtual string Texts { get; set; }
    }
}
