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
    public sealed class Animator
    {
        /// <summary>
        /// 
        /// </summary>
        public AnimationCollection Animations{ get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AnimationPose Pose{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentAnimationName
        {
            get
            {
                return currentAnimationName;
            }
            set
            {
                currentAnimationName = !Animations.ContainsKey(value) ? value : null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Animation CurrentAnimation
        {
            get { return CurrentAnimationName == null ? null : Animations[CurrentAnimationName]; }
        }

        private string currentAnimationName = null;

        public float Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {

        }
    }
}
