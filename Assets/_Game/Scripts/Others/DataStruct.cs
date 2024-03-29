public enum CameraState {
	MainMenu,
	Gameplay,
	Shop
}


public enum EventID {
	Play,
	CharacterDeath,
	LoadSkin,
	TrySkin,
	Home,
	Revive,
	AddCoin,
	Lose,
	NextLevel,
	Dance,
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

public enum SoundType {
	ThrowWeapon,
	Click,
	Count,
	WeaponHit,
	VoiceDead,
	SizeUp,
	Lose,
	Win,
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

public enum SettingType {
	Sound,
	Vibrate
}

public enum ParticleType {
	SizeUp
}

public enum PoolType {
	None = 0,

	Bot = 1,

	Bullet_Hammer_1 = 2,
	Bullet_Hammer_2 = 3,
	Bullet_Hammer_3 = 4,
	Bullet_Candy_1 = 5,
	Bullet_Candy_2 = 6,
	Bullet_Candy_3 = 7,
	Bullet_Boomerang_1 = 8,
	Bullet_Boomerang_2 = 9,
	Bullet_Boomerang_3 = 10,
	
	TextCombat = 11,
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

public enum AccessoryType {
	None = 0,
	Book = 1,
	CaptainShield = 2,
	Headphone = 3,
	Shield = 4,
}

public enum HatType {
	None = 0,
	Arrow = 1,
	Cap = 2,
	Cowboy = 3,
	Crown = 4,
	Ear = 5,
	StrawHat = 6,
	Headphone = 7,
	Horn = 8,
	Police = 9,
	Beard = 10,
	Flower = 11,
	Hair = 12,
}

public enum SkinType {
	Normal = 0,
	Devil = 1,
	Angel = 2,
	Witch = 3,
	Deadpool = 4,
	Thor = 5,
}

public enum PrefKey {
	PlayerData
}


