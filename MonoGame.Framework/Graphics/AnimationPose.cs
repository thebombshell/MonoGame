using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// A pose generated from an animation.
    /// </summary>
    public sealed class AnimationPose : Dictionary<string, Matrix>
    {
        /// <summary>
        /// Applies this pose to a models bones
        /// </summary>
        /// <param name="model">The model to apply the pose to</param>
        public void ApplyToModel(Model model)
        {
            foreach (ModelBone bone in model.Bones)
            {
                if (ContainsKey(bone.Name))
                {
                    bone.Transform = this[bone.Name];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        public void Set(AnimationPose other)
        {
            foreach (KeyValuePair<string, Matrix> pair in other)
            {
                this[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        /// returns a copy of this pose only consisting of the masked bone
        /// </summary>
        /// <param name="names">a collection of names defining the bone mask</param>
        /// <returns>a copy of this pose only consisting of the masked bones</returns>
        public AnimationPose Mask(IReadOnlyCollection<string> names)
        {
            AnimationPose output = new AnimationPose();
            foreach (string name in names)
            {
                if (ContainsKey(name))
                {
                    output.Add(name, this[name]);
                }
            }
            return output;
        }

        /// <summary>
        /// returns this poses bone mask
        /// </summary>
        /// <returns>a collection of bone names which can act as this poses mask</returns>
        public IReadOnlyCollection<string> GetMask()
        {
            return Keys.ToArray();
        }

        /// <summary>
        /// blends 2 poses together, by the given amount, merging and linearly interpolating the matrices
        /// </summary>
        /// <param name="a">the first operand</param>
        /// <param name="b">the second operand</param>
        /// <param name="x">the amount to blend them (uncapped)</param>
        /// <returns></returns>
        public static AnimationPose Blend(AnimationPose a, AnimationPose b, float x)
        {
            AnimationPose output = new AnimationPose();
            foreach (KeyValuePair<string, Matrix> pair in a)
            {
                output.Add(pair.Key, b.ContainsKey(pair.Key) ? Matrix.Lerp(pair.Value, b[pair.Key], x) : pair.Value);
            }
            foreach (KeyValuePair<string, Matrix> pair in b)
            {
                if (!output.ContainsKey(pair.Key))
                    output.Add(pair.Key, pair.Value);
            }
            return output;
        }
    }
}
