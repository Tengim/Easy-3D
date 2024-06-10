using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Easy3D
{
    internal class UI
    {
        Canvas canvas;
        public UI(Canvas _canvas)
        {
            canvas = _canvas;
        }
        public void DrawMap(List<Rectangle> blocks, Player SelfPlayer)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                
            }
        }
    }
}
