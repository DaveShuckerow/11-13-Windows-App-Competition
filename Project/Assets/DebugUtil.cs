﻿#define DEBUG

using System;
using System.Diagnostics;

public class DebugUtil
{
    [Conditional("DEBUG")]
    public static void Assert(bool condition)
    {
        if (!condition) throw new Exception();
    }
}
