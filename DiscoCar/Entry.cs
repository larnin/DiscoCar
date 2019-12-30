using HarmonyLib;
using Spectrum.API.Interfaces.Plugins;
using Spectrum.API.Interfaces.Systems;
using System.Reflection;
using UnityEngine;

namespace DiscoCar
{
    public class Entry : IPlugin, IUpdatable
    {
        static public PluginOptions options{get; private set;}

        static public TargetBase target { get; private set; }
        static public EffectBase effect { get; private set; }

        public void Initialize(IManager manager, string ipcIdentifier)
        {
            options = new PluginOptions(manager);
            
            Harmony harmony = new Harmony("com.reherc.discocar");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            UpdateCurrentEffect();
            UpdateCurrentTarget();
        }

        public void Update()
        {
            if (target != null)
                target.Update(Time.deltaTime);
            if(effect != null)
                effect.Clean();
        }

        public static void UpdateCurrentTarget()
        {
            target = TargetBase.MakeTarget(options.targetType);
        }

        public static void UpdateCurrentEffect()
        {
            effect = EffectBase.MakeEffect(options.effectType);
        }
    }
}
