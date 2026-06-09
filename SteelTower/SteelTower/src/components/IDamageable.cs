// SPDX-FileCopyrightText: 2023 The1Krutz <the1krutz@gmail.com>
// SPDX-License-Identifier: MIT

namespace SteelTower;

public interface IDamageable {
  void TakeDamage(Damage incoming);
  void TakeDamage(DamageOverTime incoming);
}

public enum DamageType {
  Healing,
  Normal,
  ArmorPiercing,
  Fire,
}

public struct Damage {
  public float Amount {
    get; set;
  }
  public DamageType Type {
    get; set;
  }

  public Damage(float amount, DamageType type = DamageType.Normal) {
    Amount = amount;
    Type = type;
  }

  public static Damage operator *(Damage dmg, float delta) {
    dmg.Amount *= delta;
    return dmg;
  }
}

public struct DamageOverTime {
  public float Amount {
    get; set;
  }
  public DamageType Type {
    get; set;
  }
  public float Duration {
    get; set;
  }

  public DamageOverTime(float amount, float duration, DamageType type = DamageType.Normal) {
    Amount = amount;
    Duration = duration;
    Type = type;
  }
}
