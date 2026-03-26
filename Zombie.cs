using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Defenders;

public class Zombie : Player
{

    public Zombie(int newX, int newY, int newWidth, int newHeight, Texture2D newImage, int newHP, int newSpeed, Dictionary<string, Texture2D> newSprites) : base(newX, newY, newWidth, newHeight, newImage, newHP, newSpeed, newSprites )
    {
    }

    public void Chase(int playerX, int playerY)
    {

        Vector2 movement = new Vector2(playerX - x, playerY - y);

            if (movement.Length() > 0)
                movement = Vector2.Normalize(movement);
            
            x += (int)(movement.X * speed);
            y += (int)(movement.Y * speed);

            if (movement.X < 0) currentDirection = "left";
            else if (movement.X > 0) currentDirection = "right";
            else if (movement.Y < 0) currentDirection = "up";
            else if (movement.Y > 0) currentDirection = "down";

            image = sprites[currentDirection];
    
    }
}