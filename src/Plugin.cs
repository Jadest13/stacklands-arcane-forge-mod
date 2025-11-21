namespace ArcaneForgeNS
{
	public class Plugin : Mod
	{
        public void Awake()
		{
			Plugin.Instance = this;
			Plugin.StaticLogger = this.Logger;

            StaticLogger.Log("ArcaneForge is Loading...");
            SokLoc.instance.LoadTermsFromFile(System.IO.Path.Combine(this.Path, "localization.tsv"), false);
            this.Harmony.PatchAll(typeof(Patches));
		}

		public override void Ready()
        {
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedResources, Consts.Idea(Consts.COMPRESSION_FORGE), 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedResources, Consts.Idea(Consts.GEODE), 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedResources, Consts.Idea(Consts.MAGIC_DUST_MINE), 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedResources, Consts.Idea(Consts.ETERNAL_FLAME), 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedResources, Consts.Idea(Consts.ENCHANTED_SMELTER), 1);
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedResources, Consts.Idea(Consts.ORE_EXCAVATOR), 1);

            StaticLogger.Log("ArcaneForge is Successfully Loaded!");
        }

		public static Plugin Instance;
		public static ModLogger StaticLogger;
	}
}
