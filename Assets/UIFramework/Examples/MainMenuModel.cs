using UIFramework.Binding;

public class MainMenuModel
{
    public BindableProperty<string> PlayerName { get; } = new BindableProperty<string>("Player");
    public BindableProperty<int> Coins { get; } = new BindableProperty<int>(0);
    public BindableProperty<bool> IsSoundEnabled { get; } = new BindableProperty<bool>(true);
}