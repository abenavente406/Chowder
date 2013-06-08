using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using GameHelperLibrary.Shapes;
using Chowder.States;
using Chowder.Prototype.Levels;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Chowder.Prototype.Entities;

namespace Chowder.Prototype.States
{
    class GameplayState_Proto : BaseGameState
    {
        DrawableRectangle rect;
        DrawableCircle circ;

        Camera camera;
        LevelManager levelManager;

        public GameplayState_Proto(Game game, GameStateManager manager)
            : base(game, manager)
        {

        }

        public override void Initialize()
        {
            camera = new Camera(Vector2.Zero + new Vector2(ProjectData.GAMEWIDTH / 2, ProjectData.GAMEHEIGHT / 2),
                ProjectData.GAMEWIDTH, ProjectData.GAMEHEIGHT);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            rect = new DrawableRectangle(GameRef.GraphicsDevice, new Vector2(32, 64), Color.White, true);
            circ = new DrawableCircle(GameRef.GraphicsDevice, 16, Color.MediumVioletRed, true);

            levelManager = new LevelManager();
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            var batch = GameRef.spriteBatch;
            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, 
                DepthStencilState.Default, RasterizerState.CullNone, null, camera.GetTransformation(ProjectData.GraphicsDevice));
            {
                levelManager.Draw(batch, gameTime);
                base.Draw(gameTime);
            }
            batch.End();
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds * 10;
            camera.Update(EntityManager.Player.Position);

            levelManager.Update(gameTime);

            base.Update(gameTime);
        }
    }
}
