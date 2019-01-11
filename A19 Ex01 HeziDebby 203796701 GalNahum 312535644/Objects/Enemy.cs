using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class Enemy : Sprite, ICollidable2D
    {
        private const int k_TextureWidthDivider = 2;
        private const int k_TextureHeightDivider = 3;
        private const int k_NumOfFrames = 2;
        private const int k_MaxAmmo = 1;

        private RandomActionComponent m_RandomShootingNotifier;
        private Gun m_Gun;

        private int m_Seed = 0;
        public int Seed
        {
            get { return m_Seed; }
            set { m_Seed = value; }
        }

        public Enemy(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
        {
            m_TintColor = Color.Red;
            m_Gun = new Gun(k_MaxAmmo, i_Game, this.GetType());
        }

        public override void Initialize()
        {
            base.Initialize();

            m_Gun.Initialize(Color.Blue);

            m_RandomShootingNotifier = new RandomActionComponent(1, 30, Seed);
            m_RandomShootingNotifier.RandomTimeAchieved += Shoot;

            initAnimations();
        }

        private void initAnimations()
        {
            TimeSpan animationLength = TimeSpan.FromSeconds(0.5);

           CellAnimator celAnimation = new CellAnimator(animationLength, k_NumOfFrames, (int)CellIdx.Y, TimeSpan.Zero);

            float spinsPerSecond = MathHelper.TwoPi * 6;
            animationLength = TimeSpan.FromSeconds(1.2);

            SpinAnimator spinner = new SpinAnimator("Spinner", spinsPerSecond, animationLength);
            ShrinkAnimator shrinker = new ShrinkAnimator("Shrinker", animationLength);

            spinner.Finished += new EventHandler(onDyingAnimationFinish);

            this.Animations.Add(celAnimation);
            this.Animations.Add(spinner);
            this.Animations.Add(shrinker);

            this.Animations.Enabled = true;
            this.Animations["Spinner"].Enabled = false;
            this.Animations["Shrinker"].Enabled = false;
        }

        private void onDyingAnimationFinish(object i_Sender, EventArgs i_EventArgs)
        {
            this.Dispose(true);
            this.Animations.Pause();
            this.Visible = false;
        }

        private Vector2 m_CellIdx = Vector2.Zero;
        public Vector2 CellIdx
        {
            get { return m_CellIdx; }
            set { m_CellIdx = value; }
        }

        protected override void InitBounds()
        {
            m_WidthBeforeScale = Texture.Width / k_TextureWidthDivider;
            m_HeightBeforeScale = Texture.Height / k_TextureHeightDivider;

            InitSourceRectangle();

            InitOrigins();
        }

        protected override void InitSourceRectangle()
        {
            base.InitSourceRectangle();

            this.SourceRectangle = new Rectangle(
                (int)CellIdx.Y * m_SourceRectangle.Height,
                (int)CellIdx.X * m_SourceRectangle.Width,
                (int)(m_WidthBeforeScale),
                (int)(m_HeightBeforeScale));
        }

        protected override void InitOrigins()
        {
            this.RotationOrigin = SourceRectangleCenter;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (!(i_Collidable is ICollidableByPixels))
            {
                this.Animations["Spinner"].Enabled = true;
                this.Animations["Shrinker"].Enabled = true;
            }

            OnCollision(this, EventArgs.Empty);
        }

        public override void Update(GameTime gameTime)
        {
            m_RandomShootingNotifier.Update(gameTime);

            base.Update(gameTime);
        }

        public void Shoot()
        {
            m_Gun.Shoot(new Vector2(Position.X + Width / 2, Position.Y));
        }

        public override bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;

            if (i_Source is Bullet && (i_Source as Bullet).OwnerType != this.GetType())
            {
                collided = base.CheckCollision(i_Source);
            }

            return collided;
        }

        public void IncreaseCellAnimation(TimeSpan i_TimeToJump)
        {
            (this.Animations["CellAnimation"] as CellAnimator).UpdateCellTime(i_TimeToJump);
        }

        protected override void Dispose(bool i_Disposing)
        {
            base.Dispose(i_Disposing);
            m_RandomShootingNotifier.Enabled = false;
        }
    }
}
