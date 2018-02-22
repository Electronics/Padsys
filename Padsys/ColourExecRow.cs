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
            new ColourExecButton(Color.Orange),
            new ColourExecButton(Color.Yellow),
            new ColourExecButton(Color.Green),
            new ColourExecButton(Color.Cyan),
            new ColourExecButton(Color.Blue),
            new ColourExecButton(Color.Magenta),
            new ColourExecButton(Color.White)
        };

        public static void writeLowlightToRow(Interface lInt, ColourExecButton[] buttons, int row) {
            int column = 0;
            foreach(ColourExecButton b in buttons) {
                lInt.setLED(column , row , b.lowlight);

                column++;
            }
        }
    }
}
