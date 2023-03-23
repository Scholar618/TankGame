using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TankGame1.Properties;

namespace TankGame1
{
    class PlayerTank : TankFather
    {
        private static Image[] imgs = {
            Resources.p1tankU,
            Resources.p1tankD,
            Resources.p1tankL,
            Resources.p1tankR,
        };

        public int ZDLevel
        {
            get;
            set;
        }

        public PlayerTank(int x, int y, int speed, int life, Dirction dir)
            :base(x,y,imgs,speed,life,dir)
        {
            Born();
        }

        public override void Born()
        {
            SingleObject.GetSingle().AddGameObject(new TankBorn(this.X, this.Y));
        }

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    this.Dir = Dirction.Up;
                    base.Move();
                    break;
                case Keys.S:
                    this.Dir = Dirction.Down;
                    base.Move();
                    break;
                case Keys.A:
                    this.Dir = Dirction.Left;
                    base.Move();
                    break;
                case Keys.D:
                    this.Dir = Dirction.Right;
                    base.Move();
                    break;
                case Keys.J:
                    Fire();
                    break;
            }
        }
        public override void Fire()
        {
            switch (ZDLevel)
            {
                case 0:
                    SingleObject.GetSingle().AddGameObject(new PlayerZD(this, 10, 10, 1));
                        break;
                case 1:
                    SingleObject.GetSingle().AddGameObject(new PlayerZD(this, 20, 10, 1));
                        break;
                case 2:
                    SingleObject.GetSingle().AddGameObject(new PlayerZD(this, 30, 10, 1));
                        break;
            }

            SingleObject.GetSingle().AddGameObject(new PlayerZD(this, 10, 10, 1));
        }

        public override void IsOver()
        {
            SoundPlayer sp = new SoundPlayer(Resources.hit);
            sp.Play();
 //           SingleObject.GetSingle().AddGameObject(new Boom(this.X - 15, this.Y - 15));
        }

    }

 }
