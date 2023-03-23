using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using TankGame1.Properties;
namespace TankGame1
{
    class SingleObject
    {
        private SingleObject()
        { }

        public static SingleObject _singleObject = null;


        public static SingleObject GetSingle()
        {
            if (_singleObject == null)
            {
                _singleObject = new SingleObject();
            }
            return _singleObject;
        }

        public PlayerTank PT
        {
            get;
            set;
        }

        //将我们的敌人存储到泛型集合中
        List<EnemyTank> listEnemyTank = new List<EnemyTank>();
        List<PlayerZD> listPlayerZD = new List<PlayerZD>();
        List<EnemyZD> listEnemyZD = new List<EnemyZD>();
        List<Boom> listBoom = new List<Boom>();
        List<TankBorn> listTankBorn = new List<TankBorn>();
        List<ZhuangBei> listZB = new List<ZhuangBei>();
        List<Wall> listWall = new List<Wall>();
        public void AddGameObject(GameObject go)
        {
            if (go is PlayerTank)
            {
                PT = go as PlayerTank;
            }
            else if (go is EnemyTank)
            {
                listEnemyTank.Add(go as EnemyTank);
            }
            else if (go is PlayerZD)
            {
                listPlayerZD.Add(go as PlayerZD);
            }
            else if (go is EnemyZD)
            {
                listEnemyZD.Add(go as EnemyZD);
            }
            else if (go is Boom)
            {
                listBoom.Add(go as Boom);
            }
            else if (go is TankBorn)
            {
                listTankBorn.Add(go as TankBorn);
            }
            else if (go is ZhuangBei)
            {
                listZB.Add(go as ZhuangBei);
            }
            else if(go is Wall)
            {
                listWall.Add(go as Wall);
            }
        }

        public void RemoveGameObject(GameObject go)
        {
            if (go is Boom)
            {
                listBoom.Remove(go as Boom);
            }
            if (go is PlayerZD)
            {
                listPlayerZD.Remove(go as PlayerZD);
            }
            if (go is EnemyZD)
            {
                listEnemyZD.Remove(go as EnemyZD);
            }
            if (go is EnemyTank)
            {
                listEnemyTank.Remove(go as EnemyTank);
            }
            if (go is TankBorn)
            {
                listTankBorn.Remove(go as TankBorn);
            }
            if (go is ZhuangBei)
            {
                listZB.Remove(go as ZhuangBei);
            }
            if(go is Wall)
            {
                listWall.Remove(go as Wall);
            }
        }

        public void Draw(Graphics g)
        {
            PT.Draw(g);
            for (int i = 0; i < listEnemyTank.Count; i++)
            {
                listEnemyTank[i].Draw(g);
            }
            for (int i = 0; i < listPlayerZD.Count; i++)
            {
                listPlayerZD[i].Draw(g);
            }
            for (int i = 0; i < listEnemyZD.Count; i++)
            {
                listEnemyZD[i].Draw(g);
            }
            for (int i = 0; i < listBoom.Count; i++)
            {
                listBoom[i].Draw(g);
            }
            for (int i = 0; i < listTankBorn.Count; i++)
            {
                listTankBorn[i].Draw(g);
            }
            for (int i = 0; i < listZB.Count; i++)
            {
                listZB[i].Draw(g);
            }
            for (int i = 0; i < listWall.Count; i++)
            {
                listWall[i].Draw(g);
            }
        }

