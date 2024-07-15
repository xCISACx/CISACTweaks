using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CISACTweaks;

public class NoPassingThroughWallsProjectiles : GlobalProjectile
{
    public override void SetDefaults(Projectile projectile)
    {
        if (projectile.aiStyle == 28 && projectile.friendly == false)
        {
            projectile.tileCollide = true;
        }
    }
    
    /*public override void AI(Projectile projectile)
    {
        base.AI(projectile);
        if (projectile.aiStyle == 28 && projectile.friendly == false)
        {
            if (projectile.tileCollide)
            {
                projectile.active = false;
            }
        }
    }*/
}