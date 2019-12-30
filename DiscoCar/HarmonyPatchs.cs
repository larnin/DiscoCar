using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using Object = System.Object;

namespace DiscoCar
{
    [HarmonyPatch(typeof(BackLightsWidget), "SetVisual", new System.Type[] { typeof(float), typeof(Color) })]
    [HarmonyPatch(typeof(BackLightsWidget), "SetVisual", new System.Type[] { typeof(float), typeof(float), typeof(Color) })]
    public class BackLightsWidgetSetVisual
    {
        static void Postfix(BackLightsWidget __instance)
        {
            if (Entry.options.targetType != TargetType.Flames)
                return;

            TargetFlames target = Entry.target as TargetFlames;
            if (target == null)
                return;

            target.AddMaterial(__instance.backLightMat_, (Object obj, Color value) => 
            {
                var mat = obj as Material;
                if (mat == null)
                    return;

                mat.SetColor("_Color", value);
                mat.SetColor("_Color2", value);
            }, "Backlight");
        }
    }

    [HarmonyPatch(typeof(JetFlame), "UpdateMaterialProperties")]
    public class JetFlameUpdateMaterialProperties
    {
        static void Postfix(JetFlame __instance)
        {
            if (Entry.options.targetType != TargetType.Flames)
                return;

            TargetFlames target = Entry.target as TargetFlames;
            if (target == null)
                return;

            if (!Entry.options.discoFlames)
                return;

            Color color = new Color(Random.value, Random.value, Random.value, 1);

            target.AddMaterial(__instance.propertyBlock_, (Object obj, Color value) =>
            {
                var mat = obj as MaterialPropertyBlock;
                mat.SetColor(JetFlame.id_EmitColor1_, color);
                mat.SetColor(JetFlame.id_EmitColor2_, color);
                __instance.renderer_.SetPropertyBlock(mat);
            }, "JetFlame");
        }
    }

    //[HarmonyPatch(typeof(CarLogic), "Update")]
    //public class CarLogicUpdate
    //{
    //    static void Postfix(CarLogic __instance)
    //    {
    //        var comps = __instance.GetComponentsInChildren<ColorChanger>();

    //        foreach (var comp in comps)
    //        {
    //            foreach (var r in comp.rendererChangers_)
    //            {
    //                List<Color> colors = new List<Color>();
    //                for (int i = 0; i < (int)(ColorChanger.ColorType.Size_); i++)
    //                    colors.Add(new Color(Random.value, Random.value, Random.value));

    //                r.SetColors(colors, 0xFFFFFFFF);

    //                foreach (var u in r.uniformChangers_)
    //                {
    //                    colors.Clear();
    //                    for (int i = 0; i < (int)(ColorChanger.ColorType.Size_); i++)
    //                        colors.Add(new Color(Random.value, Random.value, Random.value));
    //                    u.SetColor(r.materials_[u.materialIndex_], colors, 0xFFFFFFFF);
    //                }

    //            }
    //        }
    //    }
    //}
}