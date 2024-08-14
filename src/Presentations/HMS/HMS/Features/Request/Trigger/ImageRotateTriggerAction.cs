namespace HMS;

public class ImageRotateTriggerAction : TriggerAction<VisualElement>
{
    public double RotationAngle { get; set; } // Set this property to the desired rotation angle

    protected override void Invoke(VisualElement sender)
    {
        if (sender is Image image)
        {
            // Rotate the image using animation
            image.RotateTo(RotationAngle, 250); // Adjust duration as needed
        }
    }
}
