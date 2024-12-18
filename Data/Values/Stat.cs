using Data.Enums;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Values
{
    public class Stat : IDisplay
    {
        [Key]
        private int _id;
        private string _name;
        private string _description;
        private StatTypeEnum _type;

        public Stat() { }

        public Stat(string name, string description, StatTypeEnum type)
        {
            _name = name;
            _description = description;
            _type = type;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public StatTypeEnum Type { get => _type; set => _type = value; }

        public void DisplaySelf()
        {
            Console.WriteLine($"Name : {_name}");
            Console.WriteLine($"Description : {_description}");
            Console.WriteLine($"Type : {_type}");
        }
    }
}
