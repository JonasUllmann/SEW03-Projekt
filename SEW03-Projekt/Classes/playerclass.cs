
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
        private int angle;
        private int power;
        private Point playerpos;
        


        public Point Playerpos { get => playerpos; set => playerpos = value; }
        public int Angle { get => angle; set => angle = value; }
        public int Power { get => power; set => power = value; }

        public Playerobject(string name, int health, int maxhealth, Point playerpos) 
        { 
        
            this.name = name;
            this.health = health;
            this.maxhealth = maxhealth;
            this.playerpos = playerpos;
            angle = 45;
            power = 50;
            
        
        }

        public void useammo(int ammotype)
        {
            ammocount[ammotype]--;
        }




    }

}
