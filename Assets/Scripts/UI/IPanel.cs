public interface IPanel
{
    public ICanvasController CanvasController { get; }

    public void SetupPanel(ICanvasController canvasReference);
    public void ClosePanel();
}
