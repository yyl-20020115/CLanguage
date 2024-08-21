﻿using CLanguage.Interpreter;
using CLanguage.Syntax;

namespace CLanguage.Tests;

public class TestsBase
{
    protected static CInterpreter Run (string code, params int[] expectedErrors) 
        => Run (code, null, expectedErrors);

    protected static CInterpreter Run (string code, MachineInfo mi, params int[] expectedErrors)
    {
        var fullCode = "void start() { __cinit(); main(); } " + code;
        var printer = new TestPrinter (expectedErrors);
        var i = CLanguageService.CreateInterpreter (fullCode, mi ?? new TestMachineInfo (), printer: printer);
        printer.CheckForErrors ();
        i.Reset ("start");
        i.Run ();
        return i;
    }

    protected static Executable Compile (string code, params int[] expectedErrors)
    {
        return Compile (code, null, expectedErrors);
    }

    protected static Executable Compile (string code, MachineInfo mi, params int[] expectedErrors)
    {
        var fullCode = "void start() { __cinit(); main(); } " + code;
        var printer = new TestPrinter (expectedErrors);
        var i = CLanguageService.CreateInterpreter (fullCode, mi ?? new TestMachineInfo (), printer: printer);
        printer.CheckForErrors ();
        return i.Executable;
    }

    protected TranslationUnit Parse (string code, params int[] expectedErrors)
    {
        return Parse (code, null, expectedErrors);
    }

    protected static TranslationUnit Parse (string code, MachineInfo mi, params int[] expectedErrors)
    {
        var fullCode = "void start() { __cinit(); main(); } " + code;
        var printer = new TestPrinter (expectedErrors);
        var tu = CLanguageService.ParseTranslationUnit (fullCode, printer);
        printer.CheckForErrors ();
        return tu;
    }
}

