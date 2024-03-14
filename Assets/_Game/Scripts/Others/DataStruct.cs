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
	Buy = 0,
	Equip = 1,
	Equipped = 2
}

public enum ShopType {
	Hat = 0,
	Pant = 1,
	Accessory = 2,
	Skin = 3,
	Weapon = 4
}

public enum ItemState {
	Buy = 0,
	Bought = 1,
	Equipped = 2,
	Selecting = 3
}

public enum ParticleType {
	
}

public enum PoolType {
	None = 0,

	Bot = 1,

	Weapon_Hammer_1 = 2,
	Weapon_Hammer_2 = 3,
	Weapon_Hammer_3 = 4,
	Weapon_Candy_1 = 5,
	Weapon_Candy_2 = 6,
	Weapon_Candy_3 = 7,
	Weapon_Boomerang_1 = 8,
	Weapon_Boomerang_2 = 9,
	Weapon_Boomerang_3 = 10,

	Bullet_Hammer_1 = 11,
	Bullet_Hammer_2 = 12,
	Bullet_Hammer_3 = 13,
	Bullet_Candy_1 = 14,
	Bullet_Candy_2 = 15,
	Bullet_Candy_3 = 16,
	Bullet_Boomerang_1 = 17,
	Bullet_Boomerang_2 = 18,
	Bullet_Boomerang_3 = 19,

	Skin_Normal = 20,
	Skin_Devil = 21,
	Skin_Angle = 22,
	Skin_Witch = 23,
	Skin_Deadpool = 24,
	Skin_Thor = 25,

	Hat_Arrow = 26,
	Hat_Cap = 27,
	Hat_Cowboy = 28,
	Hat_Crown = 29,
	Hat_Ear = 30,
	Hat_StrawHat = 31,
	Hat_Headphone = 32,
	Hat_Horn = 33,
	Hat_Police = 34,

	Acc_Book = 35,
	Acc_Captain = 36,
	Acc_Headphone = 37,
	Acc_Shield = 38,

	TargetIndicator = 39,
}

public enum Anim {
	run = 0,
	idle = 1,
	die = 2,
	dance = 3,
	attack = 4,
	win = 5,
}

public enum GameTag {
	Character = 0
}

public enum PantType {
	Pant_1 = 0,
	Pant_2 = 1,
	Pant_3 = 2,
	Pant_4 = 3,
	Pant_5 = 4,
	Pant_6 = 5,
	Pant_7 = 6,
	Pant_8 = 7,
	Pant_9 = 8,
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
	Level = 0,
	Coin = 1,
	SoundIsOn = 2,
	VibrateIsOn = 3,
	RemoveAds = 4,
	Tutorial = 5,
	PlayerWeapon = 6,
	PlayerHat = 7,
	PlayerPant = 8,
	PlayerAccessory = 9,
	PlayerSkin = 10,
}


