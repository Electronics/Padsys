using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padsys {
    class ColourExecButton {
        public static float lowlight_default = 0.5f;

        Color highlight;
        Color lowlight;

        public ColourExecButton() {
            highlight = new Color();
            lowlight = new Color();
        }
        public ColourExecButton(Color highlight) {
            this.highlight = highlight;
            lowlight = Color.FromArgb((int)(highlight.R * lowlight_default) , (int)(highlight.G * lowlight_default) , (int)(highlight.B * lowlight_default));
        }
        public ColourExecButton(Color highlight, Color lowlight) {
            this.highlight = highlight;
            this.lowlight = lowlight;
        }
    }
}
