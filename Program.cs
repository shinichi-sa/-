class Program
{
    static int[] Maps = new int[100];//静态字段模拟全局变量
    static int[] PlayerPos = new int[2];//声明静态数组储存玩家坐标  
    static string[] PlayerNames = new string[2];//存储俩玩家的姓名
    static bool[] Flags = new bool[2];//两个玩家的标记 //Flags[0]默认是false 
    static void Main()
    {
        GameShow();
        #region 玩家姓名
        Console.WriteLine("请输入玩家A的姓名");
        PlayerNames[0] = Console.ReadLine();
        while (PlayerNames[0] == "")
        {
            Console.WriteLine("玩家A的姓名不能为空 ，请重新输入");
            PlayerNames[0] = Console.ReadLine();
        }
        Console.WriteLine("请输入玩家B的姓名");
        PlayerNames[1] = Console.ReadLine();
        while (PlayerNames[1] == "" || PlayerNames[1] == PlayerNames[0])
        {
            if (PlayerNames[1] == "")
            {
                Console.WriteLine("玩家B的姓名不能为空 ，请重新输入");
                PlayerNames[1] = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("玩家B的姓名不能和玩家A的姓名相同 ，请重新输入");
                PlayerNames[1] = Console.ReadLine();
            }
        }
        #endregion
        //玩家姓名输入完毕，首先清屏
        Console.Clear();
        GameShow();
        Console.WriteLine("{0}的士兵用A表示", PlayerNames[0]);
        Console.WriteLine("{0}的士兵用B表示", PlayerNames[1]);
        InitailMap();
        DrawMap();
        while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
        {
            if (Flags[0] == false)
            {
                PlayGame(0);
            }
            else
            {
                Flags[0] = false;
            }
            if(PlayerPos [0] >= 99)
            {
                Console.WriteLine();
                Console.WriteLine("{0}无耻的赢了{1}",PlayerNames[0],PlayerNames[1]);
                break; 
            }
            if (Flags[1] == false)
            {
                PlayGame(1);
            }else
            {
                Flags[1] = false;
            }
            if (PlayerPos[1] >= 99)
            {
                Console.WriteLine();
                Console.WriteLine("{0}无耻的赢了{1}", PlayerNames[1], PlayerNames[0]);
                break;
            }
        }      
        Console.ReadKey(); Win();
    }
    public static void Win()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                                          ◆                      ");
        Console.WriteLine("                    ■                  ◆               ■        ■");
        Console.WriteLine("      ■■■■  ■  ■                ◆■         ■    ■        ■");
        Console.WriteLine("      ■    ■  ■  ■              ◆  ■         ■    ■        ■");
        Console.WriteLine("      ■    ■ ■■■■■■       ■■■■■■■   ■    ■        ■");
        Console.WriteLine("      ■■■■ ■   ■                ●■●       ■    ■        ■");
        Console.WriteLine("      ■    ■      ■               ● ■ ●      ■    ■        ■");
        Console.WriteLine("      ■    ■ ■■■■■■         ●  ■  ●     ■    ■        ■");
        Console.WriteLine("      ■■■■      ■             ●   ■   ■    ■    ■        ■");
        Console.WriteLine("      ■    ■      ■            ■    ■         ■    ■        ■");
        Console.WriteLine("      ■    ■      ■                  ■               ■        ■ ");
        Console.WriteLine("     ■     ■      ■                  ■           ●  ■          ");
        Console.WriteLine("    ■    ■■ ■■■■■■             ■              ●         ●");
        Console.ResetColor();
    }
    public static void PlayGame(int playerNumber)
    {
        Random r = new Random();
        int rNumber = r.Next(1, 7);
        Console.WriteLine();
        Console.WriteLine("{0}按任意键开始掷骰子", PlayerNames[playerNumber]);
        Console.ReadKey(true);
        Console.WriteLine("{0}掷骰子出了{1}", PlayerNames[playerNumber], rNumber);
        PlayerPos[playerNumber] += rNumber;
        ChangePos();
        Console.ReadKey(true);
        //Console.WriteLine("{0}按任意键开始行动", PlayerNames[playerNumber]);
        //Console.ReadKey(true);
        //Console.WriteLine("{0}行动完了", PlayerNames[playerNumber]);
        //Console.ReadKey(true);
        if (PlayerPos[playerNumber] == PlayerPos[1 - playerNumber])
        {
            Console.WriteLine("玩家{0}踩到了玩家{1}，玩家{2}退6格", PlayerNames[playerNumber], PlayerNames[1 - playerNumber], PlayerNames[1 - playerNumber]);
            PlayerPos[1 - playerNumber] -= 6;
            ChangePos();
            Console.ReadKey(true);
        }
        else
        {
            switch (Maps[PlayerPos[playerNumber]])
            {
                case 0:
                    Console.WriteLine("玩家{0}踩到了方块，安全。", PlayerNames[playerNumber]);
                    Console.ReadKey(); break;
                case 1:
                    Console.WriteLine("玩家{0}踩到了幸运轮盘，请选择1--交换位置，2--轰炸对方。", PlayerNames[playerNumber]);
                    string input = Console.ReadLine();
                    while (true)
                    {
                        if (input == "1")
                        {
                            Console.WriteLine("玩家{0}选择和玩家{1}交换位置", PlayerNames[playerNumber], PlayerNames[1 - playerNumber]);
                            Console.ReadKey(true);
                            int temp = PlayerPos[playerNumber];
                            PlayerPos[playerNumber] = PlayerPos[1 - playerNumber];
                            PlayerPos[1 - playerNumber] = temp;
                            Console.WriteLine("交换完成，按任意键继续游戏");
                            Console.ReadKey(true);
                            break;
                        }
                        else if (input == "2")
                        {
                            Console.WriteLine("玩家{0}选择轰炸玩家{1}，玩家{2}退六格", PlayerNames[playerNumber], PlayerNames[1 - playerNumber], PlayerNames[1 - playerNumber]);
                            Console.ReadKey(true);
                            PlayerPos[1 - playerNumber] -= 6;
                            //ChangePos();
                            Console.WriteLine("玩家{0}行动完了", PlayerNames[1 - playerNumber]);
                            Console.ReadKey(true);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("只能输入1--交换位置，2--轰炸对方");
                            input = Console.ReadLine();
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine("玩家{0}踩到了地雷，退6格", PlayerNames[playerNumber]);
                    Console.ReadKey(true);
                    PlayerPos[playerNumber] -= 6;
                    ChangePos();

                    break;
                case 3:
                    Console.WriteLine("玩家{0}踩到了暂停，暂停一回合", PlayerNames[playerNumber]);
                    Flags[playerNumber] = true;
                    Console.ReadKey(true);

                    break;
                case 4:
                    Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", PlayerNames[playerNumber]);
                    PlayerPos[playerNumber] += 10;
                    ChangePos();
                    Console.ReadKey(true);
                    break;
            }
        }
        ChangePos();
        Console.Clear();
        DrawMap();
    }//玩游戏主程序
    public static void DrawMap()//地图
    {
        Console.WriteLine("图例:幸运轮盘:◎   地雷:☆   暂停:▲   时空隧道:卐");
        for (int i = 0; i < 30; i++)//第一行
        {
            Console.Write(DrawStringMap(i));
        }
        Console.WriteLine();//画完第一横行需要换行
        for (int i = 30; i < 35; i++)   //第二行
        {
            for (int j = 0; j <= 28; j++)
            {
                Console.Write("  ");
            }
            Console.Write(DrawStringMap(i));
            Console.WriteLine();
        }
        for (int i = 64; i >= 35; i--)
        {
            Console.Write(DrawStringMap(i));
        }
        Console.WriteLine();
        for (int i = 65; i <= 69; i++)
        {
            Console.WriteLine(DrawStringMap(i));
        }
        for (int i = 70; i < 100; i++)
        {
            Console.Write(DrawStringMap(i));
        }
    }
    public static string DrawStringMap(int i)
    {
        string str = "";
        if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == i)
        //如果玩家A和玩家B坐标相同,并且都在地图上，画一个括号
        {
            str = "<>";
        }
        else if (PlayerPos[0] == i)
        {
            str = "A";

        }
        else if (PlayerPos[1] == i)
        {
            str = "B";
        }
        else
        {
            switch (Maps[i])
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Green;
                    str = "□";
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    str = "◎";
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    str = "☆";
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    str = "▲";
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    str = "卐";
                    break;
            }


        }//第一行
        return str;
    }//玩家相同位置
    public static void InitailMap()//道具
    {
        int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//幸运轮盘◎
        for (int i = 0; i < luckyturn.Length; i++)
        {
            //int index = luckyturn[i];
            Maps[luckyturn[i]] =1;
        }

        int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷☆
        for (int i = 0; i < landMine.Length; i++)
        {
            Maps[landMine[i]] = 2;
        }

        int[] pause = { 9, 27, 60, 93 };//暂停▲
        for (int i = 0; i < pause.Length; i++)
        {
            Maps[pause[i]] = 3;
        }

        int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//时空隧道卐
        for (int i = 0; i < timeTunnel.Length; i++)
        {
            Maps[timeTunnel[i]] =4;
        }
    }
    public static void GameShow()//游戏头
    {

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("******************");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("******************");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("******************");
        Console.WriteLine("****飞行器v1.0****");
        Console.WriteLine("******************");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("******************");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("******************");
    }
    public static void ChangePos()
    {
        if (PlayerPos[0] < 0)
        {
            PlayerPos[0] = 0;
        }
        if (PlayerPos[0] > 99)
        {
            PlayerPos[0] = 99;
        }
        if (PlayerPos[1] < 0)
        {
            PlayerPos[1] = 0;
        }
        if (PlayerPos[1] > 99)
        {
            PlayerPos[1] = 99;
        }
    }//当玩家发生改变的时候 ，防止玩家出地图
}//月总爱您（ლ(⌒▽⌒ლ)）