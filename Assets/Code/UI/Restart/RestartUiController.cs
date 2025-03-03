using Code.Bootstrap.Loading;
using Code.UI.OpenClose.MoveApart;
using DG.Tweening;

namespace Code.UI.Restart
{
	public class RestartUiController
	{
		private readonly LoadingController _loadingController;
		private readonly MoveApartOpenClose _openClose;
		private readonly RestartUiConfigSO _config;

		public RestartUiController(RestartUiView view, RestartUiConfigSO config, LoadingController loadingController)
		{
			_loadingController = loadingController;
			_config = config;
			
			_openClose = new MoveApartOpenClose(view, config);
			view.AddButtonCallback(Restart);
		}

		public void OpenWithDelay() => DOVirtual.DelayedCall(_config.AfterDeathDelay, Open);

		public void Open() => _openClose.Open();

		private void Restart()
		{
			_loadingController.FadeAndLoad("Game");
		}
	}
}