// SPDX-FileCopyrightText: 2023 The1Krutz <the1krutz@gmail.com>
// SPDX-License-Identifier: MIT

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SteelTower;

public class SteelTowerGame : Game {
  private readonly GraphicsDeviceManager _graphics;

  private readonly int _screenWidth = 2560;
  private readonly int _screenHeight = 1440;

  private SpriteBatch _spriteBatch;
  private PlayerTower _player;
  private PawnManager _enemyManager;
  private PawnManager _projectileManager;

  private Texture2D _enemySprite;

  private readonly int _enemyCountGoal = 25;

  public SteelTowerGame() {
    _graphics = new GraphicsDeviceManager(this) {
      PreferredBackBufferWidth = _screenWidth,
      PreferredBackBufferHeight = _screenHeight
    };
    _graphics.ApplyChanges();

    Content.RootDirectory = "Content";
    IsMouseVisible = true;
  }

  protected override void Initialize() {
    _spriteBatch = new SpriteBatch(GraphicsDevice);

    _enemyManager = new PawnManager(this);
    _projectileManager = new PawnManager(this);

    Components.Add(_enemyManager);
    Components.Add(_projectileManager);

    _player = new PlayerTower() {
      Position = new Vector2(_screenWidth / 2, _screenHeight / 2),
    };

    base.Initialize();
  }

  protected override void LoadContent() {
    _player.Sprite = Content.Load<Texture2D>("scifiStructure_01");
    _enemySprite = Content.Load<Texture2D>("scifiUnit_01");
  }

  protected override void Update(GameTime gameTime) {
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
      Exit();
    }

    _player.Update();

    for (int i = 0; i < _enemyCountGoal; i++) {
      for (int j = 0; j < _enemyCountGoal; j++) {
        if (_enemyManager.PawnCount < _enemyCountGoal * _enemyCountGoal) {
          Vector2 spawnPosition = new(100 * i, 100 * j);
          Vector2 targetVector = Vector2.Normalize(spawnPosition - _player.Position) * 50; // @todo - when you add enemy attacks, you might use the range here instead of flat 50

          _enemyManager.AddPawn(new Enemy() {
            Position = spawnPosition,
            TargetPosition = _player.Position + targetVector,
            Sprite = _enemySprite,
            MaxSpeed = 200
          });
        }
      }
    }

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime) {
    GraphicsDevice.Clear(Color.CornflowerBlue);

    _player.Draw(_spriteBatch);

    base.Draw(gameTime);
  }
}
