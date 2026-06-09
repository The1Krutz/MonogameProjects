// SPDX-FileCopyrightText: 2023 The1Krutz <the1krutz@gmail.com>
// SPDX-License-Identifier: MIT

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SteelTower;

public class Pawn {
  public Vector2 Position {
    get; set;
  } = Vector2.Zero;

  public Vector2 TargetPosition {
    get; set;
  } = Vector2.Zero;

  // max speed in pixels per second. 100 is probably too slow, but whatever
  public float MaxSpeed {
    get; set;
  }

  public Texture2D Sprite {
    get; set;
  }

  public Rectangle Bounds {
    get {
      // @todo - Add a check for if this gets accessed before the Sprite is assigned
      return Sprite.Bounds;
    }
  }

  public virtual void UpdatePosition(float deltaTime) {
    Vector2 targetVector = TargetPosition - Position;

    if (targetVector.LengthSquared() == 0) {
      return;
    }

    float maxSpeedThisFrame = MaxSpeed * deltaTime;
    if (targetVector.Length() <= maxSpeedThisFrame) {
      // close enough to finish movement this frame
      Position = TargetPosition;
    } else {
      targetVector.Normalize();
      Position += targetVector * maxSpeedThisFrame;
    }
  }
}
