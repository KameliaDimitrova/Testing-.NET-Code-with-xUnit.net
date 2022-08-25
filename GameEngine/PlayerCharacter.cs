using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameEngine;
public class PlayerCharacter : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    // This method is called by the Set accessor of each property.
    // The CallerMemberName attribute that is applied to the optional propertyName
    // parameter causes the property name of the caller to be substituted as an argument.
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    private int _health = 100;

    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName => $"{FirstName} {LastName}";

    public string Nickname { get; set; }

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            NotifyPropertyChanged();
        }
    }

    public bool IsNoob { get; set; }

    public List<string>? Weapons { get; set; }

    public event EventHandler<EventArgs>? PlayerSlept;

    public PlayerCharacter()
    {
        FirstName = this.GenerateRandomFirstName();

        IsNoob = true;

        this.CreateStartingWeapons();
    }

    private string GenerateRandomFirstName()
    {
        var possibleRandomStartingNames = new[]
        {
            "Danieth",
            "Derick",
            "Shalnor",
            "GTothlop",
            "Boldraketeethtop",
        };

        return possibleRandomStartingNames[
            new Random().Next(0, possibleRandomStartingNames.Length)];
    }

    private void CreateStartingWeapons()
    {
        Weapons = new List<string>()
        {
            "Long Bow",
            "Short Bow",
            "Short Sword",
        };
    }

    public void Sleep()
    {
        var healthIncrease = CalculateHealthIncrease();

        Health += healthIncrease;

        OnPlayerSlept(EventArgs.Empty);
    }

    private void OnPlayerSlept(EventArgs e)
    {
        PlayerSlept?.Invoke(this, e);
    }

    private int CalculateHealthIncrease()
    {
        var rnd = new Random();

        return rnd.Next(1, 101);
    }

    public void TakeDamage(int damage)
    {
        Health = Math.Max(1, Health -= damage);
    }
}
