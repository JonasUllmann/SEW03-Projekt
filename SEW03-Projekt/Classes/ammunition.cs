using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEW03_Projekt.Classes
{
    public class ammunition
    {

        public string name;
        public int radius;
        public int damage;
        public int weight;

        public ammunition(string name, int radius, int damage, int weight)
        {
            this.name = name;
            this.radius = radius;
            this.damage = damage;
            this.weight = weight;
        }   
    }
}
