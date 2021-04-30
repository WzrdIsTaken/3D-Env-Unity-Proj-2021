using UnityEditor;

[CustomEditor(typeof(CameraMovement))] [CanEditMultipleObjects]
public class CameraMovementEditor : Editor
{
    CameraMovement cameraMovement;

    void OnEnable()
    {
        cameraMovement = (CameraMovement)target;
    }

    public override void OnInspectorGUI()
    {
        using (EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();

            if (check.changed)
            {
                switch (cameraMovement.GetCameraMode())
                {
                    case CameraMovement.CameraMode.NONE:
                        break;
                    case CameraMovement.CameraMode.FOLLOW:
                        break;
                    case CameraMovement.CameraMode.ROTATE:
                        break;
                    case CameraMovement.CameraMode.ADVANCED_ROTATE:
                        break;
                }
            }
        }
    }
}