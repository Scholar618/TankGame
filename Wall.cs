using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankGame1.Properties;
namespace TankGame1
{
    class Wall:GameObject
    {
        private static Image imge = Resources.wall;
        public Wall(int x,int y):base(x,y,imge.Width, imge.Height)
        {}

        public override void Draw(Graphics g)
        {
            g.DrawImage(imge, this.X, this.Y);
        }

    }
}
