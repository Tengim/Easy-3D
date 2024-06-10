using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Easy3D
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Player SelfPlayer;

        private List<Block> Blocks;
        private List<Rectangle> rects;
        private List<Rectangle> Map_rects;

        private TextBlock FPSBox;
        private int iterations;
        private DateTime lastFrameTime = DateTime.Now;

        private RayCast RayCasting;

        public MainWindow()
        {
            InitializeComponent();
            FPS.HorizontalAlignment = HorizontalAlignment.Right;

            Constants.ScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            Constants.ScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            Constants.ScreenHalfHeight = Constants.ScreenHeight / 2;

            InitTextures();

            double LineWidth = Constants.ScreenWidth / Constants.ScreenResolution;
            SelfPlayer = new Player(Constants.BlockSize + Constants.BlockSize / 2, Constants.BlockSize + Constants.BlockSize / 2, 90);

            rects = new List<Rectangle>(Constants.ScreenResolution);
            SetBlocks();

            for (int i = 0; i < Constants.ScreenResolution; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = LineWidth + 1;
                rect.Height = 0;
                rect.Fill = Brushes.Green;
                Canvas.SetLeft(rect, LineWidth * i);
                MainCanvas.Children.Add(rect);
                rects.Add(rect);
            }
            RayCasting = new RayCast(rects, Blocks);
            FPSBox = FPS;
            iterations = 0;

            CompositionTarget.Rendering += Update;
        }

        private void Update(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double deltaTime = (now - lastFrameTime).TotalSeconds;
            lastFrameTime = now;

            RayCasting.RayCastRun(SelfPlayer);
            SelfPlayer.PlayerCameraMove();
            SelfPlayer.PlayerMove(Blocks);
             
            double fps = 1.0 / deltaTime;

            FPSBox.Text = $"FPS: {fps:0}" +
                $"\nScreenResolution: {Constants.ScreenResolution}" +
                $"\nFOV: {Constants.ScreenFOV}";
        }

        public void InitTextures()
        {
            Constants.myTextures = new List<MyTexture>();
            foreach(String path in Constants.TexturePath)
            {
                MyTexture texture = new MyTexture();
                texture.BrushList = Constants.CreateNewImageBrush(path, texture.BrushList, 50);
                texture.Name = path;
                Constants.myTextures.Add(texture);
            }
        }

        public void SetBlocks()
        {
            if (Blocks != null)
            {
                Blocks.Clear();
            }
            else
            {
                Blocks = new List<Block>();
            }
            for (int i = 0; i < Constants.Map.Length; i++)
            {
                for (int j = 0; j < Constants.Map[i].Length; j++)
                {
                    switch (Constants.Map[i][j])
                    {
                        case ' ':
                            break;
                        case 'B':
                            Blocks.Add(new Block(Constants.BlockSize * j, Constants.BlockSize * i, Constants.BlockSize, Constants.BlockSize, Constants.GetMyTexture("Source/brick.png")));
                            break;
                        case 'C':
                            Blocks.Add(new Block(Constants.BlockSize * j, Constants.BlockSize * i, Constants.BlockSize, Constants.BlockSize, Constants.GetMyTexture("Source/block.png")));
                            break;
                    }
                }
            }
        }
    }
}