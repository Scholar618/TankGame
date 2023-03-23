using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame1
{

    enum Dirction
    {
        Up,
        Down,
        Left,
        Right
    }

    abstract class GameObject
    {

        #region 游戏对象的属性
        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public int Speed
        {
            get;
            set;
        }

        public int Life
        {
            get;
            set;
        }

        public Dirction Dir
        {
            get;
            set;
        }
        #endregion

        //初始化对象
        public GameObject(int x, int y, int width, int height, int speed, int life, Dirction dir)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Speed = speed;
            this.Life = life;
            this.Dir = dir;
        }

        public abstract void Draw(Graphics g);

        /// <summary>
        /// 游戏对象移动的方法 我们在移动的时候 根据当前游戏对象的方向
        /// 进行移动
        /// </summary>
        public virtual void Move()
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
                this.X = 0;
            }
            if (this.Y <= 0)
            {
                this.Y = 0;
            }
            if (this.X >= 530)
            {
                this.X = 530;
            }
            if (this.Y >= 470)
            {
                this.Y = 470;
            }


        }


        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }

        public GameObject(int x, int y, int width, int height):this
            (x, y, width, height, 0, 0, 0)
        {

        }

        public GameObject(int x, int y):this(x,y,0,0,0,0,0)
        {

        }

    }
}
