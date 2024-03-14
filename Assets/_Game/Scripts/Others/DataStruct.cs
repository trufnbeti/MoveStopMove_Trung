public enum CameraState {
	MainMenu,
	Gameplay,
	Shop
}


public enum EventID {
	Play,
	CharacterDeath,
}

public enum GameState {
	MainMenu = 1,
	Gameplay = 2,
	Setting = 3,
	Finish = 4,
	Revive = 5,
}

public enum StateButton {
	Buy,
	Equip,
	Equipped
}

public enum ShopType {
	Hat,
	Pant,
	Accessory,
	Skin,
	Weapon
}

public enum ItemState {
	Buy,
	Bought,
	Equipped,
	Selecting
}

public enum ParticleType {
	
}

public enum PoolType {
	None,

	Bot,

	Weapon_Hammer_1,
	Weapon_Hammer_2,
	Weapon_Hammer_3,
	Weapon_Candy_1,
	Weapon_Candy_2,
	Weapon_Candy_3,
	Weapon_Boomerang_1,
	Weapon_Boomerang_2,
	Weapon_Boomerang_3,

	Bullet_Hammer_1,
	Bullet_Hammer_2,
	Bullet_Hammer_3,
	Bullet_Candy_1,
	Bullet_Candy_2,
	Bullet_Candy_3,
	Bullet_Boomerang_1,
	Bullet_Boomerang_2,
	Bullet_Boomerang_3,

	Skin_Normal,
	Skin_Devil,
	Skin_Angle,
	Skin_Witch,
	Skin_Deadpool,
	Skin_Thor,

	Hat_Arrow,
	Hat_Cap,
	Hat_Cowboy,
	Hat_Crown,
	Hat_Ear,
	Hat_StrawHat,
	Hat_Headphone,
	Hat_Horn,
	Hat_Police,

	Acc_Book,
	Acc_Captain,
	Acc_Headphone,
	Acc_Shield,

	TargetIndicator,
}

public enum Anim {
	run,
	idle,
	die,
	dance,
	attack,
	win,
}

public enum GameTag {
	Character
}

public enum PantType {
	Pant_1,
	Pant_2,
	Pant_3,
	Pant_4,
	Pant_5,
	Pant_6,
	Pant_7,
	Pant_8,
	Pant_9,
}

public enum WeaponType {
	Hammer_1 = PoolType.Weapon_Hammer_1,
	Hammer_2 = PoolType.Weapon_Hammer_2,
	Hammer_3 = PoolType.Weapon_Hammer_3,
	Candy_1 = PoolType.Weapon_Candy_1,
	Candy_2 = PoolType.Weapon_Candy_2,
	Candy_3 = PoolType.Weapon_Candy_3,
	Boomerang_1 = PoolType.Weapon_Boomerang_1,
	Boomerang_2 = PoolType.Weapon_Boomerang_2,
	Boomerang_3 = PoolType.Weapon_Boomerang_3,
}

public enum AccessoryType {
	None = 0,
	Book = PoolType.Acc_Book,
	CaptainShield = PoolType.Acc_Captain,
	Headphone = PoolType.Acc_Headphone,
	Shield = PoolType.Acc_Shield,
}

public enum HatType {
	None = 0,
	Arrow = PoolType.Hat_Arrow,
	Cap = PoolType.Hat_Cap,
	Cowboy = PoolType.Hat_Cowboy,
	Crown = PoolType.Hat_Crown,
	Ear = PoolType.Hat_Ear,
	StrawHat = PoolType.Hat_StrawHat,
	Headphone = PoolType.Hat_Headphone,
	Horn = PoolType.Hat_Horn,
	Police = PoolType.Hat_Police,
}

public enum SkinType {
	Normal = PoolType.Skin_Normal,
	Devil = PoolType.Skin_Devil,
	Angle = PoolType.Skin_Angle,
	Witch = PoolType.Skin_Witch,
	Deadpool = PoolType.Skin_Deadpool,
	Thor = PoolType.Skin_Thor,
}

public enum BulletType {
	Hammer_1 = PoolType.Bullet_Hammer_1,
	Hammer_2 = PoolType.Bullet_Hammer_2,
	Hammer_3 = PoolType.Bullet_Hammer_3,
	Candy_1 = PoolType.Bullet_Candy_1,
	Candy_2 = PoolType.Bullet_Candy_2,
	Candy_3 = PoolType.Bullet_Candy_3,
	Boomerang_1 = PoolType.Bullet_Boomerang_1,
	Boomerang_2 = PoolType.Bullet_Boomerang_2,
	Boomerang_3 = PoolType.Bullet_Boomerang_3,
}

public enum PrefKey {
	Level,
	Coin,
	SoundIsOn,
	VibrateIsOn,
	RemoveAds,
	Tutorial,
	PlayerWeapon,
	PlayerHat,
	PlayerPant,
	PlayerAccessory,
	PlayerSkin,
}


