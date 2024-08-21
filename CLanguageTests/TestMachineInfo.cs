﻿using CLanguage.Interpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CLanguage.Tests;

public class TestMachineInfo : MachineInfo
{
    public TestMachineInfo ()
    {
        CharSize = 1;
        ShortIntSize = 2;
        IntSize = 2;
        LongIntSize = 4;
        LongLongIntSize = 8;
        FloatSize = 4;
        DoubleSize = 8;
        LongDoubleSize = 8;
        PointerSize = 2;
        HeaderCode = "";

        AddInternalFunction ("void assertAreEqual (int expected, int actual)", AssertAreEqual);
        AddInternalFunction ("void assertU16AreEqual (unsigned int expected, unsigned int actual)", AssertU16AreEqual);
        AddInternalFunction ("void assert32AreEqual (long expected, long actual)", Assert32AreEqual);
        AddInternalFunction ("void assertU32AreEqual (unsigned long expected, unsigned long actual)", AssertU32AreEqual);
        AddInternalFunction ("void assertBoolsAreEqual (bool expected, bool actual)", AssertBoolsAreEqual);
        AddInternalFunction ("void assertFloatsAreEqual (float expected, float actual)", AssertFloatsAreEqual);
        AddInternalFunction ("void assertDoublesAreEqual (double expected, double actual)", AssertDoublesAreEqual);
        AddInternalFunction ("int memcmp (void* s1, void* s2, int n)", Memcmp);
    }

    static void AssertAreEqual (CInterpreter state)
    {
        var expected = state.ReadArg (0);
        var actual = state.ReadArg (1);
        Assert.AreEqual ((short)expected, (short)actual);
    }

    static void AssertU16AreEqual (CInterpreter state)
    {
        var expected = state.ReadArg (0);
        var actual = state.ReadArg (1);
        Assert.AreEqual ((ushort)expected, (ushort)actual);
    }

    static void Assert32AreEqual (CInterpreter state)
    {
        var expected = state.ReadArg (0);
        var actual = state.ReadArg (1);
        Assert.AreEqual ((int)expected, (int)actual);
    }

    static void AssertU32AreEqual (CInterpreter state)
    {
        var expected = state.ReadArg (0);
        var actual = state.ReadArg (1);
        Assert.AreEqual ((uint)expected, (uint)actual);
    }

    static void AssertFloatsAreEqual (CInterpreter state)
    {
        var expected = state.ReadArg (0);
        var actual = state.ReadArg (1);
        Assert.AreEqual ((float)expected, (float)actual, 1.0e-6f);
    }

    static void AssertDoublesAreEqual (CInterpreter state)
    {
        var expected = state.ReadArg (0);
        var actual = state.ReadArg (1);
        Assert.AreEqual ((double)expected, (double)actual, 1.0e-12);
    }

    static void AssertBoolsAreEqual (CInterpreter state)
    {
        var expected = state.ReadArg (0);
        var actual = state.ReadArg (1);
        Assert.AreEqual ((int)expected, (int)actual);
    }

    static void Memcmp (CInterpreter interpreter)
    {
        var s1 = interpreter.ReadArg (0).PointerValue;
        var s2 = interpreter.ReadArg (1).PointerValue;
        var n = interpreter.ReadArg (2).UInt32Value;
        while (n > 0) {
            var v1 = interpreter.ReadMemory (s1).UInt8Value;
            var v2 = interpreter.ReadMemory (s2).UInt8Value;
            if (v1 != v2) {
                interpreter.Push (v1 - v2);
                return;
            }
            s1++;
            s2++;
            n--;
        }
        interpreter.Push (0);
    }
}

