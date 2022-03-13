using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logger
{
    const bool DEBUG = true;

    // A utility function like this will help clean up performance on long builds,
    // as well as allow access to the console without requiring a file import UnityEngine directly
    public static void Write(string message)
    {
        if (DEBUG)
        {
            Debug.Log(message);
        }
    }
}
