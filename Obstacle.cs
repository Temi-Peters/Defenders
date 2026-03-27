using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Defenders;
public class Obstacle 
{
    protected int x;
    protected int y;
    private int width;
    private int height;
    protected Texture2D image;
    public Rectangle Bounds => new Rectangle(x, y, width, height);

    public Obstacle(int newX, int newY, int newWidth,int newHeight, Texture2D newImage)
    {
        x = newX;
        y = newY;
        width = newWidth;
        height = newHeight;
        image = newImage;

    }
        public Texture2D Image => image;
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Width => width;
        public int Height => height;
}