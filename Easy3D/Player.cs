using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Easy3D
{
    internal class Player
    {
        public double RayGap = Constants.RayGap;

        private double PosX;
        private double PosY;
        private double Player_Alpha;

        private int FOV = Constants.ScreenFOV;
        private int speed = 1;
        public Player(int posX, int posY, int player_Alpha)
        {
            PosX = posX;
            PosY = posY;
            Player_Alpha = player_Alpha;
        }

        public void PlayerCameraMove()
        {
            if (Keyboard.IsKeyDown(Key.Left))
            {
                SetPlayerAlpha(Player_Alpha - 0.6);
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                SetPlayerAlpha(Player_Alpha + 0.6);
            }

        }
        public void PlayerMove(List<Block> blocks)
        {
            speed = 1;
            double OfsetX = 0;
            double OfsetY = 0;
            UpdateRunPlayer();
            if (Keyboard.IsKeyDown(Key.W))
            {
                OfsetX += Math.Cos(Constants.MathRadians(Player_Alpha)) * speed;
                OfsetY += Math.Sin(Constants.MathRadians(Player_Alpha)) * speed;
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                OfsetX += Math.Cos(Constants.MathRadians(Player_Alpha - 90)) * 1;
                OfsetY += Math.Sin(Constants.MathRadians(Player_Alpha - 90)) * 1;
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                OfsetX += Math.Cos(Constants.MathRadians(Player_Alpha + 90)) * 1;
                OfsetY += Math.Sin(Constants.MathRadians(Player_Alpha + 90)) * 1;
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                OfsetX += Math.Cos(Constants.MathRadians(Player_Alpha + 180)) * 1;
                OfsetY += Math.Sin(Constants.MathRadians(Player_Alpha + 180)) * 1;
            }
            foreach (Block CurrentBlock in blocks)
            {
                for(int i = 0;i<Constants.PlayerSize;i+=10)
                {
                    if (Constants.InRect(PosX + Constants.PlayerSize,PosY + i,CurrentBlock.PosX, CurrentBlock.PosY,CurrentBlock.Width,CurrentBlock.Height))
                    {
                        if(OfsetX > 0)
                        {
                            OfsetX = 0;
                        }
                    }
                    if (Constants.InRect(PosX - Constants.PlayerHalfSize, PosY + i, CurrentBlock.PosX, CurrentBlock.PosY, CurrentBlock.Width, CurrentBlock.Height))
                    {
                        if (OfsetX < 0)
                        {
                            OfsetX = 0;
                        }
                    }
                    if (Constants.InRect(PosX + i, PosY + Constants.PlayerSize, CurrentBlock.PosX, CurrentBlock.PosY, CurrentBlock.Width, CurrentBlock.Height))
                    {
                        if (OfsetY > 0)
                        {
                            OfsetY = 0;
                        }
                    }
                    if (Constants.InRect(PosX + i, PosY - Constants.PlayerSize, CurrentBlock.PosX, CurrentBlock.PosY, CurrentBlock.Width, CurrentBlock.Height))
                    {
                        if (OfsetY < 0)
                        {
                            OfsetY = 0;
                        }
                    }
                }

            }
            SetPlayerPos(PosX + OfsetX, PosY + OfsetY);
        }
        private void UpdateRunPlayer()
        {
            if ((Keyboard.IsKeyDown(Key.LeftShift)) && Keyboard.IsKeyDown(Key.W))
            {
                FOV = Constants.ScreenFOV + 4;
                RayGap = (double)FOV / Constants.ScreenResolution;
                speed = 3;
            }
            else
            {
                FOV = Constants.ScreenFOV;
                RayGap = (double)FOV / Constants.ScreenResolution;
                speed = 1;
            }
        }
        public double GetPosX() { return PosX;}
        public double GetPosY() { return PosY;}
        public double GetPlayer_Alpha() { return Player_Alpha;}

        public void SetPlayerAlpha(double alpha)
        {
            Player_Alpha = alpha;
        }
        public void SetPlayerPos(double posX, double posY) {  
            PosX = posX; 
            PosY = posY;
        }
    }
}
