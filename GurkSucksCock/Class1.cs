using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;

namespace GurkSucksCock
{
    public class Class1 : RocketPlugin<config>
    {
        protected override void Load()
        {
            UnturnedPlayerEvents.OnPlayerDeath += death;
        }
        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerDeath -= death;
        }
        private void death(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            if (player == null || murderer == CSteamID.Nil)
                return;

            if (cause == EDeathCause.GUN || cause == EDeathCause.MELEE || cause == EDeathCause.MISSILE)
            {
                var k = PlayerTool.getPlayer(murderer);
                if (k == null)
                    return;
                var l = k.equipment.asset;
                if (l != null)
                {

                    


                    var j = Configuration.Instance.Guns.Find(x => x.gunid == l.id);
                    if (j == null)
                        return;

                    EffectManager.triggerEffect(new TriggerEffectParameters
                    {
                        asset = (EffectAsset)Assets.find(EAssetType.EFFECT, j.effectId),
                        position = player.Position
                    });

                }
            }
        }
    }

    public class config : IRocketPluginConfiguration
    {
        public List<GunStuff> Guns { get; set; }

        public void LoadDefaults()
        {
            Guns = new List<GunStuff>
            {
                new GunStuff
                {
                    gunid = 353,
                    effectId = 5
                }
            };
        }
    }

    public class GunStuff
    {
        public ushort gunid { get; set; }
        public ushort effectId { get; set; }
    }
}