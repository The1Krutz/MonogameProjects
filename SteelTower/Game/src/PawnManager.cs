// SPDX-FileCopyrightText: 2023 The1Krutz <the1krutz@gmail.com>
// SPDX-License-Identifier: MIT

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SteelTower;

/// <summary>
/// PawnManager class inherits from DrawableGameComponent, so it has all the LoadContent, Update,
/// Draw, etc override methods. It's here to manage a list of Pawns and batch them
/// </summary>
public class PawnManager : DrawableGameComponent
{
  protected SpriteBatch _spriteBatch;
  protected List<Pawn> _pawns;

  public int PawnCount
  {
    get
    {
      return _pawns.Count;
    }
  }

  public PawnManager(Game game) : base(game)
  {
    _pawns = new();
  }

  public void AddPawn(Pawn pawn)
  {
    _pawns.Add(pawn);
  }

  public bool RemovePawn(Pawn pawn)
  {
    return _pawns.Remove(pawn);
  }

  public override void Initialize()
  {
    _spriteBatch = new SpriteBatch(GraphicsDevice);

    base.Initialize();
  }

  public override void Update(GameTime gameTime)
  {
    float deltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

    foreach (Pawn pawn in _pawns)
    {
      // @todo - yikes, but this is probably the place to have enemies check collisions.
      // then if there is a collision, update the TargetPosition to somewhere new that avoids the collision
      pawn.UpdatePosition(deltaTime);
    }

    base.Update(gameTime);
  }

  public override void Draw(GameTime gameTime)
  {
    _spriteBatch.Begin();

    foreach (Pawn pawn in _pawns)
    {
      _spriteBatch.Draw(pawn.Sprite, pawn.Position, Color.White);
    }

    _spriteBatch.End();

    base.Draw(gameTime);
  }
}
