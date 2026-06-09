// SPDX-FileCopyrightText: 2023 The1Krutz <the1krutz@gmail.com>
// SPDX-License-Identifier: MIT

using System;

namespace SteelTower;

public class FastTimer {
  public float CurrentValue {
    get; private set;
  }
  public float RollOverPeriod { get; } = 1.0f;
  public bool IsStopped {
    get; private set;
  }
  public bool OneShot {
    get; set;
  }
  private readonly Action _onRollover;

  public FastTimer(float rollOverPeriod, Action onRollover, bool oneShot = false, bool activateImmediately = false) {
    RollOverPeriod = rollOverPeriod;
    CurrentValue = 0.0f;
    _onRollover = onRollover;
    OneShot = oneShot;
    IsStopped = !activateImmediately;
  }

  public int Update(float delta) {
    if (IsStopped) {
      return 0;
    }

    int shotCount = 0;

    CurrentValue += delta;
    while (CurrentValue >= RollOverPeriod) {
      CurrentValue -= RollOverPeriod;
      _onRollover();

      if (OneShot) {
        Stop();
        return 1;
      } else {
        shotCount++;
      }
    }

    if (shotCount > 1) {
      // Console.WriteLine("multiple shot frame! {0}", shotCount);
    }
    return shotCount;
  }

  public void Start() {
    CurrentValue = 0.0f;
    IsStopped = false;
  }

  public void Stop() {
    IsStopped = true;
  }
}
