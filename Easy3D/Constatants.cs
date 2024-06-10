using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace Easy3D
{
    internal class Constants
    {
        public static int ScreenResolution = 320;
        public static int ScreenFOV = 60;
        public static double RayGap = (double)ScreenFOV / ScreenResolution;

        public static String[] TexturePath = { 
            "Source/brick.png",
            "Source/block.png"
        };
        public static int TextureSize = 50;
        public static List<MyTexture> myTextures;


        public static double ScreenWidth = 100;
        public static double ScreenHeight = 700;
        public static double ScreenHalfHeight = ScreenHeight / 2;
        public const int BlockSize = 100;
        public const int BlockHeight = 300;
        public const int PlayerSize = 20;
        public const int PlayerHalfSize = PlayerSize / 2;

        public static int RayDistanse = BlockSize * 10;

        public static readonly string[] Map = new string[]
        {
            "BCBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
            "B C   B      B    B B        BB      B",
            "B C B BBB BBBB  B B    BB  B B  B BB B",
            "B   B B B BBBB  B   B    BBB BBBB BB B",
            "B B B B         BBB   BBBB         B  ",
            "B BBB BBBB BBBB    BB   B B BBBBB BBBB",
            "B B            BBB B  BBB B B B B B",
            "B  BB BBBB B BBB B  B B B B B   B B",
            "B B      B B     BBBB B B B BB BB B",
            "B B BBBB B BBBBBBB B BB B         B",
            "B                  B     BBBBBBBBBB",
            "BBBBBBBBBBBBBBBBBBBBBBBBBBB"
        };
        public static bool InRect(double pointX, double pointY, int BlockX, int BlockY, int Width, int Height)
        {
            if ((pointX >= BlockX && pointX <= BlockX + Width) &&
                (pointY >= BlockY && pointY <= BlockY + Height))
                return true;
            return false;
        }
        public static double MathRadians(double alpha)
        {
            return alpha * Math.PI / 180;
        }

        public static MyTexture GetMyTexture(string Name)
        {
            foreach(MyTexture texture in myTextures)
            {
                if (texture.Name == Name)
                {
                    return texture;
                }
            }
            return null;
        }
        public static ImageBrush[] CreateNewImageBrush(string imagePath,ImageBrush[] imageBrushes, int columnCount)
        {
            imageBrushes = new ImageBrush[50];

            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            int columnWidth = (int)Math.Ceiling((double)bitmapImage.PixelWidth / columnCount);

            for (int i = 0; i < columnCount; i++)
            {
                int startX = i * columnWidth;
                int endX = Math.Min((i + 1) * columnWidth, bitmapImage.PixelWidth);

                Int32Rect sourceRect = new Int32Rect(startX, 0, endX - startX, bitmapImage.PixelHeight);
                CroppedBitmap croppedBitmap = new CroppedBitmap(bitmapImage, sourceRect);

                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = croppedBitmap;

                imageBrushes[i] = imageBrush;
            }

            return imageBrushes;
        }
    }
}
