// SPDX-FileCopyrightText: 2023 The1Krutz <the1krutz@gmail.com>
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SteelTower;

public class PlayerTower : Pawn
{
  // actions handlers that should be called every frame the key is held down
  // ie: turning, forward motion
  private readonly Dictionary<Keys, Action> _continuousKeys = new(){
   {Keys.A, ()=>Console.WriteLine("continuous: A")},
   {Keys.S, ()=>Console.WriteLine("continuous: S")},
   {Keys.D, ()=>Console.WriteLine("continuous: D")}
  };

  // action handlers that should only be called the first frame the key is pressed, then ignored until it's been let up
  // ie: jump, use item
  private readonly Dictionary<Keys, Action> _singlePressKeys = new(){
   {Keys.Q, ()=>Console.WriteLine("single: Q")},
   {Keys.W, ()=>Console.WriteLine("single: W")},
   {Keys.E, ()=>Console.WriteLine("single: E")}
  };
  private readonly HashSet<Keys> _singlePressIgnore = new();

  private readonly Health _health;

  public PlayerTower()
  {
    _health = new Health(OnHealthChanged, OnHealthDepleted, 1000);
  }

  // exposing just this instead of the whole Health object
  public void TakeDamage(Damage incomingDamage)
  {
    _health.TakeDamage(incomingDamage);
  }

  private void OnHealthDepleted()
  {
    // @todo - this should trigger losing the game
    Console.WriteLine("health update: dead");
  }

  private void OnHealthChanged(float newHealth)
  {
    // @todo - this should update whatever UI and probably trigger a visual effect
    Console.WriteLine("health update: {0} remaining", newHealth);
  }

  public void Update()
  {
    KeyboardState keyboardState = Keyboard.GetState();

    foreach (KeyValuePair<Keys, Action> handler in _continuousKeys)
    {
      if (keyboardState.IsKeyDown(handler.Key))
      {
        handler.Value();
      }
    }

    foreach (KeyValuePair<Keys, Action> handler in _singlePressKeys)
    {
      if (_singlePressIgnore.Contains(handler.Key))
      {
        if (keyboardState.IsKeyUp(handler.Key))
        {
          _singlePressIgnore.Remove(handler.Key);
        }
      }
      else if (keyboardState.IsKeyDown(handler.Key))
      {
        _singlePressIgnore.Add(handler.Key);
        handler.Value();
      }
    }
  }

  public void Draw(SpriteBatch spriteBatch)
  {
    spriteBatch.Begin();
    spriteBatch.Draw(Sprite, Position, Color.White);
    spriteBatch.End();
  }
}
