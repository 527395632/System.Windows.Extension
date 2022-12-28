using System;
using System.Windows.Media.Effects;

namespace System.Windows.Extension.Media.Effects
{
    public class ColorComplementEffect : EffectBase
    {
        private static readonly PixelShader Shader;

        static ColorComplementEffect()
        {
            Shader = new PixelShader
            {
                UriSource = new Uri("pack://application:,,,/System.Windows.Extension;component/Resources/Effects/ColorComplementEffect.ps")
            };
        }

        public ColorComplementEffect()
        {
            PixelShader = Shader;

            UpdateShaderValue(InputProperty);
        }
    }
}