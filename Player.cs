using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Defenders;

    public class Player : Obstacle
    {
        protected int hp;
        private int maxHP;
        protected int speed;
        protected string currentDirection = "down";
        protected Dictionary<string, Texture2D> sprites;
        public float barHP { get => maxHP;}
        

        public Player(int newX, int newY, int newWidth, int newHeight,Texture2D newImage ,float MaxHP, int newSpeed, Dictionary<string, Texture2D> newSprites) : base(newX, newY, newWidth, newHeight, newImage)
        {
            hp = (int)MaxHP;
            maxHP = (int)MaxHP;
            speed = newSpeed;
            sprites = newSprites;
            
        }

        public void Update(bool hero_left,bool hero_right, bool hero_up, bool hero_down)
        {
            Vector2 movement = new Vector2(0, 0);

            if (hero_left) movement.X -= 1;
            if (hero_right) movement.X += 1;
            if (hero_up) movement.Y -= 1;
            if (hero_down) movement.Y += 1;

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

        public string CurrentDirection => currentDirection;

        public int HP { get => hp; set => hp = value; }
        public int Speed => speed;

    }

