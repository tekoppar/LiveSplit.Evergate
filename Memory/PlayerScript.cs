using System.Runtime.InteropServices;
using Tem.TemClass;

namespace LiveSplit.Evergate {

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct PlayerScriptPtr {
        [FieldOffset(0x168)]
        public bool facingLeft;
        [FieldOffset(0x169)]
        public bool onGround;
        [FieldOffset(0x16A)]
        public bool onWall;
        [FieldOffset(0x16B)]
        public bool onBoostedTrajectory;
        [FieldOffset(0x16C)]
        public bool horizontalBoostActive;
        [FieldOffset(0x16D)]
        public bool adoptMovingTerrainActive;
        [FieldOffset(0x16E)]
        public bool pushingOnWall;
        [FieldOffset(0x17B)]
        public bool groundUsable;
        [FieldOffset(0x17C)]
        public bool leftWallHit;
        [FieldOffset(0x17D)]
        public bool rightWallHit;
        [FieldOffset(0x1C0)]
        public bool usingExternalMobility;
        [FieldOffset(0x1D4)]
        public bool _inGel_k__BackingField;
        [FieldOffset(0x1D6)]
        public bool _inFierce_k__BackingField;
        [FieldOffset(0x208)]
        public bool _inEMPZone_k__BackingField;
        [FieldOffset(0x20A)]
        public bool hasJumpResetLeft;
        [FieldOffset(0x210)]
        public bool canDashNow;
        [FieldOffset(0x3BC)]
        public bool falling;
        [FieldOffset(0x3F4)]
        public float gelReentryTimer;
        [FieldOffset(0x3F8)]
        public bool inClimaxMode;
        [FieldOffset(0x418)]
        public float boostOutOfControlTime;
        [FieldOffset(0x420)]
        public float regroundTimer;
        [FieldOffset(0x424)]
        public float jumpFreshTimer;
        [FieldOffset(0x428)]
        public float rollFreshTimer;
        [FieldOffset(0x430)]
        public float attackFreshTimer;
        [FieldOffset(0x440)]
        public float i2_3_boostCooldown;
        [FieldOffset(0x444)]
        public Vector3 currPos;
        [FieldOffset(0x450)]
        public Vector3 prevPos;
        [FieldOffset(0x45C)]
        public Vector3 currVel;
        [FieldOffset(0x468)]
        public Vector3 prevVel;
    }

    public class PlayerScript {
        public bool facingLeft;
        public bool onGround;
        public bool onWall;
        public bool onBoostedTrajectory;
        public bool horizontalBoostActive;
        public bool adoptMovingTerrainActive;
        public bool pushingOnWall;
        public bool groundUsable;
        public bool leftWallHit;
        public bool rightWallHit;
        public bool usingExternalMobility;
        public bool _inGel_k__BackingField;
        public bool _inFierce_k__BackingField;
        public bool _inEMPZone_k__BackingField;
        public bool hasJumpResetLeft;
        public bool canDashNow;
        public bool falling;
        public float gelReentryTimer;
        public bool inClimaxMode;
        public float boostOutOfControlTime;
        public float regroundTimer;
        public float jumpFreshTimer;
        public float rollFreshTimer;
        public float attackFreshTimer;
        public float i2_3_boostCooldown;
        public bool canJump;
        public Vector3 currPos;
        public Vector3 prevPos;
        public Vector3 currVel;
        public Vector3 prevVel;

        public PlayerScript() { }

        public PlayerScript(PlayerScriptPtr ptr, bool canJump) {
            this.currPos = ptr.currPos;
            this.prevPos = ptr.prevPos;
            this.currVel = ptr.currVel;
            this.prevVel = ptr.prevVel;
            this.facingLeft = ptr.facingLeft;
            this.onGround = ptr.onGround;
            this.onWall = ptr.onWall;
            this.onBoostedTrajectory = ptr.onBoostedTrajectory;
            this.horizontalBoostActive = ptr.horizontalBoostActive;
            this.adoptMovingTerrainActive = ptr.adoptMovingTerrainActive;
            this.pushingOnWall = ptr.pushingOnWall;
            this.groundUsable = ptr.groundUsable;
            this.leftWallHit = ptr.leftWallHit;
            this.rightWallHit = ptr.rightWallHit;
            this.usingExternalMobility = ptr.usingExternalMobility;
            this._inGel_k__BackingField = ptr._inGel_k__BackingField;
            this._inFierce_k__BackingField = ptr._inFierce_k__BackingField;
            this._inEMPZone_k__BackingField = ptr._inEMPZone_k__BackingField;
            this.hasJumpResetLeft = ptr.hasJumpResetLeft;
            this.canDashNow = ptr.canDashNow;
            this.falling = ptr.falling;
            this.gelReentryTimer = ptr.gelReentryTimer;
            this.inClimaxMode = ptr.inClimaxMode;
            this.boostOutOfControlTime = ptr.boostOutOfControlTime;
            this.regroundTimer = ptr.regroundTimer;
            this.jumpFreshTimer = ptr.jumpFreshTimer;
            this.rollFreshTimer = ptr.rollFreshTimer;
            this.attackFreshTimer = ptr.attackFreshTimer;
            this.i2_3_boostCooldown = ptr.i2_3_boostCooldown;
            this.canJump = canJump;
        }
    }
}