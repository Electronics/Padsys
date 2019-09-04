using LaunchpadNET;
using Rug.Osc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static LaunchpadNET.Interface;

namespace Padsys {
    public partial class Padsys : Form {
        private Button[,] buttons = new Button[9,8];
        private Button[] topButtons = new Button[8];
        Interface lInt = new Interface();

		private OscSender osc;

        public static int num_colourRows = 6;

        public Padsys() {
			InitializeComponent();
			updateOSCAddress();
            initLaunchpad();

            for(int j=0;j<8;j++) {
                for (int i=0;i<8;i++) {
                    //lInt.setLED(j , i , j * 8 + i);
                }
            }

            //ArtEngine.RecievingDMX += new EventHandler(ourDmxReveivedEventFunction);
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
                lInt.OnLaunchpadKeyPressed += new LaunchpadKeyEventHandler(keyPressedEvent);
                lInt.OnLaunchpadKeyReleased += new LaunchpadKeyEventHandler(keyReleasedEvent);
                return true;
            }
            return false;
        }

        private void keyPressedEvent(object sender , LaunchpadKeyEventArgs e) {
            if (e.GetY() < num_colourRows && e.GetY() >= 0) {
                if (e.GetX() < 8) {
                    // this is a coloured exec row, assuming row -1 and column 8 are not part of the selection
                    ColourExecRow.writeLowlightToRow(lInt , ColourExecRow.colours , e.GetY()); // write low_light to entire row
                } else {
                    // end group selection button
                }

				lInt.setLED(e.GetX() , e.GetY() , ColourExecRow.colours[e.GetX()].highlight);
            } else {
				lInt.setLED(e.GetX() , e.GetY() , ColourExecRow.colours[0].highlight); // white for non-colour rows
			}

			if(e.GetX()==8) {
				osc.Send(new OscMessage($"/rpc" , "3H"));
			}

			int oscX = e.GetX() + 2;
			if (oscX == 10) oscX = 1; // shift the end circle buttons to the front
			int oscY = e.GetY() + 1;
			
			osc.Send(new OscMessage($"/exec/{textBox_execPage.Text}/{oscX + oscY * 9}", 1));
        }

        private void keyReleasedEvent(object sender, LaunchpadKeyEventArgs e) {
            if(e.GetY()>=num_colourRows || e.GetX()==8  || e.GetY()==-1) {
                // if it's not a colour selection key, release the playback
               int oscX = e.GetX() + 2;
				if (oscX == 10) oscX = 1; // shift the end circle buttons to the front
				int oscY = e.GetY() + 1;
			
				osc.Send(new OscMessage($"/exec/{textBox_execPage.Text}/{oscX + oscY * 9}", 0));
				lInt.setLED(e.GetX() , e.GetY() , ColourExecRow.colours[0].lowlight);
            }
        }

        private void Padsys_Load(object sender , EventArgs e) {
        

            generateButtons();
            // presuming we have already connected...
            for(int i=0;i<num_colourRows;i++) {  
                ColourExecRow.writeLowlightToRow(lInt , ColourExecRow.colours , i);
            }
        }

        private void onUIbuttonPress(object sender, System.EventArgs e) {
            Tuple<int,int> pos = getUIButtonPos(sender);
            ColorDialog dialog = new ColorDialog();
            if(dialog.ShowDialog() == DialogResult.OK) {
                //lInt.setLED(pos.Item2 , pos.Item1 , dialog.Color);
                lInt.pulseLED(pos.Item1 , pos.Item2 , dialog.Color);
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

		private void textBox_ip_Validating(object sender , System.ComponentModel.CancelEventArgs e) {
			if(IsValidIP(textBox_ip.Text)) {
				textBox_ip.BackColor = Color.White;
			} else {
				textBox_ip.BackColor = Color.LightCoral;
			}
		}

		public bool IsValidIP(string addr)
		{
			// from https://stackoverflow.com/questions/10927523/text-box-only-allow-ip-address-in-windows-application
			//create our match pattern
			string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
			//create our Regular Expression object
			Regex check = new Regex(pattern);
			//boolean variable to hold the status
			bool valid = false;
			//check to make sure an ip address was provided
			if (addr == "")
			{
				//no address provided so return false
				valid = false;
			}
			else
			{
				//address provided so use the IsMatch Method
				//of the Regular Expression object
				valid = check.IsMatch(addr, 0);
			}
			//return the results
			return valid;
		}

		private void textBox_port_Validating(object sender , System.ComponentModel.CancelEventArgs e) {
			int n;
			if(int.TryParse(textBox_port.Text, out n)) {
				textBox_port.BackColor = Color.White;
			} else {
				textBox_port.BackColor = Color.LightCoral;
			}
		}

		private void updateOSCAddress() {
			osc = new OscSender(IPAddress.Parse(textBox_ip.Text), int.Parse(textBox_port.Text));
			osc.Connect();
		}

		private void textBox_ip_Validated(object sender , EventArgs e) {
			updateOSCAddress();
		}

		private void textBox_port_Validated(object sender , EventArgs e) {
			updateOSCAddress();
		}

		private void textBox1_Validating(object sender , System.ComponentModel.CancelEventArgs e) {

		}

		private void textBox_execPage_Validating(object sender , System.ComponentModel.CancelEventArgs e) {
			int n;
			if(int.TryParse(textBox_execPage.Text, out n)) {
				textBox_execPage.BackColor = Color.White;
			} else {
				textBox_execPage.BackColor = Color.LightCoral;
			}
		}
	}
}
