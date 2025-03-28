using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _thirdPersonController;
    [SerializeField] private FallingBehaviour _fallingBehaviour;
    [SerializeField] private ThirdPersonAnimation _thirdPersonAnimation;


    public ThirdPersonController ThirdPersonController { get { return _thirdPersonController; } }
    public FallingBehaviour FallingBehaviour { get { return _fallingBehaviour; } }
    public ThirdPersonAnimation ThirdPersonAnimation { get { return _thirdPersonAnimation; } }

}
