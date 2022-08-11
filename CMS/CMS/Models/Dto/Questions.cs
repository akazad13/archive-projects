using System;
using System.Xml.Serialization;

namespace CMS.Models.Dto
{
    [Serializable]
    [XmlRoot("Products"), XmlType("Products")]
    public class Questions
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Answer { get; set; }
    }
}
