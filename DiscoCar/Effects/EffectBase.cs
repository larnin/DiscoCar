using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscoCar
{
    public abstract class EffectBase
    {
        public static EffectBase MakeEffect(EffectType type)
        {
            switch (type)
            {
                case EffectType.Gradient:
                case EffectType.Random:
                case EffectType.RandomGradient:
                    break;
            }

            return null;
        }

    }
}
