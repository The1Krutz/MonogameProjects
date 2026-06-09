// SPDX-FileCopyrightText: 2023 The1Krutz <the1krutz@gmail.com>
// SPDX-License-Identifier: MIT

namespace SteelTower.Test;

public class HealthTests {
  private readonly Health _health;

  private readonly TestSpy _onHealthDepletedSpy = new();
  private readonly TestSpy<float> _onHealthChangedSpy = new();

  public HealthTests() {
    _health = new Health(
      _onHealthChangedSpy.SpyMethod,
      _onHealthDepletedSpy.SpyMethod,
      10,
      5);
  }

  [Fact]
  public void TakeDamage_DamageReducesCurrentHealth() {
    _health.TakeDamage(new Damage(3));
    Assert.Equal(2, _health.CurrentHealth);
    Assert.Equal(1, _onHealthChangedSpy.CalledCount);
    Assert.Equal(2, _onHealthChangedSpy.LastCalledWith);
  }

  [Fact]
  public void TakeDamage_HealingIncreasesCurrentHealth() {
    _health.TakeDamage(new Damage(3, DamageType.Healing));
    Assert.Equal(8, _health.CurrentHealth);
    Assert.Equal(8, _onHealthChangedSpy.LastCalledWith);
  }

  [Fact]
  public void TakeDamage_DeathCallbackProperlyCalled() {
    _health.TakeDamage(new Damage(10));
    Assert.Equal(0, _health.CurrentHealth);
    Assert.Equal(1, _onHealthDepletedSpy.CalledCount);
    Assert.Equal(1, _onHealthChangedSpy.CalledCount);
  }

  [Fact]
  public void TakeDamage_HealthDoesNotGoBelowZero() {
    _health.TakeDamage(new Damage(100));
    Assert.Equal(0, _health.CurrentHealth);
    Assert.Equal(1, _onHealthDepletedSpy.CalledCount);
    Assert.Equal(1, _onHealthChangedSpy.CalledCount);
  }

  [Fact]
  public void TakeDamage_HealthDoesNotGoAboveMaximum() {
    _health.TakeDamage(new Damage(100, DamageType.Healing));
    Assert.Equal(10, _health.CurrentHealth);
    Assert.Equal(1, _onHealthChangedSpy.CalledCount);
    Assert.Equal(10, _onHealthChangedSpy.LastCalledWith);
  }
}
