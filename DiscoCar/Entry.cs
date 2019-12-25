using HarmonyLib;
using Spectrum.API.Configuration;
using Spectrum.API.GUI.Controls;
using Spectrum.API.GUI.Data;
using Spectrum.API.Interfaces.Plugins;
using Spectrum.API.Interfaces.Systems;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#pragma warning disable IDE0051

namespace DiscoCar
{
    public class Entry : IPlugin, IUpdatable
    {
        public static Settings Config;

        public void Initialize(IManager manager, string ipcIdentifier)
        {
            Config = new Settings("Config");
            foreach (var item in new Dictionary<string, object>()
            {
                {"DiscoFlames", false },
                {"DiscoOverheat", false }
            })
                if (!Config.ContainsKey(item.Key))
                    Config[item.Key] = item.Value;
            Config.Save();


            manager.Menus.AddMenu(MenuDisplayMode.Both, new MenuTree("discocar.main", "DISCO CAR [FF0000](SEIZURE WARNING !)[-]")
            {
                new CheckBox(MenuDisplayMode.Both, "discocar.main.flames", "DISCO FLAMES")
                .WithGetter(() => {
                    return Config.GetItem<bool>("DiscoFlames");
                })
                .WithSetter((value) => {
                    Config["DiscoFlames"] = value;
                    Config.Save();
                }),

                new CheckBox(MenuDisplayMode.Both, "discocar.main.overheat", "DISCO OVERHEAT")
                .WithGetter(() => {
                    return Config.GetItem<bool>("DiscoOverheat");
                })
                .WithSetter((value) => {
                    Config["DiscoOverheat"] = value;
                    Config.Save();
                })
            });

            Harmony harmony = new Harmony("com.reherc.discocar");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void Update()
        {
            var mats = UnityEngine.Object.FindObjectsOfType<Material>();
            foreach(var mat in mats)
            {
                foreach(var name in MaterialEx.supportedColors_)
                {
                    Color c = new Color(Random.value, Random.value, Random.value);
                    mat.SetColor(name, c);
                }
            }
        }
    }

    [HarmonyPatch(typeof(BackLightsWidget), "SetVisual", new System.Type[] { typeof(float), typeof(Color) })]
    [HarmonyPatch(typeof(BackLightsWidget), "SetVisual", new System.Type[] { typeof(float), typeof(float), typeof(Color) })]
    public class BackLightsWidgetSetVisual
    {
        static void Postfix(BackLightsWidget __instance)
        {
            if (!Entry.Config.GetItem<bool>("DiscoOverheat")) return;

            Color color = new Color(Random.value, Random.value, Random.value, 1);
            __instance.backLightMat_.SetColor("_Color", color);
            __instance.backLightMat_.SetColor("_Color2", color);
        }
    }

    [HarmonyPatch(typeof(JetFlame), "UpdateMaterialProperties")]
    public class JetFlameUpdateMaterialProperties
    {
        static void Postfix(JetFlame __instance)
        {
            if (!Entry.Config.GetItem<bool>("DiscoFlames")) return;

            Color color = new Color(Random.value, Random.value, Random.value, 1);

            __instance.propertyBlock_.SetColor(JetFlame.id_EmitColor1_, color);
            __instance.propertyBlock_.SetColor(JetFlame.id_EmitColor2_, color);
            __instance.renderer_.SetPropertyBlock(__instance.propertyBlock_);
        }
    }

    [HarmonyPatch(typeof(CarLogic), "Update")]
    public class CarLogicUpdate
    {
        static void Postfix(CarLogic __instance)
        {
            var comps = __instance.GetComponentsInChildren<ColorChanger>();

            foreach (var comp in comps)
            {
                foreach (var r in comp.rendererChangers_)
                {
                    List<Color> colors = new List<Color>();
                    for (int i = 0; i < (int)(ColorChanger.ColorType.Size_); i++)
                        colors.Add(new Color(Random.value, Random.value, Random.value));

                    r.SetColors(colors, 0xFFFFFFFF);

                    foreach (var u in r.uniformChangers_)
                    {
                        colors.Clear();
                        for (int i = 0; i < (int)(ColorChanger.ColorType.Size_); i++)
                            colors.Add(new Color(Random.value, Random.value, Random.value));
                        u.SetColor(r.materials_[u.materialIndex_], colors, 0xFFFFFFFF);
                    }

                }
            }
        }
    }
}
