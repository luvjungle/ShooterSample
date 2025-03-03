namespace Code.UI.OpenClose
{
	public interface IUiOpenClose
	{
		bool Opened { get; }
		void Open();
		void Close();
	}
}