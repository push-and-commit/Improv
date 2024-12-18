using Data.Interfaces;
using Data.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.People
{
    public class Audience : IDisplay
    {
        [Key]
        private int _id;
        private string _name;
        private string _description;
        private int _level;
        private int _quantity;
        private int _prizeId;
        [ForeignKey("PrizeId")]
        private Prize _prize;
        private List<Skill> _skills;

        public Audience() { }

        public Audience(string name, string description, int level, int quantity, Prize prize, List<Skill> skills)
        {
            _name = name;
            _description = description;
            _level = level;
            _quantity = quantity;
            _prize = prize;
            _skills = skills;

            _prizeId = prize.Id;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Level { get => _level; set => _level = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public virtual int PrizeId { get => _prizeId; set => _prizeId = value; }
        public virtual Prize Prize { get => _prize; set => _prize = value; }
        public virtual List<Skill> Skills { get => _skills; set => _skills = value; }

        public void DisplaySelf()
        {
            Console.WriteLine($"Name : {_name}");
            Console.WriteLine($"Description : {_description}");
            Console.WriteLine($"Level : {_level}");
            Console.WriteLine($"Number of people in the audience : {_quantity}");
            Console.WriteLine($"Prize : {_prize.Money} Improv Coins and {_prize.Experience} points of experience");
            Console.WriteLine($"Skills :");
            foreach (Skill skill in _skills)
            {
                Console.WriteLine(skill.Name);
            }
        }
    }
}
