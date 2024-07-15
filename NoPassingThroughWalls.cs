using System.Collections.Generic;
using CISACTweaks.Common.Configs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CISACTweaks;

class NoPassingThroughWalls : GlobalNPC
{
    //private List<int> _npcids = new() { NPCID.BurningSphere, NPCID.WaterSphere, NPCID.ChaosBall, NPCID.ChaosBallTim};
    
    // To see how this config option was added, see ExampleModConfig.cs
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModContent.GetInstance<MainConfig>().DisableProjectilesThroughWallsToggle;
    }
    
    public override void SetDefaults(NPC npc)
    {
        if (npc.aiStyle == 9)
        {
            npc.noTileCollide = false;
        }
        /*if (_npcids.Contains(npc.type))
        {
            npc.noTileCollide = false;
        }*/
    }

    public override void AI(NPC npc)
    {
        base.AI(npc);
        if (npc.aiStyle == 9)
        {
            if (npc.collideX || npc.collideY)
            {
                npc.active = false;
            }
        }
    }
}