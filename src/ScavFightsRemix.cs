using Menu.Remix.MixedUI;

namespace ScavFights
{
    public class ScavFightsRemix : OptionInterface
    {
        const float TITLEX = 20f;
        const float TITLEY = 540f;
        const float CHECKBOXX = 60f;
        const float CHECKBOXY = 500f;
        public ScavFightsRemix()
        {
            healthMult = config.Bind("ScavFights_healthMult", 1, new ConfigurableInfo("", new ConfigAcceptableRange<int>(1, 5)));
            weaponSpawn = config.Bind("ScavFights_weaponSpawn", true);
        }

        public readonly Configurable<int> healthMult;
        public readonly Configurable<bool> weaponSpawn;

        private OpCheckBox OPweaponSpawn;
        private OpUpdown OPhealthMult;

        public override void Initialize()
        {
            base.Initialize();

            var config = new OpTab(this, "Options");
            Tabs = [config];

            OPweaponSpawn = new OpCheckBox(weaponSpawn, new(CHECKBOXX, CHECKBOXY));
            OPhealthMult = new OpUpdown(healthMult, new(CHECKBOXX, CHECKBOXY - 90f), 100f);

            config.AddItems(
                new OpLabel(TITLEX, TITLEY, "SCAVENGER FIGHT CLUB", true),
                OPweaponSpawn,
                new OpLabel(CHECKBOXX + 40f, CHECKBOXY, "Toggle scavengers spawning with weapons"),
                OPhealthMult,
                new OpLabel(CHECKBOXX, CHECKBOXY - 45f, "Scavenger Health Multiplier"));
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
