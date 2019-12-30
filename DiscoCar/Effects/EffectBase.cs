using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscoCar
{
    using MaterialInfo = DiscoCar.TargetBase.MaterialInfo;

    public abstract class EffectBase
    {
        public static EffectBase MakeEffect(EffectType type)
        {
            switch (type)
            {
                case EffectType.Random:
                    return new EffectRandom();
                case EffectType.Gradient:
                case EffectType.RandomGradient:
                    break;
                default:
                    break;
            }

            return null;
        }

        public abstract void Update(List<MaterialInfo> materials);

        public abstract void Clean();

    }
}