        public void PZJC()
        {
            #region 首先判断玩家的子弹是否打到了敌人的身上
            for (int i = 0; i < listPlayerZD.Count; i++)
            {
                for (int j = 0; j < listEnemyTank.Count; j++)
                {
                    if (listPlayerZD[i].GetRectangle().IntersectsWith(listEnemyTank[j].GetRectangle()))
                    {
                        //表示玩家的子弹打到了敌人的身上
                        //敌人应该减少生命值
                        listEnemyTank[j].Life -= listPlayerZD[i].Power;
                        listEnemyTank[j].IsOver();
                        //当玩家坦克的子弹击中敌人坦克的时候，应该将玩家坦克子弹移除
                        listPlayerZD.Remove(listPlayerZD[i]);
                        break;
                    }
                }
            }
            #endregion

            #region 判断敌人的子弹是否打到了玩家身上
            for (int i = 0; i < listEnemyZD.Count; i++)
            {
                //敌人子弹的矩形区域和玩家的矩形区域相交了
                if (listEnemyZD[i].GetRectangle().IntersectsWith(PT.GetRectangle()))
                {
                    PT.IsOver();
                    //将敌人子弹删除
                    listEnemyZD.Remove(listEnemyZD[i]);
                    break;
                }
            }
            #endregion

            #region 玩家是否和产生的装备发生了碰撞
            for (int i = 0; i < listZB.Count; i++)
            {
                //玩家吃到了装备
                if (listZB[i].GetRectangle().IntersectsWith(PT.GetRectangle()))
                {
                    //调用JudgeZB
                    JudgeZB(listZB[i].ZBType);
                    //移除装备
                    listZB.Remove(listZB[i]);
                    //添加吃了装备的声音
                    SoundPlayer sp = new SoundPlayer(Resources.add);
                    sp.Play();
                }
            }
            #endregion

            #region 判断敌人是否和墙体发生碰撞
            for (int i = 0; i < listWall.Count; i++)
            {
                for (int j = 0; j < listEnemyTank.Count; j++)
                {
                    if (listWall[i].GetRectangle().IntersectsWith(listEnemyTank[j].GetRectangle()))
                    {
                        //当敌人和墙体发生碰撞的时候 我们应该让敌人的坐标固定到
                        //碰撞的位置，不允许穿过墙体
                        //需要判断 敌人是从哪个方向过来发生碰撞的
                        switch (listEnemyTank[j].Dir)
                        {
                            case Dirction.Up:
                                listEnemyTank[j].Y = listWall[i].Y + listWall[i].Height;
                                break;

                            case Dirction.Down:
                                listEnemyTank[j].Y = listWall[i].Y - listWall[i].Height;
                                break;
                            case Dirction.Left:
                                listEnemyTank[j].X = listWall[i].X + listWall[i].Width;
                                break;
                            case Dirction.Right:
                                listEnemyTank[j].X = listWall[i].X - listWall[i].Width;
                                break;
                        }
                    }
                }
            }
            #endregion


            #region 判断玩家的子弹是否打到了墙体
            for (int i = 0; i < listPlayerZD.Count; i++)
            {
                for (int j = 0; j < listWall.Count; j++)
                {
                    if (listPlayerZD[i].GetRectangle().IntersectsWith(listWall[j].GetRectangle()))
                    {
                        //移除玩家子弹
                        listPlayerZD.Remove(listPlayerZD[i]);
                        //移除被击中的墙体
                        listWall.Remove(listWall[j]);
                        break;
                    }
                }
            }
            #endregion

            #region 判断玩家的子弹是否和敌人的子弹相撞
            for (int i = 0; i < listPlayerZD.Count; i++)
            {
                for (int j = 0; j < listEnemyZD.Count; j++)
                {
                    if (listPlayerZD[i].GetRectangle().IntersectsWith(listEnemyZD[j].GetRectangle()))
                    {
                        listPlayerZD.Remove(listPlayerZD[i]);
                        listEnemyZD.Remove(listEnemyZD[j]);
                        break;
                    }
                }
            }
            #endregion
        }


        public void JudgeZB(int type)
        {
            switch (type)
            {
                case 0: //吃到了五角星，速度增快
                    if (PT.ZDLevel < 2)
                    {
                        PT.ZDLevel++;
                    }
                    break;
                case 1: //吃到地雷，炸掉一片敌人
                    for(int i = 0; i < listEnemyTank.Count; i++)
                    {
                        //把敌人坦克的生命值赋值为0
                        listEnemyTank[i].Life = 0;
                        //调用敌人死亡的方法
                        listEnemyTank[i].IsOver();
                    }
                    break;
                case 2: //吃到暂停
                    for(int i = 0; i < listEnemyTank.Count; i++)
                    {
                        //让标记变为false
                        listEnemyTank[i].isStop = false;
                    }
                    break;
            }


        }
    }
}
