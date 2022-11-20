namespace GamePlay.Player.Entity.Setup.Path
{
    public class PlayerAssetsPaths
    {
        private const string _root = "Player/";
        private const string _components = _root + "Components/";
        private const string _states = _root + "States/";
        private const string _views = _root + "Views/";
        private const string _weapons = _root + "Weapons/";
        private const string _bow = _root + "Bow/";

        public const string Config = _root + "Config/";

        public const string ComponentPrefix = "PlayerComponent_";
        public const string BowComponentPrefix = "PlayerBowComponent_";
        public const string StatePrefix = "PlayerState_";
        public const string DefinitionPrefix = "PlayerStateDefinition_";
        public const string ConfigPrefix = "PlayerConfig_";
        public const string LogsPrefix = "LogSettings_";
        public const string AnimatorTriggerPrefix = "AnimatorTrigger_";
        public const string AnimatorFloatPrefix = "AnimatorFloat_";

        public const string Rotation = _components + "Rotation/";
        public const string WeaponsHandler = _components + "WeaponsHandler/";
        public const string StateMachine = _components + "StateMachine/";
        public const string InertialMovement = _components + "InertialMovement/";
        public const string Health = _components + "Health/";
        public const string ActionsState = _components + "ActionsState/";

        public const string Floating = _states + "Floating/";
        public const string Idle = _states + "Idle/";
        public const string None = _states + "None/";
        public const string RangeAttack = _states + "RangeAttack/";
        public const string Respawn = _states + "Respawn/";
        public const string Run = _states + "Run/";
        public const string Death = _states + "Death/";
        public const string Damage = _components + "Damage/";
        public const string ShipResources = _components + "ShipResources/";

        public const string Animator = _views + "Animator/";
        public const string RigidBodies = _views + "Animator/";
        public const string Sprites = _views + "Animator/";
        public const string Transform = _views + "Animator/";

        public const string BowShooter = _bow + "Shooter/";
    }
}