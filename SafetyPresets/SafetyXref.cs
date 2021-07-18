﻿using System;
using System.Linq;
using System.Reflection;
using UnhollowerRuntimeLib.XrefScans;

namespace SafetyPresets
{
    class SafetyXref
    {
        // Yoinked with love from https://github.com/BenjaminZehowlt/DynamicBonesSafety/blob/master/DynamicBonesSafetyMod.cs
        // or well, the current alternative https://github.com/loukylor/VRC-Mods/blob/main/PlayerList/Utilities/Xref.cs

        public static bool CheckMethod(MethodInfo method, string match)
        {
            try
            {
                return XrefScanner.XrefScan(method)
                    .Where(instance => instance.Type == XrefType.Global && instance.ReadAsObject().ToString().Contains(match)).Any();
            }
            catch { }
            return false;
        }
        public static bool CheckUsed(MethodInfo method, string methodName)
        {
            try
            {
                return XrefScanner.UsedBy(method)
                    .Where(instance => instance.TryResolve() != null && instance.TryResolve().Name.Contains(methodName)).Any();
            }
            catch { }
            return false;
        }
        public static bool CheckUsing(MethodInfo method, string match, Type type)
        {
            foreach (XrefInstance instance in XrefScanner.XrefScan(method))
                if (instance.Type == XrefType.Method)
                    try
                    {
                        if (instance.TryResolve().DeclaringType == type && instance.TryResolve().Name.Contains(match))
                            return true;
                    }
                    catch
                    {

                    }
            return false;
        }
    }
}
