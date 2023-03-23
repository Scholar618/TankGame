using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TankGame1.Properties;

namespace TankGame1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitialGame();
        }

        /// <summary>
        /// 初始化游戏
        /// </summary>
        private void InitialGame()
        {
            SingleObject.GetSingle().AddGameObject(new PlayerTank(200, 200, 10, 10, Dirction.Up));
            SetEnemyTanks();
            InitialMap();
        }
        Random r = new Random();

        /// <summary>
        /// 
        /// </summary>
        public void SetEnemyTanks()
        {
            for(int i = 0; i< 8; i++)
            {
                SingleObject.GetSingle().AddGameObject(new EnemyTank(r.Next(0, this.Width), r.Next(0, this.Height),
                    r.Next(0, 3), Dirction.Down));
            }
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            SingleObject.GetSingle().Draw(e.Graphics);
        }

        /// <summary>
        /// 初始化游戏地图
        /// </summary>
        public void InitialMap()
        {
            for (int i = 0; i < 10; i++)
            {
                SingleObject.GetSingle().AddGameObject(new Wall(i * 15 + 30, 100));
                SingleObject.GetSingle().AddGameObject(new Wall(95, 100 + 15 * i));

                SingleObject.GetSingle().AddGameObject(new Wall(245 - i * 7, 100 + 15 * i));
                SingleObject.GetSingle().AddGameObject(new Wall(245 + i * 7, 100 + 15 * i));
                SingleObject.GetSingle().AddGameObject(new Wall(215 + i * 15 / 2, 185));

                SingleObject.GetSingle().AddGameObject(new Wall(390 - i * 5, 100 + 15 * i));
                SingleObject.GetSingle().AddGameObject(new Wall(390 + i * 5, 100 + 15 * i));
                SingleObject.GetSingle().AddGameObject(new Wall(480 - i * 5, 100 + 15 * i));

                SingleObject.GetSingle().AddGameObject(new Wall(515, 100 + 15 * i));
                SingleObject.GetSingle().AddGameObject(new Wall(595 - i * 8, 100 + 15 * i / 2));
                SingleObject.GetSingle().AddGameObject(new Wall(530 + i * 8, 165 + 15 * i / 2));
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //让坦克不闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                | ControlStyles.AllPaintingInWmPaint, true);

            //在程序加载时播放开始的音乐
            SoundPlayer sp = new SoundPlayer(Resources.start);
            sp.Play();
        }

        /// <summary>
        /// 当键盘按下的时候会触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            SingleObject.GetSingle().PT.KeyDown(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
            //碰撞检测
            SingleObject.GetSingle().PZJC();
        }
    }
}
