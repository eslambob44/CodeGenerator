using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Layer
{
    static internal class clsMyColors
    {
        static public Color FormColor {get;private set;} = ColorTranslator.FromHtml("#161616");
        static public Color FormPanelColor { get; private set; } = ColorTranslator.FromHtml("#313131");
        static public Color PanelColor { get; private set; } = ColorTranslator.FromHtml("#1b1b1b");
        static public Color TextBoxForeColor { get; private set; } = ColorTranslator.FromHtml("#94a3b8");
        static public Color ButtonColor { get; private set; } = ColorTranslator.FromHtml("#3e6981");


    }
}
