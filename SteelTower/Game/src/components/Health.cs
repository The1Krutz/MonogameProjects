// SPDX-FileCopyrightText: 2023 The1Krutz <the1krutz@gmail.com>
// SPDX-License-Identifier: MIT

using System;
using Microsoft.Xna.Framework;

namespace SteelTower;

public class Health : IDamageable
{
  private float MaxHealth
  {
    get;
  }
  public float CurrentHealth
  {
    get => _currentHealth;
  }

  public readonly Action<float> OnHealthChanged;
  public readonly Action OnHealthDepleted;
  private float _currentHealth;

  public Health(Action<float> onHealthChanged, Action onHealthDepleted, float maxHealth = 100.0f) :
   this(onHealthChanged, onHealthDepleted, maxHealth, maxHealth)
  {
  }

  public Health(Action<float> onHealthChanged, Action onHealthDepleted, float maxHealth, float currentHealth)
  {
    OnHealthChanged = onHealthChanged;
    OnHealthDepleted = onHealthDepleted;

    MaxHealth = maxHealth;
    _currentHealth = currentHealth;
  }

  public void TakeDamage(Damage incoming)
  {
    // @todo - if you're going to do damage resistances by type, this is probably where it goes
    switch (incoming.Type)
    {
      case DamageType.Healing:
        _currentHealth += incoming.Amount;
        break;
      default:
        _currentHealth -= incoming.Amount;
        break;
    }

    _currentHealth = MathHelper.Clamp(_currentHealth, 0, MaxHealth);

    if (_currentHealth <= 0)
    {
      // @todo - add some visual for this; right now it just disappears
      OnHealthDepleted();
    }

    // Console.WriteLine("current health: {0}", _currentHealth);
    OnHealthChanged(_currentHealth);
  }

  public void TakeDamage(DamageOverTime incoming)
  {
    // TODO: add the incoming dot to a list of dots to process every tick
  }
}
