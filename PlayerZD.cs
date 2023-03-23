using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using TankGame1.Properties;

namespace TankGame1
{
    class PlayerZD : ZiDanFather
    {
        private static Image img = Resources.tankmissile;
        public PlayerZD(TankFather tf, int speed, int life, int power):base(tf,speed, life,power,img)
        {
            this.Power = power;
        }

    }

}
