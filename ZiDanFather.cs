using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankGame1
{
    class ZiDanFather : GameObject
    {
        private Image img;

        public Image Img
        {
            get { return img; }
            set { img = value; }
        }

        public int Power
        {
            get; set;
        }

        public ZiDanFather(TankFather tf, int speed, int life, int power, Image img)
            : base(tf.X + tf.Width / 2 - 6, tf.Y + tf.Height / 2 - 6, img.Width, img.Height, speed, life, tf.Dir)
        {
            this.img = img;
        }

        public override void Draw(Graphics g)
        {
            switch (this.Dir)
            {
                case Dirction.Up:
                    this.Y -= this.Speed;
                    break;
                case Dirction.Down:
                    this.Y += this.Speed;
                    break;
                case Dirction.Left:
                    this.X -= this.Speed;
                    break;
                case Dirction.Right:
                    this.X += this.Speed;
                    break;
            }
            //在游戏对象移动完成后 我们应该判断一下 当前游戏对象是否超出当前的窗体
            if (this.X <= 0)
            {
                this.X = -100;
            }
            if (this.Y <= 0)
            {
                this.Y = -100;
            }
            if (this.X >= 600)
            {
                this.X = 600;
            }
            if (this.Y >= 600)
            {
                this.Y = 600;
            }
            g.DrawImage(img, this.X, this.Y);
        }

    }
}
