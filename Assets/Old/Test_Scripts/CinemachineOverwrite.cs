using Unity.Cinemachine;
using UnityEngine;

public class CinemachineOverwrite : CinemachineExtension
{
    [SerializeField] private GameObject anchor;
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (enabled && stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos = anchor.transform.position;
            state.RawPosition = pos;
        }
    }
}