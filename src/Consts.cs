namespace ArcaneForgeNS
{
	internal static class Consts
	{
		public static string Idea(string id)
		{
			return PREFIX + "blueprint_" + id;
        }

        public static string Card(string id)
        {
            return PREFIX + id;
        }

        public const string PREFIX = "arcaneforge.";
        public const string COMPRESSION_FORGE = "compression_forge";
        public const string GEODE = "geode";
        public const string MAGIC_DUST_MINE = "magic_dust_mine";
        public const string ETERNAL_FLAME = "eternal_flame";
        public const string ENCHANTED_SMELTER = "enchanted_smelter";
        public const string ORE_EXCAVATOR = "ore_excavator";
    }
}
