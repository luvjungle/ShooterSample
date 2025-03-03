namespace Code.Player.Animation
{
	public interface IHandIkModel
	{
		public float ConstraintActivateSpeed { get; }
		public bool IkActive { get; set; }
	}
}