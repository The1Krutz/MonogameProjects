// SPDX-FileCopyrightText: 2023 The1Krutz <the1krutz@gmail.com>
// SPDX-License-Identifier: MIT

using System;
using Microsoft.Xna.Framework;

namespace SteelTower;

public static class MyExtensionMethods
{
  private static readonly Random s_random;

  static MyExtensionMethods()
  {
    s_random = new Random();
  }

  /// <summary>
  /// Called from an existing Vector2 to randomize its coordinates
  /// ie: _vector.Randomize();
  /// </summary>
  public static void Randomize(this Vector2 v, float max = 1.0f, float min = -1.0f)
  {
    float range = max - min;

    v.X = (s_random.NextSingle() * range) + min;
    v.Y = (s_random.NextSingle() * range) + min;
  }

  /// <summary>
  /// Called statically from Vector2 to give a new random vector
  /// ie: var test = Vector2.Random();
  /// </summary>
  public static Vector2 Random(float max = 1.0f, float min = -1.0f)
  {
    float range = max - min;

    return new Vector2(
      (s_random.NextSingle() * range) + min,
      (s_random.NextSingle() * range) + min);
  }
}
