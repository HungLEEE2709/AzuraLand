namespace Quantum {
    using Photon.Deterministic;

    public unsafe class PlayerCollision : SystemSignalsOnly,
        ISignalOnCollisionEnter2D,
        ISignalOnCollisionExit2D {

        public void OnCollisionEnter2D(Frame frame, CollisionInfo2D info) {
            if (frame.Unsafe.TryGetPointer<PlayerState>(info.Entity, out var state)
                && frame.Has<GroundTag>(info.Other)) {
                state->IsGrounded = true;
            }
            else if (frame.Unsafe.TryGetPointer<PlayerState>(info.Other, out var otherState)
                && frame.Has<GroundTag>(info.Entity)) {
                otherState->IsGrounded = true;
            }
        }

        public void OnCollisionExit2D(Frame frame, ExitInfo2D info) {
            if (frame.Unsafe.TryGetPointer<PlayerState>(info.Entity, out var state)
                && frame.Has<GroundTag>(info.Other)) {
                state->IsGrounded = false;
            }
            else if (frame.Unsafe.TryGetPointer<PlayerState>(info.Other, out var otherState)
                && frame.Has<GroundTag>(info.Entity)) {
                otherState->IsGrounded = false;
            }
        }
    }
}
