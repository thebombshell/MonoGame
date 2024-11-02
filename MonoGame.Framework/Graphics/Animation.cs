using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// A single total animation
    /// </summary>
    public sealed class Animation : ReadOnlyDictionary<string, AnimationObject>
    {
        /// <summary>
        /// name of the animation
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The earliest timestamp in this object
        /// </summary>
        public float StartTime { get; private set; }

        /// <summary>
        /// The latest timestamp in this object
        /// </summary>
        public float EndTime { get; private set; }

        /// <summary>
        /// The total time in seconds between the start and end timestamps in this object
        /// </summary>
        public float Duration { get { return EndTime - StartTime; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="animationObjects"></param>
        public Animation(string name, ICollection<AnimationObject> animationObjects) : base(animationObjects.ToImmutableDictionary(o => o.Name, o => o))
        {
            Name = name;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public AnimationPose Sample(float time)
        {
            AnimationPose pose = new AnimationPose();
            foreach (AnimationObject animationObject in Values)
            {
                pose.Add(animationObject.Name, animationObject.Sample(time));
            }
            return pose;
        }
    }
}
