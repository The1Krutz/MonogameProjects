// SPDX-FileCopyrightText: 2023 The1Krutz <the1krutz@gmail.com>
// SPDX-License-Identifier: MIT

namespace SteelTower.Test;

public class TestSpy {
  public int CalledCount {
    get; private set;
  }

  public void SpyMethod() {
    CalledCount++;
  }
}

public class TestSpy<T> : TestSpy where T : struct {
  public T LastCalledWith {
    get; private set;
  }

  public void SpyMethod(T arg) {
    SpyMethod();
    LastCalledWith = arg;
  }
}