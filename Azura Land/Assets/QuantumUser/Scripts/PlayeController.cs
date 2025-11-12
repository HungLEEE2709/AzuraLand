namespace Quantum
{
    using Photon.Deterministic;

    public unsafe class PlayeController : SystemMainThreadFilter<PlayeController.Filter>
    {
        public override void Update(Frame frame, ref Filter filter)
        {
            var input = frame.GetPlayerInput(0);
            filter.Body->Velocity = input->Direction * filter.PlayerInfo -> Speed ;
        }

        public struct Filter
        {
            public EntityRef Entity;
            public PhysicsBody2D* Body;
            public Transform2D* Transform;
            public PlayerState* State;
            public PlayerInfo* PlayerInfo;
        }
    }
}
