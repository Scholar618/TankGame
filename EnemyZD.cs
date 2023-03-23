using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankGame1.Properties;

namespace TankGame1
{
    class EnemyZD : ZiDanFather
    {
        private static Image img = Resources.tankmissile;
        public EnemyZD(TankFather tf, int speed, int life, int power) : base(tf, speed, life, power, img)
        {

        }
    }
}
