namespace BlazorBasicsServer.Pages
{
    public partial class CodeBehind
    {
        private bool _showMessage = false;

        private void ToggleMessage()
        {
            _showMessage = !_showMessage;
        }
    }
}
