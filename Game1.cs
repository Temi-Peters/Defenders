using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Defenders;

public class Game1 : Game
{   
    private Player hero;
    private List<Zombie> zombies;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
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
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Texture2D hero_up = Content.Load<Texture2D>("hero_up");
        Texture2D hero_down = Content.Load<Texture2D>("hero_down");
        Texture2D hero_left = Content.Load<Texture2D>("hero_left");
        Texture2D hero_right = Content.Load<Texture2D>("hero_right");

        Texture2D zombie_up = Content.Load<Texture2D>("zombie_up");
        Texture2D zombie_down = Content.Load<Texture2D>("zombie_down");
        Texture2D zombie_left = Content.Load<Texture2D>("zombie_left");
        Texture2D zombie_right = Content.Load<Texture2D>("zombie_right");
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
