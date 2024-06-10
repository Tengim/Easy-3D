using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Easy3D
{
    internal class Block
    {
        public int Width;
        public int Height;
        public int PosX;
        public int PosY;
        public MyTexture Texture;
        public Block(int posx,int posy, int width, int height,MyTexture texture)
        {
            Width = width;
            Height = height;
            PosX = posx;
            PosY = posy;
            Texture = texture;
        }
    }
}
