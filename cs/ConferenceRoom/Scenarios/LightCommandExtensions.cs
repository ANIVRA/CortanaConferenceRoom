using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Q42.HueApi;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.Original;

namespace ConferenceRoom.Scenarios
{
    public static class LightCommandExtensions
    {
        public static LightCommand SetBrightness(this LightCommand self, byte brightness)
        {
            self.Brightness = brightness;
            return self;
        }

        public static LightCommand SetTransition(this LightCommand self, TimeSpan transitionTime)
        {
            self.TransitionTime = transitionTime;
            return self;
        }

        public static LightCommand SetTransitionMs(this LightCommand self, int transitionTimeMs)
        {
            return self.SetTransition(TimeSpan.FromMilliseconds(transitionTimeMs));
        }

        public static LightCommand SetColor(this LightCommand self, double r, double g, double b)
        {
            return self.SetColor(new RGBColor(r, g, b));
        }

        public static LightCommand SetRGB(this LightCommand self, double r, double g, double b)
        {
            return self.SetColor(new RGBColor(r, g, b));
        }
    }
}
