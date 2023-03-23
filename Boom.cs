using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using TankGame1.Properties;

namespace TankGame1
{
    class Boom : GameObject
    {
        //导入图片资源
        private Image[] imgs =
        {
            Resources.blast1,
            Resources.blast2,
            Resources.blast3,
            Resources.blast4,
            Resources.blast5,
            Resources.blast6,
            Resources.blast7,
            Resources.blast8,
        };

        public Boom(int x, int y):base(x, y)
        {

        }

        public override void Draw(Graphics g)
        {
            for(int i = 0; i<imgs.Length; i++)
            {
                g.DrawImage(imgs[i], this.X, this.Y);
            }
            //爆炸图片完成就销毁
            SingleObject.GetSingle().RemoveGameObject(this);
        }
    }
}
