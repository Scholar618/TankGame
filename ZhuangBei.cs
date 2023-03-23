using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using TankGame1.Properties;
namespace TankGame1
{
    class ZhuangBei : GameObject
    {
        private static Image imgStar = Resources.star;
        private static Image imgBomb = Resources.bomb;
        private static Image imgTimer = Resources.timer;

        /// <summary>
        /// 装备的类型  0 - 五角星  1 - 地雷  2 - 计时器
        /// </summary>
        public int ZBType
        {
            get;
            set;
        }

        public ZhuangBei(int x,int y, int type)
            :base(x,y,imgStar.Width, imgStar.Height)
        {
            this.ZBType = type;
        }

        public override void Draw(Graphics g)
        {
            switch(ZBType)
            {
                case 0:
                    g.DrawImage(imgStar, this.X, this.Y);
                    break;
                case 1:
                    g.DrawImage(imgBomb, this.X, this.Y);
                    break;
                case 2:
                    g.DrawImage(imgTimer, this.X, this.Y);
                    break;
            }
        }

    }
}
