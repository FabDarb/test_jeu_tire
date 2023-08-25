using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

namespace test_jeu_tire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int large = 30;
        int haut = 30;
        bool wallOn = false;

        char[,] laBase;
        Rectangle[,] rec;
        List<Player> playerList = new List<Player>();
        
        public MainWindow()
        {
            InitializeComponent();
            laBase = new char[large, haut];
            rec = new Rectangle[large, haut];
            maping();

        }
        public void maping()
        {
            monde.Children.Clear();
            monde.RowDefinitions.Clear();
            monde.ColumnDefinitions.Clear();


            getFill();

            for(int i = 0; i < large; i++)
            {
                monde.RowDefinitions.Add(new RowDefinition());
            }
            for(int i = 0; i < haut; i++)
            {
                monde.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for(int i = 0; i < large; i++)
            {
                for(int j = 0; j < haut; j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Height = 15;
                    rect.Width = 15;
                    switch (laBase[i, j])
                    {
                        case '#':
                            rect.Fill = new SolidColorBrush(Colors.Black);
                            
                            break;
                        case '1':
                            Player p = new Player();
                            p.pv = 100;
                            p.x = i;
                            p.y = j;
                            playerList.Add(p);
                            rect.Fill = new SolidColorBrush(Colors.Yellow);
                            break;
                        case '2':
                            Player q = new Player();
                            q.pv = 100;
                            q.x = i;
                            q.y = j;
                            playerList.Add(q);
                            rect.Fill = new SolidColorBrush(Colors.Red);
                            break;
                        default:
                            rect.Fill = new SolidColorBrush(Colors.White);
                            break;
                    }
                    Grid.SetColumn(rect, j);
                    Grid.SetRow(rect, i);
                    monde.Children.Add(rect);
                    rec[i, j] = rect;
                }
            }
        }
        public void getFill()
        {
            string[] lines = File.ReadAllLines($"./map.txt");
            int nb = 0;
            foreach(var line in lines)
            {
                int nb2 = 0;
                foreach(char lettre in line)
                {
                    laBase[nb, nb2] = lettre;
                    nb2++;
                }
                nb++;
            }
        }
        void beforeMove(Player p)
        {
            if(laBase[p.y, p.x] == '#' || laBase[p.y, p.x] == '2' || laBase[p.y, p.x] == '1')
            {
                p.y = p.laterY;
                p.x = p.laterX;
                wallOn = true;
            }
            
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Player p1 = playerList[0];
            Player p2 = playerList[1];
            keep_Last(p1);
            keep_Last(p2);
            switch (e.Key)
            {
                case Key.W:
                    p1.y--;
                    beforeMove(p1);
                    break;
                case Key.S:
                    p1.y++;
                    beforeMove(p1);
                    break;
                case Key.A:
                    p1.x--;
                    beforeMove(p1);
                    break;
                case Key.D:
                    p1.x++;
                    beforeMove(p1);
                    break;
                case Key.Up:
                    p2.y--;
                    beforeMove(p2);
                    break;
                case Key.Down:
                    p2.y++;
                    beforeMove(p2);
                    break;
                case Key.Left:
                    p2.x--;
                    beforeMove(p2);
                    break;
                case Key.Right:
                    p2.x++;
                    beforeMove(p2);
                    break;
                case Key.P:
                    Dump();
                    break;
            }
            p1.scanmoove();
            p2.scanmoove();
            laBase[p1.y, p1.x] = '1';
            laBase[p2.y, p2.x] = '2';
            if(wallOn == false)
            {
                laBase[p1.laterY, p1.laterX] = ' ';
                laBase[p2.laterY, p2.laterX] = ' ';
            }
            else
            {
                wallOn = false;
            }
            if(p1.moove == false)
            {
                laBase[p1.y, p1.x] = '1';
            }
            if(p2.moove == false)
            {
                laBase[p2.y, p2.x] = '2';
            }
            refresh();
        }

        void keep_Last(Player p)
        {
            p.laterX = p.x;
            p.laterY = p.y;
        }

        public void refresh()
        {
            for (int i = 0; i < large; i++)
            {
                for (int j = 0; j < haut; j++)
                {
                    Rectangle r = rec[i, j];
                    switch (laBase[i,j])
                    {
                        case '#':
                            r.Fill = new SolidColorBrush(Colors.Black);
                            break;
                        case ' ':
                            r.Fill = new SolidColorBrush(Colors.White);
                            break;
                        case '1':
                            r.Fill = new SolidColorBrush(Colors.Yellow);
                            break;
                        case '2':
                            r.Fill = new SolidColorBrush(Colors.Red);
                            break;
                    }
                }
            }
        }
        public void Dump()
        {
            for (int row = 0; row < laBase.GetLength(0); row++)
            {
                for (int col = 0; col < laBase.GetLength(1); col++)
                {
                    Debug.Write(laBase[row, col]);
                }
                Debug.WriteLine("");

            }
        }
    }
}
