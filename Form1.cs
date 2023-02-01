
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMemory
{
    public partial class Form1 : Form
    {
        int Nmin = 0; int Nmax = 16;
        int sec = 0;
        int min = 0;
        int point = 0;
        List<int> randomList;
        Random random = new Random();
        Image[] img;
        Button[] bt;
        int buttonNumber = 0;
        Button BT1, BT2;
        int index1=-1, index2=-1;
        System.Media.SoundPlayer player1 = new System.Media.SoundPlayer("E:\\OneDrive - Centre d'Estudis Monlau\\MONLAU\\CURSOS\\DAM\\DAM2\\M7\\PMemory\\bin\\Debug\\Sounds\\Mario-Oh-No_.wav");
        System.Media.SoundPlayer player2 = new System.Media.SoundPlayer("E:\\OneDrive - Centre d'Estudis Monlau\\MONLAU\\CURSOS\\DAM\\DAM2\\M7\\PMemory\\bin\\Debug\\Sounds\\Super Mario Bros - sonido estrella Sonido de videojuegos (mp3cut.wav");
        Rectangle BoundRect;
        Rectangle OldRect = Rectangle.Empty;

        public Form1()
        {
            InitializeComponent();
            initConfig();
        }

        private void initConfig()
        {
  
            generate_N_randomNumbersNotDuplicate(Nmin, Nmax);
            

            img = new Image[]
             {
                Properties.Resources._01A,Properties.Resources._02A, Properties.Resources._03A, Properties.Resources._04A,Properties.Resources._05A,Properties.Resources._06A, Properties.Resources._07A, Properties.Resources._08A,
                Properties.Resources._01B,Properties.Resources._02B, Properties.Resources._03B, Properties.Resources._04B,Properties.Resources._05B,Properties.Resources._06B, Properties.Resources._07B, Properties.Resources._08B


             };
            bt = new Button[]{
                button1, button2, button3, button4, button5, button6, button7, button8,button9, button10, button11, button12, button13, button14, button15, button16
            };
            for (int i = 0; i < bt.Length; i++)
            {
                string name = "";
                if (i < 10) name += 0;
                name += i;
                bt[i].Name = name;
                bt[i].Image = Properties.Resources._000; //Imagen de cartas por detras
                bt[i].Enabled=false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
  
            BT1.Image = Properties.Resources._000;
            BT2.Image = Properties.Resources._000;
            timer1.Enabled=false;
            EnableMouse();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            sec++;
            if (sec == 60)
            {
                min++;
                sec = 0;
            }
            labelS.Text = sec.ToString();
            labelT.Text = min.ToString();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            dateclock();
        }

        private void generate_N_randomNumbersNotDuplicate(int Nmin, int Nmax)
        {
            string orderValues = "",randomValues = "";
            List<int> orderList = Enumerable.Range(Nmin,Nmax).ToList();

            foreach (int v in orderList) orderValues += v + " ";
            //MessageBox.Show(orderValues);
            randomList = new List<int>();
            

            for(int i = 0; i < Nmax; i++)
            {
                int index=random.Next(0,orderList.Count);
                randomList.Add(orderList[index]);
                orderList.RemoveAt(index);
            }

            foreach (int v in randomList) randomValues += v + " ";
            //MessageBox.Show(randomValues);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < bt.Length; i++)
            {
                bt[i].Enabled = true;
            }
            timer2.Enabled = true;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            min = 0;
            sec = -1;
            point = 0;
            labelPoint.Text=point.ToString();
            for (int i = 0; i < bt.Length; i++)
            {
                bt[i].Image = Properties.Resources._000;
            }
            generate_N_randomNumbersNotDuplicate(Nmin, Nmax);
        }

        private void dateclock()
        {
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            LabelDate.Text = date;
            string clock = DateTime.Now.ToString("HH:mm");
            LabelTime.Text = clock;
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string name = btn.Name;
            int index = Convert.ToInt32(name);

            btn.Image = img[randomList[index]];
            buttonNumber++;
            if(buttonNumber == 1)
            {
                BT1 = btn;
                index1 = randomList[index];
                point += 100;

            }
            if (buttonNumber == 2)
            {
                BT2 = btn;
                index2 = randomList[index];
                if (index1%(Nmax/2)!= index2%(Nmax/2))
                {
                    DisableMouse();
                    timer1.Enabled = true;
                    player1.Play();
                    point -= 100;

                }
                else
                {
                    player2.Play();
                }
                buttonNumber = 0;
                
            }
            labelPoint.Text = point.ToString();
            if (point == 800)
            {
                timer2.Enabled = false;
                MessageBox.Show("You win");
            }
        }
        private void EnableMouse()
        {
            OldRect = Rectangle.Empty;
            Cursor.Clip = OldRect;
            Cursor.Show();
        }



        private void DisableMouse()
        {
            OldRect = Cursor.Clip;
            BoundRect = new Rectangle(680, 300, 1, 1);
            Cursor.Clip = BoundRect;
            Cursor.Hide();
        }
    }
}