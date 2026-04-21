using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Defenders;

public class Zombie : Player
{
    private int AttackTimer;   
    private int AttackDamage;
    private int AttackDuration;
    private bool CanAttack;
    public bool z_CanAttack { get => CanAttack; set => CanAttack = value; }
    public Zombie(int newX, int newY, int newWidth, int newHeight, Texture2D newImage, int newHP, int newSpeed, Dictionary<string, Texture2D> newSprites, int newAttackDamage, int newAttackDuration) : base(newX, newY, newWidth, newHeight, newImage, newHP, newSpeed, newSprites )
    {
        CanAttack = true;
        AttackDamage = newAttackDamage;
        AttackDuration = newAttackDuration;
        AttackTimer = AttackDuration;
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

    public void Update(Player hero)
    {
        if (CanAttack == true)
        {
            AttackTimer--;
            if (AttackTimer <= 0)
            {
                hero.HP -= AttackDamage;
                AttackTimer = AttackDuration;
                CanAttack = true;
            }
        }
    }
}