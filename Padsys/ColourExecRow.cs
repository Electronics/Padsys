using LaunchpadNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padsys {
    class ColourExecRow {
        public static ColourExecButton[] standard = new ColourExecButton[9] {
            new ColourExecButton(Color.White),
            new ColourExecButton(Color.Red),
            new ColourExecButton(Color.OrangeRed,Color.FromArgb(35,11,0)),
            new ColourExecButton(Color.FromArgb(255,200,0),Color.FromArgb(25,20,0)),
            new ColourExecButton(Color.Green),
            new ColourExecButton(Color.Cyan),
            new ColourExecButton(Color.Blue),
            new ColourExecButton(Color.Magenta),
            new ColourExecButton(Color.White)
        };

            // rogue r1:
        public static ColourExecButton[] r1beam = new ColourExecButton[9] {
            new ColourExecButton(Color.White),
            new ColourExecButton(Color.Red),
            new ColourExecButton(Color.FromArgb(255,200,0),Color.FromArgb(25,20,0)),
            new ColourExecButton(Color.Green),
            new ColourExecButton(Color.Cyan),
            new ColourExecButton(Color.Blue),
            new ColourExecButton(Color.FromArgb(80,0,255),Color.FromArgb(8,0,25)),
            new ColourExecButton(Color.Magenta),
            new ColourExecButton(Color.White)
        };

        // CHANGE COLOUR ROW TYPE HERE:
        public static ColourExecButton[] colours = r1beam;

        public static void writeLowlightToRow(Interface lInt, ColourExecButton[] buttons, int row) {
            int column = 0;
            foreach(ColourExecButton b in buttons) {
                lInt.setLED(column , row , b.lowlight);

                column++;
            }
        }
    }
}
