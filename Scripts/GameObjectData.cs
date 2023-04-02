using System;
using System.Collections.Generic;
using UnityEngine;

namespace BRTG_ClassLibrary
{
    internal abstract class LivingThing //supreme base class times a million
    {
        internal int Blood { get; set; } //Health
        internal int Sweat { get; set; } //Stamina
        internal int Tears { get; set; } //Mental Fortitude
        internal int Power { get; set; } //Attack
        internal int Grit { get; set; } //Defence
        internal int Hunger { get; set; } //Food
        internal int Thirst { get; set; } //Water
        internal int Sight { get; set; } //Ability to See
        internal int Sound { get; set; } //Ability to Hear
    }
    internal abstract class Human : LivingThing //Human IS_A LivingThing
    {
        internal decimal CurWeight { get; set; } //Inventory Space
        internal decimal MaxWeight { get; set; } //Inventory Limit
        internal Conditions humanConditions;
        internal Clothes[] body;
        internal Weapon heldWeapon;
        internal List<Item> inventory;
        public Human()
        {
            Blood = 100;
            Sweat = 0;
            Tears = 0;
            Hunger = 100;
            Thirst = 100;
            Power = 0;
            Grit = 0;
            Sight = 100;
            Sound = 100;
            CurWeight = 0;
            MaxWeight = 100;
            humanConditions = new Conditions();
            // 0 - Head     1 - Face     2 - Inner Chest      3 - Outer Chest
            // 4 - Hands    5 - Legs     6 - Inner Feet       7 - Outer Feet
            body = new Clothes[8] { null, null, new Clothes(1), null, null, new Clothes(3), new Clothes(8) , new Clothes(7) };
            heldWeapon = null;
            inventory = new List<Item>();
            UpdateHuman();
        }
        internal void UpdateHuman()
        {
            if (RandomGen.Next() == 1)
            {
                Sweat++;
            }
            if (RandomGen.Next() == 1)
            {
                Tears++;
            }
            if (Sight > 105)
            {
                Sight -= 5;
            }
            else if (Sight < 95)
            {
                Sight += 5;
            }
            else
            {
                Sight = (int)RandomGen.Next(95, 105);
            }
            if (Sound > 105)
            {
                Sound -= 5;
            }
            else if (Sound < 95)
            {
                Sound += 5;
            }
            else
            {
                Sound = (int)RandomGen.Next(95, 105);
            }
            if (RandomGen.Next(1, 100) == 1)
            {
                Hunger--;
            }
            if (RandomGen.Next(1, 25) == 1)
            {
                Thirst--;
            }

            ApplyConditions();
            ApplyWeight();
            ApplyGrit();
            ApplyPower();

            if (Blood < 0)
            {
                Blood = 0;
            }
            if (Sweat < 0)
            {
                Sweat = 0;
            }
            if (Sweat > 100)
            {
                Sweat = 100;
                Blood--;
            }
            if (Tears < 0)
            {
                Tears = 0;
            }
            if (Tears > 100)
            {
                Tears = 100;
                Blood--;
            }
            if (Sight < 0)
            {
                Sight = 0;
            }
            if (Sight > 200)
            {
                Sight = 0;
                humanConditions.Blind = true;
            }
            if (Sound < 0)
            {
                Sound = 0;
            }
            if (Sound > 200)
            {
                Sound = 0;
                Blood--;
                humanConditions.Deaf = true;
            }
            if (Hunger < 0)
            {
                Hunger = 0;
                Blood--;
            }
            if (Thirst < 0)
            {
                Thirst = 0;
                Blood--;
            }
        }
        private void ApplyConditions()
        {
            Blood = (humanConditions.Bleeding && RandomGen.Next(1, 4) == 1) ? Blood-- : Blood;
            Blood = (humanConditions.BrokenBones && RandomGen.Next(1, 2) == 1) ? Blood-- : Blood;
            Sweat = (humanConditions.BrokenBones && RandomGen.Next(1, 4) == 1) ? Sweat++ : Sweat;
            Blood = (humanConditions.Poisoned && RandomGen.Next(1, 5) == 1) ? Blood-- : Blood;
            Tears = (humanConditions.Addicted) ? Tears++ : Tears;
            Sweat = (humanConditions.Encumbered) ? Sweat++ : Sweat;
            Sight = (humanConditions.Blind) ? 0 : Sight;
            Sound = (humanConditions.Deaf) ? 0 : Sound;
        }
        private void ApplyWeight()
        {
            CurWeight = 0;
            foreach (Item item in inventory)
            {
                CurWeight += item.Weight;
            }
            if (CurWeight > MaxWeight)
            {
                humanConditions.Encumbered = true;
            }
        }
        private void ApplyGrit()
        {
            Grit = 0;
            foreach (Clothes item in body)
            {
                if (item != null)
                {
                    Grit += item.Defence;
                }
            }
        }
        private void ApplyPower()
        {
            if(heldWeapon == null)
            {
                Power = 1;
            }
            else
            {
                Power = heldWeapon.Attack;
            }
        }
    }
    sealed internal class Conditions //Human HAS_A Conditions
    {
        internal bool Bleeding { get; set; } //Lose Health over Time
        internal bool Bruised { get; set; } //Take more Damage
        internal bool BrokenBones { get; set; } //Bleeding more add more Sweat too
        internal bool Unconscious { get; set; } //Can't do anything + Take more damage
        internal bool Happy { get; set; } //More convincing
        internal bool Sad { get; set; } //50% more/less convincing
        internal bool Angry { get; set; } //Less convincing
        internal bool Scared { get; set; } //Won't attack
        internal bool Blind { get; set; } //Can't see
        internal bool Deaf { get; set; } //Can't hear
        internal bool Mute { get; set; } // Can't talk
        internal bool Poisoned { get; set; } //Lose health
        internal bool Embarrased { get; set; } //No clothes + less convincing
        internal bool Addicted { get; set; } //Tears++
        internal bool Encumbered { get; set; } //Sweat++
    }
    sealed internal class Player : Human //Player IS_A Human
    {
        internal List<Wanderer> playerFriends;
        public Player()
        {
            playerFriends = new List<Wanderer>();
        }
        internal void AddNewItem()
        {
            uint itemChoice = RandomGen.Next(1, 44);
            switch(itemChoice)
            {
                case 1:
                    inventory.Add(new Knife());
                    break;
                case 2:
                    inventory.Add(new Machete());
                    break;
                case 3:
                    inventory.Add(new BrassKnuckles());
                    break;
                case 4:
                    inventory.Add(new Crowbar());
                    break;
                case 5:
                    inventory.Add(new Shovel());
                    break;
                case 6:
                    inventory.Add(new Pistol());
                    break;
                case 7:
                    inventory.Add(new PistolAmmo());
                    break;
                case 8:
                    inventory.Add(new SubMachineGun());
                    break;
                case 9:
                    inventory.Add(new SubMachineGunAmmo());
                    break;
                case 10:
                    inventory.Add(new Shotgun());
                    break;
                case 11:
                    inventory.Add(new ShotgunAmmo());
                    break;
                case 12:
                    inventory.Add(new Crossbow());
                    break;
                case 13:
                    inventory.Add(new CrossbowAmmo());
                    break;
                case 14:
                    inventory.Add(new SniperRifle());
                    break;
                case 15:
                    inventory.Add(new SniperRifleAmmo());
                    break;
                case 16:
                    inventory.Add(new RocketLauncher());
                    break;
                case 17:
                    inventory.Add(new RocketLauncherAmmo());
                    break;
                case 18:
                    inventory.Add(new Grenade());
                    break;
                case 19:
                    inventory.Add(new Bandaid());
                    break;
                case 20:
                    inventory.Add(new FirstAid());
                    break;
                case 21:
                    inventory.Add(new PainKillers());
                    break;
                case 22:
                    inventory.Add(new Adrenaline());
                    break;
                case 23:
                    inventory.Add(new WaterBottle());
                    break;
                case 24:
                    inventory.Add(new AlmondWater());
                    break;
                case 25:
                    inventory.Add(new EnergyBar());
                    break;
                case 26:
                    inventory.Add(new Nuts());
                    break;
                case 27:
                    inventory.Add(new Chocolate());
                    break;
                case 28:
                    inventory.Add(new PotatoChips());
                    break;
                case 29:
                    inventory.Add(new RawMeat());
                    break;
                case 30:
                    inventory.Add(new CookedMeat());
                    break;
                case 31:
                    inventory.Add(new MysteryFood());
                    break;
                case 32:
                    inventory.Add(new Clothes());
                    break;
                case 33:
                    inventory.Add(new Watch());
                    break;
                case 34:
                    inventory.Add(new SleepingBag());
                    break;
                case 35:
                    inventory.Add(new Pouch());
                    break;
                case 36:
                    inventory.Add(new SmallBag());
                    break;
                case 37:
                    inventory.Add(new Backpack());
                    break;
                case 38:
                    inventory.Add(new Matches());
                    break;
                case 39:
                    inventory.Add(new Flashlight());
                    break;
                case 40:
                    inventory.Add(new NightVisionGoggles());
                    break;
                case 41:
                    inventory.Add(new Battery());
                    break;
                case 42:
                    inventory.Add(new FlipPhone());
                    break;
                case 43:
                    inventory.Add(new SmartPhone());
                    break;
                default:
                    inventory.Add(new Trash());
                    break;
            }
        }
    }
    sealed internal class Wanderer : Human //Wanderer IS_A Human
    {
        internal string FirstName { get; set; }
        internal string LastName { get; set; }
        internal sbyte Favor { get; set; } //How much they like the player
        internal string Pronouns { get; set; } //He She They
        internal string PronounsAlt { get; set; } //His Her Their
        public Wanderer()
        {
            Favor = (sbyte)(RandomGen.Next(0, 10) - 5);
            FirstName = RandomGen.GetFirstName();
            LastName = RandomGen.GetLastName();
            Pronouns = RandomGen.GetPronouns();
            PronounsAlt = RandomGen.GetAltPronouns();
        }
    }
    internal abstract class Item
    {
        internal string Name { get; set; }
        internal decimal Weight { get; set; }
        internal abstract void UseItem(Human target);
    }
    #region Weapons
    internal abstract class Weapon : Item
    {
        internal byte Attack { get; set; }
        internal override void UseItem(Human target)
        {
            if (target.heldWeapon != null)
            {
                target.inventory.Add(target.heldWeapon);
            }
            target.heldWeapon = null;
            target.heldWeapon = this;
        }
    }
    internal abstract class MeleeWeapon : Weapon
    {
        internal byte Durability { get; set; }
    }
    internal abstract class RangedWeapon : Weapon
    {
        internal uint Ammo { get; set; }
    }
    sealed internal class Knife : MeleeWeapon
    {
        public Knife()
        {
            Name = "Knife";
            Weight = 0.5m;
            Durability = 100;
            Attack = 5;
        }
    }
    sealed internal class Machete : MeleeWeapon
    {
        public Machete()
        {
            Name = "Machete";
            Weight = 1m;
            Durability = 100;
            Attack = 15;
        }
    }
    sealed internal class BrassKnuckles : MeleeWeapon
    {
        public BrassKnuckles()
        {
            Name = "Brass Knuckles";
            Weight = 0.25m;
            Durability = 100;
            Attack = 10;
        }
    }
    sealed internal class Crowbar : MeleeWeapon
    {
        public Crowbar()
        {
            Name = "Crowbar";
            Weight = 1.5m;
            Durability = 100;
            Attack = 15;
        }
    }
    sealed internal class Shovel : MeleeWeapon
    {
        public Shovel()
        {
            Name = "Shovel";
            Weight = 2m;
            Durability = 100;
            Attack = 20;
        }
    }
    sealed internal class Pistol : RangedWeapon
    {
        public Pistol()
        {
            Name = "Pistol";
            Weight = 0.75m;
            Ammo = 18;
            Attack = 35;
        }
    }
    sealed internal class PistolAmmo : Item
    {
        public PistolAmmo()
        {
            Name = "Pistol Ammo";
            Weight = 0.05m;
        }
        internal override void UseItem(Human target)
        { 
            foreach (Item item in target.inventory)
            {
                if (item.Name == "Pistol")
                {
                    (item as RangedWeapon).Ammo += 45;
                    target.CurWeight -= Weight;
                    break;
                }
            }
        }
    }
    sealed internal class SubMachineGun : RangedWeapon
    {
        public SubMachineGun()
        {
            Name = "SMG";
            Weight = 1m;
            Ammo = 60;
            Attack = 20;
        }
    }
    sealed internal class SubMachineGunAmmo : Item
    {
        public SubMachineGunAmmo()
        {
            Name = "SMG Ammo";
            Weight = 0.05m;
        }
        internal override void UseItem(Human target)
        {
            foreach (Item item in target.inventory)
            {
                if (item.Name == "SMG")
                {
                    (item as RangedWeapon).Ammo += 180;
                    target.CurWeight -= Weight;
                    break;
                }
            }
        }
    }
    sealed internal class Shotgun : RangedWeapon
    {
        public Shotgun()
        {
            Name = "Shotgun";
            Weight = 1.1m;
            Ammo = 8;
            Attack = 50;
        }
    }
    sealed internal class ShotgunAmmo : Item
    {
        public ShotgunAmmo()
        {
            Name = "Shotgun Ammo";
            Weight = 0.05m;
        }
        internal override void UseItem(Human target)
        {
            foreach (Item item in target.inventory)
            {
                if (item.Name == "Shotgun")
                {
                    (item as RangedWeapon).Ammo += 48;
                    target.CurWeight -= Weight;
                    break;
                }
            }
        }
    }
    sealed internal class Crossbow : RangedWeapon
    {
        public Crossbow()
        {
            Name = "Crossbow";
            Weight = 1.25m;
            Ammo = 1;
            Attack = 75;
        }
    }
    sealed internal class CrossbowAmmo : Item
    {
        public CrossbowAmmo()
        {
            Name = "Crossbow Ammo";
            Weight = 0.05m;
        }
        internal override void UseItem(Human target)
        {
            foreach (Item item in target.inventory)
            {
                if (item.Name == "Crossbow")
                {
                    (item as RangedWeapon).Ammo += 10;
                    target.CurWeight -= Weight;
                    break;
                }
            }
        }
    }
    sealed internal class SniperRifle : RangedWeapon
    {
        public SniperRifle()
        {
            Name = "Sniper Rifle";
            Weight = 1.25m;
            Ammo = 5;
            Attack = 110;
        }
    }
    sealed internal class SniperRifleAmmo : Item
    {
        public SniperRifleAmmo()
        {
            Name = "Sniper Rifle Ammo";
            Weight = 0.05m;
        }
        internal override void UseItem(Human target)
        {
            foreach (Item item in target.inventory)
            {
                if (item.Name == "Sniper Rifle")
                {
                    (item as RangedWeapon).Ammo += 35;
                    target.CurWeight -= Weight;
                    break;
                }
            }
        }
    }
    sealed internal class RocketLauncher : RangedWeapon
    {
        public RocketLauncher()
        {
            Name = "Rocket Launcher";
            Weight = 1.75m;
            Ammo = 1;
            Attack = 250;
        }
    }
    sealed internal class RocketLauncherAmmo : Item
    {
        public RocketLauncherAmmo()
        {
            Name = "Rocket Launcher Ammo";
            Weight = 0.5m;
        }
        internal override void UseItem(Human target)
        {
            foreach (Item item in target.inventory)
            {
                if (item.Name == "Rocket Launcher")
                {
                    (item as RangedWeapon).Ammo += 6;
                    target.CurWeight -= Weight;
                    break;
                }
            }
            
        }
    }
    sealed internal class Grenade : RangedWeapon
    {
        public Grenade()
        {
            Name = "Grenade";
            Weight = 0m;
            Ammo = 1;
            Attack = 255;
        }
    }
    #endregion
    #region Healing
    sealed internal class Bandaid : Item
    {
        public Bandaid()
        {
            Name = "Bandaid";
            Weight = 0.1m;
        }
        internal override void UseItem(Human target)
        {
            target.Blood += 10;
            if (target.Blood > 100)
            {
                target.Blood = 100;
            }
            target.humanConditions.Bleeding = false;
        }
    }
    sealed internal class FirstAid : Item
    {
        public FirstAid()
        {
            Name = "First Aid";
            Weight = 1m;
        }
        internal override void UseItem(Human target)
        {
            target.Blood += 50;
            if (target.Blood > 100)
            {
                target.Blood = 100;
            }
            target.humanConditions.Bleeding = false;
            target.humanConditions.BrokenBones = false;
            target.humanConditions.Poisoned = false;
        }
    }
    sealed internal class PainKillers : Item
    {
        public PainKillers()
        {
            Name = "Pain Killers";
            Weight = 0.1m;
        }
        internal override void UseItem(Human target)
        {
            target.Blood += 20;
            if (target.Blood > 100)
            {
                target.Blood = 100;
            }
            if ((RandomGen.Next(1, 10) == 1) && target.Tears >= 50)
            {
                target.humanConditions.Addicted = true;
            }
        }
    }
    sealed internal class Adrenaline : Item
    {
        public Adrenaline()
        {
            Name = "Adrenaline";
            Weight = 0.1m;
        }
        internal override void UseItem(Human target)
        {
            target.Blood += 25;
            target.Sweat -= 25;
            if ((RandomGen.Next(1, 10) == 1) && target.Tears >= 50)
            {
                target.humanConditions.Addicted = true;
            }
        }
    }
    #endregion
    #region Food & Water
    internal abstract class Container : Item
    {
        internal byte Capacity { get; set; }
    }
    sealed internal class WaterBottle : Container
    {
        public WaterBottle()
        {
            Name = "Water Bottle";
            Weight = 0.75m;
            Capacity = 100;
        }
        internal override void UseItem(Human target)
        {
            Capacity -= 10;
            target.Thirst += 20;
            if (target.Thirst > 100)
            {
                target.Thirst = 100;
            }
        }
    }
    sealed internal class AlmondWater : Container
    {
        public AlmondWater()
        {
            Name = "Almond Water";
            Weight = 0.75m;
            Capacity = 100;
        }
        internal override void UseItem(Human target)
        {
            Capacity -= 10;
            target.Thirst = 100;
            target.Hunger = 100;
            target.Blood += 5;
            if (target.Blood > 100)
            {
                target.Blood = 100;
            }
            if ((RandomGen.Next(1, 20) == 1) && target.Tears >= 25)
            {
                target.humanConditions.Addicted = true;
            }
        }
    }
    sealed internal class EnergyBar : Item
    {
        public EnergyBar()
        {
            Name = "Energy Bar";
            Weight = 0.05m;
        }
        internal override void UseItem(Human target)
        {
            target.Hunger += 10;
            if (target.Hunger > 100)
            {
                target.Hunger = 100;
            }
        }
    }
    sealed internal class Nuts : Item
    {
        public Nuts()
        {
            Name = "Nuts";
            Weight = 0.01m;
        }
        internal override void UseItem(Human target)
        {
            target.Hunger += 5;
            if (target.Hunger > 100)
            {
                target.Hunger = 100;
            }
        }
    }
    sealed internal class Chocolate : Item
    {
        public Chocolate()
        {
            Name = "Chocolate";
            Weight = 0.1m;
        }
        internal override void UseItem(Human target)
        {
            target.Hunger += 10;
            if (target.Hunger > 100)
            {
                target.Hunger = 100;
            }
        }
    }
    sealed internal class PotatoChips : Item
    {
        public PotatoChips()
        {
            Name = "Potato Chips";
            Weight = 0.15m;
        }
        internal override void UseItem(Human target)
        {
            target.Hunger += 5;
            if (target.Hunger > 100)
            {
                target.Hunger = 100;
            }
        }
    }
    sealed internal class RawMeat : Item
    {
        public RawMeat()
        {
            Name = "Raw Meat";
            Weight = 1m;
        }
        internal override void UseItem(Human target)
        {
            target.Hunger += 35;
            if (target.Hunger > 100)
            {
                target.Hunger = 100;
            }
            if (RandomGen.Next(1, 10) == 1)
            {
                target.humanConditions.Poisoned = true;
            }
        }
    }
    sealed internal class CookedMeat : Item
    {
        public CookedMeat()
        {
            Name = "Cooked Meat";
            Weight = 1m;
        }
        internal override void UseItem(Human target)
        {
            target.Hunger += 50;
            if (target.Hunger > 100)
            {
                target.Hunger = 100;
            }
        }
    }
    sealed internal class MysteryFood : Item
    {
        public MysteryFood()
        {
            Name = "??? (Food)";
            Weight = 1.5m;
        }
        internal override void UseItem(Human target)
        {
            target.Hunger += (int)RandomGen.Next(1, 100);
            if (target.Hunger > 100)
            {
                target.Hunger = 100;
            }
            if (RandomGen.Next(1, 10) == 1)
            {
                target.humanConditions.Poisoned = true;
            }
        }
    }
    #endregion
    #region Clothes
    sealed internal class Clothes : Item
    {
        internal byte Defence { get; set; }
        internal byte Durability { get; set; }
        internal byte WearIndex { get; set; }
        public Clothes(uint choice = 0)
        {
            if (choice == 0)
            {
                choice = RandomGen.Next(1, 15);
            }
            //15 other options
            switch(choice)
            {
                case 1:
                    Name = "Shirt";
                    Weight = 0.1m;
                    Defence = 1;
                    Durability = 10;
                    WearIndex = 2;
                    break;
                case 2:
                    Name = "Jeans";
                    Weight = 0.1m;
                    Defence = 1;
                    Durability = 15;
                    WearIndex = 5;
                    break;
                case 3:
                    Name = "Pants";
                    Weight = 0.1m;
                    Defence = 1;
                    Durability = 10;
                    WearIndex = 5;
                    break;
                case 4:
                    Name = "Hat";
                    Weight = 0.1m;
                    Defence = 1;
                    Durability = 15;
                    WearIndex = 0;
                    break;
                case 5:
                    Name = "Vest";
                    Weight = 0.1m;
                    Defence = 2;
                    Durability = 35;
                    WearIndex = 3;
                    break;
                case 6:
                    Name = "Jacket";
                    Weight = 0.1m;
                    Defence = 1;
                    Durability = 20;
                    WearIndex = 3;
                    break;
                case 7:
                    Name = "Shoes";
                    Weight = 0.1m;
                    Defence = 1;
                    Durability = 25;
                    WearIndex = 7;
                    break;
                case 8:
                    Name = "Socks";
                    Weight = 0.1m;
                    Defence = 1;
                    Durability = 10;
                    WearIndex = 6;
                    break;
                case 9:
                    Name = "Poncho";
                    Weight = 0.1m;
                    Defence = 1;
                    Durability = 30;
                    WearIndex = 3;
                    break;
                case 10:
                    Name = "Gloves";
                    Weight = 0.1m;
                    Defence = 1;
                    Durability = 25;
                    WearIndex = 4;
                    break;
                case 11:
                    Name = "Ski Mask";
                    Weight = 0.1m;
                    Defence = 3;
                    Durability = 75;
                    WearIndex = 1;
                    break;
                case 12:
                    Name = "Coat";
                    Weight = 0.1m;
                    Defence = 1;
                    Durability = 50;
                    WearIndex = 3;
                    break;
                case 13:
                    Name = "Knee Pads";
                    Weight = 0.1m;
                    Defence = 5;
                    Durability = 75;
                    WearIndex = 5;
                    break;
                case 14:
                    Name = "Light Armor";
                    Weight = 5m;
                    Defence = 25;
                    Durability = 100;
                    WearIndex = 3;
                    break;
                case 15:
                    Name = "Heavy Armor";
                    Weight = 10m;
                    Defence = 75;
                    Durability = 150;
                    WearIndex = 3;
                    break;
                default:
                    Name = "Dunce Cap";
                    Weight = 999.9m;
                    Defence = 0;
                    Durability = 255;
                    WearIndex = 0;
                    break;
            }
        }
        internal override void UseItem(Human target)
        {
            target.body[WearIndex] = null;
            target.body[WearIndex] = this;
        }
    }
    #endregion
    #region Utility
    internal abstract class Electronic : Item
    {
        internal byte Charge { get; set; }
    }
    sealed internal class Watch : Misc
    {
        public Watch()
        {
            Name = "Watch";
            Weight = 0.1m;
        }
    }
    sealed internal class SleepingBag : Item
    {
        public SleepingBag()
        {
            Name = "Sleeping Bag";
            Weight = 2;
        }
        internal override void UseItem(Human target)
        {
            target.humanConditions.Unconscious = true;
        }
    }
    sealed internal class Pouch : Item
    {
        public Pouch()
        {
            Name = "Pouch";
            Weight = 0m;
        }
        internal override void UseItem(Human target)
        {
            target.MaxWeight = 110;
        }
    }
    sealed internal class SmallBag : Item
    {
        public SmallBag()
        {
            Name = "Small Bag";
            Weight = 0m;
        }
        internal override void UseItem(Human target)
        {
            target.MaxWeight = 125;
        }
    }
    sealed internal class Backpack : Item
    {
        public Backpack()
        {
            Name = "Backpack";
            Weight = 0;
        }
        internal override void UseItem(Human target)
        {
            target.MaxWeight = 175;
        }
    }
    sealed internal class Matches : Item
    {
        internal uint Count { get; set; }
        public Matches()
        {
            Name = "Matches";
            Weight = 0.1m;
            Count = 10;
        }
        internal override void UseItem(Human target)
        {
            if (target.Sight < 50 && Count >= 1 && target.humanConditions.Blind == false)
            {
                Count--;
                target.Sight = 50;
            }
        }
    }
    sealed internal class Flashlight : Electronic
    {
        public Flashlight()
        {
            Name = "Flashlight";
            Weight = 0.5m;
            Charge = 100;
        }
        internal override void UseItem(Human target)
        {
            if (target.Sight < 75 && Charge >= 10 && target.humanConditions.Blind == false)
            {
                Charge -= 10;
                target.Sight = 75;
            }
        }
    }
    sealed internal class NightVisionGoggles : Electronic
    {
        public NightVisionGoggles()
        {
            Name = "Night Vision Goggles";
            Weight = 0.75m;
            Charge = 100;
        }
        internal override void UseItem(Human target)
        {
            if (target.Sight < 25 && Charge > 0 && target.humanConditions.Blind == false)
            {
                Charge--;
                target.Sight = 100;
            }
        }
    }
    sealed internal class Battery : Item
    {
        public Battery()
        {
            Name = "Battery";
            Weight = 0.1m;
        }
        internal override void UseItem(Human target)
        {
            foreach (Item item in target.inventory)
            {
                if(item is Electronic && (item as Electronic).Charge == 0)
                {
                    (item as Electronic).Charge = 100;
                }
            }
        }
    }
    sealed internal class FlipPhone : Electronic
    {
        public FlipPhone()
        {
            Name = "Flip Phone";
            Weight = 0.15m;
            Charge = 100;
        }
        internal override void UseItem(Human target)
        {
            if (target.Sight < 45 && Charge > 0 && target.humanConditions.Blind == false)
            {
                Charge--;
                target.Sight = 45;
            }
        }
    }
    sealed internal class SmartPhone : Electronic
    {
        public SmartPhone()
        {
            Name = "Smart Phone";
            Weight = 0.15m;
            Charge = 95;
        }
        internal override void UseItem(Human target)
        {
            if (target.Sight < 80 && Charge > 0 && target.humanConditions.Blind == false)
            {
                Charge--;
                target.Sight = 80;
            }
        }
    }
    #endregion
    #region Misc.
    internal abstract class Misc : Item
    {
        internal override void UseItem(Human target)
        {
            //Do nothing
        }
    }
    sealed internal class Trash : Misc
    {
        public Trash()
        {
            switch (RandomGen.Next(1, 11))
            {
                case 1:
                    Name = "Plush";
                    break;
                case 2:
                    Name = "Mirror";
                    break;
                case 3:
                    Name = "Paint Brush";
                    break;
                case 4:
                    Name = "Comb";
                    break;
                case 5:
                    Name = "Paperclip";
                    break;
                case 6:
                    Name = "Toothpaste";
                    break;
                case 7:
                    Name = "Crayons";
                    break;
                case 8:
                    Name = "Dice";
                    break;
                case 9:
                    Name = "Yarn";
                    break;
                case 10:
                    Name = "Nails";
                    break;
                case 11:
                    Name = "Photo";
                    break;
                default:
                    Name = "Other Trash";
                    break;
            }
            Weight = 0.1m;
        }
    }
    #endregion
    sealed internal class Entity : LivingThing //Entity IS_A LivingThing
    {
        internal string Name { get; set; }
        public Entity()
        {
            switch (RandomGen.Next(1, 9))
            {
                case 1:
                    Name = "Smiler";
                    Blood = 100;
                    Sweat = 25;
                    Power = 5;
                    Grit = 5;
                    break;
                case 2:
                    Name = "Deathmoth";
                    Blood = 100;
                    Sweat = 25;
                    Power = 1;
                    Grit = 0;
                    break;
                case 3:
                    Name = "Clump";
                    Blood = 75;
                    Sweat = 0;
                    Power = 1;
                    Grit = 5;
                    break;
                case 4:
                    Name = "Duller";
                    Blood = 1;
                    Sweat = 99;
                    Power = 0;
                    Grit = 100;
                    break;
                case 5:
                    Name = "Hound";
                    Blood = 50;
                    Sweat = 0;
                    Power = 15;
                    Grit = 10;
                    break;
                case 6:
                    Name = "Faceling";
                    Blood = 100;
                    Sweat = 50;
                    Power = 1;
                    Grit = 0;
                    break;
                case 7:
                    Name = "Skin Stealer";
                    Blood = 100;
                    Sweat = 25;
                    Power = 20;
                    Grit = 25;
                    break;
                case 8:
                    Name = "Crawler";
                    Blood = 10;
                    Sweat = 80;
                    Power = 2;
                    Grit = 1;
                    break;
                case 9:
                    Name = "Death Rat";
                    Blood = 5;
                    Sweat = 10;
                    Power = 2;
                    Grit = 1;
                    break;
                default:
                    Name = "ERROR-man";
                    Blood = 404;
                    Sweat = -2000;
                    Power = 9001;
                    Grit = 7;
                    break;
            }
            Tears = 0;
            Hunger = 0;
            Thirst = 0;
            Sight = 0;
            Sound = 0;

        }
    }
    sealed internal class TheBackrooms
    {
        internal bool[,] vRoomData;
        internal List<Room> roomsList;
        internal byte eventTimer = 10;
        internal byte currentE = 0;
        public TheBackrooms()
        {
            roomsList = new List<Room>();
            vRoomData = new bool[8, 8];
        }
        private void NewRoom(long xCor, long yCor)
        {
            ResetV();
            eventTimer--;
            string temp = "";
            uint roomX = RandomGen.Next(1, 6);
            uint roomY = RandomGen.Next(1, 6);
            byte newEvent = 0;
            //Generate Room
            for (int y = 1; y < roomY; y++)
            {
                    for (int x = 1; x < roomX; x++)
                    {
                        vRoomData[x, y] = true;
                    }
            }
            //Generate Pillar
            if ((RandomGen.Next(1, 4) == 3) && (roomX > 2 && roomY > 2))
            {
                uint pillarX = RandomGen.Next(1, (roomX-1));
                uint pillarY = RandomGen.Next(1, (roomY-1));
                uint startX = RandomGen.Next(1, (roomX - pillarX + 1));
                uint startY = RandomGen.Next(1, (roomY - pillarY + 1));
                for (uint y = startY; y < (startY+pillarY-1); y++)
                {
                    for (uint x = startX; x < (startX+pillarX-1); x++)
                    {
                        vRoomData[x, y] = false;
                    }
                }
            }
            //Generate Exits
            vRoomData[0, RandomGen.Next(1, roomY)] = true;
            vRoomData[7, RandomGen.Next(1, roomY)] = true;
            vRoomData[RandomGen.Next(1, roomX), 0] = true;
            vRoomData[RandomGen.Next(1, roomX), 7] = true;
            bool placeable;
            for (int y = 1; y < 7; y++)
            { //Left and Right
                placeable = false;
                for (int x = 1; x < 6; x++)
                {
                    if (vRoomData[x,y])
                    {
                        placeable = true;
                    }
                }
                if (placeable)
                {
                    if (RandomGen.Next() == 7)
                    { //Left
                        vRoomData[0, y] = true;
                    }
                    if (RandomGen.Next() == 5)
                    { //Right
                        vRoomData[7, y] = true;
                    }
                }
            }
            for (int x = 1; x < 7; x++)
            { //Up and Down
                placeable = false;
                for (int y = 1; y < 7; y++)
                {
                    if (vRoomData[x, y])
                    {
                        placeable = true;
                    }
                }
                if (placeable)
                {
                    if (RandomGen.Next() == 3)
                    { //Up
                        vRoomData[x, 0] = true;
                    }
                    if (RandomGen.Next() == 1)
                    { //Down
                        vRoomData[x, 7] = true;
                    }
                }
            }
            //Generate Room Code
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (vRoomData[x, y])
                    {
                        temp += "1";
                    }
                    else
                    {
                        temp += "0";
                    }
                }
            }
            //Events
            if (eventTimer == 0)
            {
                eventTimer = (byte)RandomGen.Next(10, 25);
                newEvent = (byte)RandomGen.Next(1, 8);
            }
            roomsList.Add(new Room(Convert.ToUInt64(temp, 2), xCor, yCor, newEvent));
            newEvent = 0;
        }
        internal void LoadRoom(long xCor, long yCor)
        {
            ResetV();
            string temp = "";
            bool found = false;
            foreach (Room room in roomsList)
            {
                if (room.Coordinates[0] == xCor && room.Coordinates[1] == yCor)
                {
                    temp = Convert.ToString((long)room.RoomData, 2).PadLeft(64, '0');
                    currentE = room.RoomEvent;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                int count = 0;
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        if (temp[count] == '1')
                        {
                            vRoomData[x, y] = true;
                        }
                        else
                        {
                            vRoomData[x, y] = false;
                        }
                        count++;
                    }
                }
            }
            else
            {
                NewRoom(xCor, yCor);
                LoadRoom(xCor, yCor);
            }
        }
        internal void ResetV()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    vRoomData[x, y] = false;
                }
            }
        }
    }
    internal struct Room //TheBackrooms HAS_A Room
    {
        internal long[] Coordinates { get; set; }
        internal ulong RoomData { get; set; }
        internal byte RoomEvent { get; set; }
        public Room(ulong var, long x, long y, byte gameEvent = 0)
        {
            Coordinates = new long[2] {x, y};
            RoomEvent = gameEvent;
            RoomData = var;
        }
        public void ResetEvent()
        {
            RoomEvent = 0;
        }
    }
    static internal class RandomGen
    {
        private const uint _seed = 73;
        private static uint _iter = 22;
        private static uint _num = _seed;
        private static string[] _fnames = new string[50] {
        "Harvey", "Adelia", "Ely", "Dina", "Carter", "Ronnie", "Jenny", "Babette", "Maura", "Virgee", "Misti", "George", "Doris", "Vanessa", "Karissa", "Calla", "Phillis", "Rickie", "Prosper"
       ,"Shleby", "Winthrop", "Daniel", "Lynda", "Christal", "Jenni", "Dirk", "Cammie", "Trix", "Millard", "Frieda","Audrey", "Trish", "Bailey", "Esther", "Eldon", "Derek", "Shelby", "Tyron", "Nik"
       , "Clem Young", "Connor", "Erin", "Joseph", "Benjamin", "Ethan", "Mark", "Ally", "Boston", "Roxie", "John"};
        private static string[] _lnames = new string[50] {
        "Winchester", "Blakesley", "Pemberton", "Dick", "Kingson", "Miller", "Tolbert", "Jack", "Colt", "Beasley", "Michaels", "Pierce", "Clayton", "Gibbs", "Blackburn", "Wilkie", "Pickering"
      , "Timberlake", "Glass", "Bowman", "Haywood", "Derby", "Landon", "Poindexter", "Woodhams", "Dallas", "Eady", "Blackflame", "Haden", "Farmer", "Christison", "Kidd", "Jervis", "Dobbs", "Hilton"
      , "Russell", "Humphrey", "Clarkson", "Lambborn", "Young", "Douglass", "Keur", "Simon", "De Cler", "Brook-Rodney", "Bowler", "Wollerman", "Zeng", "Cunningham", "Smith"};
        private static string[] _pro = new string[3] { "he", "she", "they" };
        private static string[] _proALT = new string[3] { "his", "her", "their" };
        public static uint Next(uint minValue = 1, uint maxValue = 10)
        {
            if (_num == 1)
            {
                _iter++;
                _num = _iter;
            }
            if (_num % 2 == 0)
            {
                _num /= 2;
            }
            else
            {
                _num = (3 * _num) + 1;
            }
            return _num % (maxValue - minValue + 1) + minValue;
        }
        public static string GetFirstName()
        {
            return _fnames[Next(1, 50) - 1];
        }
        public static string GetLastName()
        {
            return _lnames[Next(1, 50) - 1];
        }
        public static string GetPronouns()
        {
            return _pro[Next(1, 3) - 1];
        }
        public static string GetAltPronouns()
        {
            return _proALT[Next(1, 3) - 1];
        }
    }
}