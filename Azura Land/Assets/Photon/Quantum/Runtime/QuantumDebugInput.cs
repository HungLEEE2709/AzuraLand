namespace Quantum {
  using Photon.Deterministic;
  using UnityEngine;

  public class QuantumDebugInput : MonoBehaviour {
    private void OnEnable() {
      QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
    }

    public void PollInput(CallbackPollInput callback) {
      Quantum.Input input = new Quantum.Input();

      FP x = 0;
      FP y = 0;

      if (UnityEngine.Input.GetKey(KeyCode.RightArrow)) {
        x = 1;
      } else if (UnityEngine.Input.GetKey(KeyCode.LeftArrow)) {
        x = -1; 
      }

      input.Direction = new FPVector2(x, y);

      input.Jump = UnityEngine.Input.GetKey(KeyCode.UpArrow);

      callback.SetInput(input, DeterministicInputFlags.Repeatable);
    }
  }
}