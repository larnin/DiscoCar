using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace DiscoCar
{
    [HarmonyPatch(typeof(BackLightsWidget), "Start", new System.Type[] { typeof(float), typeof(Color) })]
    public class BackLightsWidgetStart
    {
        static void Postfix(BackLightsWidget __instance)
        {
            if (Entry.options.targetType != TargetType.Flames)
                return;

            TargetFlames target = Entry.target as TargetFlames;
            if (target == null)
                return;

            target.AddMaterial(__instance.backLightMat_, (Material mat, Color value) => { mat.SetColor("_Color", value); }, "_Color");
            target.AddMaterial(__instance.backLightMat_, (Material mat, Color value) => { mat.SetColor("_Color2", value); }, "_Color2");
        }
    }

    [HarmonyPatch(typeof(JetFlame), "Awake")]
    public class JetFlameAwake
    {
        static bool Prefix(JetFlame __instance)
        {
            return Entry.options.targetType != TargetType.Flames;

            Color color = new Color(Random.value, Random.value, Random.value, 1);

            __instance.propertyBlock_.SetColor(JetFlame.id_EmitColor1_, color);
            __instance.propertyBlock_.SetColor(JetFlame.id_EmitColor2_, color);
            __instance.renderer_.SetPropertyBlock(__instance.propertyBlock_);
        }
    }

    [HarmonyPatch(typeof(JetFlame), "UpdateMaterialProperties")]
    public class JetFlameUpdateMaterialProperties
    {
        static bool Prefix(JetFlame __instance)
        {
            return Entry.options.targetType != TargetType.Flames;

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
