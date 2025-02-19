using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SteeringWheel : XRBaseInteractable
{
    [SerializeField] private Transform wheelTransform;

    public UnityEvent<float> OnWheelRotated;

    private float currentAngle = 0.0f;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        currentAngle = FindWheelAngle();
        Debug.Log($"Руль схвачен! Начальный угол: {currentAngle}");
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
                RotateWheel();
        }
    }

    private void RotateWheel()
    {
        float totalAngle = FindWheelAngle();
        float angleDifference = totalAngle - currentAngle;

        Debug.Log($"currentAngle: {currentAngle}, totalAngle: {totalAngle}, angleDifference: {angleDifference}");

        if (Mathf.Abs(angleDifference) > 0.1f)  // Фильтр мелких движений
        {
            wheelTransform.Rotate(transform.forward, -angleDifference, Space.World);
            currentAngle = totalAngle;
            OnWheelRotated?.Invoke(angleDifference);
        }
    }

    private float FindWheelAngle()
    {
        float totalAngle = 0;

        foreach (IXRSelectInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindLocalPoint(interactor.transform.position);
            float angle = ConvertToAngle(direction);
            float sensitivity = FindRotationSensitivity();

            Debug.Log($"Interactor: {interactor.transform.name}, Direction: {direction}, Angle: {angle}, Sensitivity: {sensitivity}");

            totalAngle += angle * sensitivity;
        }

        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector3 position)
    {
        return (Vector2)transform.InverseTransformPoint(position);
    }

    private float ConvertToAngle(Vector2 direction)
    {
        return Vector2.SignedAngle(Vector2.up, direction);
    }

    private float FindRotationSensitivity()
    {
        return interactorsSelecting.Count > 0 ? 1.0f / interactorsSelecting.Count : 1.0f;
    }
}