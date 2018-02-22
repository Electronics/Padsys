using LaunchpadNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LaunchpadNET.Interface;

namespace Padsys {
    public partial class Padsys : Form {
        private Button[,] buttons = new Button[9,8];
        private Button[] topButtons = new Button[8];
        Interface lInt = new Interface();

        Random r = new Random();
        Timer timer1;
        public void initTimer() {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_tick);
            timer1.Interval = 100;
            timer1.Start();
        }
        private void timer1_tick(object sender, EventArgs e) {
            if (!lInt.isConnected()) return;
            int rand = 0;
            while(rand==0) rand = r.Next(0 , 15);
            //Console.WriteLine(rand);
            //lInt.setLED(r.Next(-1,8) , r.Next(0,9) , (0x10 * (rand%4)) + (rand >> 2)+0x0C);
        }

        public Padsys() {
            InitializeComponent();
            initLaunchpad();
            initTimer();

            for(int j=0;j<8;j++) {
                for (int i=0;i<8;i++) {
                    //lInt.setLED(j , i , j * 8 + i);
                }
            }
        }

        private void initLaunchpad() {
            if(findAndConnectToLaunchpad()) {
                
            } else {
                Timer connectTimer = new Timer();
                connectTimer.Interval = 1000;
                connectTimer.Tick += delegate (object sender , EventArgs e) {
                    if (findAndConnectToLaunchpad()) {
                        connectTimer.Stop();
                    }
                };
                connectTimer.Start();
            }
        }
        private bool findAndConnectToLaunchpad() {
            List<LaunchpadDevice> tempDevices = new List<LaunchpadDevice>();
            tempDevices = lInt.getConnectedLaunchpads().ToList();
            Console.WriteLine($"[Connect] Searching for launchpads, found {tempDevices.Count}");
            if (tempDevices.Count > 0) {
                Console.WriteLine($"[Connect] Connecting to {tempDevices.First()._midiName}");
                lInt.connect(tempDevices.First());
                lInt.clearAllLEDs();
                lInt.OnLaunchpadKeyPressed += new LaunchpadKeyEventHandler(testKeyEvent);
                return true;
            }
            return false;
        }

        private void testKeyEvent(object sender, LaunchpadKeyEventArgs e) {
            if (e.GetY() >= -1 && e.GetY() >= -1) {
                Random r = new Random();
                int red, grn, blu,h,s,v;
                h = r.Next(0 , 360);
                s = r.Next(50 , 400);s=s>100?100:s;
                v = r.Next(0 , 600);v=v>100?100:v;
                HsvToRgb( h,  s/100,  v/100, out red , out grn , out blu);
                //Console.WriteLine($"Key: {e.GetX()} {e.GetY()}: {h} {s} {v} -> {red} {grn} {blu}");
                lInt.setLED(e.GetX() , e.GetY() , Color.FromArgb(red , grn , blu));
                
            }
        }

        private void Padsys_Load(object sender , EventArgs e) {
            generateButtons();
        }

        private void onUIbuttonPress(object sender, System.EventArgs e) {
            Tuple<int,int> pos = getUIButtonPos(sender);
            ColorDialog dialog = new ColorDialog();
            if(dialog.ShowDialog() == DialogResult.OK) {
                //lInt.setLED(pos.Item2 , pos.Item1 , dialog.Color);
                lInt.flashLED(pos.Item1 , pos.Item2 , dialog.Color);
                Console.WriteLine($"Woah {pos.Item1} {pos.Item2} {dialog.Color.Name}");
            }
        }
        private Tuple<int, int> getUIButtonPos(object button) {
            int i = 0;
            foreach(object b in topButtons) {
                if (b == button) return new Tuple<int , int>(i , -1);
                i++;
            }
            for(i=0;i<9;i++) {
                for(int j=0;j<8;j++) {
                    if (button == buttons[i , j]) return new Tuple<int , int>(i , j);
                }
            }
            return null;
        }

        private void generateButtons() {
            //top buttons
            for (int i = 0; i < topButtons.GetLength(0); i++) {
                topButtons[i] = new RoundButton();
                topButtons[i].Size = new Size(50 , 50);
                topButtons[i].Location = new Point(i * 60 , 0);
                topButtons[i].Click += onUIbuttonPress;
                buttonPanel.Controls.Add(topButtons[i]);
            }
            // square buttons
            for (int i = 0; i < buttons.GetLength(0); i++) {
                for (int j = 0; j < buttons.GetLength(1); j++) {
                    if(i==8) { // if we're on the circular letter buttons
                        buttons[i , j] = new RoundButton();
                    } else {
                        buttons[i , j] = new Button();
                    }
                    buttons[i , j].Size = new Size(50 , 50);
                    buttons[i , j].Location = new Point(i * 60 , j * 60 + 60);
                    buttons[i , j].Click += onUIbuttonPress;
                    buttonPanel.Controls.Add(buttons[i , j]);
                }
            }
        }

        void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
{    
  double H = h;
  while (H < 0) { H += 360; };
  while (H >= 360) { H -= 360; };
  double R, G, B;
  if (V <= 0)
    { R = G = B = 0; }
  else if (S <= 0)
  {
    R = G = B = V;
  }
  else
  {
    double hf = H / 60.0;
    int i = (int)Math.Floor(hf);
    double f = hf - i;
    double pv = V * (1 - S);
    double qv = V * (1 - S * f);
    double tv = V * (1 - S * (1 - f));
    switch (i)
    {

      // Red is the dominant color

      case 0:
        R = V;
        G = tv;
        B = pv;
        break;

      // Green is the dominant color

      case 1:
        R = qv;
        G = V;
        B = pv;
        break;
      case 2:
        R = pv;
        G = V;
        B = tv;
        break;

      // Blue is the dominant color

      case 3:
        R = pv;
        G = qv;
        B = V;
        break;
      case 4:
        R = tv;
        G = pv;
        B = V;
        break;

      // Red is the dominant color

      case 5:
        R = V;
        G = pv;
        B = qv;
        break;

      // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

      case 6:
        R = V;
        G = tv;
        B = pv;
        break;
      case -1:
        R = V;
        G = pv;
        B = qv;
        break;

      // The color is not defined, we should throw an error.

      default:
        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
        R = G = B = V; // Just pretend its black/white
        break;
    }
  }
  r = Clamp((int)(R * 255.0));
  g = Clamp((int)(G * 255.0));
  b = Clamp((int)(B * 255.0));
}

/// <summary>
/// Clamp a value to 0-255
/// </summary>
int Clamp(int i)
{
  if (i < 0) return 0;
  if (i > 255) return 255;
  return i;
}
    }
}
