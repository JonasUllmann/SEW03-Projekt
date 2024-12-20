
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SEW03_Projekt.Classes
{

    public class playerobject
    {

        private int health;
        private int maxhealth;
        private string name;
        private int stamina;
        private List<int> ammocount;

        public playerobject(string name, int health, int maxhealth) 
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
