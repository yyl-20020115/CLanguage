﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLanguage.Interpreter;

namespace CLanguage.Tests;

[TestClass]
public class CastTests
{
    private static CInterpreter Run (string code)
    {
        var fullCode = "void start() { __cinit(); main(); } " + code;
        var i = CLanguageService.CreateInterpreter (fullCode, new ArduinoTestMachineInfo (), printer: new TestPrinter ());
        i.Reset ("start");
        i.Run ();
        return i;
    }

    [TestMethod]
    public void CharMasksInt ()
    {
        var i = Run (@"
void main () {
    assertAreEqual (' ', (char)0x2020);
}");
    }
}
