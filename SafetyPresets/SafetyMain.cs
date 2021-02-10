﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace SafetyPresets
{
    internal class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            Settings.SetupDefaults();
            Prefs.SetupPrefs();
            UI.SetupUIX();
            CompatChecks.CheckEnvironment();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (buildIndex==-1)
            {
                MelonCoroutines.Start(Helpers.ChangeOnInstance());
            }
            //ShinigamiSettings.OnSceneWasLoaded(buildIndex);
        }

    }
}
