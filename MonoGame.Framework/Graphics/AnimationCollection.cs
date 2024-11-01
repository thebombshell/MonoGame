using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// A collection of animaitons
    /// </summary>
    public sealed class AnimationCollection : ReadOnlyDictionary<string, Animation>
    {
        /// <summary>
        /// Creates an animation collection
        /// </summary>
        /// <param name="animations"></param>
        public AnimationCollection(ICollection<Animation> animations) : base(animations.ToDictionary(a => a.Name, a => a))
        {
        }
    }
}
