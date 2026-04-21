using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Defenders;

public class Game1 : Game
{   
    private Player hero;
    private List<Zombie> zombies;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Combat weapon;
    private MouseState previousMouseState;
    private SpriteFont font;
    public List<Zombie> Zombies => zombies;
    private Texture2D heroHPDisplay;
    
    public Game1()
    {
        Console.WriteLine("Game1 created");
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        if (weapon != null) return;
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Texture2D hero_up = Content.Load<Texture2D>("hero_up");
        Texture2D hero_down = Content.Load<Texture2D>("hero_down");
        Texture2D hero_left = Content.Load<Texture2D>("hero_left");
        Texture2D hero_right = Content.Load<Texture2D>("hero_right");

        Texture2D zombie_up = Content.Load<Texture2D>("zombie_up");
        Texture2D zombie_down = Content.Load<Texture2D>("zombie_down");
        Texture2D zombie_left = Content.Load<Texture2D>("zombie_left");
        Texture2D zombie_right = Content.Load<Texture2D>("zombie_right");

        heroHPDisplay = new Texture2D(GraphicsDevice, 1, 1);
        heroHPDisplay.SetData(new Color[] { Color.White });

        Dictionary<string, Texture2D> heroSprites = new Dictionary<string, Texture2D>
        {
            ["up"] = hero_up,
            ["down"] = hero_down,
            ["left"] = hero_left,
            ["right"] = hero_right,
        };

        Dictionary<string, Texture2D> zombieSprites = new Dictionary<string, Texture2D>
        {
            ["up"] = zombie_up,
            ["down"] = zombie_down,
            ["left"] = zombie_left,
            ["right"] = zombie_right,
        };

        hero = new Player(100, 225, 30, 30, hero_down, 100, 5, heroSprites);
        zombies = new List<Zombie>
        {
            new Zombie(670, 200, 30, 30, zombie_down, 50, 2, zombieSprites, 5, 30),
            new Zombie(670, 225, 30, 30, zombie_down, 50, 2, zombieSprites, 5, 30),
            new Zombie(670, 250, 30, 30, zombie_down, 50, 2, zombieSprites, 5, 30)
        };

        weapon = new Combat(5, 15);
        font = Content.Load<SpriteFont>("GameFont");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardState keyState = Keyboard.GetState();
        hero.Update
        (
            keyState.IsKeyDown(Keys.A),
            keyState.IsKeyDown(Keys.D),
            keyState.IsKeyDown(Keys.W),
            keyState.IsKeyDown(Keys.S)
        );

        hero.X = Math.Clamp(hero.X, 0, 770);
        hero.Y = Math.Clamp(hero.Y, 0, 420);

        foreach (Zombie z in zombies)
        {
            Vector2 zombiePos = new Vector2(z.X, z.Y);
            Vector2 heroPos = new Vector2(hero.X, hero.Y);

            if (Vector2.Distance(zombiePos, heroPos) < 200 && !hero.Bounds.Intersects(z.Bounds))
            {
                z.Chase(hero.X, hero.Y);
            }
            if (z.Bounds.Intersects(hero.Bounds))
            {
                z.Update(hero);
            }
        }

        MouseState mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
        {
            weapon.Attack(hero.X, hero.Y, hero.CurrentDirection);
        }
        previousMouseState = mouseState;

        weapon.CheckHit(zombies);
        weapon.Update();
        zombies.RemoveAll(z => z.HP <= 0);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        _spriteBatch.Draw(hero.Image, new Rectangle(hero.X, hero.Y, hero.Width, hero.Height), Color.White);
        foreach (Zombie z in zombies)
        {
            _spriteBatch.Draw(z.Image, new Rectangle(z.X, z.Y, z.Width, z.Height), Color.White);
            _spriteBatch.DrawString(font, $"HP: {z.HP}", new Vector2(z.X, z.Y - 15), Color.Red);
        }
        _spriteBatch.Draw(heroHPDisplay, new Rectangle(10, 10, 100, 10), Color.DarkRed);
        _spriteBatch.Draw(heroHPDisplay, new Rectangle(10, 10, (int)((float)hero.HP / hero.barHP * 100), 10), Color.Red);
        _spriteBatch.End();
        base.Draw(gameTime);
    }

  
}