namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class PlayerSpawn : SystemSignalsOnly, ISignalOnPlayerAdded, ISignalOnPlayerRemoved
    {
        public void OnPlayerAdded(Frame frame, PlayerRef player, bool firstTime)
        {
            var playerData = frame.GetPlayerData(player);
            var spawnedPlayer = frame.Create(playerData.PlayerAvatar);
            var playerInfor = frame.Get<PlayerInfo>(spawnedPlayer);
            playerInfor.PlayerRef = player;
            frame.Set(spawnedPlayer, playerInfor);
        }
        public void OnPlayerRemoved(Frame frame, PlayerRef player)
        {
            var players = frame.GetComponentIterator<PlayerInfo>();
            foreach (var item in players)
            {
                if (item.Component.PlayerRef == player)
                {
                    frame.Destroy(item.Entity);
                }
            }
        }
    }
}
