using System;
using System.Collections.Generic;
using System.Text;

namespace PlushkinForms.Models
{
    class Bookmark
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public int User { get; set; }

        public override bool Equals(object obj)
        {
            Bookmark friend = obj as Bookmark;
            return Id == friend.Id;
        }

        public override int GetHashCode()
        {
            int hashCode = -778921025;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Url);
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + User.GetHashCode();
            return hashCode;
        }
    }
}
