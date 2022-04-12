namespace Weapons
{
    public interface IShootWeapon
    {
        //Basic WeaponShooting
        void ShootWeapon();
        
    }

    public interface IReloadWeapon
    {
        void ReloadWeapon();
    }

    public interface IReadyWeapon
    {
        void ReadyWeapon();
    }
}