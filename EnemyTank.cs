using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankGame1.Properties;
using System.Drawing;
using System.Media;
namespace TankGame1
{
    class EnemyTank : TankFather
    {
        private static Image[] imgs1 = {
                                       Resources.enemy1U,
                                       Resources.enemy1D,
                                       Resources.enemy1L,
                                       Resources.enemy1R
                                       };
        private static Image[] imgs2 = {
                                       Resources.enemy2U,
                                       Resources.enemy2D,
                                       Resources.enemy2L,
                                       Resources.enemy2R
                                       };
        private static Image[] imgs3 = {
                                       Resources.enemy3U,
                                       Resources.enemy3D,
                                       Resources.enemy3L,
                                       Resources.enemy3R
                                       };

        //存储敌人坦克的速度
        private static int _speed;
        //存储敌人坦克的生命
        private static int _life;

        public int EnemyTankType
        {
            get;
            set;
        }


        /// <summary>
        /// 通过一个静态方法设置敌人坦克的速度
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int SetSpeed(int type)
        {
            switch (type)
            {
                case 0:
                    _speed = 5;
                    break;
                case 1:
                    _speed = 6;
                    break;
                case 2:
                    _speed = 7;
                    break;
            }
            return _speed;
        }


        /// <summary>
        /// 通过一个静态方法设置敌人坦克的生命
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int SetLife(int type)
        {
            switch (type)
            {
                case 0:
                    _life = 1;
                    break;
                case 1:
                    _life = 2;
                    break;
                case 2:
                    _life = 3;
                    break;
            }
            return _life;
        }

        public EnemyTank(int x,int y, int type,Dirction dir)
            : base(x, y, imgs1, SetSpeed(type), SetLife(type), dir)
        {
            this.EnemyTankType = type;
            Born();
        }


        public bool isStop = true;
        public int stopTime = 0;
        public override void Draw(Graphics g)
        {
            bornTime++;
            if (bornTime % 20 == 0)
            {
                //敌人坦克绘制出来
                isMove = true;
            }
            if (isMove)
            {
                if(isStop)
                {
                    Move();
                }
                else
                {
                    stopTime++;
                    if (stopTime % 50 == 0)
                    {
                        isStop = true;
                    }
                }
                switch (EnemyTankType)
                {
                    case 0:
                        switch (this.Dir)
                        {
                            case Dirction.Up:
                                g.DrawImage(imgs1[0], this.X, this.Y);
                                break;
                            case Dirction.Down:
                                g.DrawImage(imgs1[1], this.X, this.Y);
                                break;
                            case Dirction.Left:
                                g.DrawImage(imgs1[2], this.X, this.Y);
                                break;
                            case Dirction.Right:
                                g.DrawImage(imgs1[3], this.X, this.Y);
                                break;
                        }
                        break;
                    case 1:
                        switch (this.Dir)
                        {
                            case Dirction.Up:
                                g.DrawImage(imgs2[0], this.X, this.Y);
                                break;
                            case Dirction.Down:
                                g.DrawImage(imgs2[1], this.X, this.Y);
                                break;
                            case Dirction.Left:
                                g.DrawImage(imgs2[2], this.X, this.Y);
                                break;
                            case Dirction.Right:
                                g.DrawImage(imgs2[3], this.X, this.Y);
                                break;
                        }
                        break;
                    case 2:
                        switch (this.Dir)
                        {
                            case Dirction.Up:
                                g.DrawImage(imgs3[0], this.X, this.Y);
                                break;
                            case Dirction.Down:
                                g.DrawImage(imgs3[1], this.X, this.Y);
                                break;
                            case Dirction.Left:
                                g.DrawImage(imgs3[2], this.X, this.Y);
                                break;
                            case Dirction.Right:
                                g.DrawImage(imgs3[3], this.X, this.Y);
                                break;
                        }
                        break;
                }
                if (r.Next(0, 100) < 3)
                    Fire();
            }
           
        }

        public override void Fire()
        {
            SingleObject.GetSingle().AddGameObject(new EnemyZD(this, 10, 10, 1));
        }

        public override void IsOver()
        {
            if (this.Life == 0)
            {
                //出现爆炸的图片
                SingleObject.GetSingle().AddGameObject(new Boom(this.X - 15, this.Y - 15));
                //被击中了将其删掉
                SingleObject.GetSingle().RemoveGameObject(this);
                //播放坦克爆炸的声音
                SoundPlayer sp = new SoundPlayer(Resources.fire);
                sp.Play();
                //当敌人坦克死亡的时候，会有一定几率重生坦克
                if (r.Next(0, 100) >= 80)
                {
                    SingleObject.GetSingle()
                        .AddGameObject(new EnemyTank(r.Next(0, 700), r.Next(0, 600), r.Next(0, 3), Dirction.Down));
                }
                //一定的几率产生装备
                if (r.Next(0, 100) >= 60)
                {
                    SingleObject.GetSingle().AddGameObject(new ZhuangBei(this.X, this.Y, r.Next(0, 3)));
                }

            }
            else//敌人被击中但没死亡
            {
                SoundPlayer sp = new SoundPlayer(Resources.hit);
                sp.Play();
            }
        }

        /// <summary>
        /// 敌人坦克出生的方法
        /// </summary>
        public override void Born()
        {
            SingleObject.GetSingle().AddGameObject(new TankBorn(this.X, this.Y));
        }


        static Random r = new Random();
        /// <summary>
        /// 当敌人
        /// </summary>
        public override void Move()
        {
            base.Move();
            //给一个很小的概率 产生随机数
            if (r.Next(0, 100) < 10)
            {
                switch (r.Next(0, 4))
                {
                    case 0:
                        this.Dir = Dirction.Up;
                        break;
                    case 1:
                        this.Dir = Dirction.Down;
                        break;
                    case 2:
                        this.Dir = Dirction.Left;
                        break;
                    case 3:
                        this.Dir = Dirction.Right;
                        break;
                }
            }
        }
    }
}
