using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Easy3D
{
    internal class RayCast
    {
        static List<Rectangle> Rects;
        static List<Block> Blocks;
        public RayCast(List<Rectangle> rect, List<Block> blocks) {
            Rects = rect;
            Blocks = blocks;
        }
        private ImageBrush UpdateImage( double PointX, double PointY,Block block)
        {
            int size = 1;
            int doublesize = size * 2;
            int ofset = 0;

            if (Constants.InRect(PointX, PointY, block.PosX + size, block.PosY + block.Height - size, block.Width - doublesize, size) ||
                Constants.InRect(PointX, PointY, block.PosX + size, block.PosY, block.Width - doublesize, size))
            {
                //ofset = ((int)PointX % Constants.BlockSize) % Constants.TextureSize;
                ofset = ((int)PointX % Constants.BlockSize) / (Constants.BlockSize / Constants.TextureSize);
            }
            else
            {
                //ofset = ((int)PointY % Constants.BlockSize) % Constants.TextureSize;
                ofset = ((int)PointY % Constants.BlockSize) / (Constants.BlockSize / Constants.TextureSize);
            }

            return block.Texture.BrushList[ofset];
        }

        public void RayRun(Player player,double Alpha,int RayNum)
        {

            Rectangle CurrentRect = Rects[RayNum];

            double posX = (int)player.GetPosX();
            double posY = (int)player.GetPosY();

            double dx = Math.Cos(Constants.MathRadians(Alpha));
            double dy = Math.Sin(Constants.MathRadians(Alpha));

            //int DX = 0;
            //int DY = 0;
            //double Long_x = 0;
            //double Long_y = 0;

            //int blockx = (int)player.GetPosX() / Constants.BlockSize;
            //int blocky = (int)player.GetPosY() / Constants.BlockSize;

            ////Вертикали
            //if (dx > 0)
            //{
            //    posX = blockx + Constants.BlockSize;
            //    DX = 1;
            //}
            //else
            //{
            //    posX = blockx;
            //    DX = -1;
            //}
            //for (int i = 0; i < Constants.RayDistanse / Constants.BlockSize; i++)
            //{
            //    Long_y = (posX - player.GetPosX()) / dx;
            //    posY = player.GetPosY() + Long_y * dy;
            //    foreach (Block block in Blocks)
            //    {
            //        if (Constants.InRect(posX, posY, block))
            //        {
            //            break;
            //        }
            //    }
            //    posX += DX * Constants.BlockSize;
            //}
            ////Горизонтали
            //if (dy > 0)
            //{
            //    posY = blocky + Constants.BlockSize;
            //    DY = 1;
            //}
            //else
            //{
            //    posY = blocky;
            //    DY = -1;
            //}
            //for (int i = 0; i < Constants.RayDistanse / Constants.BlockSize; i++)
            //{
            //    Long_x = (posY - player.GetPosY()) / dy;
            //    posX = player.GetPosX() + Long_x * dx;
            //    foreach (Block block in Blocks)
            //    {
            //        if (Constants.InRect(posX, posY, block))
            //        {
            //            break;
            //        }
            //    }
            //    posY += DY + Constants.BlockSize;
            //}
            //double Long = 0;
            //if (Long_x > Long_y)
            //{
            //    Long = Long_y;
            //}
            //else Long = Long_x;
            //CurrentRect.Height = Constants.ScreenHalfHeight / ((Long + 1) * Math.Cos(Constants.MathRadians(player.GetPlayer_Alpha() - Alpha))) * 150;
            //Canvas.SetTop(CurrentRect, Constants.ScreenHalfHeight - (CurrentRect.Height / 2));

            for (int j = 0; j < Constants.RayDistanse; j++)
            {
                posX = posX + dx;
                posY = posY + dy;
                if (((int)(posX) % Constants.BlockSize == 0 || (int)(posY) % Constants.BlockSize == 0) ||
                    ((int)(posX - dx) % Constants.BlockSize == 0 || (int)(posY - dy) % Constants.BlockSize == 0))
                {
                    foreach (Block block in Blocks)
                    {
                        if (Constants.InRect(posX, posY, block.PosX, block.PosY, block.Width, block.Height))
                        {
                            CurrentRect.Height = Constants.ScreenHalfHeight / ((j + 1) * Math.Cos(Constants.MathRadians(player.GetPlayer_Alpha() - Alpha))) * Constants.BlockHeight;
                            Canvas.SetTop(CurrentRect, Constants.ScreenHalfHeight - (CurrentRect.Height / 2));

                            CurrentRect.Fill = UpdateImage(posX, posY,block);

                            return;
                        }
                    }
                    CurrentRect.Height = 0;
                }
            }
        }
        public void RayCastRun(Player player)
        {
            double Alpha;
            for(int i = 0; i < Rects.Count;i++)
            {
                Alpha = player.GetPlayer_Alpha() - (Rects.Count * player.RayGap) / 2 + i * player.RayGap;
                RayRun(player,Alpha,i);
            }

        }
    }
}