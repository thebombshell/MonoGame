using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    public class AnimatedModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Model Model { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AnimationCollection Animations { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IPoseProvider Animator{ get; set; }

        virtual public void Update(GameTime gameTime)
        {

        }
    }
}
