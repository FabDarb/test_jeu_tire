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

        char[,] laBase;
        Rectangle[,] rec;
        List<Player> playerList = new List<Player>();
        
        public MainWindow()
        {
            InitializeComponent();
            laBase = new char[haut, large];
            rec = new Rectangle[haut, large];
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
                    switch (laBase[i,j])
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
                    rec[j, i] = rect;
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
            if(laBase[p.y, p.x] == '#')
            {
                p.y = p.laterY;
                p.x = p.laterX;
            }
            
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Player p1 = playerList[0];
            Player p2 = playerList[1];
            switch (e.Key)
            {
                case Key.W:
                    keep_Last(p1);
                    p1.y--;
                    beforeMove(p1);
                    break;
                case Key.S:
                    keep_Last(p1);
                    p1.y++;
                    beforeMove(p1);
                    break;
                case Key.A:
                    keep_Last(p1);
                    p1.x--;
                    beforeMove(p1);
                    break;
                case Key.D:
                    keep_Last(p1);
                    p1.x++;
                    beforeMove(p1);
                    break;
                case Key.Up:
                    keep_Last(p2);
                    p2.y--;
                    beforeMove(p2);
                    break;
                case Key.Down:
                    keep_Last(p2);
                    p2.y++;
                    beforeMove(p2);
                    break;
                case Key.Left:
                    keep_Last(p2);
                    p2.x--;
                    beforeMove(p2);
                    break;
                case Key.Right:
                    keep_Last(p2);
                    p2.x++;
                    beforeMove(p2);
                    break;
                case Key.P:
                    Dump();
                    break;
            }
            laBase[p1.x, p1.y] = '1';
            laBase[p2.x, p2.y] = '2';
            laBase[p1.laterX, p1.laterY] = ' ';
            laBase[p2.laterX, p2.laterY] = ' ';
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
                    Rectangle r = rec[j, i];
                    switch (laBase[j,i])
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
