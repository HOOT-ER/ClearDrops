
using System;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("ClearDrops", "HOOTER", "1.0.0")]
    public class ClearDrops : RustPlugin
    {
        [ChatCommand("cleardrops")]
        void ClearDropsCommand(BasePlayer player, string cmd, string[] args)
        {
            if (player == null)
            {
                return;
            }

            if (!IsAdmin(player))
            {
                return;
            }

            if (args.Length != 1)
            {
                SendReply(player, "Usage: /cleardrops <distance>");
                return;
            }

            int distance = 0;

            if (!int.TryParse(args[0], out distance))
            {
                SendReply(player, "Error: distance must be an integer\nUsage:/cleardrops <distance>");
                return;
            }

            var position = player.transform.position;
            var droppedEntities = new List<DroppedItem>();

            Vis.Entities(position, distance, droppedEntities);

            SendReply(player, "Clearing drops, please wait");
            
            foreach (var droppedEntity in droppedEntities)
            {
                if (droppedEntity != null)
                {
                    droppedEntity.Kill();
                }
            }

            Puts("Exited");

            SendReply(player, "Done");
        }

        bool IsAdmin(BasePlayer player) { 
        
            if (player.IsAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
