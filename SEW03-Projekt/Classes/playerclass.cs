
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEW03_Projekt.Classes
{

    public class Playerobject
    {

        private int health;
        private int maxhealth;
        private string name;
        private int stamina;
        private List<int> ammocount;

        private int playerxpos;
        private int playerypos;


        public int Playerxpos { get => playerxpos; set => playerxpos = value; }
        public int Playerypos { get => playerypos; set => playerypos = value; }

        public Playerobject(string name, int health, int maxhealth) 
        { 
        
            this.name = name;
            this.health = health;
            this.maxhealth = maxhealth;
            
        
        }

        public void useammo(int ammotype)
        {
            ammocount[ammotype]--;
        }




    }

}
