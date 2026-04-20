using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Defenders;

    public class Combat
    {
        private int attackDamage;
        private Rectangle Hitbox;
        private bool isHitboxActive;
        private bool canAttack;
        private int attackDuration;
        private int attackTimer;   
        private List<Zombie> alreadyHitZombies = new List<Zombie>();

        public int AttackDamage { get => attackDamage; }
        public int AttackDuration { get => attackDuration; }
        public bool CanAttack { get => canAttack;  }
        public bool IsHitboxActive { get => isHitboxActive; }

        public Combat(int newAttackDamage, int newAttackDuration)
        {
            attackDamage = newAttackDamage;
            attackDuration = newAttackDuration;
            canAttack = true;
        }
        public void Attack(int playerX, int playerY, string direction)
        {
            System.Console.WriteLine($"Combat created | canAttack: {canAttack}");
            System.Console.WriteLine($"Attack called | canAttack: {canAttack}");
            if (canAttack == true)
            {
                isHitboxActive = true;  
                attackTimer = attackDuration;
                canAttack = false;

                if (direction == "right")
                    Hitbox = new Rectangle(playerX + 30, playerY, 40, 30);
                if (direction == "left")
                    Hitbox = new Rectangle(playerX - 30, playerY, 40, 30);
                if (direction == "up")
                    Hitbox = new Rectangle(playerX, playerY - 40, 40, 30);
                if (direction == "down")
                    Hitbox = new Rectangle(playerX, playerY + 30, 40, 30);
            }
        }

        public void Update()
        {
            if (isHitboxActive)
            {
                attackTimer--;
                Console.WriteLine($"Timer: {attackTimer}");
                if (attackTimer <= 0)
                {
                    isHitboxActive = false;
                    canAttack = true;
                    alreadyHitZombies.Clear();
                }
            }
        }

        public void CheckHit(List<Zombie> zombies)
        {
            if (isHitboxActive)
            {
                foreach (Zombie z in zombies)
                {
                    if (Hitbox.Intersects(z.Bounds))
                    {
                        if (!alreadyHitZombies.Contains(z))
                        {
                            alreadyHitZombies.Add(z);
                            z.HP -= attackDamage;
                        }
                    }
                }
            }
        }
    }