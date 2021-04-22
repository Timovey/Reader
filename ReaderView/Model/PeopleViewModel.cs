using System.ComponentModel;

namespace ReaderView.Model
{
    public class PeopleViewModel
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        public string Surname { get; set; }

    }
}
