﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Q42.HueApi;
using Q42.HueApi.Interfaces;

namespace ConferenceRoom.Scenarios
{
    public class ReleaseNightScenario
    {
        private readonly IHueClient _hueClient;

        public ReleaseNightScenario(IHueClient hueClient)
        {
            _hueClient = hueClient;
        }

        public async Task ExecuteAsync(CancellationToken token)
        {
            //using (var snd = new SoundPlayer(Resources.redalertfull))
            //{
            //    snd.Load();
            try
            {
                await Task.Delay(500, token);
                await _hueClient.SendCommandAsync(
                    new LightCommand().TurnOn().SetRGB(0, 0, 1).SetBrightness(64));
                //snd.PlayLooping();

                await Task.Delay(250, token);
                for (var i = 0; i < 24; ++i)
                {
                    await _hueClient.SendCommandAsync(new LightCommand().SetBrightness(255));
                    await Task.Delay(750, token);

                    await _hueClient.SendCommandAsync(
                        new LightCommand().SetBrightness(0).SetTransitionMs(400));
                    await Task.Delay(1000, token);

                }
            }
            catch (TaskCanceledException) { }

            //snd.Stop();
            await Task.Delay(200);
            await _hueClient.SendCommandAsync(
                new LightCommand().SetRGB(1, 1, 1).SetBrightness(255));
            //}
        }
    }
}