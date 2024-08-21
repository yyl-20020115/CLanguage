using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CLanguage.Tests;

class TestPrinter (params int[] expectedErrors) : Report.Printer
{
    private readonly int[] expectedErrors = expectedErrors;
    private readonly List<int> encounteredErrors = new List<int> ();

    public override void Print (Report.AbstractMessage msg)
    {
        if (msg.MessageType != "Info") {
            encounteredErrors.Add (msg.Code);
            if (!expectedErrors.Contains (msg.Code)) {
                Assert.Fail (msg.ToString ());
            }
        }
    }

    public void CheckForErrors ()
    {
        foreach (var e in expectedErrors) {
            if (!encounteredErrors.Contains (e)) {
                Assert.Fail ("Expected error " + e + " but never got it.");
            }
        }
    }
}

